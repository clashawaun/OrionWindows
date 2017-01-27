// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationAction.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The authentication action.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication.Actions
{

    /// <summary>
    /// The authentication action.
    /// </summary>
    public class AuthenticationAction
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the action type.
        /// </summary>
        public AuthenticationActionType ActionType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is mandatory.
        /// </summary>
        public bool IsMandatory { get; set; }
    }
}
