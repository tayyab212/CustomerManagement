using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Models
{
    public class Customer
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
    }
}
