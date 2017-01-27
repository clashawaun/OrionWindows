// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataParser.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the IDataParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Utils
{
    /// <summary>
    /// The DataParser interface.
    /// </summary>
    public interface IDataParser
    {
        /// <summary>
        /// Parse Data
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <typeparam name="T">
        /// Type to return
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T ParseData<T>(string data);
    }
}
