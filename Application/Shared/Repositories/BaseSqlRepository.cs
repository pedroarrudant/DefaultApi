using System;
using System.Data;
using System.Data.SqlClient;
using Application.Shared.Helpers;
using Dapper;
using Polly;
using Polly.Retry;

namespace Application.Shared.Repositories
{
    public class BaseSqlRepository
    {
        private readonly IDbConnection _connection;

        public BaseSqlRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        internal async Task<int> ExecuteAsync(string queryPath, CancellationToken cancellationToken, object? parameters = null, int retries = 3)
        {
            string query = ResourceHelper.Get(queryPath);

            RetryPolicy retryPolicy = Policy.Handle<SqlException>()
                .WaitAndRetry(retries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            int result = await retryPolicy.Execute(() => _connection.ExecuteAsync(new CommandDefinition(query, parameters, cancellationToken: cancellationToken))).ConfigureAwait(false);

            return result;
        }

        internal async Task<int> ExecuteWithoutPutputAsync(string queryPath, CancellationToken cancellationToken, object? parameters = null, int retries = 3)
        {
            string query = ResourceHelper.Get(queryPath);

            RetryPolicy retryPolicy = Policy.Handle<SqlException>()
                .WaitAndRetry(retries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            int result = await retryPolicy.Execute(() => _connection.QuerySingleAsync(new CommandDefinition(query, parameters, cancellationToken: cancellationToken))).ConfigureAwait(false);

            return result;
        }

        internal async Task<IEnumerable<T>> QueryAsync<T>(string queryPath, CancellationToken cancellationToken, object? parameters = null, int retries = 3)
        {
            string query = ResourceHelper.Get(queryPath);

            RetryPolicy retryPolicy = Policy.Handle<SqlException>()
                .WaitAndRetry(retries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            var result = await retryPolicy.Execute(() => _connection.QueryAsync<T>(new CommandDefinition(query, parameters, cancellationToken: cancellationToken))).ConfigureAwait(false);

            return result;
        }

        internal async Task<T> QueryFirstOrDefault<T>(string queryPath, CancellationToken cancellationToken, object? parameters = null, int retries = 3)
        {
            string query = ResourceHelper.Get(queryPath);

            RetryPolicy retryPolicy = Policy.Handle<SqlException>()
                .WaitAndRetry(retries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            var result = await retryPolicy.Execute(() => _connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(query, parameters, cancellationToken: cancellationToken))).ConfigureAwait(false);

            return result;
        }
    }
}

