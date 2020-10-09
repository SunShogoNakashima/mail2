﻿using MailKit.Net.Smtp;
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
            var host = "smtp.gmail.com";
            var port = 465;

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    //開発用のSMTPサーバが暗号化に対応していないときは、次の行をコメントアウト
                    //smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.Auto);
                    //認証設定
                    smtp.Authenticate("shougorian3@gmail.com", "");

                    //送信するメールを作成する
                    var mail = new MimeKit.MimeMessage();
                    var builder = new MimeKit.BodyBuilder();
                    mail.From.Add(new MimeKit.MailboxAddress("", "shougorian3@gmail.com"));
                    mail.To.Add(new MimeKit.MailboxAddress("", "shogo.nakashima.1109@gmail.com"));
                    mail.Subject = "メールタイトル";
                    MimeKit.TextPart textPart = new MimeKit.TextPart("Plain");
                    textPart.Text = "メール本文";
                    //string filename = "添付ファイルの名前";
                    //添付ファイルがあった時の処理
                    //if (!string.IsNullOrEmpty(filename))
                    //{
                        //var filePath = Server.MapPath("~/App_Data/doc/" + filename);
                    //    //var mimeType = MimeKit.MimeTypes.GetMimeType(filePath);
                    //    //var attachment = new MimeKit.MimePart(mimeType);
                    //    //using (var file = File.OpenRead(filePath))
                    //    //{
                    //    //    attachment.Content = new MimeKit.MimeContent(file);
                    //        attachment.ContentDisposition = new MimeKit.ContentDisposition();
                    //        attachment.ContentTransferEncoding = MimeKit.ContentEncoding.Base64;
                    //        //attachment.FileName = Path.GetFileName(filePath);
                    //        var multipart = new MimeKit.Multipart("mixed");
                    //        multipart.Add(textPart);
                    //        multipart.Add(attachment);
                    //        mail.Body = multipart;
                    //        //メールを送信する
                    //        smtp.Send(mail);
                    //    }
                    //}
                    //else
                    //{
                        var multipart = new MimeKit.Multipart("mixed");
                        multipart.Add(textPart);
                        mail.Body = multipart;
                        //メールを送信する
                        smtp.Send(mail);
                    //}
                    ////添付ファイルがあった時の処理
                    //if (!string.IsNullOrEmpty(filename))
                    //{
                    //    var filePath = Server.MapPath("~/App_Data/doc/" + filename);
                    //    var mimeType = MimeKit.MimeTypes.GetMimeType(filePath);
                    //    var attachment = new MimeKit.MimePart(mimeType);
                    //    using (var file = File.OpenRead(filePath))
                    //    {
                    //        attachment.Content = new MimeKit.MimeContent(file);
                    //        attachment.ContentDisposition = new MimeKit.ContentDisposition();
                    //        attachment.ContentTransferEncoding = MimeKit.ContentEncoding.Base64;
                    //        attachment.FileName = Path.GetFileName(filePath);
                    //        var multipart = new MimeKit.Multipart("mixed");
                    //        multipart.Add(textPart);
                    //        multipart.Add(attachment);
                    //        mail.Body = multipart;
                    //        //メールを送信する
                    //        smtp.Send(mail);
                    //    }
                    //}
                    //else
                    //{
                    //    var multipart = new MimeKit.Multipart("mixed");
                    //    multipart.Add(textPart);
                    //    mail.Body = multipart;
                    //    //メールを送信する
                    //    smtp.Send(mail);
                    //}

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
