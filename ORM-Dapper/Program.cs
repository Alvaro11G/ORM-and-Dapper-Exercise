using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string? connString = config.GetConnectionString("DefaultConnection");
            Console.WriteLine(connString);
            #endregion

            IDbConnection conn = new MySqlConnection(connString);

            DapperDepartmentRepository Drepo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Department table");
            var depos = Drepo.GetAllDepartments();

            foreach (var depo in depos)
            {
                Console.WriteLine($"ID: {depo.DepartmentID} Name: {depo.Name}");
            }

            var Prepo = new DapperProductRepository(conn);

            Console.WriteLine("Product table");
            var products = Prepo.GetAllProducts();

            foreach(var product in products)
            {
                Console.WriteLine($"Name: {product.Name} CategoryID: {product.CategoryID} Price: {product.Price}");
            }

            Console.WriteLine("Would you like to update the product table? (Yes/No)");
            string? YorN = Console.ReadLine();

            if (YorN.ToLower() == "yes")
            {
                Console.WriteLine($"What is the productID of the product you would like to update?");
                var productID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"What is the new name you would like for the product with an id of {productID}?");
                var updatedName = Console.ReadLine();

                Prepo.UpdateProduct(updatedName, productID);
            }

        }
    }
}
