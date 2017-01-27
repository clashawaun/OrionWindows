// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The set up config delegate
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Utils
{
    using System.Collections.Generic;

    /// <summary>
    /// The set up config delegate
    /// </summary>
    /// <param name="config">
    /// The config.
    /// </param>
    public delegate void SetUpConfig(IConfig config);

    /// <summary>
    /// The config.
    /// </summary>
    public class Config : IConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        public Config()
        {
            SetUpConfig setup = Config.DefaultSetup;
            setup.Invoke(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        /// <param name="setupMethod">
        /// The setup method.
        /// </param>
        public Config(SetUpConfig setupMethod)
        {
            setupMethod.Invoke(this);
        }

        /// <summary>
        /// Gets or sets the API endpoint.
        /// </summary>
        public string ApiEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public IDataParser Parser { get; set; }

        /// <summary>
        /// Gets or sets the log folder.
        /// </summary>
        public string LogFolder { get; set; }

        /// <summary>
        /// Gets or sets the active log types.
        /// </summary>
        public List<string> ActiveLogTypes { get; set; }

        /// <summary>
        /// The default setup.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public static void DefaultSetup(IConfig config)
        {
            config.ApiEndpoint = "http://api.orion.shanecraven.com";
            config.ActiveLogTypes = new List<string>();
            config.Parser = new JsonParser();
        }
    }
}
