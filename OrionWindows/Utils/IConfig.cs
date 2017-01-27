// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfig.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The Config interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Utils
{
    using System.Collections.Generic;

    /// <summary>
    /// The Config interface.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Gets or sets the API endpoint.
        /// </summary>
        string ApiEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the parser.
        /// </summary>
        IDataParser Parser { get; set; }

        /// <summary>
        /// Gets or sets the log folder.
        /// </summary>
        string LogFolder { get; set; }

        /// <summary>
        /// Gets or sets the active log types.
        /// </summary>
        List<string> ActiveLogTypes { get; set; } 
    }
}
