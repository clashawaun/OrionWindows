// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticatorHelper.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The authenticator helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsDesktop.Authenticator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The authenticator helper.
    /// </summary>
    public class AuthenticatorHelper
    {
        /// <summary>
        /// Convert dictionary into header parameters
        /// </summary>
        /// <param name="headerParams">
        /// The dictionary to convert
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ConvertDictToParams(Dictionary<string, string> headerParams)
        {
            var result = headerParams.Aggregate(string.Empty, (current, pair) => current + $"{pair.Key}=\"{Uri.EscapeDataString(pair.Value)}\",");
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }

        /// <summary>
        /// Generate HMAC SHA 256 hash
        /// </summary>
        /// <param name="signature">
        /// The signature to sign.
        /// </param>
        /// <param name="key">
        /// The key to hash.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GenerateHash(string signature, string key)
        {          
            using (var hmac = new HMACSHA256(Convert.FromBase64String(key)))
            {
                return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(signature)));
            }
        }

        /// <summary>
        /// The calculate hash.
        /// </summary>
        /// <param name="httpContent">
        /// The http requests body contents</param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<byte[]> CalculateHash(HttpContent httpContent)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = null;
                var content = await httpContent.ReadAsByteArrayAsync();
                if (content.Length != 0)
                {
                    hash = md5.ComputeHash(content);
                }

                return hash;
            }
        }
    }
}
