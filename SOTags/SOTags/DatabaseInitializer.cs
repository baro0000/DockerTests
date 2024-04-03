using SOTags.ApplicationServices.Components;
using SOTags.ApplicationServices.Components.Connectors.StackOverflow;
using SOTags.DataAccess;
using SOTags.DataAccess.Components;
using SOTags.DataAccess.Entities;

namespace SOTags
{
    public class DatabaseInitializer
    {
        private readonly IStackOverflowConnector connector;
        private readonly IInitialDataLoader dataLoader;
        private readonly IStackOverflowJsonReader jsonReader;

        public DatabaseInitializer(IStackOverflowConnector connector, IInitialDataLoader dataLoader, IStackOverflowJsonReader jsonReader)
        {
            this.connector = connector;
            this.dataLoader = dataLoader;
            this.jsonReader = jsonReader;
        }
        public async Task Initialize(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseDbContext>();

                // Ensure the database is created and apply migrations
                await dbContext.Database.EnsureCreatedAsync();

                // Check if Tags table is empty
                if (!dbContext.Tags.Any())
                {
                    await connector.DownloadData();
                    if (connector.Error == null)
                    {
                        List<Tag> tagList = jsonReader.ReadFile();
                        dataLoader.LoadData(tagList, dbContext);
                    }
                }
            }
        }
    }
}
