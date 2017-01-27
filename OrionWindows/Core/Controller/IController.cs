// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IController.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The Controller interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Core.Controller
{
    /// <summary>
    /// The Controller interface.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        IOrion Context { get; set; }
    }
}
