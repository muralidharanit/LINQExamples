using System.Numerics;
using System.Runtime.InteropServices;

public class Program
{
    private static List<Customer>? customers;
    private static List<Order>? orders;

    public static void Main(string[] args)
    {
        FillData();

        Console.WriteLine("===========================");
        Console.WriteLine("=========InnerJoin=========");
        Console.WriteLine("===========================");
        InnerJoin();

        Console.WriteLine();
        Console.WriteLine("===========================");
        Console.WriteLine("=========CrossJoin=========");
        Console.WriteLine("===========================");
        CrossJoin();

        Console.WriteLine();
        Console.WriteLine("===========================");
        Console.WriteLine("=========LeftJoin=========");
        Console.WriteLine("===========================");
        LeftJoin();

        Console.WriteLine();
        Console.WriteLine("===========================");
        Console.WriteLine("=========Ordering==========");
        Console.WriteLine("===========================");
        Ordering();
    }

    private static void Ordering()
    {
        var result = from customer in customers
                     orderby customer.Age descending
                     select customer;

        foreach (var customer in result)
        {
            Console.WriteLine($"Name: {customer.Name}, Age: {customer.Age}");
        }
    }

    private static void InnerJoin()
    {
        var innerJoin = from customer in customers
                        join order in orders
                        on customer.Id equals order.CustomerId
                        select new
                        {
                            customer.Name,
                            order.ProductName
                        };

        //Retrives the data if "CustomerId" match in both table. 
        foreach (var result in innerJoin)
        {
            Console.WriteLine($"Customer: {result.Name}, Product: {result.ProductName}");
        }
    }

    private static void LeftJoin()
    {
        var leftJoin = from customer in customers
                       join order in orders on customer.Id equals order.CustomerId into customerOrders
                       from co in customerOrders.DefaultIfEmpty()
                       select new
                       {
                           customer.Name,
                           Product = co?.ProductName ?? "No Order"
                       };

        foreach (var result in leftJoin)
        {
            Console.WriteLine($"Customer: {result.Name}, Product: {result.Product}");
        }
    }

    private static void CrossJoin()
    {
        var crossJoin = from customer in customers
                        from order in orders
                        select new
                        {
                            customer.Name,
                            order.ProductName
                        };

        foreach (var result in crossJoin)
        {
            Console.WriteLine($"Customer: {result.Name}, Product: {result.ProductName}");
        }
    }

    private static void FillData()
    {
        customers = new List<Customer>
        {
            new Customer { Id = 1, Age = 35, Name = "Muralidharan" },
            new Customer { Id = 2, Age = 30, Name = "Bharathi" },
            new Customer { Id = 3, Age = 12, Name = "Varshaa" },
            new Customer { Id = 4, Age = 5,  Name = "Sidtharth" },
            new Customer { Id = 5, Age = 60, Name = "Vasa" },
            new Customer { Id = 6, Age = 32, Name = "Ram" },
            new Customer { Id = 7, Age = 30, Name = "Boopathy" }
        };

        orders = new List<Order>
        {
            new Order { Id = 1, CustomerId = 1, ProductName = "Laptop" },
            new Order { Id = 2, CustomerId = 2, ProductName = "Phone" },
            new Order { Id = 3, CustomerId = 1, ProductName = "Tablet" },
            new Order { Id = 5, CustomerId = 2, ProductName = "SmartTV" },
            new Order { Id = 4, CustomerId = 4, ProductName = "Toys" },
            new Order { Id = 3, CustomerId = 3, ProductName = "Stationary" },
        };
    }
}

public class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; internal set; }
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? ProductName { get; set; }
}


