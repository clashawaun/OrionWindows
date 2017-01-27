// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfirmPhoneRequirement.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The confirm phone requirement.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication.Actions
{
    /// <summary>
    /// The confirm phone requirement.
    /// </summary>
    public class ConfirmPhoneRequirement
    {
        /// <summary>
        /// Gets or sets the phone verification digits.
        /// </summary>
        public string PhoneVerificationDigits { get; set; }
    }
}
