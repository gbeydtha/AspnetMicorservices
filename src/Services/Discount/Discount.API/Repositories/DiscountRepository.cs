﻿using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); 
        }
        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSetttings:ConnectionString"));
            
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("Select * from Coupon where ProductName = @ProductName", new { ProductName = productName }); 
            if(coupon == null)
            {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" }; 
            }

            return coupon; 
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSetttings:ConnectionString"));
            var affected = await connection.ExecuteAsync(
                "insert into coupon(produtname, description, amount) values(@ProductName, @Description, @Amount)", 
                new {ProductName =coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if(affected == 0)
            {
                return false; 
            }

            return true; 
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
              (_configuration.GetValue<string>("DatabaseSetttings:ConnectionString"));

            var affected = await connection.ExecuteAsync(
                "update coupon set produtname = @ProductName, description = @Description, amount = @Amount where id = @Id",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            if (affected == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSetttings:ConnectionString"));

            var affected = await connection.ExecuteAsync( "delete from  coupon where  produtname = @ProductName", new { ProductName = productName });

            if (affected == 0)
            {
                return false;
            }

            return true;
        }




    }
}
