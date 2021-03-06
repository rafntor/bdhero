﻿// Copyright 2012-2014 Andrew C. Dvorak
//
// This file is part of BDHero.
//
// BDHero is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BDHero is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with BDHero.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace DotNetUtils.Concurrency
{
    internal static class PromiseStatic
    {
        internal static readonly TimeSpan DefaultInterval = TimeSpan.FromSeconds(1d / 10d);
        internal static readonly AtomicInteger AtomicLogIdCounter = new AtomicInteger();
    }

    /// <summary>
    ///     Default implementation of the <see href="IPromise{TResult}"/> interface.
    /// </summary>
    public class Promise<TResult> : IPromise<TResult>
    {
        private static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private delegate void ProgressPromiseHandler(object state);

        #region Event handlers

        private readonly ConcurrentLinkedSet<BeforePromiseHandler<TResult>> _beforeHandlers =
            new ConcurrentLinkedSet<BeforePromiseHandler<TResult>>();

        private readonly ConcurrentLinkedSet<DoWorkPromiseHandler<TResult>> _doWorkHandlers =
            new ConcurrentLinkedSet<DoWorkPromiseHandler<TResult>>();

        private readonly ConcurrentLinkedSet<CancellationRequestedPromiseHandler<TResult>> _cancellationRequestedHandlers =
            new ConcurrentLinkedSet<CancellationRequestedPromiseHandler<TResult>>();

        private readonly ConcurrentLinkedSet<CanceledPromiseHandler<TResult>> _canceledHandlers =
            new ConcurrentLinkedSet<CanceledPromiseHandler<TResult>>();

        private readonly ConcurrentLinkedSet<SuccessPromiseHandler<TResult>> _successHandlers =
            new ConcurrentLinkedSet<SuccessPromiseHandler<TResult>>();

        private readonly ConcurrentLinkedSet<FailurePromiseHandler<TResult>> _failureHandlers =
            new ConcurrentLinkedSet<FailurePromiseHandler<TResult>>();

        private readonly ConcurrentLinkedSet<AlwaysPromiseHandler<TResult>> _alwaysHandlers =
            new ConcurrentLinkedSet<AlwaysPromiseHandler<TResult>>();

        private readonly ConcurrentMultiValueDictionary<Type, ProgressPromiseHandler> _progressHandlers =
            new ConcurrentMultiValueDictionary<Type, ProgressPromiseHandler>();

        #endregion

        #region Event queues

        private readonly ConcurrentMultiValueDictionary<Type, object> _progressEventStates =
            new ConcurrentMultiValueDictionary<Type, object>();

        private readonly ConcurrentQueue<DateTime> _cancellationRequestQueue = new ConcurrentQueue<DateTime>();
        private readonly ConcurrentQueue<DateTime> _successQueue = new ConcurrentQueue<DateTime>();
        private readonly ConcurrentQueue<DateTime> _finishedQueue = new ConcurrentQueue<DateTime>();

        #endregion

        private volatile bool _hasStarted;

        private readonly int _logId = PromiseStatic.AtomicLogIdCounter.GetAndIncrement();

        private readonly IInvoker _uiInvoker;

        private readonly ManualResetEventSlim _stopped = new ManualResetEventSlim(true);

        private readonly AtomicValue<TResult> _result = new AtomicValue<TResult>();
        private readonly AtomicValue<bool> _hasResult = new AtomicValue<bool>();

        private readonly Timer _timer = new Timer(PromiseStatic.DefaultInterval.TotalMilliseconds) { AutoReset = true };

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private readonly AtomicValue<Exception> _lastException = new AtomicValue<Exception>();

        #region Constructors

        /// <summary>
        ///     Constructs a new <see href="Promise{TResult}"/> instance that invokes callback event handlers on
        ///     the <b>current thread</b>.
        /// </summary>
        public Promise()
            : this(new Control())
        {
        }

        /// <summary>
        ///     Constructs a new <see href="Promise{TResult}"/> instance that invokes callback event handlers on
        ///     the given <paramref name="synchronizingObject"/>'s owner thread.
        /// </summary>
        /// <param name="synchronizingObject">
        ///     Object whose owner thread will be used to invoke UI callbacks.
        /// </param>
        public Promise(ISynchronizeInvoke synchronizingObject)
        {
            LogDebug("Promise() C'TOR");

            _uiInvoker = new UIInvoker(synchronizingObject);
            _timer.SynchronizingObject = synchronizingObject;
            _timer.Elapsed += OnTimerElapsed;
            _cancellationTokenSource.Token.Register(OnCancellationRequested);
        }

        #endregion

        #region Events

        private void OnTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            LogDebug("OnTimerElapsed()");
            DispatchEvents();
        }

        private void OnCancellationRequested()
        {
            LogDebug("OnCancellationRequested()");
            _cancellationRequestQueue.Enqueue(DateTime.Now);
        }

        #endregion

        #region Public

        #region Result

        public TResult Result
        {
            get
            {
                if (!_hasResult)
                {
                    throw new InvalidOperationException("Promise.Result has not been set.");
                }
                return _result;
            }
            set
            {
                _result.Value = value;
                _hasResult.Value = true;
            }
        }

        #endregion

        #region UI invoker

        public IInvoker UIInvoker
        {
            get { return _uiInvoker; }
        }

        #endregion

        #region Properties

        public TimeSpan Interval
        {
            get { return TimeSpan.FromMilliseconds(_timer.Interval); }
            set { _timer.Interval = value.TotalMilliseconds; }
        }

        public Exception LastException
        {
            get { return _lastException.Value; }
            private set { _lastException.Value = value; }
        }

        public bool IsCancellationRequested
        {
            get { return _cancellationTokenSource.IsCancellationRequested; }
        }

        public bool IsCompleted
        {
            get { return _stopped.IsSet; }
        }

        #endregion

        #region Event handlers

        public IPromise<TResult> Before(BeforePromiseHandler<TResult> handler)
        {
            _beforeHandlers.Add(handler);
            return this;
        }

        public IPromise<TResult> Work(DoWorkPromiseHandler<TResult> handler)
        {
            _doWorkHandlers.Add(handler);
            return this;
        }

        public IPromise<TResult> CancellationRequested(CancellationRequestedPromiseHandler<TResult> handler)
        {
            _cancellationRequestedHandlers.Add(handler);
            return this;
        }

        public IPromise<TResult> Canceled(CanceledPromiseHandler<TResult> handler)
        {
            _canceledHandlers.Add(handler);
            return this;
        }

        public IPromise<TResult> Fail(FailurePromiseHandler<TResult> handler)
        {
            _failureHandlers.Add(handler);
            return this;
        }

        public IPromise<TResult> Done(SuccessPromiseHandler<TResult> handler)
        {
            _successHandlers.Add(handler);
            return this;
        }

        public IPromise<TResult> Always(AlwaysPromiseHandler<TResult> handler)
        {
            _alwaysHandlers.Add(handler);
            return this;
        }

        #endregion

        #region Progress handlers

        public IPromise<TResult> Progress<TState>(ProgressPromiseHandler<TResult, TState> handler) where TState : class
        {
            var handlerWrapper = new ProgressPromiseHandler(o => handler(this, o as TState));
            _progressHandlers.Enqueue(typeof(TState), handlerWrapper);
            return this;
        }

        public IPromise<TResult> Progress<TState>(TState state) where TState : class
        {
            _progressEventStates.Enqueue(typeof(TState), state);
            return this;
        }

        #endregion

        #region Start

        public IPromise<TResult> Start()
        {
            LogDebug("Start()");

            PreventMultipleStarts();

            _hasStarted = true;

            if (IsCancellationRequested)
            {
                LogWarn("Start() - Already cancelled!");
                return this;
            }

            _stopped.Reset();

            InvokeBeforeHandlers();
            StartTimer();
            StartTask();

            return this;
        }

        #endregion

        #region Cancel

        public IPromise<TResult> Cancel()
        {
            LogDebug("Cancel()");
            CancelImpl();
            return this;
        }

        public IPromise<TResult> CancelWith(CancellationToken token)
        {
            token.Register(CancelImpl);
            if (token.IsCancellationRequested)
            {
                LogWarn("CancelWith() - ALREADY CANCELLED!?!");
                CancelImpl();
            }
            return this;
        }

        #endregion

        #region Wait

        private bool ShouldWait
        {
            get { return !IsCancellationRequested && !IsCompleted; }
        }

        public IPromise<TResult> Wait()
        {
            var time = DateTime.Now.ToString("O");

            LogDebug("Wait() - {0} - before...", time);

            if (ShouldWait)
                _stopped.Wait();

            LogDebug("Wait() - {0} - after", time);
            
            return this;
        }

        public IPromise<TResult> Wait(int millisecondsTimeout)
        {
            var time = DateTime.Now.ToString("O");

            LogDebug("Wait({0}) - {1} - before...", millisecondsTimeout, time);

            if (ShouldWait)
                _stopped.Wait(millisecondsTimeout);

            LogDebug("Wait({0}) - {1} - after...", millisecondsTimeout, time);

            return this;
        }

        public IPromise<TResult> Wait(TimeSpan timeout)
        {
            var time = DateTime.Now.ToString("O");

            LogDebug("Wait({0}) - {1} - before...", timeout, time);

            if (ShouldWait)
                _stopped.Wait(timeout);

            LogDebug("Wait({0}) - {1} - after...", timeout, time);

            return this;
        }

        #endregion

        #endregion

        #region Private

        #region Start

        private void PreventMultipleStarts()
        {
            if (_hasStarted)
            {
                throw new InvalidOperationException("Promise.Start() cannot be called more than once");
            }
        }

        private void InvokeBeforeHandlers()
        {
            foreach (var beforeHandler in _beforeHandlers.ToList())
            {
                beforeHandler(this);

                if (IsCancellationRequested)
                {
                    return;
                }
            }
        }

        private void StartTimer()
        {
            LogDebug("StartTimer()");
            _timer.Start();
        }

        private void StartTask()
        {
            LogDebug("StartTask()");

            Task.Factory.StartNew(TryDoWork, _cancellationTokenSource.Token);

            if (IsCancellationRequested)
            {
                FinishWork();
            }
        }

        #endregion

        #region Work

        /// <remarks>
        /// Background thread.
        /// </remarks>
        private void TryDoWork()
        {
            try
            {
                Work();
            }
            catch (OperationCanceledException)
            {
                // Ignore
            }
            catch (Exception exception)
            {
                LastException = exception;
            }
            finally
            {
                FinishWork();
            }
        }

        /// <remarks>
        /// Background thread.
        /// </remarks>
        private void Work()
        {
            foreach (var doWorkHandler in _doWorkHandlers.ToList())
            {
                doWorkHandler(this);

                if (IsCancellationRequested)
                {
                    return;
                }
            }

            _successQueue.Enqueue(DateTime.Now);
        }

        private void FinishWork()
        {
            _finishedQueue.Enqueue(DateTime.Now);
        }

        #endregion

        #region Cancel

        private void CancelImpl()
        {
            LogDebug("CancelImpl()");
            _cancellationTokenSource.Cancel();
        }

        #endregion

        #region Event dispatch

        #region Main loop

        private void DispatchEvents()
        {
            LogDebug("DispatchEvents()");
            DispatchProgressEvents();
            DispatchCancellationRequestedEvents();
            DispatchCompletionEvents();
        }

        #endregion

        #region Progress events

        private void DispatchProgressEvents()
        {
            var handlerTypes = _progressHandlers.GetKeys();
            var eventTypes = _progressEventStates.GetKeys();

            var i = 0;
            foreach (var eventType in eventTypes)
            {
                object state;
                while (_progressEventStates.TryDequeue(eventType, out state))
                {
                    foreach (var handlerType in handlerTypes)
                    {
                        var handlers = _progressHandlers.GetValues(handlerType);
                        foreach (var handler in handlers)
                        {
                            LogDebug("DispatchProgressEvents() - handler {0}", i++);
                            handler(state);
                        }
                    }
                }
            }
        }

        #endregion

        #region Cancellation requested events

        private void DispatchCancellationRequestedEvents()
        {
            var i = 0;
            DateTime dateTime;
            while (_cancellationRequestQueue.TryDequeue(out dateTime))
            {
                var handlers = _cancellationRequestedHandlers.ToList();
                foreach (var handler in handlers)
                {
                    LogDebug("DispatchCancellationRequestedEvents() - handler {0}", i++);
                    handler(this);
                }
            }
        }

        #endregion

        #region Completion events

        private void DispatchCompletionEvents()
        {
            DateTime dateTime;
            if (!_finishedQueue.TryDequeue(out dateTime))
                return;

            LogDebug("DispatchCompletionEvents()");

            BeforeDispatchCompletionEvents();

            DispatchCanceledEvents();
            DispatchFailEvents();
            DispatchSuccessEvents();
            DispatchAlwaysEvents();

            StopTimer();
        }

        /// <summary>
        ///     Called immediately prior to <see href="DispatchCompletionEvents"/>.
        ///     Subclasses may override this method to set the value of <see href="Result"/> before any completion events
        ///     are invoked.
        /// </summary>
        protected virtual void BeforeDispatchCompletionEvents()
        {
        }

        private void DispatchCanceledEvents()
        {
            if (!IsCancellationRequested)
                return;

            var i = 0;
            var handlers = _canceledHandlers.ToList();
            foreach (var handler in handlers)
            {
                LogDebug("DispatchCanceledEvents() - handler {0}", i++);
                handler(this);
            }
        }

        private void DispatchFailEvents()
        {
            if (IsCancellationRequested || LastException == null)
                return;

            var i = 0;
            var handlers = _failureHandlers.ToList();
            foreach (var handler in handlers)
            {
                LogDebug("DispatchFailEvents() - handler {0}", i++);
                handler(this);
            }
        }

        private void DispatchSuccessEvents()
        {
            if (IsCancellationRequested || LastException != null)
                return;

            var i = 0;
            var handlers = _successHandlers.ToList();
            foreach (var handler in handlers)
            {
                LogDebug("DispatchSuccessEvents() - handler {0}", i++);
                handler(this);
            }
        }

        private void DispatchAlwaysEvents()
        {
            var i = 0;
            var handlers = _alwaysHandlers.ToList();
            foreach (var handler in handlers)
            {
                LogDebug("DispatchAlwaysEvents() - handler {0}", i++);
                handler(this);
            }
        }

        private void StopTimer()
        {
            LogDebug("StopTimer()");
            _timer.Stop();
            _stopped.Set();
        }

        #endregion

        #endregion

        #region Logging

        private string GetLogFormat(string format)
        {
            return string.Format("{0} - [{1,-5}, {2,-5}] - {3}", _logId, IsCancellationRequested, IsCompleted, format);
        }

        private void LogDebug(string format, params object[] args)
        {
#if DEBUG_CONCURRENCY
            format = GetLogFormat(format);
            Logger.DebugFormat(format, args);
#endif
        }

        private void LogWarn(string format, params object[] args)
        {
            format = GetLogFormat(format);
            Logger.WarnFormat(format, args);
        }

        #endregion

        #endregion
    }
}
