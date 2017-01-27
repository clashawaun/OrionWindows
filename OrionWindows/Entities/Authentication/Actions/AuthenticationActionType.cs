// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationActionType.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The authentication action type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication.Actions
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The authentication action type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthenticationActionType
    {
        /// <summary>
        /// The two factor authentication.
        /// </summary>
        TwoFactorAuthentication,

        /// <summary>
        /// The confirm email.
        /// </summary>
        ConfirmEmail,

        /// <summary>
        /// The confirm phone.
        /// </summary>
        ConfirmPhone,

        /// <summary>
        /// The password reset.
        /// </summary>
        PasswordReset
    }
}
