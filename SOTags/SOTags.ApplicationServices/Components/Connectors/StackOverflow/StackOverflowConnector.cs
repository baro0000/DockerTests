using System.IO.Compression;

namespace SOTags.ApplicationServices.Components.Connectors.StackOverflow
{
    public class StackOverflowConnector : IStackOverflowConnector
    {
        public string? Error { get; set; }

        public async Task DownloadData()
        {
            int pageSize = 100;
            int page = 1;
            bool hasMore = true;
            HttpClient client = new HttpClient();
            var file = Config.stackOverflowJsonFile;

            if (File.Exists(file))
            {
                File.Delete(file);
            }

            try
            {
                while (hasMore)
                {
                    string apiUrl = $"{Config.stackOverflowUrl}2.3/tags?site=stackoverflow&pagesize={pageSize}&page={page}&key={Config.apiKey}";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    string responseBody = "";

                    if (response.IsSuccessStatusCode)
                    {
                        if (response.Content.Headers.ContentEncoding.Contains("gzip"))
                        {
                            using (Stream stream = await response.Content.ReadAsStreamAsync())
                            using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress))
                            using (StreamReader reader = new StreamReader(gzipStream))
                            {
                                responseBody = await reader.ReadToEndAsync();
                            }
                        }

                        using (var writer = File.AppendText(file))
                        {
                            writer.WriteLine(responseBody);
                        }

                        if (page >= 10)
                        {
                            break;
                        }

                        if (!responseBody.Contains("\"has_more\":true"))
                        {
                            hasMore = false;
                        }

                        page++; 
                    }
                    else
                    {
                        throw new Exception("Couldn't connect with Stack Overflow page");
                    }
                }
            }
            catch (Exception ex)
            {
                Error = "Error occured: " + ex.Message;
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}