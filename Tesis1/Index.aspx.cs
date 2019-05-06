using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Script.Services;
using System.Web.Services;
using Imagen;


namespace Tesis1
{
    [ScriptService]
    public partial class Index : System.Web.UI.Page
    {
        static Prediccion resultados = new Prediccion();
        static Prediccion clsPrediccion = new Prediccion();
        protected void Page_Load(object sender, EventArgs e)
        {

        }        

        [WebMethod]
        public static void ImagenCls(string imageData)
        {
            
            byte[] data = Convert.FromBase64String(imageData);
            //byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(imageData));
            clsPrediccion.MakePredictionRequestAsync(data).Wait();
            //resultados = clsPrediccion.account.predictions;            

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (CbxCasco.Checked)
            {
                
            }
        }
    }
}