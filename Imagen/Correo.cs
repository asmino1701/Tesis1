using System;
using System.Diagnostics;
using System.Net.Mail;

namespace Imagen
{
    public class Correo
    {
        public void EnviarCorreo()
        {
            string sMessage;
            MailMessage message = new MailMessage();

            try
            {

                SmtpClient client = new SmtpClient("smtp.gmail.com", 465);

                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("andresmino1701@gmail.com", "Sebastian1996");
                MailAddress fromCorreo = new MailAddress("andresmino1701@gmail.com", "Admin de Alertas");
                // MailAddress to = new MailAddress("juan.loachamin@grupobusiness.it,sofia.chavez@grupobusiness.it");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                message.From = fromCorreo;
                message.Body = "Error de carga de archivos";
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = "Error de sincronización";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.To.Add("andres.mino@grupobusiness.it");

                Attachment attach = new Attachment(@"C:\Users\AndrésMiño\Pictures\nuevos.png");

                message.Attachments.Add(attach);



                client.Send(message);
                sMessage = "Correo enviado.";
            }
            catch (Exception ex)
            {
                sMessage = "Coudn't send the message!\n " + ex.Message;
                Debug.WriteLine("Error al enviar el correo" + sMessage);
                //throw;
            }
            finally
            {
                Debug.WriteLine("Holaa final ");
            }
        }

    }
}
