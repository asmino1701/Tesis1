using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

public class Class1
{
	public Class1()
	{
        
    }

    static void Main(string[] args)
    {
        Task.Run(async () =>
        {
            var endpoint = "https://tesis.documents.azure.com:443/";
            var masterKey = "cyH0VdFWruvk6NWe61tmddJh5FyPGeu0oTcairDIQipDwnZOQIn48OcI0Y7iClrkR3IQZoZZwjnSEwV4blBgeg==";
            using (var client = new DocumentClient(new Uri(endpoint), masterKey))
            {
                Console.WriteLine("\r\n>>>>>>>>>>>>>>>> Creating Database <<<<<<<<<<<<<<<<<<<");
                // Create new database Object  
                //Id defines name of the database  
                var databaseDefinition = new Database { Id = "Tesis1" };
                var database = await client.CreateDatabaseIfNotExistsAsync(databaseDefinition);
                Console.WriteLine("Database testDb created successfully");

                //Create new database collection  
                Console.WriteLine("\r\n>>>>>>>>>>>>>>>> Creating Collection <<<<<<<<<<<<<<<<<<<");
                var collectionDefinition = new DocumentCollection { Id = "Tesis1_Resources" };
                var collection = await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("Tesis1"),
                    collectionDefinition);
                Console.WriteLine("Collection Tesis1_Resources created successfully");

                //Insert new Document  
                Console.WriteLine("\r\n>>>>>>>>>>>>>>>> Creating Document <<<<<<<<<<<<<<<<<<<");
                dynamic doc1Definition = new
                {
                    title = "Star War IV ",
                    rank = 600,
                    category = "Sci-fi"
                };
                var document1 = await client.CreateDocumentAsync(
                    UriFactory.CreateDocumentCollectionUri("testDb", "testDocumentCollection"),
                    doc1Definition);
            }

        }).Wait();
    }
}
