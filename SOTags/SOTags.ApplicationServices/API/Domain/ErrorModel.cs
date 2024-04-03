namespace SOTags.ApplicationServices.API.Domain
{
    public class ErrorModel
    {
        public string Error { get; }
        public ErrorModel(string error)
        {
            Error = error;
        }
    }
}
