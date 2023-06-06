namespace DogApp.Application.Models
{
    public sealed record ErrorResponse
    {
        public IEnumerable<ErrorModel> Errors { get; set; }

        public ErrorResponse(params ErrorModel[] errors) 
        {
            Errors = errors;
        }

        public ErrorResponse(IEnumerable<ErrorModel> errors) 
        {
            Errors = errors;
        }
    }
}
