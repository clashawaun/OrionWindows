// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticatorHelper.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The authenticator helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsUniversal.Authenticator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;

    using Windows.Security.Cryptography;
    using Windows.Security.Cryptography.Core;

    /// <summary>
    /// The authenticator helper.
    /// </summary>
    public class AuthenticatorHelper
    {
        /// <summary>
        /// Convert dictionary into http header parameters.
        /// </summary>
        /// <param name="headerParams">
        /// The dictionary to convert.
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
        /// The signature.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GenerateHash(string signature, string key)
        {
            var sigGenerator = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha256);
            var generatedHash = sigGenerator.CreateHash(Convert.FromBase64String(key).AsBuffer());
            generatedHash.Append(CryptographicBuffer.ConvertStringToBinary(signature, BinaryStringEncoding.Utf8));
            return CryptographicBuffer.EncodeToBase64String(generatedHash.GetValueAndReset());
        }

        /// <summary>
        /// Calculate MD5 hash for http body of the request
        /// </summary>
        /// <param name="httpContent">
        /// The http request body content
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<byte[]> CalculateHash(HttpContent httpContent)
        {
            var content = await httpContent.ReadAsByteArrayAsync();
            if (content.Length == 0)
            {
                return null;
            }

            var hashAlgorithm = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            var buffer = CryptographicBuffer.CreateFromByteArray(content);
            var hashResult = hashAlgorithm.HashData(buffer);
            byte[] result = null;
            CryptographicBuffer.CopyToByteArray(hashResult, out result);
            return result;
        }
    }
}
