// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemAccount.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The system account.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace OrionWindows.Entities.Organisation
{
    using OrionWindows.Entities.Authentication;

    /// <summary>
    /// The system account.
    /// </summary>
    public class SystemAccount
    {
        /// <summary>
        /// Gets or sets the public key.
        /// </summary>
        public Key PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the secret key.
        /// </summary>
        public Key SecretKey { get; set; }
    }
}
