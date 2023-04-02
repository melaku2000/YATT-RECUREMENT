namespace YattCommon.Dtos.Account
{
    public class AuthDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime TokenExpireTime { get; set; }
    }
}
