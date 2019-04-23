using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Script.Services;
using System.Web.Services;
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
            //byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(imageData));
            clsPrediccion.MakePredictionRequestAsync(data).Wait();
            Console.ReadKey();
        }

    }
}