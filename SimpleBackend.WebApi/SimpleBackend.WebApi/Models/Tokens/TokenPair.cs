using System;

namespace SimpleBackend.WebApi.Models.Tokens
{
    /// <summary>
    /// Единица токена
    /// </summary>
    public sealed class TokenPair
    {
    
        /// <summary>
        ///  Время окончания жизни токена
        /// </summary>
        public DateTime EndTime { get; set; } 
        
        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; set; }
        
    }
}