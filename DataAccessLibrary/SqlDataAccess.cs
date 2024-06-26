﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public string ConnectionStringName { get; set; } = "DefaultConnection";

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string connectionString = _config.GetConnectionString("DefaultConnection");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);

                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString("DefaultConnection");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, parameters);
            }

        }
        
        public async Task<T> ExecuteScalar<T>(string sql, object parameters = null)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<T>(sql, parameters);
            }
        }
        public async Task<T> LoadSingleRecord<T, U>(string sql, U parameters)
        {
            string connectionString = _config.GetConnectionString("DefaultConnection");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);

                return data;
            }
        }
        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            string connectionString = _config.GetConnectionString("DefaultConnection");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}