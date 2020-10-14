using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace mail2
{
    class SendMail
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string FromAdd { get; set; }
        public string FromAddPass { get; set; }
        public string ToAdd { get; set; }
        public string MailSubject { get; set; }
        public string MailText { get; set; }
        public bool UseSmtpAuth { get; set; }

        public void Send()
        {
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    // プロパティの値は、変数に入れなくても直接使用OK
                    smtp.Connect(Host, Port, MailKit.Security.SecureSocketOptions.Auto);

                    //認証設定
                    if (UseSmtpAuth)
                    {
                        smtp.Authenticate(FromAdd, FromAddPass);
                    }
                    var mail = new MimeKit.MimeMessage();
                    var builder = new MimeKit.BodyBuilder();
                    mail.From.Add(new MimeKit.MailboxAddress("", FromAdd));
                    mail.To.Add(new MimeKit.MailboxAddress("", ToAdd));
                    mail.Subject = MailSubject;
                    MimeKit.TextPart textPart = new MimeKit.TextPart("Plain");
                    textPart.Text = MailText;

                    var multipart = new MimeKit.Multipart("mixed");
                    multipart.Add(textPart);
                    mail.Body = multipart;
                    //メールを送信する
                    smtp.Send(mail);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                finally
                {
                    //SMTPサーバから切断する
                    smtp.Disconnect(true);
                }
            }
        }

    }

    class Info
    {
        public string host { get; set; }
        public int port { get; set; }
        public string fromAdd { get; set; }
        public string fromAddPass { get; set; }
        public string toAdd { get; set; }
        public string mailSubject { get; set; }
        public string mailText { get; set; }
    }
}