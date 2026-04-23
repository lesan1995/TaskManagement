namespace TaskManagement.SharedKernel.Results
{
    public class ValidationError
    {
        public string? Identifier { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
