namespace Spendr.Models;

public class Expense {
    public string id { get; set; } = Guid.NewGuid().ToString();

    public required string Username {get; set; }

    public required string Description { get; set; }
    public required double Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}