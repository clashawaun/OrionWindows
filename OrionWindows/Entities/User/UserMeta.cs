// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserMeta.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the UserMeta type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.User
{
    /// <summary>
    /// The user meta.
    /// </summary>
    public class UserMeta
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the organisation id.
        /// </summary>
        public string OrganisationId { get; set; }
    }
}
