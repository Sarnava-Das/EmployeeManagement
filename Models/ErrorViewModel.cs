namespace EmployeeManagement.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }     // Stores the current request ID

        public string? Message { get; set; }       // Custom error message to display

        public int? StatusCode { get; set; }       // HTTP status code (e.g., 404, 500)

        public bool ShowRequestId =>               // Determines whether to show RequestId
            !string.IsNullOrEmpty(RequestId);
    }
}
