using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stage.Data;

namespace Stage.Models
{
    internal static class ProductGenerator
    {
        internal static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new StoreDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<StoreDbContext>>());
            if (context.Products.Any())
                return;

            context.Products.AddRange(
                new Product
                {
                    ProductId = 1,
                    Class = "Identifier1",
                    Type = "PRODUCT",
                    Name = "Levis 511 Slim Fit",
                    Reference = "Ref1",
                    Price = 899,
                    //ImageUrl = "https://testimage.se",
                    //ItemUrl = "https://testitem.se",
                    DiscountAmount = 0
                }, new Product
                {
                    ProductId = 2,
                    Class = "Identifier1",
                    Type = "PRODUCT",
                    Name = "Levis 501 Jeans",
                    Reference = "Ref2",
                    Price = 1190,
                    DiscountAmount = 0
                },
                new Product
                {
                    ProductId = 3,
                    Class = "Identifier1",
                    Type = "PRODUCT",
                    Name = "Levis 512 Jeans",
                    Reference = "Ref3",
                    Price = 1000,
                    DiscountAmount = 100,
                    VatPercentage = 10
                },
                new Product
                {
                    ProductId = 4,
                    Class = "Identifier1",
                    Type = "PRODUCT",
                    Name = "Levis 520 Jeans",
                    Reference = "Ref4",
                    Price = 1000,
                    DiscountPercent = 20,
                    VatPercentage = 20
                }); ;

            context.SaveChanges();
        }
    }
}
