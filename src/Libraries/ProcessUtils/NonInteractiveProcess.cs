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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using DotNetUtils.Extensions;
using OSUtils.JobObjects;

namespace ProcessUtils
{
    /// <summary>
    /// Represents a child console (CLI) process that runs without any user interaction on the UI thread.
    /// The process's stdout and stderr streams are captured and passed to the
    /// <see href="StdOut"/> and <see href="StdErr"/> event handlers, respectively.
    /// Allows for pausing (suspending) and resuming the process, as well as receiving a notification
    /// when the process exits along with its exit code.
    /// </summary>
    public class NonInteractiveProcess : INotifyPropertyChanged
    {
        private readonly IJobObjectManager _jobObjectManager;

        private static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets or sets the path to the executable.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if ExePath is modified after the process has started.</exception>
        public string ExePath
        {
            get { return _exePath; }
            set
            {
                if (State != NonInteractiveProcessState.Ready)
                    throw new InvalidOperationException("Cannot modify ExePath after process has started.");
                _exePath = value;
            }
        }
        private string _exePath;

        /// <summary>
        /// Gets or sets the list of arguments passed to the executable.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if Arguments is modified after the process has started.</exception>
        public ArgumentList Arguments
        {
            get { return _arguments; }
            set
            {
                if (State != NonInteractiveProcessState.Ready)
                    throw new InvalidOperationException("Cannot modify Arguments after process has started.");
                _arguments = value;
            }
        }
        private ArgumentList _arguments = new ArgumentList();

        public string FullCommand
        {
            get
            {
                var list = new ArgumentList(ExePath);
                list.AddAll(Arguments);
                return list.ToString();
            }
        }

        private bool _hasStarted;
        private bool _hasExited;

        protected bool? CleanExit;

        /// <summary>
        /// Gets the system process ID.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the process.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the internal state of the process.
        /// </summary>
        public NonInteractiveProcessState State
        {
            get { return _state; }
            private set
            {
                if (value != _state)
                {
                    Logger.InfoFormat("Process \"{0}\" changing state from {1} to {2}", ExePath, _state, value);
                    _state = value;
                    PropertyChanged.Notify(() => State);
                }
            }
        }
        private NonInteractiveProcessState _state = NonInteractiveProcessState.Ready;

        /// <summary>
        /// Gets the total elapsed run time of the process.
        /// This property may be accessed at any time and in any state.
        /// </summary>
        public TimeSpan RunTime { get { return _stopwatch.Elapsed; } }
        private readonly Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// Gets the exit code of the process.
        /// </summary>
        public int ExitCode { get; private set; }

        /// <summary>
        /// Invoked immediately before the process starts running.
        /// </summary>
        public event EventHandler BeforeStart;

        /// <summary>
        /// Event handler for processing individual lines of output from stdout.
        /// </summary>
        public event BackgroundProcessStdOutHandler StdOut;

        /// <summary>
        /// Event handler for processing individual lines of output from stderr.
        /// </summary>
        public event BackgroundProcessStdErrHandler StdErr;

        /// <summary>
        /// Invoked when the process is killed manually, terminates abnormally (aborts or crashes), or completes successfully.
        /// </summary>
        public event BackgroundProcessExitedHandler Exited;

        /// <summary>
        /// Invoked whenever the state of the process changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the last error that occurred (or <c>null</c> if no errors have occurred).
        /// </summary>
        public Exception Exception { get; protected set; }

        #region Constructor

        /// <summary>
        ///     Constructs a new <see href="NonInteractiveProcess"/> object that uses the given
        ///     <paramref name="jobObjectManager"/> to ensure that child processes are terminated
        ///     if the parent process exits prematurely.
        /// </summary>
        /// <param name="jobObjectManager"></param>
        public NonInteractiveProcess(IJobObjectManager jobObjectManager)
        {
            _jobObjectManager = jobObjectManager;
        }

        #endregion

        #region Start / Kill

        /// <summary>
        /// Starts the NonInteractiveProcess synchronously and blocks (i.e., does not return) until the process exits,
        /// either by completing successfully or terminating unsuccessfully.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public NonInteractiveProcess Start()
        {
            try
            {
                StartImpl();
            }
            catch (Exception e)
            {
                Logger.Error("Error occurred while starting/running NonInteractiveProcess", e);
                Exception = e;
                Kill(true);
                throw;
            }
            finally
            {
                ProcessExitedSync();
            }
            return this;
        }

        protected virtual void OnBeforeStart(Process process)
        {
        }

        protected virtual void OnStart(Process process)
        {
        }

        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        private void StartImpl()
        {
            if (State != NonInteractiveProcessState.Ready)
                throw new InvalidOperationException("NonInteractiveProcess.Start() cannot be called more than once.");

            if (!File.Exists(ExePath))
                throw new FileNotFoundException(string.Format("Unable to Start NonInteractiveProcess: File \"{0}\" does not exist", ExePath));

            using (var process = CreateProcess())
            {
                using (var jobObject = _jobObjectManager.CreateJobObject())
                {
                    process.StartInfo.FileName = ExePath;
                    process.StartInfo.Arguments = Arguments.ToString();
                    process.EnableRaisingEvents = true;
                    process.Exited += ProcessExitedAsync;

                    OnBeforeStart(process);

                    if (BeforeStart != null)
                        BeforeStart(this, EventArgs.Empty);

                    process.OutputDataReceived += (sender, args) => HandleStdOut(args.Data);
                    process.ErrorDataReceived += (sender, args) => HandleStdErr(args.Data);

                    Logger.Info(FullCommand);

                    process.Start();

                    _hasStarted = true;

                    Id = process.Id;
                    Name = process.ProcessName;
                    State = NonInteractiveProcessState.Running;

                    if (_jobObjectManager.IsAssignedToJob(process))
                    {
                        Logger.Warn("WARNING: Child process already belongs to a Job Object.  If the parent process crashes, the child process will continue to run in the background until it finishes executing.");
                    }
                    else
                    {
                        jobObject.Assign(process);
                        jobObject.KillOnClose();
                    }

                    _stopwatch.Start();

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    Logger.InfoFormat("Waiting for process \"{0}\" w/ PID = {1} to exit...", ExePath, Id);

                    OnStart(process);

                    process.WaitForExit();

                    _hasExited = true;

                    Logger.InfoFormat("Process \"{0}\" w/ PID = {1} exited", ExePath, Id);

                    ExitCode = process.ExitCode;
                }
            }
        }

        private bool ShouldKeepRunning
        {
            get
            {
                return State == NonInteractiveProcessState.Running ||
                       State == NonInteractiveProcessState.Paused;
            }
        }

        /// <summary>
        /// Manually aborts the process immediately.
        /// </summary>
        public void Kill()
        {
            Kill(false);
        }

        /// <summary>
        /// Manually aborts the process immediately.
        /// </summary>
        /// <param name="dueToError">
        /// Indicates that the process is being killed programmatically due to an error rather than by user action.
        /// </param>
        public void Kill(bool dueToError)
        {
            if (!CanKill)
                return;

            Logger.InfoFormat("Killing process \"{0}\" w/ PID = {1}{2}...", ExePath, Id, dueToError ? " due to an error being thrown" : "");

            State = dueToError ? NonInteractiveProcessState.Error : NonInteractiveProcessState.Killed;

            try
            {
                GetProcess().Kill();
            }
            catch (Exception exception)
            {
                Logger.WarnFormat("Unable to kill process \"{0}\": Exception was thrown:\n{1}", ExePath, exception);
            }
        }

        private bool IsValidProcess
        {
            get
            {
                if (!_hasStarted || _hasExited)
                    return false;

                try
                {
                    using (var process = GetProcess())
                    {
                        return process != null && !process.HasExited;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error("Exception thrown while trying to access process information", e);
                }

                return false;
            }
        }

        private bool CanKill
        {
            get
            {
                var validState = (State == NonInteractiveProcessState.Running ||
                                  State == NonInteractiveProcessState.Paused);
                return validState && IsValidProcess;
            }
        }

        #endregion

        #region Output handling (stdout and stderr)

        private void HandleStdOut(string line)
        {
            if (StdOut != null)
                StdOut(line);
            AfterOutputLineHandled();
        }

        private void HandleStdErr(string line)
        {
            if (StdErr != null)
                StdErr(line);
            AfterOutputLineHandled();
        }

        /// <summary>
        /// Called after StdOut and StdErr are invoked.
        /// Used to calculate progress or do any other work that takes place every time a line of output has been parsed.
        /// </summary>
        protected virtual void AfterOutputLineHandled()
        {
        }

        #endregion

        #region Exit handling

        private bool IsUncleanExit
        {
            get { return CleanExit.HasValue && CleanExit.Value == false; }
        }

        protected virtual bool IsError
        {
            get
            {
                if (State == NonInteractiveProcessState.Error)
                    return true;

                if (ExitCode != 0)
                    return true;

                if (Exception != null)
                    return true;

                if (IsUncleanExit)
                    return true;

                return false;
            }
        }

        protected virtual void BeforeProcessExited()
        {
        }

        /// <summary>
        /// Invoked synchronously by <see href="Start()"/> after waiting for the process to exit.
        /// </summary>
        private void ProcessExitedSync()
        {
            BeforeProcessExited();

            _hasExited = true;

            LogExit("synchronous");

            _stopwatch.Stop();

            if (State != NonInteractiveProcessState.Killed)
            {
                State = IsError ? NonInteractiveProcessState.Error : NonInteractiveProcessState.Completed;
            }

            if (Exited != null)
                Exited(State, ExitCode, Exception, RunTime);
        }

        /// <summary>
        /// Invoked asynchronously by the <see href="Process.Exited"/> event.
        /// </summary>
        private void ProcessExitedAsync(object sender, EventArgs eventArgs)
        {
            BeforeProcessExited();

            _hasExited = true;

            LogExit("asynchronous");

            var process = sender as Process;
            if (process == null) return;
        }

        private void LogExit(string eventType)
        {
            var message = string.Format("Process \"{0}\" exited {1} ({2} event)",
                                        ExePath,
                                        CleanExit.HasValue ? (CleanExit.Value ? "cleanly" : "uncleanly") : "",
                                        eventType);

            if (CleanExit.HasValue && CleanExit.Value == false)
                Logger.Warn(message);
            else
                Logger.Info(message);
        }

        #endregion

        #region Pause / Resume

        public void Pause()
        {
            if (!CanPause) return;

            Logger.InfoFormat("Pausing process \"{0}\"", ExePath);

            GetProcess().Suspend();
            _stopwatch.Stop();
            State = NonInteractiveProcessState.Paused;
        }

        public void Resume()
        {
            if (!CanResume) return;

            Logger.InfoFormat("Resuming process \"{0}\"", ExePath);

            GetProcess().Resume();
            _stopwatch.Start();
            State = NonInteractiveProcessState.Running;
        }

        private bool CanPause
        {
            get
            {
                return State == NonInteractiveProcessState.Running && IsValidProcess;
            }
        }

        private bool CanResume
        {
            get
            {
                return State == NonInteractiveProcessState.Paused && IsValidProcess;
            }
        }

        #endregion

        #region Factory methods

        /// <summary>
        /// Attempts to retrieve the Process object for the current process ID.
        /// </summary>
        /// <returns>A Process object of the process is still running; otherwise null if it has already terminated.</returns>
        private Process GetProcess()
        {
            try { return Process.GetProcessById(Id); }
            catch { return null; }
        }

        private static Process CreateProcess()
        {
            return new Process
                       {
                           StartInfo =
                               {
                                   UseShellExecute = false,
                                   RedirectStandardOutput = true,
                                   RedirectStandardError = true,
                                   CreateNoWindow = true,
                                   WindowStyle = ProcessWindowStyle.Hidden,
                               }
                       };
        }

        #endregion
    }

    /// <summary>
    /// Describes the state of a <see href="NonInteractiveProcess"/>.
    /// States are mutually exclusive; a NonInteractiveProcess can only have one state at a time.
    /// </summary>
    public enum NonInteractiveProcessState
    {
        /// <summary>
        /// Process has not yet started.
        /// </summary>
        Ready,

        /// <summary>
        /// Process has started and is currently running.
        /// </summary>
        Running,

        /// <summary>
        /// Process has started but is paused (suspended).
        /// </summary>
        Paused,

        /// <summary>
        /// Process was killed manually by calling <see href="NonInteractiveProcess.Kill()"/>.
        /// </summary>
        Killed,

        /// <summary>
        /// Process terminated due to an unrecoverable error.
        /// </summary>
        Error,

        /// <summary>
        /// Process finished running and completed successfully.
        /// </summary>
        Completed
    }

    /// <summary>
    /// Event handler for processing individual lines of output from stdout.
    /// </summary>
    public delegate void BackgroundProcessStdOutHandler(string line);

    /// <summary>
    /// Event handler for processing individual lines of output from stderr.
    /// </summary>
    public delegate void BackgroundProcessStdErrHandler(string line);

    /// <summary>
    /// Event handler that is invoked when a NonInteractiveProcess is killed manually, terminates abnormally (aborts or crashes), or completes successfully.
    /// </summary>
    public delegate void BackgroundProcessExitedHandler(NonInteractiveProcessState state, int exitCode, Exception exception, TimeSpan runTime);

    /// <summary>
    /// Event handler that is invoked whenever the state of a NonInteractiveProcess changes.
    /// </summary>
    public delegate void BackgroundProcessStateChangedHandler(NonInteractiveProcessState state);
}
