using RestSharp;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Imagen.Modelos;
using System;
using System.Collections.ObjectModel;

namespace Imagen
{
    public class Prediccion
    {        
        Correo enviar = new Correo();
        private ObservableCollection<MdPredicciones> _posts;
        public async Task<MdPredicciones> MakePredictionRequestAsync(byte[] imageFilePath)
        {
            string predicciones;
            MdPredicciones account = new MdPredicciones();
            
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
                Debug.WriteLine(_posts);
                return account;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return account;

        }


    }
}
