namespace Spendr.Utilities;

public class ApiResponse<T>
    {
        public required string status { get; set; }
        public required string message { get; set; }
        public T? data { get; set; }
        public object? error { get; set; }
    }