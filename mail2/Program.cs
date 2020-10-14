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
            // メールを送信する機能全体を別クラスにする（そうしておくと、再利用しやすい、ミナジンのプロジェクトに移植しやすい）
            var sendMail = new SendMail();

            //以下に必要な値を入力
            sendMail.Host = "smtp.gmail.com";//ホスト名
            sendMail.Port = 465;//ポート番号
            sendMail.FromAdd = "windowsmailingtest@gmail.com"; //送信元アドレス
            sendMail.FromAddPass = "Sun-0036"; //送信元アドレスパスワード
            sendMail.ToAdd = "shunyatakano0711@gmail.com"; //送信先アドレス
            sendMail.MailSubject = "エラー通知ﾃｽﾄ";
            sendMail.MailText = "お疲れ様です。\r\nエラー通知のテストメールを送信いたします。";//メール本文
            sendMail.UseSmtpAuth = true; // true=SMTP認証を使用する / false=SMTP認証を使用しない

            // メール送信
            sendMail.Send();
        }

    }
}
