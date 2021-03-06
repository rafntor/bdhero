﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace UILib.WinForms
{
    /// <summary>
    ///     Hooks into the <see href="Control.WndProc"/> message handler to allow classes outside of a <see href="Control"/>
    ///     to receive, handle, and suppress native window messages.
    /// </summary>
    /// <seealso href="http://stackoverflow.com/a/3539204/467582"/>
    /// <seealso href="http://msdn.microsoft.com/en-us/library/system.windows.forms.nativewindow(v=vs.100).aspx"/>
    public class WndProcHook
    {
        private readonly WndProcHookImpl _impl;

        /// <summary>
        ///     Invoked whenever the hooked control's <see href="Control.WndProc"/> method is called.
        /// </summary>
        public event WndProcEventHandler WndProcMessage;

        /// <summary>
        ///     Constructs a new <see href="WndProcHook"/> instance that listens for <see href="Control.WndProc"/> messages
        ///     sent to the given <paramref name="control"/>.
        /// </summary>
        /// <param name="control">
        ///     A control to listen to for window messages.
        /// </param>
        public WndProcHook(Control control)
        {
            _impl = new WndProcHookImpl(control);
            _impl.WndProcMessage += OnWndProcMessage;
        }

        /// <summary>
        ///     Invokes the default window procedure associated with this window. 
        /// </summary>
        /// <param name="m">
        ///     The message that is currently being processed. 
        /// </param>
        public void DefWndProc(ref Message m)
        {
            _impl.DefWndProc(ref m);
        }

        private void OnWndProcMessage(ref Message message, HandledEventArgs args)
        {
            if (WndProcMessage != null)
                WndProcMessage(ref message, args);
        }

        #region Private implementation

        private class WndProcHookImpl : NativeWindow
        {
            private readonly Control _control;

            public event WndProcEventHandler WndProcMessage;

            public WndProcHookImpl(Control control)
            {
                _control = control;

                if (_control == null)
                    return;

                _control.HandleCreated += HandleCreated;
                _control.HandleDestroyed += HandleDestroyed;

                if (_control.IsHandleCreated)
                    HijackHandle();
            }

            protected override void WndProc(ref Message m)
            {
                var args = new HandledEventArgs();

                if (WndProcMessage != null)
                    WndProcMessage(ref m, args);

                if (args.Handled)
                    return;

                base.WndProc(ref m);
            }

            private void HandleCreated(object sender, EventArgs args)
            {
                HijackHandle();
            }

            private void HandleDestroyed(object sender, EventArgs eventArgs)
            {
                ReleaseHandle();
            }

            private void HijackHandle()
            {
                AssignHandle(_control.Handle);
            }
        }

        #endregion
    }
}
