// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfirmEmailVerification.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The confirm email verification.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication.Actions
{
    /// <summary>
    /// The confirm email verification.
    /// </summary>
    public class ConfirmEmailVerification
    {
        /// <summary>
        /// Gets or sets the verification code.
        /// </summary>
        public string VerificationCode { get; set; }
    }
}
