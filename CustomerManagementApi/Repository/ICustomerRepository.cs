using CustomerManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        void SaveCustomers(List<Customer> customers);
    }
}
