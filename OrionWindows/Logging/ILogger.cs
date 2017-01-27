// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the ILogger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Logging
{
    /// <summary>
    /// The Logger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// The log info.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void LogInfo(string message);

        /// <summary>
        /// The log error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void LogError(string message);

        /// <summary>
        /// The log critical.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void LogCritical(string message);

        /// <summary>
        /// The log debug.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void LogDebug(string message);

        /// <summary>
        /// The log warning.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void LogWarning(string message);
    }
}
