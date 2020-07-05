using Dapper;
using SampleMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMvcApp.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetAsync(int id);
        Task<Customer> GetAsync(string auth0_id);
        Task<IEnumerable<Customer>> GetCustomersAsync();
    }

    public class CustomerService : ICustomerService
    {
        private readonly ISqlService _sqlService;

        public CustomerService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            const string sql = "SELECT top 1000 * FROM dbo.Customer";
            using (var conn = _sqlService.Connection)
            {
                return await conn.QueryAsync<Customer>(sql);
            }
        }

        public async Task<Customer> GetAsync(int id)
        {
            const string sql = "SELECT * FROM dbo.Customer where id = @id";
            using (var conn = _sqlService.Connection)
            {
                return (await conn.QueryAsync<Customer>(sql, new
                {
                    Id = id
                })).FirstOrDefault();

            }
        }

        public async Task<Customer> GetAsync(string auth0_id)
        {
            const string sql = "SELECT * FROM dbo.Customer where Auth0_ref = @Auth0_ref";
            using (var conn = _sqlService.Connection)
            {
                return (await conn.QueryAsync<Customer>(sql, new
                {
                    Auth0_ref = auth0_id
                })).FirstOrDefault();
            }
        }
    }
}
