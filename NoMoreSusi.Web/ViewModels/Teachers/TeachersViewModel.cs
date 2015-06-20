using AutoMapper;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Teachers
{
    public class TeachersViewModel : IMapFrom<Teacher>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Teacher, TeachersViewModel>()
                .ForMember(s => s.Email, s => s.MapFrom(st => st.User.Email));
        }
    }
}