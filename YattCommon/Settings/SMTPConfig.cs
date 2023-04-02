namespace YattCommon.Settings
{
    public class SMTPConfigModel
    {
        public string? SenderAddress { get; set; }
        public string? SenderDisplayName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSl { get; set; }
        public bool UseDefaultCrefential { get; set; }
        public bool IsBodyHTML { get; set; }
    }
}
