using CustomerManagementApi.Models;
using CustomerManagementApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CustomerManagementApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = customerRepository.GetCustomers();
            return Ok(customers);
        }

        [HttpPost]
        public IActionResult PostCustomers(List<Customer> request)
        {
            if (request == null || request.Count == 0)
            {
                return BadRequest("No customers provided in the request.");
            }

            var validCustomers = GetValidCustomers(request);
            if (validCustomers.Count == 0)
            {
                return BadRequest("Invalid customer data.");
            }

            var existingCustomers = customerRepository.GetCustomers();
            var uniqueValidCustomers = GetUniqueValidCustomers(validCustomers, existingCustomers);

            var updatedCustomers = existingCustomers.Concat(uniqueValidCustomers).ToList();
            var sortedCustomers = SortCustomers(updatedCustomers);

            customerRepository.SaveCustomers(sortedCustomers);

            return Ok("Customers added successfully.");
        }

        private List<Customer> GetValidCustomers(List<Customer> request)
        {
            return request.Where(customer =>
                customer != null &&
                !string.IsNullOrEmpty(customer.FirstName) &&
                !string.IsNullOrEmpty(customer.LastName) &&
                customer.Age > 18
            ).ToList();
        }

        private List<Customer> GetUniqueValidCustomers(List<Customer> validCustomers, List<Customer> existingCustomers)
        {
            return validCustomers.Where(customer => !existingCustomers.Any(c => c.Id == customer.Id)).ToList();
        }

        private List<Customer> SortCustomers(List<Customer> customers)
        {
            return customers.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();
        }
    }

}
