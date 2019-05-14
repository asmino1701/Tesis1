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
        public static bool cascos = false;
        public static bool chalecos = false;
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
            Prediccion.Predicciones(data, correo, cascos, chalecos).Wait();
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