using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Numerics;
#nullable disable

namespace LINQ.DataSources
{
    public class Linq_01
    {
        public static void Run()
        {
            Console.WriteLine("Get all products from the Seafood category. Print each product's name and price.\n");
            //1. Get all products from the "Seafood" category. Print each product's name and price.
            var seafoodProducts = Source.ProductList
                .Where(p => p.Category == "Seafood")
                .Select(p => new { p.ProductName, p.UnitPrice });

            foreach (var product in seafoodProducts)
            {
                Console.WriteLine($"Product: {product.ProductName}, Price: {product.UnitPrice:C2}");
            }
            Console.WriteLine("\n--------------------------------------------------\n");

            //2. Get a list of only the product names from ProductList. Print each name.
            var products = Source.ProductList
                .Select(p => p.ProductName);
            Console.WriteLine("Get a list of only the product names from ProductList. Print each name.\n");
            foreach(var product in products)
            {
                Console.WriteLine($"Product: {product}");
            }

            Console.WriteLine("\n--------------------------------------------------\n");

            //3. Sort all products by UnitPrice (ascending). Print each product's name and price.
            var sortedProducts = Source.ProductList
                .OrderBy(p => p.UnitPrice)
                .Select(p => new { p.ProductName, p.UnitPrice });
            
            Console.WriteLine("Sort all products by UnitPrice (ascending). Print each product's name and price.\n");
            foreach(var product in sortedProducts)
            {
                Console.WriteLine($"Product: {product.ProductName}, Price: {product.UnitPrice:C2}");
            }

            Console.WriteLine("\n--------------------------------------------------\n");

            //4. Get all products where UnitPrice is between 10 and 30
                var productsInRange = Source.ProductList
                .Where(p => p.UnitPrice >= 10 && p.UnitPrice <= 30)
                .Select(p => new { p.ProductName, p.UnitPrice });
            Console.WriteLine("Get all products where UnitPrice is between 10 and 30.\n");
            foreach(var product in productsInRange)
            {
                Console.WriteLine($"Product: {product.ProductName}, Price: {product.UnitPrice:C2}");
            }

            Console.WriteLine("\n--------------------------------------------------\n");

            //5. Get all products that are in stock (UnitsInStock > 0) and belong to the "Condiments" category
            Console.WriteLine("Get all products that are in stock (UnitsInStock > 0) and belong to the \"Condiments\" category.\n");
            var ProductInStock = Source.ProductList
                 .Where(p => p.UnitsInStock > 0 && p.Category== "Condiments")
                .Select(p => new { p.ProductName, p.UnitsInStock });
            foreach(var product in ProductInStock)
            {
                Console.WriteLine($"Product: {product.ProductName}, UnitsInStock: {product.UnitsInStock}");
            }

            Console.WriteLine("\n--------------------------------------------------\n");

            //6. Create a new anonymous type with three properties:
            Console.WriteLine("Create a new anonymous type with three properties: ProductName, UnitPrice , Availability \n");
            var result = Source.ProductList
                .Select(p => new { Name = p.ProductName, Price= p.UnitPrice, Availability = p.UnitsInStock > 0 ? "In Stock" : "Out of Stock" });
            foreach(var product in result)
            {
                Console.WriteLine($"Product: {product.Name}, Price: {product.Price:C2}, Availability: {product.Availability}");
            }

            Console.WriteLine("\n--------------------------------------------------\n");


            // 7. Print each product's name along with its position (1-based) in the list. Expected format: 1.Chai, 2.Chang, etc.
            // in this question we can make many ways to get the position of the product in the list
            Console.WriteLine("Print each product's name along with its position (1-based) in the list.\n");
            var productWithPosition = Source.ProductList
                 .Select(p => p.ProductName);
                int i = 0;
                foreach(var item in productWithPosition)
            {
                Console.WriteLine($"{i++}. {item}");
            }

            Console.WriteLine("\n--------------------------------------------------\n");

            //8. Sort ProductList by Category ascending, then within each category, sort by UnitPrice descending
            Console.WriteLine("Sort ProductList by Category ascending, then within each category, sort by UnitPrice descending.\n");
            var SortedProducts = from p in Source.ProductList
                                 orderby p.Category ascending, p.UnitPrice descending
                                 select new { p.ProductName, p.Category, p.UnitPrice };
            foreach (var product in SortedProducts)
            {
                Console.WriteLine($"Product: {product.ProductName}, Category: {product.Category}, Price: {product.UnitPrice:C2}");
            }

            Console.WriteLine("\n--------------------------------------------------\n");

            //9. Get all products from the "Beverages" category, sorted by UnitsInStock descending. Print name and stock.
            var productsInBeverages = Source.ProductList
                .Where(p => p.Category == "Beverages")
                .OrderByDescending(p => p.UnitsInStock)
                .Select(p => new { p.ProductName, p.UnitsInStock });
            foreach(var product in productsInBeverages)
            {
                Console.WriteLine($"Product: {product.ProductName}, UnitsInStock: {product.UnitsInStock}");
            }

            Console.WriteLine("\n--------------------------------------------------\n");

            //10. Using QUERY SYNTAX with a compound from clause, list all orders placed in 1997 or later showing CustomerID an OrderDate.
            var ordersIn1997OrLater = from c in Source.CustomerList
                                      from o in c.Orders
                                      where o.OrderDate.Year >= 1997
                                      orderby o.OrderDate ascending
                                      select new { c.CustomerID, o.OrderDate };
            foreach(var order in ordersIn1997OrLater)
            {
                Console.WriteLine($"CustomerID: {order.CustomerID}, OrderDate: {order.OrderDate:d}");
            }

            // The rest of assignment => next session we didn't take it

        }
    }
}