using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Rooms
{
	public class EditRoomViewModel : IMapFrom<Room>, IHaveCustomMappings
	{
		public int Id { get; set; }

		public Facultity Facultity { get; set; }

		public int Number { get; set; }

		public int PeopleCount { get; set; }

		public IEnumerable<Facultity> Facultities { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Room, EditRoomViewModel>()
				.ForMember(m => m.Facultities, opt => opt.MapFrom(m => Enum.GetValues(typeof(Facultity)).Cast<Facultity>()));
		}
	}
}