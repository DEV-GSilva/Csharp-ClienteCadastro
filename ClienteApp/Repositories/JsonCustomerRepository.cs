using System.Text.Json;
using ClienteApp.Models;

namespace ClienteApp.Repositories
{
    public class JsonCustomerRepository : ICustomerRepository
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        public JsonCustomerRepository(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        private async Task<List<Customer>> ReadAllAsync()
        {
            var text = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Customer>>(text, _opts) ?? new List<Customer>();
        }
        
        private async Task SaveAllAsync(List<Customer> customers)
        {
            var text = JsonSerializer.Serialize(customers, _opts);
            await File.WriteAllTextAsync(_filePath, text);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() => await ReadAllAsync();
        public async Task<Customer?> GetByIdAsync(Guid id) => (await ReadAllAsync()).FirstOrDefault(c => c.Id == id);
        public async Task AddAsync(Customer customer)
        {
            var list = await ReadAllAsync();
            list.Add(customer);
            await SaveAllAsync(list);
        }

        public async Task UpdateAsync(Customer customer)
        {
            var list = await ReadAllAsync();
            var idx = list.FindIndex(c => c.Id == customer.Id);
            if (idx == -1) throw new KeyNotFoundException("Cliente não encontrado.");
            list[idx] = customer;
            await SaveAllAsync(list);
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var list = await ReadAllAsync();
            list.RemoveAll(c => c.Id == id);
            await SaveAllAsync(list);
        }
    }
}