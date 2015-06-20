using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NoMoreSusi.Models;
using NoMoreSusi.Web.ViewModels.Rooms;

namespace NoMoreSusi.Web.ViewModels.Lectures
{
	public class EditLectureForPageViewModel
	{
		public EditLectureViewModel EditLectureViewModel { get; set; }

		public IEnumerable<DayOfWeek> Dates { get; set; }

		[Display(Name = "Facultity")]
		public IEnumerable<Facultity> Facultities { get; set; }

		[Display(Name = "Room")]
		public IEnumerable<RoomViewModel> Rooms { get; set; }
	}
}