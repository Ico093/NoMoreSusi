using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Rooms
{
	public class AddRoomViewModel:IMapFrom<Room>
	{
		public Facultity Facultity { get; set; }

		public int Number { get; set; }

		public int PeopleCount { get; set; }
	}
}