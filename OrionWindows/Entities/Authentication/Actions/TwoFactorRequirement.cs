// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwoFactorRequirement.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the TwoFactorRequirement type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication.Actions
{
    /// <summary>
    /// The two factor requirement.
    /// </summary>
    public class TwoFactorRequirement
    {
        /// <summary>
        /// Gets or sets the phone verification digits.
        /// </summary>
        public string PhoneVerificationDigits { get; set; }
    }
}
