// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlParser.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the XmlParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Utils
{
    using System;

    /// <summary>
    /// The xml parser.
    /// </summary>
    public class XmlParser : IDataParser
    {
        /// <summary>
        /// The parse data.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <typeparam name="T">
        /// The type to parse the data into
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// XML currently not supported
        /// </exception>
        public T ParseData<T>(string data)
        {
            throw new NotImplementedException();
        }
    }
}
