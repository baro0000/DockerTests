using SOTags.DataAccess.Entities;

namespace SOTags.ApplicationServices.Components
{
    public interface IStackOverflowJsonReader
    {
        List<Tag> ReadFile();
    }
}