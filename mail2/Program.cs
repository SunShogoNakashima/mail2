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
            //認証がある場合
            var host = "smtp.gmail.com";
            var port = 465;
            var fromAdd = "windowsmailingtest@gmail.com"; //送信元アドレス
            var fromAddPass = "Sun-0036"; //送信元アドレスパスワード
            var toAdd = "shogo.nakashima.1109@gmail.com"; //送信先アドレス

            /* 
             * //認証がない場合
            var host = "localhost";
            var port = 25;
            var fromAdd = "windowsmailingtest@gmail.com"; //送信元アドレス
            var fromAddPass = ""; //送信元アドレスパスワード
            var toAdd = "local@testmail.com"; //送信先アドレス
            */
            var mailSubject = "エラー通知ﾃｽﾄ";
            var mailText = "お疲れ様です。\r\nエラー通知のテストメールを送信いたしまします。";

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    if (fromAddPass == "")
                    {
                        smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.Auto);
                        //認証設定をコメントアウト
                        //smtp.Authenticate(fromAdd, fromAddPass);

                        //送信するメールを作成する
                        var mail = new MimeKit.MimeMessage();
                        var builder = new MimeKit.BodyBuilder();
                        mail.From.Add(new MimeKit.MailboxAddress("", fromAdd));
                        mail.To.Add(new MimeKit.MailboxAddress("", toAdd));
                        //メールタイトル
                        mail.Subject = mailSubject;
                        //メール本文
                        MimeKit.TextPart textPart = new MimeKit.TextPart("Plain");
                        textPart.Text = mailText;

                        var multipart = new MimeKit.Multipart("mixed");
                        multipart.Add(textPart);
                        mail.Body = multipart;
                        //メールを送信する
                        smtp.Send(mail);

                    }
                    else
                    {
                        smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.Auto);
                        //認証設定
                        smtp.Authenticate(fromAdd, fromAddPass);

                        //送信するメールを作成する
                        var mail = new MimeKit.MimeMessage();
                        var builder = new MimeKit.BodyBuilder();
                        mail.From.Add(new MimeKit.MailboxAddress("", fromAdd));
                        mail.To.Add(new MimeKit.MailboxAddress("", toAdd));
                        //メールタイトル
                        mail.Subject = mailSubject;
                        //メール本文
                        MimeKit.TextPart textPart = new MimeKit.TextPart("Plain");
                        textPart.Text = mailText;

                        var multipart = new MimeKit.Multipart("mixed");
                        multipart.Add(textPart);
                        mail.Body = multipart;
                        //メールを送信する
                        smtp.Send(mail);
                    }
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
}
