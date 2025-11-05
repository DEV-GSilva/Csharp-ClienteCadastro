using ClienteApp.Models;
using ClienteApp.Repositories;

namespace ClienteApp.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repo;
        public CustomerService(ICustomerRepository repo) => _repo = repo;
        public async Task<IEnumerable<Customer>> ListAsync() => await _repo.GetAllAsync();

        public async Task<Customer> CreateAsync(string name, string email, string phone)
        {
            if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("O nome do cliente é obrigatório.");
            if(!email.Contains("@")) throw new ArgumentException("O email do cliente é inválido.");
            var customer = new Customer
            {
                Name = name.Trim(),
                Email = email.Trim(),
                Phone = phone?.Trim() ?? ""
            };
            await _repo.AddAsync(customer);
            return customer;
        }

        public async Task UpdateAsync(Customer c) => await _repo.UpdateAsync(c);
        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);
        public async Task<Customer?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);
    }
}