// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Key.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The API key.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using OrionWindows.Entities.Authentication.Actions;

    /// <summary>
    /// The API key.
    /// </summary>
    public class Key : IOrionEntity
    {
        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [JsonProperty(PropertyName = "Key")]
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the pending actions.
        /// </summary>
        public ICollection<AuthenticationAction> PendingActions { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty(PropertyName = "KeyType")]
        public KeyType Type { get; set; }
    }
}
