using ClienteApp.Repositories;
using ClienteApp.Services;
using ClienteApp.Models;

const string dataPath = "Data/customers.json";
Directory.CreateDirectory("Data");

var repo = new JsonCustomerRepository(dataPath);
var service = new CustomerService(repo);

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Sistema de Gerenciamento de Clientes ===");
    Console.WriteLine("1. Listar Clientes");
    Console.WriteLine("2. Adicionar Cliente");
    Console.WriteLine("3. Atualizar Cliente");
    Console.WriteLine("4. Deletar Cliente");
    Console.WriteLine("5. Ver detalhes");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");
    var opt = Console.ReadLine();

    try
    {
        if (opt == "1")
        {
            var list = await service.ListAsync();
            foreach (var c in list) Console.WriteLine($"{c.Id} | {c.Name} | {c.Email} | {c.Phone}");
            Console.WriteLine("Enter para continuar...");
            Console.ReadLine();
        }
        else if (opt == "2")
        {
            Console.Write("Nome: ");
            var name = Console.ReadLine()!;
            Console.Write("Email: ");
            var email = Console.ReadLine()!;
            Console.Write("Telefone: ");
            var phone = Console.ReadLine()!;
            var created = await service.CreateAsync(name, email, phone);
            Console.WriteLine($"Criado: {created.Id}");
            Console.ReadLine();
        }
        else if (opt == "3")
        {
            Console.Write("Id do CLiente: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                Console.WriteLine("Enter para continuar...");
                Console.ReadLine();
                continue;
            }
            var c = await service.GetByIdAsync(id);
            if (c == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                Console.WriteLine("Enter para continuar...");
                Console.ReadLine();
                continue;
            }
            Console.Write($"Nome ({c.Name}): ");
            var name = Console.ReadLine();
            Console.Write($"Email ({c.Email}): ");
            var email = Console.ReadLine();
            Console.Write($"Telefone ({c.Phone}): ");
            var phone = Console.ReadLine();

            var updated = c with
            {
                Name = string.IsNullOrWhiteSpace(name) ? c.Name : name.Trim(),
                Email = string.IsNullOrWhiteSpace(email) ? c.Email : email.Trim(),
                Phone = string.IsNullOrWhiteSpace(phone) ? c.Phone : phone.Trim()
            };
            await service.UpdateAsync(updated);
            Console.WriteLine("Cliente atualizado.");
            Console.ReadLine();
        }
        else if (opt == "4")
        {
            Console.Write("Id do CLiente: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                Console.WriteLine("Enter para continuar...");
                Console.ReadLine();
                continue;
            }
            await service.DeleteAsync(id);
            Console.WriteLine("Cliente excluído.");
            Console.ReadLine();
        }
        else if (opt == "5")
        {
            Console.Write("Id do CLiente: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                Console.WriteLine("Enter para continuar...");
                Console.ReadLine();
                continue;
            }
            var c = await service.GetByIdAsync(id);
            if (c == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                Console.WriteLine("Enter para continuar...");
                Console.ReadLine();
                continue;
            }
            Console.WriteLine($"ID: {c.Id}");
            Console.WriteLine($"Nome: {c.Name}");
            Console.WriteLine($"Email: {c.Email}");
            Console.WriteLine($"Telefone: {c.Phone}");
            Console.WriteLine($"Criado em: {c.CreatedAt}");
            Console.WriteLine("Enter para continuar...");
            Console.ReadLine();
        }
        else if (opt == "0")
        {
            break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
        Console.ReadLine();
    }
}