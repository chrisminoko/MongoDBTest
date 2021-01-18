using MongoDB.Driver;
using MongoDBTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBTest.Service
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _product;

        public ProductService()
        {
            var client = new MongoClient("mongodb+srv://Admin:Password@01cluster0.3gkcr.mongodb.net/TestDb?retryWrites=true&w=majority");
            var database = client.GetDatabase("TestDb");
            _product = database.GetCollection<Product>("Product");
        }

        public Product Create(Product product)
        {
            _product.InsertOne(product);
            return product;
        }

        public IList<Product> Read() =>
            _product.Find(sub => true).ToList();

        public Product Find(string id) =>
            _product.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(Product product) =>
            _product.ReplaceOne(sub => sub.Id == product.Id, product);

        public void Delete(string id) =>
            _product.DeleteOne(sub => sub.Id == id);
    }
}
