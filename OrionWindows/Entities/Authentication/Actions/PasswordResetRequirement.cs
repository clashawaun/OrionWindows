// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordResetRequirement.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The password reset requirement.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication.Actions
{
    /// <summary>
    /// The password reset requirement.
    /// </summary>
    public class PasswordResetRequirement
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }
    }
}
