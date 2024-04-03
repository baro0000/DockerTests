using AutoMapper;

namespace SOTags.ApplicationServices.Mappings
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            this.CreateMap<SOTags.DataAccess.Entities.Tag, API.Domain.Models.Tag>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Count, y => y.MapFrom(z => z.Count));
        }
    }
}
