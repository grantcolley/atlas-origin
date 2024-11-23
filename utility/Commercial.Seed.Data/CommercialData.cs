using Commercial.Core.Models;

namespace Commercial.Test.Data
{
    public static class CommercialData
    {
        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = [];

            var customerLines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "_customers.csv"));
            var productLines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "_products.csv"));

            foreach (var customerLine in customerLines.Skip(1))
            {
                var c = customerLine.Split(',');

                var customer = new Customer
                {
                    Title = c[0],
                    FirstName = c[1],
                    Surname = c[2],
                    Address1 = c[3],
                    Address2 = c[4],
                    PostCode = c[5],
                    Country = c[6],
                    Telephone = c[7],
                    Email = c[8],
                    SortCode = c[9],
                    AccountNumber = c[10]
                };

                var products = productLines.Where(p => p.StartsWith($"{c[0]} {c[1]} {c[2]}"));

                foreach (var product in products)
                {
                    var p = product.Split(",");

                    customer.Products.Add(new Product
                    {
                        Name = p[1],
                        ProductType = p[2],
                        RateType = p[3],
                        RepaymentType = p[4],
                        Duration = int.Parse(p[5]),
                        Rate = decimal.Parse(p[6]),
                        StartDate = DateTime.ParseExact(p[7], "yyyy-MM-ddTHH:mm:ss.fffz", null),
                        Value = decimal.Parse(p[8])
                    });
                }

                customers.Add(customer);
            }

            return customers;
        }
    }
}
