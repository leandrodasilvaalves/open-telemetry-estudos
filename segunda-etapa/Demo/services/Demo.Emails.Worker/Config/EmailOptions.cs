namespace Demo.Emails.Worker.Config
{
    public class EmailOptions
    {
        public string Smtp { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmaiFrom { get; set; }
    }
}