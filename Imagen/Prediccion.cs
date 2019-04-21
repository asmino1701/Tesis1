using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Imagen
{
   public class Prediccion
    {
        public async Task MakePredictionRequest(byte[] imageFilePath)
        {
            try
            {
                var client = new HttpClient();

                // Request headers - replace this example key with your valid Prediction-Key.
                client.DefaultRequestHeaders.Add("Prediction-Key", "99ae86db5b3046ae92c4ff77f27ccc78");
                //var predictionKey = "99ae86db5b3046ae92c4ff77f27ccc78";
                //var publishedModelName="Tesis 1";

                // Prediction URL - replace this example URL with your valid Prediction URL.
                string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/a64cecf1-d2ce-4dca-b48a-e133c2665cc5/detect/iterations/Tesis%201/image";

                HttpResponseMessage response;

                // Request body. Try this sample with a locally stored image.
                //byte[] byteData = GetImageAsByteArray(imageFilePath);

                using (var content = new ByteArrayContent(imageFilePath))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(url, content);
                    //Debug.WriteLine(response);
                    //CustomVisionPredictionClient endpoint = new CustomVisionPredictionClient()
                    //{
                    //    ApiKey = predictionKey,
                    //    Endpoint = url
                    //};
                    //var result = endpoint.DetectImage("a64cecf1-d2ce-4dca-b48a-e133c2665cc5", publishedModelName, imageFilePath);
                    //foreach (var c in result.Predictions)
                    //{
                    //    Debug.WriteLine($"\t{c.TagName}: {c.Probability:P1}");
                    //}
                    Debug.WriteLine(await response.Content.ReadAsStringAsync());

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
    }

}
