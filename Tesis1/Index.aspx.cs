using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Imagen;

namespace Tesis1
{
    [ScriptService]
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        

        [WebMethod]
        public static void ImagenCls(string imageData)
        {
            Prediccion clsPrediccion = new Prediccion();

            byte[] data = Convert.FromBase64String(imageData);
            //byte[] data = GetImageAsByteArray(imageData);
            clsPrediccion.MakePredictionRequest(data).Wait();

        }

        

    }
}