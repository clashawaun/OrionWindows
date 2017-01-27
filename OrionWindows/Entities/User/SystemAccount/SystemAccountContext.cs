// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemAccountContext.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The system account context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.User.SystemAccount
{
    /// <summary>
    /// The system account context.
    /// </summary>
    public class SystemAccountContext
    {
        /// <summary>
        /// Gets or sets the application bind id.
        /// </summary>
        public string ApplicationBindId { get; set; }

        /// <summary>
        /// Gets or sets the organisation bind id.
        /// </summary>
        public string OrganisationBindId { get; set; }
    }
}
