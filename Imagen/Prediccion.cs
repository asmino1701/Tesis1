using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imagen;

namespace Imagen
{
    public class Prediccion
    {
        Correo enviar = new Correo();        
        public async Task MakePredictionRequest(byte[] imageFilePath)
        {
            enviar.EnviarCorreo();
            var client = new HttpClient();
            var project = "a64cecf1-d2ce-4dca-b48a-e133c2665cc5";

            // Request headers - replace this example key with your valid Prediction-Key.
            client.DefaultRequestHeaders.Add("Prediction-Key", "99ae86db5b3046ae92c4ff77f27ccc78");
            var predictionKey = "99ae86db5b3046ae92c4ff77f27ccc78"; 

            // Prediction URL - replace this example URL with your valid Prediction URL.
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/a64cecf1-d2ce-4dca-b48a-e133c2665cc5/detect/iterations/Tesis%201/image";

            HttpResponseMessage response;

            // Request body. Try this sample with a locally stored image.
            //byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(imageFilePath))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);
                Debug.WriteLine(await response.Content.ReadAsStringAsync());


                //CustomVisionPredictionClient endpoint = new CustomVisionPredictionClient()
                //{
                //    ApiKey = predictionKey,
                //    Endpoint = url
                //};

                //// Make a prediction against the new project
                //Console.WriteLine("Making a prediction:");
                //var imageFile = Path.Combine("Images", "test", "test_image.jpg");
                //using (var stream = File.OpenRead(imageFile))
                //{
                //    var result = endpoint.PredictImage(project, File.OpenRead(imageFile));

                //    // Loop over each prediction and write out the results
                //    foreach (var c in result.Predictions)
                //    {
                //        Console.WriteLine($"\t{c.TagName}: {c.Probability:P1} [ {c.BoundingBox.Left}, {c.BoundingBox.Top}, {c.BoundingBox.Width}, {c.BoundingBox.Height} ]");
                //    }
                //}
            }
        }

       
    }
}
