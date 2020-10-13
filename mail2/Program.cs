using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace mail2
{
    class Program
    {
        static void Main(string[] args)
        {
            var info = new Info();

            //以下に必要な値を入力
            info.host = "smtp.gmail.com";//ホスト名
            info.port = 465;//ポート番号
            info.fromAdd = "windowsmailingtest@gmail.com"; //送信元アドレス
            info.fromAddPass = "Sun-0036"; //送信元アドレスパスワード
            info.toAdd = "shogo.nakashima.1109@gmail.com"; //送信先アドレス
            info.mailSubject = "エラー通知ﾃｽﾄ";
            info.mailText = "お疲れ様です。\r\nエラー通知のテストメールを送信いたします。";//メール本文

            var host = info.host;
            var port = info.port;
            var fromAdd = info.fromAdd;
            var fromAddPass = info.fromAddPass;
            var toAdd = info.toAdd;
            var mailSubject = info.mailSubject;
            var mailText = info.mailText;

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {

                    smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.Auto);
                    
                    //認証設定
                    if (fromAddPass != "")
                    {    
                        smtp.Authenticate(fromAdd, fromAddPass);
                    }
                    var mail = new MimeKit.MimeMessage();
                    var builder = new MimeKit.BodyBuilder();
                    mail.From.Add(new MimeKit.MailboxAddress("", fromAdd));
                    mail.To.Add(new MimeKit.MailboxAddress("", toAdd));
                    mail.Subject = mailSubject;
                    MimeKit.TextPart textPart = new MimeKit.TextPart("Plain");
                    textPart.Text = mailText;

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
