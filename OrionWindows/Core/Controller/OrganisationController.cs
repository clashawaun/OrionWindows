// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrganisationController.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the OrganisationController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Core.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OrionWindows.Entities.Core;
    using OrionWindows.Entities.Organisation;

    /// <summary>
    /// The Organisation controller.
    /// </summary>
    public class OrganisationController : IController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationController"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public OrganisationController(IOrion context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        public IOrion Context { get; set; }

        /// <summary>
        /// Get applications registered with a 3rd party application (Your application will need matching permissions)
        /// NOTE: An OrgUserStandardAuthenticator is required when calling this method
        /// </summary>
        /// <returns>
        /// The <see cref="OrionResult{IList}"/>.
        /// </returns>
        public async Task<OrionResult<IList<Application>>> ExternalGetOrganisationApplicationsAsync()
        {
            var result = await this.Context.Communicator.Get("Organisation/External/Application/List");

            try
            {
                result.IsOk(true);
                return new OrionResult<IList<Application>>(this.Context.Config.Parser.ParseData<List<Application>>(result.Result));
            }
            catch (Exception)
            {
                return new OrionResult<IList<Application>>(null);
            }
        }

        /// <summary>
        /// Get applications registered with a 3rd party application (Your application will need matching permissions)
        /// NOTE: An OrgUserStandardAuthenticator is required when calling this method
        /// </summary>
        /// <returns>
        /// The <see cref="OrionResult{IList}"/>.
        /// </returns>
        public OrionResult<IList<Application>> ExternalGetOrganisationApplications()
        {
            var task = Task.Run(this.ExternalGetOrganisationApplicationsAsync);
            task.Wait();
            return task.Result;
        }
    }
}
