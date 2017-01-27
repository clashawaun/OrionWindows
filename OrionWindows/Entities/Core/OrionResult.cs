// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrionResult.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The orion result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Core
{
    using OrionWindows.Core.Controller;

    /// <summary>
    /// The orion result.
    /// </summary>
    /// <typeparam name="T">
    /// Result type
    /// </typeparam>
    public class OrionResult<T>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionResult{T}"/> class.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        public OrionResult(T result)
        {
            this.Result = result;
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public T Result { get; set; }
    }
}
