using System;
using System.Text.Json;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace TicketsFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Function1))]
        public async Task Run([QueueTrigger("tickets", Connection = "AzureWebJobsStorage")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");

            // NOTE: the JSON deserializer is case-sensitive
            string json = message.MessageText;

            // Deserialize the message JSON into a Ticket Object
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Deserialize JSON into a Ticket object
            Ticket? ticket = JsonSerializer.Deserialize<Ticket>(json);

            if (ticket == null)
            {
                _logger.LogError("failed to message");
                return;
            }

            _logger.LogInformation($"Ticket received: ConcertId = {ticket.concertId}, Name = {ticket.name}");

            // get connection string from app settings
            string? connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("SQL connection string is not set in the environment variables.");
            }

            // Using statement ensures the connection is closed when done
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync(); // Note the ASYNC

                var query = "INSERT INTO dbo.Tickets (concertId, name, email, phone, quantity, creditCard, expiration, securityCode, address, city, province, postalCode, country) " +
                            "VALUES (@concertId, @name, @email, @phone, @quantity, @creditCard, @expiration, @securityCode, @address, @city, @province, @postalCode, @country)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@concertId", ticket.concertId);
                    cmd.Parameters.AddWithValue("@name", ticket.name);
                    cmd.Parameters.AddWithValue("@email", ticket.email);
                    cmd.Parameters.AddWithValue("@phone", ticket.phone);
                    cmd.Parameters.AddWithValue("@quantity", ticket.quantity);
                    cmd.Parameters.AddWithValue("@creditCard", ticket.creditCard);
                    cmd.Parameters.AddWithValue("@expiration", ticket.expiration);
                    cmd.Parameters.AddWithValue("@securityCode", ticket.securityCode);
                    cmd.Parameters.AddWithValue("@address", ticket.address);
                    cmd.Parameters.AddWithValue("@city", ticket.city);
                    cmd.Parameters.AddWithValue("@province", ticket.province);
                    cmd.Parameters.AddWithValue("@postalCode", ticket.postalCode);
                    cmd.Parameters.AddWithValue("@country", ticket.country);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            _logger.LogInformation("Ticket data added to the database");
        }
    }
}
