using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
           return _connection.Query<Product>("SELECT * FROM Products");
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) VALUES (@productName, @price, @categoryID);",
                new { productName = name, price = price, categoryID = categoryID });
        }

        public void UpdateProduct(string? updatedName, int productID)
        {
            _connection.Execute("UPDATE Products SET name = @updatedName WHERE ProductID = @productID;",
                new { updatedName = updatedName, productID = productID});
        }
    }
}
