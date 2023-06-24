using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerManagementSimulator
{
    public class Program
    {
        private const string ServerUrl = "https://localhost:44327";
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task Main()
        {
            await SendPostCustomersRequests();
            await SendGetCustomersRequests();
        }

        private static async Task SendPostCustomersRequests()
        {
            var random = new Random();
            var customers = new List<Customer>();
            var appendix = GetAppendix();

            for (int i = 0; i < 10; i++)
            {
                var firstName = appendix[random.Next(appendix.Length)];
                var lastName = appendix[random.Next(appendix.Length)];
                var age = random.Next(10, 91);
                var id = i + 1;

                var customer = new Customer
                {
                    LastName = lastName,
                    FirstName = firstName,
                    Age = age,
                    Id = id
                };

                customers.Add(customer);
            }

            var requestContent = JsonConvert.SerializeObject(customers);
            var response = await HttpClient.PostAsync($"{ServerUrl}/api/customers", new StringContent(requestContent, System.Text.Encoding.Default, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("POST customers request successful.");
            }
            else
            {
                Console.WriteLine("POST customers request failed.");
            }
        }

        private static async Task SendGetCustomersRequests()
        {
            var response = await HttpClient.GetAsync($"{ServerUrl}/api/customers");

            if (response.IsSuccessStatusCode)
            {
                var customers = await response.Content.ReadAsStringAsync();
                Console.WriteLine("GET customers request successful. Response:");
                Console.WriteLine(customers);
            }
            else
            {
                Console.WriteLine("GET customers request failed.");
            }
        }

        private static string[] GetAppendix()
        {
            return new[]
            {
                "Aaaa",
                "Bbbb",
                "Cccc",
                "Dddd",
                "Eeee",
                "Ffff"
            };
        }
    }

    public class Customer
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
    }
}
