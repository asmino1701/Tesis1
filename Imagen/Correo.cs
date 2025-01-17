﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Imagen
{
    public class Correo
    {
        public void EnviarCorreo(byte[] imagen, string destinatario)
        {
            string sMessage;
            MailMessage message = new MailMessage();            
            try
            {

                SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587);

                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("andresmino1701@hotmail.com", "pijthhpfdsobvxyf");
                MailAddress fromCorreo = new MailAddress("andresmino1701@hotmail.com", "Admin de Alertas");                
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                message.From = fromCorreo;
                message.IsBodyHtml = true;
                using (StreamReader reader = File.OpenText(@"C:/Users/AndrésMiño/Documents/GitHub/Tesis1/Tesis1/Correo.html"))
                {
                    message.Body = reader.ReadToEnd();
                }
                //message.Body = "Se ha encontrado alguna incoherencia entre objetos y personas. Por favor verifique y, de ser el caso, tome las medidas correctivas.";
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = "Alerta! Posible falla de seguridad.";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.To.Add(destinatario);
                ContentType ct = new ContentType(MediaTypeNames.Image.Jpeg);                
                ct.Name = "img_" + DateTime.Now.ToString() + ".jpeg";
                Attachment attach = new Attachment(new System.IO.MemoryStream(imagen), ct);                

                message.Attachments.Add(attach);

                client.Send(message);
                sMessage = "Correo enviado.";
                Debug.WriteLine(sMessage);
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
