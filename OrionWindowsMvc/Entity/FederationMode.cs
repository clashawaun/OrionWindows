// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FederationMode.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the FederationMode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsMvc.Entity
{
    /// <summary>
    /// The federation mode.
    /// </summary>
    public enum FederationMode
    {
        /// <summary>
        /// Orion Federation will prompt user and ask if they want to login/register or continue with roaming account.
        /// </summary>
        RoamEnabled,

        /// <summary>
        /// Orion federation will return a roaming user.
        /// </summary>
        RoamOnly,

        /// <summary>
        /// Orion federation will prompt user and ask to login/register.
        /// </summary>
        AuthOnly
    }
}
