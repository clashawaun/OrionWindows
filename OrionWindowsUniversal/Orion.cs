// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Orion.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The orion.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsUniversal
{
    using System.Collections.Generic;

    using OrionWindows;
    using OrionWindows.Core.Communication;
    using OrionWindows.Core.Controller;
    using OrionWindows.Entities.Authentication;
    using OrionWindows.Logging;
    using OrionWindows.Utils;

    using OrionWindowsUniversal.Logging;

    using Windows.Storage;

    /// <summary>
    /// The orion.
    /// </summary>
    public class Orion : IOrion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Orion"/> class.
        /// </summary>
        public Orion()
        {
            this.Config = new Config(this.SetConfigWithDefaults);
            this.Communicator = new ApiCommunicator(this);
            this.Logger = new DeleteMeLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orion"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public Orion(ILogger logger)
        {
            this.Config = new Config(this.SetConfigWithDefaults);
            this.Communicator = new ApiCommunicator(this);
            this.Logger = logger;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orion"/> class.
        /// </summary>
        /// <param name="setupConfigMethod">
        /// The setup config method.
        /// </param>
        public Orion(SetUpConfig setupConfigMethod)
        {
            this.Config = new Config(setupConfigMethod);
            this.Communicator = new ApiCommunicator(this);
            this.Logger = new DeleteMeLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orion"/> class.
        /// </summary>
        /// <param name="setupConfigMethod">
        /// The setup config method.
        /// </param>
        /// <param name="authenticator">
        /// The authenticator.
        /// </param>
        public Orion(SetUpConfig setupConfigMethod, IAuthenticator authenticator)
        {
            this.Config = new Config(setupConfigMethod);
            this.Communicator = new ApiCommunicator(this) { ApiAuthenticator = authenticator};
            this.Logger = new DeleteMeLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orion"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public Orion(IConfig config)
        {
            this.Config = config;
            this.Communicator = new ApiCommunicator(this);
            this.Logger = new DeleteMeLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orion"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="authenticator">
        /// The authenticator.
        /// </param>
        public Orion(IConfig config, IAuthenticator authenticator)
        {
            this.Config = config;
            this.Communicator = new ApiCommunicator(this) { ApiAuthenticator = authenticator };
            this.Logger = new DeleteMeLogger();
        }

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        public IConfig Config { get; set; }

        /// <summary>
        /// Gets or sets the communicator.
        /// </summary>
        public ICommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Create an authentication controller with the current context.
        /// </summary>
        /// <returns>
        /// The <see cref="Authentication"/>.
        /// </returns>
        public AuthenticationController CreateAuthenticationController()
        {
            return (AuthenticationController)ControllerFactory.CreateController(ControllerType.Authentication, this);
        }

        /// <summary>
        /// Create a user controller with the current context.
        /// </summary>
        /// <returns>
        /// The <see cref="UserController"/>.
        /// </returns>
        public UserController CreateUserController()
        {
            return (UserController)ControllerFactory.CreateController(ControllerType.User, this);
        }

        /// <summary>
        /// Create a user controller with the current context.
        /// </summary>
        /// <returns>
        /// The <see cref="OrganisationController"/>.
        /// </returns>
        public OrganisationController CreateOrganisationController()
        {
            return (OrganisationController)ControllerFactory.CreateController(ControllerType.Organisation, this);
        }

        /// <summary>
        /// Method sets the config with default values.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public void SetConfigWithDefaults(IConfig config)
        {
            config.ActiveLogTypes = new List<string>() { LogType.Info, LogType.Critical, LogType.Debug, LogType.Error, LogType.Warning };
            config.ApiEndpoint = "http://api.orion.shanecraven.com";
            config.LogFolder = ApplicationData.Current.LocalFolder.Path;
            config.Parser = new JsonParser();
        }
    }
}
