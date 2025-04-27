using CloStyle.Domain.Entities;
using CloStyle.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CloStyle.Infrastructure.Seeders
{
    public class CloStyleSeeder
    {
        private readonly CloStyleDbContext _dbContext;

        public CloStyleSeeder(CloStyleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Sizes.Any())
                {
                    var sizes = new List<Size>
                    {
                        new Size {Name = "XS" },
                        new Size {Name = "S" },
                        new Size {Name = "M" },
                        new Size {Name = "L" },
                        new Size {Name = "XL" },
                        new Size {Name = "XXL" }
                    };
                    _dbContext.Sizes.AddRange(sizes);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.Categories.Any())
                {
                    var categories = new List<Category>
                    {
                        new Category {Name = "Shoes" },
                        new Category {Name = "T-Shirts" },
                        new Category {Name = "Hoodie" },
                        new Category {Name = "Dress" },
                        new Category {Name = "Jeans" },
                        new Category {Name = "Sweater" },
                        new Category {Name = "Shorts" },
                        new Category {Name = "Jacket" },
                        new Category {Name = "Skirt" },
                        new Category {Name = "Blouse" },
                        new Category {Name = "Tank top" },
                    };
                    _dbContext.Categories.AddRange(categories);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.Genders.Any())
                {
                    var genders = new List<Gender>
                    {
                        new Gender {Name = "Male" },
                        new Gender {Name = "Female" },
                        new Gender {Name = "Unisex" }
                    };
                    _dbContext.Genders.AddRange(genders);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.Brands.Any())
                {
                    var brands = new List<Brand>
                    {
                        new Brand {Name = "Nike" , ImgPath = "/images/brands/nike.jpg"},
                        new Brand {Name = "Adidas", ImgPath = "/images/brands/adidas.png"}
                    };
                    _dbContext.Brands.AddRange(brands);
                    await _dbContext.SaveChangesAsync();
                }

                var nikeBrand = _dbContext.Brands.First(b => b.Name == "Nike");
                var adidasBrand = _dbContext.Brands.First(b => b.Name == "Adidas");
                

                if (!_dbContext.Products.Any())
                {
                    var products = new List<Product>
                    {
                        new Product {Name = "Air Max", Price = 120.99m, BrandId = nikeBrand.Id, CategoryId = 1, GenderId = 1, Description = "Comfortable running shoes" },
                        new Product {Name = "Ultraboost", Price = 150.49m, BrandId = adidasBrand.Id, CategoryId = 1, GenderId = 1, Description = "High-performance sports shoes" },
                        new Product {Name = "Nike T-Shirt", Price = 30.00m, BrandId = nikeBrand.Id, CategoryId = 2, GenderId = 2, Description = "Casual cotton t-shirt"}
                    };

                    _dbContext.Products.AddRange(products);
                    await _dbContext.SaveChangesAsync();
                }

                var airMax = _dbContext.Products.First(p => p.Name == "Air Max");
                var ultraboost = _dbContext.Products.First(p => p.Name == "Ultraboost");
                var nikeTShirt = _dbContext.Products.First(p => p.Name == "Nike T-Shirt");

                if (!_dbContext.ProductSizes.Any())
                {
                    var productSizes = new List<ProductSize>
                    {
                        new ProductSize { ProductId = airMax.Id, SizeId = 1, Stock = 100 },
                        new ProductSize { ProductId = airMax.Id, SizeId = 2, Stock = 150 },
                        new ProductSize { ProductId = airMax.Id, SizeId = 3, Stock = 75 },
                        new ProductSize {ProductId = ultraboost.Id, SizeId = 3, Stock = 50 },
                        new ProductSize {ProductId = nikeTShirt.Id, SizeId = 1, Stock = 75 }
                    };
                    foreach (var ps in productSizes)
                    {
                        Console.WriteLine($"ProductId: {ps.ProductId}, SizeId: {ps.SizeId}, Stock: {ps.Stock}");
                    }
                    _dbContext.ProductSizes.AddRange(productSizes);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}

