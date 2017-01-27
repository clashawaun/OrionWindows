// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiCommunicator.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The base api communicator class. This is responsible for sending get, post, and delete requests
//   to an Orion api endpoint
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Core.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using OrionWindows.Entities;
    using OrionWindows.Entities.Authentication;
    using OrionWindows.Entities.Core;
    using OrionWindows.Logging;

    /// <summary>
    /// The API communicator.
    /// </summary>
    public class ApiCommunicator : ICommunicator
    {
        /// <summary>
        /// The orion context.
        /// </summary>
        private readonly IOrion context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCommunicator"/> class.
        /// </summary>
        /// <param name="context">
        /// The orion context.
        /// </param>
        public ApiCommunicator(IOrion context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets the API authenticator.
        /// </summary>
        public IAuthenticator ApiAuthenticator { get; set; }

        /// <summary>
        /// Sends a POST http request to an Orion endpoint
        /// </summary>
        /// <param name="payload">
        /// The payload.
        /// </param>
        /// <param name="apiExtension">
        /// The API extension.
        /// </param>
        /// <param name="queryString">
        /// Additional Query Strings.
        /// </param>
        /// <returns>
        /// The <see cref="IApiResult"/>.
        /// </returns>
        public async Task<IApiResult> Post(object payload, string apiExtension, Dictionary<string, string> queryString = null )
        {
            using (var client = new HttpClient())
            {
                this.context.Logger.LogInfo("SerializeObject for transit via POST");
                var content = JsonConvert.SerializeObject(payload);
                await this.SetDefaultHeaders(client, new StringContent(content, Encoding.UTF8, "application/json"));

                this.context.Logger.LogInfo("Start Async Post");
                var response = await client.PostAsync($"{this.context.Config.ApiEndpoint}/api/{apiExtension}{ConvetDictToParams(queryString)}", new StringContent(content, Encoding.UTF8, "application/json"));

                this.context.Logger.LogInfo("Post complete - response received, reading content ");
                var responseString = await response.Content.ReadAsStringAsync();

                this.context.Logger.LogInfo("Content loaded, creating result object and returning control C=" + responseString);

                try
                {
                    return new ApiResult()
                               {
                                   Result = responseString,
                                   StatusCode = response.StatusCode
                               };
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sends a GET http request to an Orion endpoint
        /// </summary>
        /// <param name="apiExtension">
        /// The API extension.
        /// </param>
        /// <param name="queryString">
        /// Additional Query Strings.
        /// </param>
        /// <returns>
        /// The <see cref="IApiResult"/>.
        /// </returns>
        public async Task<IApiResult> Get(string apiExtension, Dictionary<string, string> queryString = null)
        {
            using (var client = new HttpClient())
            {
                this.context.Logger.LogInfo("Get has been called, preparing headers for transit");
                await this.SetDefaultHeaders(client, new StringContent(string.Empty, Encoding.UTF8, "application/json"));

                this.context.Logger.LogInfo("Start Async Get");
                var response = await client.GetAsync($"{this.context.Config.ApiEndpoint}/api/{apiExtension}{ConvetDictToParams(queryString)}");

                this.context.Logger.LogInfo("GET complete - response recieved, reading content ");
                var responseString = await response.Content.ReadAsStringAsync();

                this.context.Logger.LogInfo("Content loaded, creating result object and returning control. The JSOn response is = " + responseString);

                try
                {
                    return new ApiResult()
                    {
                        Result = responseString,
                        StatusCode = response.StatusCode
                    };
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sends a DELETE http request to an Orion Endpoint
        /// </summary>
        /// <param name="apiExtension">
        /// The API extension.
        /// </param>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// The <see cref="IApiResult"/>.
        /// </returns>
        /// <exception  cref="NotImplementedException">
        /// This method is not currently supported in Osiris Endpoints
        /// </exception>
        public Task<IApiResult> Delete(string apiExtension, Dictionary<string, string> queryString)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets default headers for HTTP requests.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task SetDefaultHeaders(HttpClient httpClient, HttpContent content)
        {
            this.context.Logger.LogInfo("Set JSON headers for request");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (this.ApiAuthenticator != null)
            {
                this.context.Logger.LogInfo("Set Authentication header using assigned scheme");
                httpClient.DefaultRequestHeaders.Authorization =
                    await this.ApiAuthenticator.GenerateAuthorizationHeader(content);

                this.context.Logger.LogInfo("Authentication header params " +
                                            httpClient.DefaultRequestHeaders.Authorization.Parameter + ", scheme = " +
                                            httpClient.DefaultRequestHeaders.Authorization.Scheme);
            }
            else
            {
                this.context.Logger.LogInfo("Authenticator is null, no authorization headers generated");
            }
        }

        /// <summary>
        /// Converts a given dictionary into query strings.
        /// </summary>
        /// <param name="queryStringParams">
        /// The query string dictionary.
        /// </param>
        /// <returns>
        /// The query string <see cref="string"/>.
        /// </returns>
        private string ConvetDictToParams(Dictionary<string, string> queryStringParams)
        {
            if (queryStringParams == null || queryStringParams.Count == 0)
            {
                return string.Empty;
            }

            var result = "?";
            result += queryStringParams.Aggregate(string.Empty, (current, pair) => current + $"{pair.Key}=\"{Uri.EscapeDataString(pair.Value)}\"&");

            if (!string.IsNullOrEmpty(result))
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }
    }
}
