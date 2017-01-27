// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FederationStatus.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The federation status.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsMvc.Entity
{
    /// <summary>
    /// The federation status.
    /// </summary>
    public class FederationStatus
    {
        /// <summary>
        /// Gets or sets a value indicating whether is optional.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether authenticated.
        /// </summary>
        public bool Authenticated { get; set; }
    }
}
