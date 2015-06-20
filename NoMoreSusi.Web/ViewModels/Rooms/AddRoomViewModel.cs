using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Rooms
{
	public class AddRoomViewModel:IMapFrom<Room>,IHaveCustomMappings
	{
		public AddRoomViewModel()
		{
			Facultities = Enum.GetValues(typeof (Facultity)).Cast<Facultity>();
		}

		public Facultity Facultity { get; set; }

		public int Number { get; set; }

		public int PeopleCount { get; set; }

		public IEnumerable<Facultity> Facultities { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Room, AddRoomViewModel>()
				.ForMember(m => m.Facultities, opt => opt.MapFrom(m => Enum.GetValues(typeof(Facultity)).Cast<Facultity>()));
		}
	}
}