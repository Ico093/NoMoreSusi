using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Rooms
{
	public class RoomViewModel:IMapFrom<Room>
	{
		public int Id { get; set; }

		public Facultity Facultity { get; set; }

		public int Number { get; set; }

		public int PeopleCount { get; set; }
	}
}