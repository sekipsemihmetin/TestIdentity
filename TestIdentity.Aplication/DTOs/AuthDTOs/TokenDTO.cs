namespace TestIdentity.Aplication.DTOs.AuthDTOs
{
    /// <summary>
    /// Token DTO'su - JWT token bilgilerini içerir / Token DTO containing JWT token information
    /// </summary>
    public class TokenDTO
    {
        /// <summary>
        /// Access token / Erişim token'ı
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Refresh token / Yenileme token'ı
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Token'ın sona erme tarihi / Token expiration date
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Token tipi / Token type
        /// </summary>
        public string TokenType { get; set; } = "Bearer";
    }
}

