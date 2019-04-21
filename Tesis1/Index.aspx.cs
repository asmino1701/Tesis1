using System;
using System.IO;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

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
            byte[] data = Convert.FromBase64String(imageData);
            
        }

    }
}