using Imagen.Modelos;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Imagen
{
    public class Prediccion
    {
        Correo enviar = new Correo();
        public MdPredicciones account = new MdPredicciones();
        public static List<Prediction> resPredicciones = new List<Prediction>();
        byte[] imagen;
        public async Task<List<Prediction>> MakePredictionRequestAsync(byte[] imageFilePath)
        {
            string predicciones;
            imagen = imageFilePath;

            try
            {
                HttpContent content = new ByteArrayContent(imageFilePath);
                var client = new RestClient("https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/a64cecf1-d2ce-4dca-b48a-e133c2665cc5/detect/iterations/Iteration8/image");
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
                predicciones = response.Content;
                account = JsonConvert.DeserializeObject<MdPredicciones>(predicciones);
                resPredicciones = account.predictions;
                Debug.WriteLine(account.predictions);
                
                //enviar.EnviarCorreo(imageFilePath);
                return account.predictions;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return account.predictions;

        }

        
    }
}
