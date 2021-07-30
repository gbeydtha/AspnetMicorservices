using Catalog.API.Entites;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(P => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfigurationProducts()); 
            }
        }

        private static IEnumerable<Product> GetPreconfigurationProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name= "IPhone X",
                    Summery = "This phone is the company's biggest change to its flagship smartphone in years.",
                    Description= "This phone is the company's biggest change to its flagship smartphone in years.",
                    Category= "Smart Phone",
                    ImageFile= "product-1.png",
                    Price= 950.00M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name= "IPhone 12",
                    Summery = "This phone is the company's biggest change to its flagship smartphone in years.",
                    Description= "This phone is the company's biggest change to its flagship smartphone in years.",
                    Category= "Smart Phone",
                    ImageFile= "product-2.png",
                    Price= 1200.00M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name= "IPhone 12x",
                    Summery = "This phone is the company's biggest change to its flagship smartphone in years.",
                    Description= "This phone is the company's biggest change to its flagship smartphone in years.",
                    Category= "Smart Phone",
                    ImageFile= "product-3.png",
                    Price= 1500.00M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name= "Samsung Note 12",
                    Summery = "This phone is the company's biggest change to its flagship smartphone in years.",
                    Description= "This phone is the company's biggest change to its flagship smartphone in years.",
                    Category= "Smart Phone",
                    ImageFile= "product-4.png",
                    Price= 1500.00M
                }
            };   
        }
    }
}
