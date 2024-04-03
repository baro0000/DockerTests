using SOTags.DataAccess.Entities;

namespace SOTags.DataAccess.Components
{
    public class InitialDataLoader : IInitialDataLoader
    {
        public void LoadData(List<Tag> tagList, DatabaseDbContext context)
        {
            foreach (var tag in tagList)
            {
                context.Tags.Add(tag);
            }
            context.SaveChanges();
        }
    }
}
