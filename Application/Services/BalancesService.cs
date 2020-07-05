using SampleMvcApp.Models;
using System;
using Dapper;
using System.Threading.Tasks;

namespace SampleMvcApp.Services
{
    public interface IBalancesService
    {
        Task<BalanceViewModel> GetBalancesAsync(int id);
        Task<BalanceViewModel> GetBalancesAsync(string auth0_id);

    }

    public class BalancesService : IBalancesService
    {
        private readonly ISqlService _sqlService;
        private readonly ICustomerService _customerService;

        public BalancesService(ISqlService sqlService, ICustomerService customerService)
        {
            _sqlService = sqlService;
            _customerService = customerService;
        }

        public async Task<BalanceViewModel> GetBalancesAsync(int id)
        {
            const string sql = "SELECT * FROM dbo.Account where CustomerId = @CustomerId";
            var customer = await _customerService.GetAsync(id);
            using (var conn = _sqlService.Connection)
            {
                var accounts = await conn.QueryAsync<Account>(sql, new 
                {
                    CustomerId = id
                });

                return new BalanceViewModel()
                {
                    Accounts = accounts,
                    Name = $"{customer.FirstName} {customer.LastName}"
                };
            }
        }

        public async Task<BalanceViewModel> GetBalancesAsync(string auth0_id)
        {
            var user = await _customerService.GetAsync(auth0_id);
            return await GetBalancesAsync(user.Id);
        }
    }
}
