using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwinTokenAuthenticationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Wait for the async stuff to run...
            Run().Wait();

            // Then Write Done...
            Console.WriteLine("");
            Console.WriteLine("Done! Press the Enter key to Exit...");
            Console.ReadLine();
        }
        static async Task Run()
        {
            // Create an http client provider:
            const string hostUriString = "http://localhost:8080";
            var provider = new ApiClientProvider(hostUriString);
            string accessToken;
            Dictionary<string, string> tokenDictionary;

            try
            {
                // Pass in the credentials and retrieve a token dictionary:
                tokenDictionary = await provider.GetTokenDictionary(
                        "john@example.com", "password");
                accessToken = tokenDictionary["access_token"];
            }
            catch (AggregateException ex)
            {
                // If it's an aggregate exception, an async error occurred:
                Console.WriteLine(ex.InnerExceptions[0].Message);
                Console.WriteLine("Press the Enter key to Exit...");
                Console.ReadLine();
                return;
            }
            catch (Exception ex)
            {
                // Something else happened:
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press the Enter key to Exit...");
                Console.ReadLine();
                return;
            }

            // Write the contents of the dictionary:
            foreach (var kvp in tokenDictionary)
            {
                Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
                Console.WriteLine("");
            }

            var baseUri = new Uri(hostUriString);
            var valuesClient = new ValuesClient(baseUri, accessToken);

            Console.WriteLine("Read all the values...");
            var values = await valuesClient.GetValuesAsync();
            foreach (var value in values)
            {
                Console.WriteLine(value);
                Console.WriteLine("");
            }

            Console.WriteLine("Get value one...");
            var apiValue = await valuesClient.GetValueAsync(1);
            Console.WriteLine("Value one: "+apiValue);

        }
    }
}
