using System;
using System.IO;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Timers;

namespace Tesis1
{
    [ScriptService]
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Timer tiempo = new Timer();
            //tiempo.Interval = 5000;
            //tiempo.Elapsed += new ElapsedEventHandler(UrlImagen);
            //tiempo.Start();
        }


        [WebMethod]
        public static void ImagenCls(string imageData)
        {
            byte[] data = Convert.FromBase64String(imageData);
            string Pic_Path = HttpContext.Current.Server.MapPath("MyPicture.png");
            using (FileStream fs = new FileStream(Pic_Path, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    
                    //bw.Write(data);
                    //bw.Close();
                }
            }
        }

    }
}