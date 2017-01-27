﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteMeLogger.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the DeleteMeLogger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsDesktop.Logging
{
    using OrionWindows.Logging;

    /// <summary>
    /// The delete me logger 
    /// Default logger which does not log, developer can implement ILogger for custom logging.
    /// </summary>
    public class DeleteMeLogger : ILogger 
    {
        /// <summary>
        /// The log info.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogInfo(string message)
        {
            return;
        }

        /// <summary>
        /// The log error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogError(string message)
        {
            return;
        }

        /// <summary>
        /// The log critical.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogCritical(string message)
        {
            return;
        }

        /// <summary>
        /// The log debug.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogDebug(string message)
        {
            return;
        }

        /// <summary>
        /// The log warning.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogWarning(string message)
        {
            return;
        }
    }
}
