// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Application.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Organisation
{
    /// <summary>
    /// The application.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the logo path.
        /// </summary>
        public string LogoPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether roaming enabled.
        /// </summary>
        public bool RoamingEnabled { get; set; }

        /// <summary>
        /// Gets or sets the Organisation.
        /// </summary>
        public Organisation Organisation { get; set; }
    }
}
