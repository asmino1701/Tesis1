using Imagen;
using Imagen.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Script.Services;
using System.Web.Services;

namespace Tesis1
{
    [ScriptService]
    public partial class Index : System.Web.UI.Page
    {
        static Correo enviar = new Correo();
        static List<Prediction> resultados = new List<Prediction>();
        static List<Prediction> resultadosFiltrados = new List<Prediction>();
        static Prediccion clsPrediccion = new Prediccion();
        static bool cascos = false;
        static bool chalecos = false;
        public static string correo = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Recibo la imagen del lado del cliente
        /// </summary>
        /// <param name="imageData"></param>
        [WebMethod]
        public static void ImagenCls(string imageData)
        {            
            byte[] data = Convert.FromBase64String(imageData);
            Predicciones(data).Wait();
        }

        /// <summary>
        /// Evaluo los resultados obtenidos del servicio
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task Predicciones(byte[] data)
        {
            var contObj1 = 0;
            var contObj2 = 0;
            var contPersona = 0;
            var cont = 0;
            resultados = await clsPrediccion.MakePredictionRequestAsync(data);
            Debug.WriteLine(resultados);
            foreach (Prediction respuesta in resultados)
            {
                cont++;
                Debug.WriteLine(respuesta);
                if (respuesta.probability >= 0.60)
                {
                    resultadosFiltrados.Add(respuesta);
                    switch (respuesta.tagName.ToString())
                    {
                        case "casco":
                            contObj1++;
                            break;
                        case "chaleco":
                            contObj2++;
                            break;
                        case "persona":
                            contPersona++;
                            break;
                        default:
                            break;
                    }
                                        
                    
                }
                //Comparo si ya acabó de recorrer los resultados
                if (cont == resultados.Count)
                {
                    //Comparo qué checkboxes están activos para validar la información
                    if (cascos && !chalecos)//si se selecciona casco
                    {
                        if (contObj1 != contPersona)
                        {
                            //Envío el correo de alerta
                            enviar.EnviarCorreo(data, correo);
                        }
                    }
                    else if (chalecos && !cascos)//si se selecciona chaleco
                    {
                        if (contObj2 != contPersona)
                        {
                            //Envío el correo de alerta
                            enviar.EnviarCorreo(data, correo);
                        }
                    }
                    else if (cascos && chalecos)//si se selecciona casco y chaleco
                    {
                        if (((contObj1 != contPersona) && (contObj2 != contPersona)) || ((contObj1 == contPersona) && (contObj2 != contPersona)) || ((contObj1 != contPersona) && (contObj2 == contPersona)))
                        {
                            //Envío el correo de alerta
                            enviar.EnviarCorreo(data, correo);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Guardo la información seleccionada en la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            //Almaceno el correo ingresado al que se requiere realizar el envío de las notificaciones
            correo = TxtCorreo.Text;
            //Valido los objetos seleccionados
            if (CbxCasco.Checked)
            {
                cascos = true;
            }
            else
            {
                cascos = false;
            }

            if (CbxChaleco.Checked)
            {
                chalecos = true;
            }
            else
            {
                chalecos = false;
            }
        }
    }
}