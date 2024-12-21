//using Microsoft.EntityFrameworkCore;
using Npgsql;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.ISellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Infrastructure.Repositories
{
    public class SellersRepository : ISellersRepository
    {
        private readonly string _connectionString;

        public SellersRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException();
        }

        public async Task AddSellerAsync(Seller seller)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("INSERT INTO sellers (id, mail, hashed_password, name, surname, patronymic, manager_id) VALUES (@id, @mail, @hashedPassword, @name, @surname, @patronymic, @managerId)", connection))
                {
                    command.Parameters.AddWithValue("id", seller.Id);
                    command.Parameters.AddWithValue("mail", seller.Mail);
                    command.Parameters.AddWithValue("hashedPassword", seller.HashedPassword);
                    command.Parameters.AddWithValue("name", seller.Name);
                    command.Parameters.AddWithValue("surname", seller.Surname);
                    command.Parameters.AddWithValue("patronymic", seller.Patronymic);
                    command.Parameters.AddWithValue("managerId", seller.ManagerId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteSellerAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("DELETE FROM sellers WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Seller> GetSellerByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT * FROM sellers WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Seller
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("id")),
                                Mail = reader.GetString(reader.GetOrdinal("mail")),
                                HashedPassword = reader.GetString(reader.GetOrdinal("hashed_password")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Surname = reader.GetString(reader.GetOrdinal("surname")),
                                Patronymic = reader.GetString(reader.GetOrdinal("patronymic")),
                                ManagerId = reader.GetGuid(reader.GetOrdinal("manager_id"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<Seller>> GetSellersByManagerId(Guid managerId)
        {
            var sellers = new List<Seller>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT * FROM sellers WHERE manager_id = @managerId", connection))
                {
                    command.Parameters.AddWithValue("managerId", managerId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            sellers.Add(new Seller
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("id")),
                                Mail = reader.GetString(reader.GetOrdinal("mail")),
                                HashedPassword = reader.GetString(reader.GetOrdinal("hashed_password")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Surname = reader.GetString(reader.GetOrdinal("surname")),
                                Patronymic = reader.GetString(reader.GetOrdinal("patronymic")),
                                ManagerId = reader.GetGuid(reader.GetOrdinal("manager_id"))
                            });
                        }
                    }
                }
            }
            return sellers;
        }

        public bool SellerExists(Guid id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT COUNT(*) FROM sellers WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    var count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public async Task UpdateSellerAsync(Seller seller)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("UPDATE sellers SET mail = @mail, hashed_password = @hashedPassword, name = @name, surname = @surname, patronymic = @patronymic, manager_id = @managerId WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", seller.Id);
                    command.Parameters.AddWithValue("mail", seller.Mail);
                    command.Parameters.AddWithValue("hashedPassword", seller.HashedPassword);
                    command.Parameters.AddWithValue("name", seller.Name);
                    command.Parameters.AddWithValue("surname", seller.Surname);
                    command.Parameters.AddWithValue("patronymic", seller.Patronymic);
                    command.Parameters.AddWithValue("managerId", seller.ManagerId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
