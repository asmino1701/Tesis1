using Imagen.Modelos;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using DatosConexion;


namespace Imagen
{
    public class Prediccion
    {
        static Correo enviar = new Correo();
        public static MdPredicciones account = new MdPredicciones();
        static List<Prediction> resultados = new List<Prediction>();
        static List<Prediction> resultadosFiltrados = new List<Prediction>();
        static byte[] imagen;
        public static string correo = "";
        public static async Task<List<Prediction>> MakePredictionRequestAsync(byte[] imageFilePath)
        {
            string predicciones;
            imagen = imageFilePath;

            try
            {
                HttpContent content = new ByteArrayContent(imageFilePath);
                var client = new RestClient("https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/a64cecf1-d2ce-4dca-b48a-e133c2665cc5/detect/iterations/Iteration10/image");
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
                Debug.WriteLine(account.predictions);

                //enviar.EnviarCorreo(imageFilePath);
                //return account.predictions;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return account.predictions;

        }

        /// <summary>
        /// Evaluo los resultados obtenidos del servicio
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task Predicciones(byte[] data, string email, bool casco, bool chaleco)
        {
            var contObj1 = 0;
            var contObj2 = 0;
            var contPersona = 0;
            var cont = 0;
            try
            {
                resultados = await MakePredictionRequestAsync(data);
                Debug.WriteLine(resultados);
                if (resultados.Count >= 1)
                {
                    foreach (Prediction respuesta in resultados)
                    {
                        cont++;
                        Debug.WriteLine(respuesta);
                        if (respuesta.probability >= 0.60)
                        {
                            resultadosFiltrados.Add(respuesta);
                            switch (respuesta.tagName.ToString())
                            {
                                case "casco":
                                    contObj1++;
                                    break;
                                case "chaleco":
                                    contObj2++;
                                    break;
                                case "persona":
                                    contPersona++;
                                    break;
                                default:
                                    break;
                            }

                        }
                        //Comparo si ya acabó de recorrer los resultados
                        if (cont == resultados.Count)
                        {
                            var output = JsonConvert.SerializeObject(resultados);
                            //Comparo qué checkboxes están activos para validar la información
                            if (casco && !chaleco)//si se selecciona casco
                            {
                                if (contObj1 != contPersona)
                                {
                                    //Envío el correo de alerta
                                    enviar.EnviarCorreo(data, email);
                                    Conexion.GuardarDatos(output, data);
                                }
                            }
                            else if (chaleco && !casco)//si se selecciona chaleco
                            {
                                if (contObj2 != contPersona)
                                {
                                    //Envío el correo de alerta
                                    enviar.EnviarCorreo(data, email);
                                    Conexion.GuardarDatos(output, data);
                                }
                            }
                            else if (casco && chaleco)//si se selecciona casco y chaleco
                            {
                                if (((contObj1 != contPersona) && (contObj2 != contPersona)) || ((contObj1 == contPersona) && (contObj2 != contPersona)) || ((contObj1 != contPersona) && (contObj2 == contPersona)))
                                {
                                    //Envío el correo de alerta
                                    enviar.EnviarCorreo(data, email);
                                    Conexion.GuardarDatos(output, data);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

    }
}
