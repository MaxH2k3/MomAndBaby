using Microsoft.Extensions.Configuration;

namespace MomAndBaby.BusinessObject.Models
{
    public class GmailSetting
    {
        public string DisplayName { get; set; } = null!;
        public string SmtpServer { get; set; } = null!;
        public int Port { get; set; }
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;

        public GmailSetting()
        {
            IConfiguration config = new ConfigurationBuilder()

                    .SetBasePath(Directory.GetCurrentDirectory())

                    .AddJsonFile("appsettings.json", true, true)

                    .Build();

            config.GetSection("GmailSetting");

            DisplayName = config["GmailSetting:DisplayName"]!;
            SmtpServer = config["GmailSetting:SmtpServer"]!;
            Port = int.Parse(config["GmailSetting:Port"]!);
            Mail = config["GmailSetting:Mail"]!;
            Password = config["GmailSetting:Password"]!;
        }
    }
}
