﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace ASF.Domain.Values
{
    /// <summary>
    /// 授权访问Token
    /// </summary>
    public class AccessToken : IValueObject
    {
        public AccessToken()
        {

        }
        [JsonConstructor]
        public AccessToken(string token, string tokenType, DateTime expired, string refreshToken)
        {
            this.Token = token;
            this.TokenType = tokenType;
            this.RefreshToken = refreshToken;
            this.Expired = expired;
        }
        public AccessToken(string token, string tokenType, DateTime expired) :this(token,tokenType, expired, null)
        {
        
        }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; private set; }
        /// <summary>
        /// Token 类型
        /// </summary>
        public string TokenType { get; private set; }
        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; private set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Expired { get; private set; }
    }
}
