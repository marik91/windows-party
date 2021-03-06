﻿namespace WindowsParty.Ui.ViewModels
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    public static class UiServices
    {
        /// <summary>
        /// A value indicating whether the UI is currently busy
        /// </summary>
        private static bool _isBusy;

        /// <summary>
        /// Sets the busystate as busy.
        /// </summary>
        public static void SetBusyState()
        {
            SetBusyState(true);
        }

        /// <summary>
        /// Handles the Tick event of the dispatcherTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void DispatcherTimerTick(object sender, EventArgs e)
        {
            if (sender is DispatcherTimer dispatcherTimer)
            {
                SetBusyState(false);
                dispatcherTimer.Stop();
            }
        }

        /// <summary>
        /// Sets the busystate to busy or not busy.
        /// </summary>
        /// <param name="busy">if set to <c>true</c> the application is now busy.</param>
        private static void SetBusyState(bool busy)
        {
            if (busy == _isBusy)
            {
                return;
            }

            _isBusy = busy;
            Mouse.OverrideCursor = busy ? Cursors.Wait : null;

            if (_isBusy)
            {
                new DispatcherTimer(
                    TimeSpan.FromSeconds(0),
                    DispatcherPriority.ApplicationIdle,
                    DispatcherTimerTick,
                    Application.Current.Dispatcher);
            }
        }
    }
}