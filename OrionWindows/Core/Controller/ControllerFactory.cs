// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerFactory.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The controller factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Core.Controller
{
    /// <summary>
    /// The controller factory.
    /// </summary>
    public class ControllerFactory
    {
        /// <summary>
        /// Create a controller of the given type
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="IController"/>.
        /// </returns>
        public static IController CreateController(ControllerType type, IOrion context)
        {
            switch (type)
            {
                case ControllerType.Authentication:
                {
                    return new AuthenticationController(context);
                }
                case ControllerType.User:
                {
                    return new UserController(context);
                }
                case ControllerType.Organisation:
                {
                    return new OrganisationController(context);
                }

                default:
                    return null;
            }
        }
    }
}
