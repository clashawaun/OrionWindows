// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordResetVerification.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The password reset verification.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication.Actions
{
    /// <summary>
    /// The password reset verification.
    /// </summary>
    public class PasswordResetVerification
    {
        /// <summary>
        /// Gets or sets the verification code.
        /// </summary>
        public string VerificationCode { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}
