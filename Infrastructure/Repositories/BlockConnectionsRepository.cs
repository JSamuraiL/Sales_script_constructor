//using Microsoft.EntityFrameworkCore;
using Npgsql;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlockConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Infrastructure.Repositories
{
    public class BlockConnectionsRepository : IBlockConnectionsRepository
    {
        private readonly string _connectionString;

        public BlockConnectionsRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException();
        }

        public async Task AddBlockConnectionAsync(BlockConnection blockConnection)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("INSERT INTO block_connections (id, previous_block_id, next_block_id) VALUES (@id, @previousBlockId, @nextBlockId)", connection))
                {
                    command.Parameters.AddWithValue("id", blockConnection.Id);
                    command.Parameters.AddWithValue("previousBlockId", blockConnection.PreviousBlockId);
                    command.Parameters.AddWithValue("nextBlockId", blockConnection.NextBlockId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public bool BlockConnectionExists(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT COUNT(*) FROM block_connections WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    var count = (long) command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public async Task DeleteBlockConnectionAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("DELETE FROM block_connections WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<BlockConnection> GetBlockConnectionByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT * FROM block_connections WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new BlockConnection
                            {
                                Id = (int)reader["id"],
                                PreviousBlockId = (int)reader["previous_block_id"],
                                NextBlockId = (int)reader["next_block_id"]
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
