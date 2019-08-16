using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class Utilities
    {


        public void EnviarCorreo(string email, string titulo, string body)
        {
            using (var db = new DBPremioEntities())
            {
                SP_ObtenerEmailServidor mailServidor = db.Database.SqlQuery<SP_ObtenerEmailServidor>("ObtenerEmailServidor ").FirstOrDefault();

                string from = mailServidor.VchFrom;
                string to = email;
                string subject = titulo;

                // Configura el cliente de correo
                SmtpClient mailClient = new SmtpClient(mailServidor.VchHost, mailServidor.IntPort);
                // Setea las credenciales (for SMTP servers that require authentication)
                mailClient.Credentials = new NetworkCredential(mailServidor.VchUserName, mailServidor.VchPassword);
                // Crea el mensaje de correo
                MailMessage mailMessage = new MailMessage(from, to, subject, body);
                mailMessage.From = new System.Net.Mail.MailAddress(from, "Yanbal-Bolivia");
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                //copia correo oculto
                if (mailServidor.VchCopiaMail.Length > 0)
                {
                    mailMessage.Bcc.Add(mailServidor.VchCopiaMail);
                }
                // Activa SSL
                mailClient.EnableSsl = true; //false; // esto tiene que estar en false
                                             // Por último, vinculamos ambas vistas al mensaje...

                // Envia el correo
                try
                {
                    mailClient.Send(mailMessage);
                    // _error = "";
                }
                catch (Exception ex)
                {
                    //_error = "Error";
                }
            }
        }
    }
}