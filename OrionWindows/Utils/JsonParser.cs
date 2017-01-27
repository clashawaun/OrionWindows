// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonParser.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The json parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Utils
{
    using Newtonsoft.Json;

    /// <summary>
    /// The JSON parser.
    /// </summary>
    public class JsonParser : IDataParser
    {
        /// <summary>
        /// The parse data.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <typeparam name="T">
        /// The type to parse data into
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T ParseData<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
