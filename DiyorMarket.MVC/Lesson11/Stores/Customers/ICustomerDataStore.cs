﻿using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.Customers
{
    public interface ICustomerDataStore
    {
        public GetCustomerResponse? GetCustomers(string? searchString, int pageNumber);
        public Customer? GetCustomer(int id);
        public Stream GetExportFile();
        public Customer? CreateCustomer(Customer category);
        public Customer? UpdateCustomer(Customer category);
        public void DeleteCustomer(int id);
    }
}
