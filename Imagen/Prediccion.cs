using RestSharp;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Imagen
{
    public class Prediccion
    {
        Correo enviar = new Correo();
        public async Task MakePredictionRequestAsync(byte[] imageFilePath)
        {
            HttpContent content = new ByteArrayContent(imageFilePath);
            var client = new RestClient("https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/a64cecf1-d2ce-4dca-b48a-e133c2665cc5/detect/iterations/Tesis%201/image");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/octet-stream");
            request.AddHeader("prediction-key", "99ae86db5b3046ae92c4ff77f27ccc78");

            var body = new Parameter
            {
                Name = "file",
                Value = imageFilePath,
                Type = ParameterType.RequestBody,
            };
            request.AddParameter(body);

            IRestResponse response = client.Execute(request);
            Debug.WriteLine(response.Content);
            
        }


    }
}
