using System;
using AutoMapper;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Lectures
{
	public class EditLectureViewModel:IMapFrom<Lecture>,IHaveCustomMappings
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DayOfWeek Day { get; set; }

		public int Hour { get; set; }

		public int Length { get; set; }

		public Facultity Facultity { get; set; }

		public int RoomId { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Lecture, EditLectureViewModel>()
				.ForMember(m => m.Facultity, opt => opt.MapFrom(l => l.Room.Facultity));
		}
	}
}