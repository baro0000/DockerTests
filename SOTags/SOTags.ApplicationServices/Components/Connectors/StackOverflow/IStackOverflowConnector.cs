namespace SOTags.ApplicationServices.Components.Connectors.StackOverflow
{
    public interface IStackOverflowConnector
    {
        string Error { get; set; }
        Task DownloadData();
    }
}
