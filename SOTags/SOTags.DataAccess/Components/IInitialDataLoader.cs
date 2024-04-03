using SOTags.DataAccess.Entities;

namespace SOTags.DataAccess.Components
{
    public interface IInitialDataLoader
    {
        void LoadData(List<Tag> tagList, DatabaseDbContext context);
    }
}