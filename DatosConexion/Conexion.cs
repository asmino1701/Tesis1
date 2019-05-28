using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace DatosConexion
{
    public class Conexion
    {        
        public static void GuardarDatos(string datos, byte[] imagen)
        {
            var account = JsonConvert.DeserializeObject<List<PredictionDatos>>(datos);            
            Task.Run(async () =>
            {
                var endpoint = "https://tesis.documents.azure.com:443/";
                var masterKey = "cyH0VdFWruvk6NWe61tmddJh5FyPGeu0oTcairDIQipDwnZOQIn48OcI0Y7iClrkR3IQZoZZwjnSEwV4blBgeg==";
                using (var client = new DocumentClient(new Uri(endpoint), masterKey))
                {
                    Debug.WriteLine("\r\n>>>>>>>>>>>>>>>> Creando la base de datos si no existe <<<<<<<<<<<<<<<<<<<");
                    // Create new database Object  
                    //Id defines name of the database  
                    var databaseDefinition = new Database { Id = "Tesis1" };
                    var database = await client.CreateDatabaseIfNotExistsAsync(databaseDefinition);
                    Debug.WriteLine("Base de datos creada");

                    //Create new database collection  
                    Debug.WriteLine("\r\n>>>>>>>>>>>>>>>> Creando colección <<<<<<<<<<<<<<<<<<<");
                    var collectionDefinition = new DocumentCollection { Id = "Tesis1_Resources" };
                    var collection = await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("Tesis1"),
                        collectionDefinition);
                    Debug.WriteLine("Colección creada correctamente");

                    //Insert new Document  
                    Debug.WriteLine("\r\n>>>>>>>>>>>>>>>> Creando documento <<<<<<<<<<<<<<<<<<<");
                    dynamic doc1Definition = new
                    {
                        resultados = account,
                        adjuntos = imagen
                        
                    };
                    var document1 = await client.CreateDocumentAsync(
                        UriFactory.CreateDocumentCollectionUri("Tesis1", "Tesis1_Resources"),
                        doc1Definition);
                    Debug.WriteLine("\r\n>>>>>>>>>>>>>>>> Documento creado <<<<<<<<<<<<<<<<<<<");
                }

            }).Wait();
        }
    }
}
