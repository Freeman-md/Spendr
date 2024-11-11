using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Spendr.Models;
using Spendr.Utilities;

namespace Spendr.Expenses;

public class GetExpenses
{
    private readonly ILogger<GetExpenses> _logger;

    public GetExpenses(ILogger<GetExpenses> logger)
    {
        _logger = logger;
    }

    [Function("GetExpense")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        [CosmosDBInput(databaseName: "Spendr", containerName: "Expenses", Connection = "CosmosDBConnectionString", SqlQuery = "SELECT * FROM c")] IEnumerable<Expense> expenses
    )
    {
        _logger.LogInformation("Retrieving all expenses from the database.");

        if (expenses == null)
        {
            return await req.CreateJsonResponseAsync(HttpStatusCode.NotFound, new
            {
                status = "error",
                message = "No expenses found."
            });
        }

        // Return the list of expenses in the response
        return await req.CreateJsonResponseAsync(HttpStatusCode.OK, new
        {
            status = "success",
            data = expenses
        });

    }
}