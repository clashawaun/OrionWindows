// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwoFactorVerification.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The two factor verification.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication.Actions
{
    /// <summary>
    /// The two factor verification.
    /// </summary>
    public class TwoFactorVerification
    {
        /// <summary>
        /// Gets or sets the verification code.
        /// </summary>
        public string VerificationCode { get; set; }
    }
}
