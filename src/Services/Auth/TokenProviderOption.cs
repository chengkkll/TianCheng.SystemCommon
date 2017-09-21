using Microsoft.IdentityModel.Tokens;
using System;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TokenProviderOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromHours(24);
        /// <summary>
        /// 
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
