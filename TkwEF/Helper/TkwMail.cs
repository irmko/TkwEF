using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TkwEF.Core.Helper;

namespace TkwEF.Helper
{
    public class TkwMail
    {
        #region Fields

        private const string _robotAddress = "tkwrobot@mail.ru";

        #endregion

        public TkwMail()
        {

        }
        /// <summary>
        /// Отправка письма через сервер РД от робота разработчику
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public bool SendMailFromRobotToRazrab(string subject, Exception ex, bool isBohyHtml)
        {
            //RefreshIESettings("192.168.1.5:8080");
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_robotAddress);
                    mail.To.Add(new MailAddress(_robotAddress));
                    mail.Subject = subject;
                    mail.IsBodyHtml = isBohyHtml;
                    mail.HeadersEncoding = Encoding.Default;
                    mail.BodyEncoding = Encoding.UTF8;
                    //mail.Body = (string.Format("Машина={0}, Domain={1}, Пользователь={2};"
                    //    , Environment.MachineName, Environment.UserDomainName, Environment.UserName)
                    //    + (string.IsNullOrEmpty(body) ? string.Empty : Environment.NewLine + body.Trim())).EncodeTextToHtml();
                    SmtpException sex;

                    StringBuilder body = new StringBuilder();
                    body.AppendLine(string.Format("<b>Machine</b>={0}; <b>Domain</b>={1}; <b>User</b>={2}"
                        , Environment.MachineName, Environment.UserDomainName, Environment.UserName));
                    body.AppendLine(string.Format(@"<b>Сообщение</b> {0}", ex.Message));
                    Exception iex = ex.InnerException;
                    while (iex != null)
                    {
                        body.AppendLine(string.Format("<b>Сообщение</b> {0}", iex.Message));
                        body.AppendLine(string.Format("<b>StackTrace</b> = {0}", iex.StackTrace ?? ""));
                        body.AppendLine(string.Format("<b>TargetSite</b> = {0}", iex?.TargetSite?.ToString() ?? string.Empty));
                        iex = iex.InnerException;
                    }
                    mail.Body = body.ToString().EncodeTextToHtml();
                    SendMail(msg: mail, exc: out sex);
                }
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// Отправка письма через сервер РД от робота разработчику
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public bool SendMailFromRobotToRazrab(string subject, string body, bool isBohyHtml)
        {
            //RefreshIESettings("192.168.1.5:8080");
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_robotAddress);
                    mail.To.Add(new MailAddress(_robotAddress));
                    mail.Subject = subject;
                    mail.IsBodyHtml = isBohyHtml;
                    mail.HeadersEncoding = Encoding.Default;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.Body = (string.Format("Машина={0}, Domain={1}, Пользователь={2};"
                        , Environment.MachineName, Environment.UserDomainName, Environment.UserName)
                        + (string.IsNullOrEmpty(body) ? string.Empty : Environment.NewLine + body.Trim())).EncodeTextToHtml();
                    SmtpException ex;
                    SendMail(msg: mail, exc: out ex);
                }
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// Отправка асинхронно письма через сервер РД от робота разработчику
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public Task<bool> SendMailFromRobotToRazrabAsync(string subject, string body)
        {
            return SendMailFromRobotToRazrabAsync(subject: subject, body: body, isBohyHtml: true);
        }
        /// <summary>
        /// Отправка асинхронно письма через сервер РД от робота разработчику
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public Task<bool> SendMailFromRobotToRazrabAsync(string subject, string body, bool isBohyHtml)
        {
            return Task<bool>.Factory.StartNew(() => SendMailFromRobotToRazrab(subject: subject, body: body, isBohyHtml: isBohyHtml));
        }
        /// <summary>
        /// Отправка асинхронно письма через сервер РД от робота разработчику
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public Task<bool> SendMailFromRobotToRazrabAsync(string subject, Exception ex, bool isBohyHtml = true)
        {
            return Task<bool>.Factory.StartNew(() => SendMailFromRobotToRazrab(subject: subject, ex: ex, isBohyHtml: isBohyHtml));
        }
        /// <summary>
        /// Отправка письма через сервер РД
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        private bool SendMail(MailMessage msg, out SmtpException exc)
        {
            //RefreshIESettings("192.168.1.5:8080");
            string komu = string.Empty, komuCC = string.Empty, komuBCC = string.Empty;
            try
            {
                msg.To.Select(s => komu += "; " + s.User).ToList();
                msg.Bcc.Select(s => komuBCC += "; " + s.User).ToList();
                msg.CC.Select(s => komuCC += "; " + s.User).ToList();
                using (SmtpClient cSmtp = new SmtpClient())
                {
                    cSmtp.Host = "smtp.mail.ru";
                    cSmtp.Port = 2525;
                    cSmtp.EnableSsl = true;
                    cSmtp.Credentials = new NetworkCredential(_robotAddress, "Idhq3lu2sDU0jOOGWkXf");
                    cSmtp.Send(msg);
                    exc = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                exc = new SmtpException("Ошибка при отправке письма.", ex); ;
                return false;
            }
        }
        /// <summary>
        /// Отправка письма через сервер РД
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        private Task<SmtpException> SendMailAsync(MailMessage msg)
        {
            return Task<SmtpException>.Factory.StartNew(() =>
            {
                SmtpException ex;
                if (SendMail(msg, out ex))
                    return null;
                if (ex == null)
                    return new SmtpException("Неизвестная ошибка при отправке письма");
                return ex;
            });
        }
    }
}
