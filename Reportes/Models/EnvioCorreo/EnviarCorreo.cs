using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes.Models.EnvioCorreo
{
    public class EnviarCorreo
    {
        MailMessage correos = new MailMessage();
        SmtpClient envios = new SmtpClient();

        public bool enviarCorreo(string mensaje, string asunto, string destinatario, string ruta)
        {
            var exito = true;
            try
            {
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = mensaje;
                correos.Subject = asunto;
                correos.IsBodyHtml = true;

                string[] destino = destinatario.Split(',');


                foreach (string destinat in destino)
                {
                    correos.To.Add(destinat);
                }


                if (ruta.Equals("") == false)
                {

                    string[] rutaarchivos = ruta.Split(';');


                    foreach (string rut in rutaarchivos)
                    {
                        //System.Net.Mail.Attachment archivo = new System.Net.Mail.Attachment(ruta);
                        correos.Attachments.Add(new System.Net.Mail.Attachment(rut));
                    }

                }

                //if (rutaReporte.Equals("") == false)
                //{
                //    System.Net.Mail.Attachment archivo2 = new System.Net.Mail.Attachment(rutaReporte);
                //    correos.Attachments.Add(archivo2);
                //}

                correos.From = new MailAddress("webmaster@frigometro.com", asunto);
                envios.Credentials = new NetworkCredential("webmaster@frigometro.com", ".Frigo-2013.");

                //Datos importantes no modificables para tener acceso a las cuentas

                envios.Host = "mail.frigometro.com";
                envios.Port = 25;
                envios.EnableSsl = false;

                envios.Send(correos);
                // MessageBox.Show("El mensaje fue enviado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se envio el correo correctamente");
                exito = false;
            }

            ruta = "";
            correos.Attachments.Clear();

            return exito;


        }

        public bool enviarCorreoAlistamiento(string mensaje, string asunto, string destinatario, string ruta, string rutaPdfAlistamiento, string RazonSocial, int NoDocumento, int servicio, string rutaImagen)
        {
            String textoServicio = "";
            if (servicio == 1)
            {
                textoServicio = "Refrigeración";
            }
            else if (servicio == 2)
            {
                textoServicio = "Seco";
            }
            else if (servicio == 3)
            {
                textoServicio = "Congelación";
            }
            else
            {
                textoServicio = "Conservación";
            }


            string htmlProceso, htmlAlistamiento;// htmlEnviar;

            if (!string.IsNullOrEmpty(rutaPdfAlistamiento))
            {
                htmlAlistamiento = "<p style='color:black;' text-align:center'> Enviamos Solicitud de Alistamiento del Servicio " + "<b>" + textoServicio + "</b>" + ".</p>";
            }
            else
            {
                htmlAlistamiento = "";
            }
            if (!string.IsNullOrEmpty(ruta))
            {
                htmlProceso = "<p style='color:black;' text-align:center'> Enviamos Solicitud de Alistamiento del Servicio " + "<b>" + textoServicio + "</b>" + " con número de documento " + "<b>" + NoDocumento + "</b>" + ".</p>";
            }
            else
            {
                htmlProceso = "";
            }
            var plainView = AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
            //then we create the Html part
            string html = "" +
                   "<div style='padding:20px; text-align:center;  display: table; background-position:center top;background-repeat:no-repeat'>" +
                     "<div>" +
                     "<br/><br/>" +
                           "<img src='cid:imagen' /> " +
                               "<p style = 'color:black; text-align:center' > Solicitud de Alistamiento del Cliente " + " <b> " + RazonSocial + " </b> " + " </ p >" +
                               htmlProceso +
                               htmlAlistamiento +
                               "<br/><br/>" +
                         "</div> " +
                        "</div> ";
            LinkedResource img = new LinkedResource(rutaImagen + "\\Logo_frigometro_color_editNew.png", MediaTypeNames.Image.Jpeg);
            img.ContentId = "imagen";
            var htmlView = AlternateView.CreateAlternateViewFromString(html, null, "text/html");
            htmlView.LinkedResources.Add(img);
            correos.AlternateViews.Add(plainView);
            correos.AlternateViews.Add(htmlView);


            var exito = true;
            try
            {
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = mensaje;
                correos.Subject = asunto;
                correos.IsBodyHtml = true;

                string[] destino = destinatario.Split(',');



                foreach (string destinat in destino)
                {
                    correos.To.Add(destinat);
                }

                if (!string.IsNullOrEmpty(ruta))
                {
                    correos.Attachments.Add(new System.Net.Mail.Attachment(ruta));
                }
                if (!string.IsNullOrEmpty(rutaPdfAlistamiento))
                {
                    correos.Attachments.Add(new System.Net.Mail.Attachment(rutaPdfAlistamiento));
                }
                correos.From = new MailAddress("webmaster@frigometro.com", asunto);
                envios.Credentials = new NetworkCredential("webmaster@frigometro.com", ".Frigo-2013.");
                //Datos importantes no modificables para tener acceso a las cuentas

                envios.Host = "mail.frigometro.com";
                envios.Port = 25;
                envios.EnableSsl = false;

                envios.Send(correos);
                // MessageBox.Show("El mensaje fue enviado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se envio el correo correctamente");
                exito = false;
            }

            ruta = "";
            correos.Attachments.Clear();

            return exito;

        }

        public bool enviarCorreoAgendamientoVehiculo(string mensaje, string asunto, string destinatario, string ruta, string RazonSocial, int NoDocumento, int servicio, string rutaImagen, string Nombreconductor, string IdVehiculo, string FechaCita, TimeSpan HoraCita)
        {
            String textoServicio = "";
            if (servicio == 1)
            {
                textoServicio = "Refrigeración";
            }
            else if (servicio == 2)
            {
                textoServicio = "Seco";
            }
            else if (servicio == 3)
            {
                textoServicio = "Congelación";
            }
            else
            {
                textoServicio = "Conservación";
            }


            string htmlProceso;// htmlEnviar;


                htmlProceso = "<p style='color:black;'> Su Solicitud de Agendamiento fue realizado con éxito en nuestra plataforma Web-Clientes para el Servicio de " + " <b>" + textoServicio + "</b>" + " con número de agendamiento: " + " <b>" + NoDocumento +"." + " </b>" + "<br><br>" +
                               " <b>" + "Datos del Agendamiento Solicitado" + "</b>" + "<br>" +
                               " <b>" + "Fecha: " + "</b>" + FechaCita + " <b>" + " Hora: " + "</b>" + HoraCita + "<br>" +
                               " <b>" + "Nombre del conductor: " + "</b>" + Nombreconductor + "<br>" +
                               " <b>" + "Placa del vehículo: " + "</b>" + IdVehiculo + "<br><br>" +
                               " <b>" + "Nota:" + "</b>" + " El vehículo debe presentarse con 30 minutos de anticipación a la hora pactada, llegada la hora sin presentarse se dará una espera de 10 minutos. Agradecemos ser puntual." + "</p>";
         
            var plainView = AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
            //then we create the Html part
            string html = "" +
                   "<div style='max-width: 10px; max-height: 10px; text-align:center; background-color:#ffffff; background-position:center top;'>" +
                     "<div style='background-color:#ffffff;'>" +
                     "<br/><br/>" +
                           "<img style='max-width: 10px; max-height: 10px;' src='cid:imagen' /> " +
                               "<p style = 'color:black; text-align:center' > Apreciado: " + " <b> " + RazonSocial + " </b> " + " </ p >" +
                               htmlProceso +                               
                               "<br/><br/>" +
                         "</div> " +
                        "</div> ";
            LinkedResource img = new LinkedResource(rutaImagen + "\\Logo_frigometro_color_editNew.png", MediaTypeNames.Image.Jpeg);
            img.ContentId = "imagen";
            var htmlView = AlternateView.CreateAlternateViewFromString(html, null, "text/html");
            htmlView.LinkedResources.Add(img);
            correos.AlternateViews.Add(plainView);
            correos.AlternateViews.Add(htmlView);


            var exito = true;
            try
            {
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = mensaje;
                correos.Subject = asunto;
                correos.IsBodyHtml = true;

                string[] destino = destinatario.Split(',');



                foreach (string destinat in destino)
                {
                    correos.To.Add(destinat);
                }

                if (!string.IsNullOrEmpty(ruta))
                {
                    correos.Attachments.Add(new System.Net.Mail.Attachment(ruta));
                }
              
                correos.From = new MailAddress("webmaster@frigometro.com", asunto);
                envios.Credentials = new NetworkCredential("webmaster@frigometro.com", ".Frigo-2013.");
                //Datos importantes no modificables para tener acceso a las cuentas

                envios.Host = "mail.frigometro.com";
                envios.Port = 25;
                envios.EnableSsl = false;

                envios.Send(correos);
                // MessageBox.Show("El mensaje fue enviado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se envio el correo correctamente");
                exito = false;
            }

            ruta = "";
            correos.Attachments.Clear();

            return exito;

        }



        public bool enviarCorreoCosolidado(string mensaje, string asunto, string destinatario, string ruta, string rutaPdfConsolidado, int proceso, string RazonSocial, long NoDocumento, string servicio, Boolean consolidado, string rutaImagen)
        {
            //Proceso: Entrada => 1 - Salida => 2
            //opcion 1 => Movimiento opcion 2 = consolidado

            string htmlProceso, htmlConsolidad;// htmlEnviar;
            string procesoString = "";// servicioString = "";

            if (proceso == 1)
            {
                procesoString = "Entrada";
            }
            else
            {
                procesoString = "Salida";
            }

            if (!string.IsNullOrEmpty(rutaPdfConsolidado))
            {
                htmlConsolidad = /*" <p style = 'color:white; text-align:center'> Señores " + " <b> " + RazonSocial + " </b> " + " </p> " +*/
                                 "<p style='color:white; text-align:center'> Enviamos el informe <b> Consolidado </b> de <b> " + procesoString + "</b> de " + "<b>" + servicio + "</b>" + ".</p>";
            }
            else
            {
                htmlConsolidad = "";
            }

            if (!string.IsNullOrEmpty(ruta))
            {
                htmlProceso = "<p style='color:white; text-align:center'> Enviamos el informe de <b> " + procesoString + "</b> de " + "<b>" + servicio + "</b>" + " con número de documento " + "<b>" + NoDocumento + "</b>" + ".</p>";

            }
            else
            {
                htmlProceso = "";
            }


            var plainView = AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
            //then we create the Html part
            string html = "" +
                   "<div style='padding:20px; text-align:center;  display: table; width:100%; height:300px; background-color:#1178b4;background-position:center top;background-repeat:no-repeat'>" +
                     "<div style='background-color:#1178b4;'>" +
                     "<br/><br/>" +
                           "<img src='cid:imagen' /> " +
                               //"<img  id='base64image' src = 'data:image/jpeg;base64, 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' /> " +
                               "<p style = 'color:white; text-align:center' > Señores " + " <b> " + RazonSocial + " </b> " + " </ p >" +
                               htmlProceso +
                               htmlConsolidad +
                               "<br/><br/>" +
                         "</div> " +
                        "</div> ";

            //LinkedResource img = new LinkedResource(@"~\img\logo.png", MediaTypeNames.Image.Jpeg);
            LinkedResource img = new LinkedResource(rutaImagen + "\\logo.png", MediaTypeNames.Image.Jpeg);
            img.ContentId = "imagen";
            var htmlView = AlternateView.CreateAlternateViewFromString(html, null, "text/html");
            htmlView.LinkedResources.Add(img);
            correos.AlternateViews.Add(plainView);
            correos.AlternateViews.Add(htmlView);


            var exito = true;
            try
            {
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = mensaje;
                correos.Subject = asunto;
                correos.IsBodyHtml = true;

                string[] destino = destinatario.Split(',');



                foreach (string destinat in destino)
                {
                    correos.To.Add(destinat);
                }

                if (!string.IsNullOrEmpty(ruta))
                {
                    correos.Attachments.Add(new System.Net.Mail.Attachment(ruta));
                }
                if (!string.IsNullOrEmpty(rutaPdfConsolidado))
                {
                    correos.Attachments.Add(new System.Net.Mail.Attachment(rutaPdfConsolidado));
                }

                //if (ruta.Equals("") == false)
                //{

                //    string[] rutaarchivos = ruta.Split(';');


                //    foreach (string rut in rutaarchivos)
                //    {
                //        //System.Net.Mail.Attachment archivo = new System.Net.Mail.Attachment(ruta);
                //        correos.Attachments.Add(new System.Net.Mail.Attachment(rut));
                //    }

                //}

                //if (rutaReporte.Equals("") == false)
                //{
                //    System.Net.Mail.Attachment archivo2 = new System.Net.Mail.Attachment(rutaReporte);
                //    correos.Attachments.Add(archivo2);
                //}

                correos.From = new MailAddress("webmaster@frigometro.com", asunto);
                envios.Credentials = new NetworkCredential("webmaster@frigometro.com", ".Frigo-2013.");

                //Datos importantes no modificables para tener acceso a las cuentas

                envios.Host = "mail.frigometro.com";
                envios.Port = 25;
                envios.EnableSsl = false;

                envios.Send(correos);
                // MessageBox.Show("El mensaje fue enviado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se envio el correo correctamente");
                exito = false;
            }

            ruta = "";
            correos.Attachments.Clear();

            return exito;


        }


        public bool EliminarArchivos(string RutaEliminar)
        {

            string ruta = RutaEliminar; //"C:/totalposctg";*/
            DirectoryInfo folder = new DirectoryInfo(ruta);


            foreach (FileInfo archivo in folder.GetFiles())
            {

                try
                {
                    //my.Computer.FileSystem.DeleteFile(ruta + "/" + archivo.Name);
                    File.Delete(ruta + "/" + archivo.Name);


                }
                catch (Exception)
                {

                    throw;
                }




            }

            return true;

        }

    }
}
