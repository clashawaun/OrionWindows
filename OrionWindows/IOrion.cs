// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrion.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The Orion interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows
{
    using OrionWindows.Core.Communication;
    using OrionWindows.Logging;
    using OrionWindows.Utils;

    /// <summary>
    /// The Orion interface.
    /// </summary>
    public interface IOrion
    {
        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        IConfig Config { get; set; }

        /// <summary>
        /// Gets or sets the communicator.
        /// </summary>
        ICommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        ILogger Logger { get; set; }    
    }
}
