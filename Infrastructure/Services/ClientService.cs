using Core.DTO;
using Core.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class  ClientService : IClientService
    {
        private readonly MyDBcontext _context;

        public ClientService(MyDBcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetClientDto>> GetClientsAsync()
        {
            return await _context.Clients
                .Select(c => new GetClientDto(c))
                .ToListAsync();
        }

        public async Task<IEnumerable<GetClientDto>> GetClientsByPageAsync(int page, int pageSize)
        {
            return await _context.Clients
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new GetClientDto(c))
                .ToListAsync();
        }


        public async Task<GetClientDto> GetClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return null;
            }

            return new GetClientDto(client);
        }

        public async Task<IEnumerable<GetClientDto>> GetClientsWithFilterAsync(FilterClientDto filter)
        {
            var query = await _context.Clients
                .AsNoTracking()
                .Select(c => new GetClientDto(c))
                .ToListAsync();

            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(o => o.FirstName == filter.FirstName).ToList();

            if (!string.IsNullOrEmpty(filter.SecondName))
                query = query.Where(o => o.SecondName == filter.SecondName).ToList();

            if (filter.day > 0)
                query = query.Where(o => o.BirthDate.Day == filter.day.Value).ToList();

            if (filter.month > 0)
                query = query.Where(o => o.BirthDate.Month == filter.month.Value).ToList();

            if (filter.year > 0)
                query = query.Where(o => o.BirthDate.Year == filter.year.Value).ToList();

            return query;
        }

        public async Task<GetClientDto> UpdateClientAsync(int id, UpdateClientDto PutClient)
        {
            Client newClient = new Client
            {
                id = id,
                FirstName = PutClient.FirstName,
                SecondName = PutClient.SecondName,
                BirthDate = PutClient.BirthDate
            };

            _context.Entry(newClient).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return new GetClientDto(newClient);

        }

        public async Task<GetClientDto> CreateClientAsync(CreateClientDto clientDto)
        {
            var client = new Client
            {
                FirstName = clientDto.FirstName,
                SecondName = clientDto.SecondName,
                BirthDate = clientDto.BirthDate
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return new GetClientDto(client);
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.id == id);
        }


        public async Task<bool> ClientExistsAsync(int id)
        {
            return await _context.Clients.AnyAsync(c => c.id == id);
        }
    }

}
