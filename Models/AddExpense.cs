using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using Spendr.Models;
using System.Text.Json;
using System.Net;
using Spendr.Utilities;

namespace Spendr.Expenses
{

    public class AddExpenseResponse
    {
        [CosmosDBOutput(databaseName: "Spendr", containerName: "Expenses", PartitionKey = "Username", Connection = "CosmosDbConnectionString")]
        public Expense? Expense { get; set; }
        public required HttpResponseData HttpResponse { get; set; }
    }
    public class AddExpense
    {
        private readonly ILogger<AddExpense> _logger;

        public AddExpense(ILogger<AddExpense> logger)
        {
            _logger = logger;
        }

        [Function("AddExpense")]
        public async Task<AddExpenseResponse> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                var expenseData = await req.ReadAsStringAsync();
                _logger.LogInformation($"Received request body: {expenseData}");

                if (string.IsNullOrWhiteSpace(expenseData))
                    throw new ArgumentException("Invalid request body");

                if (!expenseData.Contains("Username") || !expenseData.Contains("Description") || !expenseData.Contains("Amount"))
                {
                    throw new ArgumentException("Missing required fields: 'Username', 'Description', or 'Amount'");
                }

                Expense? expense = JsonSerializer.Deserialize<Expense>(expenseData);
                if (expense == null)
                    throw new ArgumentException("Expense data is invalid");

                ValidateExpense(expense);

                _logger.LogInformation($"Expense added for {expense.Username}");

                var successResponse = new ApiResponse<Expense>
                {
                    status = "success",
                    message = "Expense created successfully",
                    data = expense
                };

                return new AddExpenseResponse
                {
                    Expense = expense,
                    HttpResponse = await req.CreateJsonResponseAsync(HttpStatusCode.Created, successResponse)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing AddExpense function");

                var errorResponse = new ApiResponse<object>
                {
                    status = "error",
                    message = "Failed to create expense",
                    error = new { details = ex.Message }
                };

                return new AddExpenseResponse
                {
                    Expense = null,
                    HttpResponse = await req.CreateJsonResponseAsync(HttpStatusCode.BadRequest, errorResponse)
                };
            }
        }

        private void ValidateExpense(Expense expense)
        {
            if (string.IsNullOrWhiteSpace(expense.Username))
            {
                throw new ArgumentException($"{nameof(expense.Username)} cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(expense.Description))
            {
                throw new ArgumentException($"{nameof(expense.Description)} cannot be null.");
            }

            if (expense.Amount <= 0)
            {
                throw new ArgumentException($"{nameof(expense.Amount)} must be greater than 0");
            }
        }
    }
}
