using MicroserviceArchitecture.Services.Customer.Contracts.Interfaces;
using MicroserviceArchitecture.Services.Customer.Model.Entities;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicroserviceArchitecture.Services.Customer.Infra.Redis.Repositories
{
    public class CustomerCacheRepository : ICustomerRepository
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IDatabase redisDatabase;

        public CustomerCacheRepository(ICustomerRepository customerRepository, IDatabase redisDatabase)
        {
            this.customerRepository = customerRepository;
            this.redisDatabase = redisDatabase;
        }

        public async Task<Client> AddAsync(Client entity)
        {
            Client client = await customerRepository.AddAsync(entity);

            await Task.WhenAll(
                SetClients(client), 
                redisDatabase.StringSetAsync($"Client:{client.Id}", JsonSerializer.Serialize(client)));

            return client;
        }

        private async Task SetClients(Client client)
        {
            RedisValue? clientValues = await redisDatabase.StringGetAsync("Clients");
            List<Client> clients = new List<Client>();
            if (clientValues != null && clientValues.HasValue && !clientValues.Value.IsNullOrEmpty)
            {
                clients = JsonSerializer.Deserialize<List<Client>>(clientValues);
            }
            clients.Add(client);
            await redisDatabase.StringSetAsync("Clients", JsonSerializer.Serialize(clients));
        }

        private async Task RemoveClients(Client client)
        {
            RedisValue? clientValues = await redisDatabase.StringGetAsync("Clients");
            List<Client> clients = new List<Client>();
            if (clientValues != null && clientValues.HasValue && !clientValues.Value.IsNullOrEmpty)
            {
                clients = JsonSerializer.Deserialize<List<Client>>(clientValues);

            }
            await redisDatabase.StringSetAsync("Clients", JsonSerializer.Serialize(clients.Where(x => x.Id != client.Id).ToList()));
        }

        public async Task DeleteAsync(Client entity)
        {
            await customerRepository.DeleteAsync(entity);
            await Task.WhenAll(
                redisDatabase.KeyDeleteAsync($"Client:{entity.Id}"),
                RemoveClients(entity));
        }

        public async Task<IReadOnlyList<Client>?> GetAllAsync()
        {
            string cacheKey = "Clients";
            RedisValue? clients = await redisDatabase.StringGetAsync(cacheKey);

            if (clients != null && clients.HasValue && !clients.Value.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<IReadOnlyList<Client>>(clients);
            }

            IReadOnlyList<Client>? result = await customerRepository.GetAllAsync();
            await redisDatabase.StringSetAsync(cacheKey, JsonSerializer.Serialize(result));
            return result;
        }

        public async Task<Client?> GetByIdAsync(Guid id)
        {
            string cacheKey = $"Client:{id}";
            RedisValue? client = await redisDatabase.StringGetAsync(cacheKey);

            if (client != null && client.HasValue && !client.Value.IsNull)
            {
                return JsonSerializer.Deserialize<Client>(client.Value);
            }

            Client? result = await customerRepository.GetByIdAsync(id);
            await redisDatabase.StringSetAsync(cacheKey, JsonSerializer.Serialize(result));
            return result;
        }

        public async Task<Client> UpdateAsync(Client entity)
        {
            Client client = await customerRepository.UpdateAsync(entity);
            await redisDatabase.StringSetAsync($"Client:{client.Id}", JsonSerializer.Serialize(client));
            return client;

        }
    }
}