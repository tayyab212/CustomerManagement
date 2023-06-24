using CustomerManagementApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Repository
{
    public class FileCustomerRepository : ICustomerRepository
    {
        private readonly string dataFilePath = "customers.json";

        public List<Customer> GetCustomers()
        {
            if (System.IO.File.Exists(dataFilePath))
            {
                var customersJson = System.IO.File.ReadAllText(dataFilePath);
                return JsonConvert.DeserializeObject<List<Customer>>(customersJson);
            }

            return new List<Customer>();
        }

        public void SaveCustomers(List<Customer> customers)
        {
            var customersJson = JsonConvert.SerializeObject(customers);
            System.IO.File.WriteAllText(dataFilePath, customersJson);
        }
    }

}
