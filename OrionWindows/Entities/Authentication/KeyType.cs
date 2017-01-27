// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyType.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The key type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace OrionWindows.Entities.Authentication
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The key type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KeyType
    {
        /// <summary>
        /// The application public key.
        /// </summary>
        ApplicationPublicKey,

        /// <summary>
        /// The application secret key.
        /// </summary>
        ApplicationSecretKey,

        /// <summary>
        /// The application temp key.
        /// </summary>
        ApplicationTempKey,

        /// <summary>
        /// The user temp key.
        /// </summary>
        UserTempKey,

        /// <summary>
        /// The user registration key.
        /// </summary>
        UserRegistrationKey,

        /// <summary>
        /// The user perm key.
        /// </summary>
        UserPermKey
    }
}
