﻿using System;
using System.IO;
using System.Net;

// ReSharper disable InconsistentNaming
namespace DotNetUtils.Exceptions
{
    /// <summary>
    ///     Static utility class containing methods for working with exceptions.
    /// </summary>
    public static class ExceptionUtils
    {
        /// <summary>
        ///     Determines if the given <paramref name="exception"/> can be reported.
        /// </summary>
        /// <param name="exception">
        ///     Exception that was thrown elsewhere in the application.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the given <paramref name="exception"/> can be reported;
        ///     otherwise <c>false</c>.
        /// </returns>
        public static bool IsReportable(Exception exception)
        {
            if (IsNonReportable(exception))
                return false;

            if (IsCanceled(exception))
                return false;

            if (IsUserError(exception))
                return false;

            return true;
        }

        private static bool IsNonReportable(Exception exception)
        {
            while (exception != null)
            {
                var reportableException = exception as ReportableException;
                if (reportableException != null && reportableException.IsReportable == false)
                    return true;

                exception = exception.InnerException;
            }
            return false;
        }

        /// <summary>
        ///     Determines if the given <paramref name="exception"/> or any of its <see href="Exception.InnerException"/>s
        ///     is an <see href="OperationCanceledException"/>.
        /// </summary>
        /// <param name="exception">
        ///     Exception that was thrown elsewhere in the application.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the given <paramref name="exception"/> or any of its <see href="Exception.InnerException"/>s
        ///     is an <see href="OperationCanceledException"/>; otherwise <c>false</c>.
        /// </returns>
        public static bool IsCanceled(Exception exception)
        {
            while (exception != null)
            {
                if (exception is OperationCanceledException)
                    return true;

                exception = exception.InnerException;
            }
            return false;
        }

        /// <summary>
        ///     Determines if the given <see href="exception"/> or any of its <see href="Exception.InnerException"/>s
        ///     is likely due to user error (error code <c>ID10T</c>).
        /// </summary>
        /// <param name="exception">
        ///     Exception that was thrown elsewhere in the application.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the given <see href="exception"/> or any of its <see href="Exception.InnerException"/>s
        ///     is likely due to user error; otherwise <c>false</c>.
        /// </returns>
        public static bool IsUserError(Exception exception)
        {
            while (exception != null)
            {
                if (exception is ID10TException ||
                    exception is InvalidDataException ||
                    exception is DirectoryNotFoundException ||
                    exception is DriveNotFoundException ||
                    exception is FileNotFoundException ||
                    exception is PathTooLongException ||
                    exception is WebException)
                {
                    return true;
                }

                exception = exception.InnerException;
            }
            return false;
        }
    }
}
