using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using NoMoreSusi.Common;
using NoMoreSusi.Data.Interfaces;
using NoMoreSusi.Models;
using NoMoreSusi.Web.ViewModels.Lectures;
using NoMoreSusi.Web.ViewModels.Rooms;

namespace NoMoreSusi.Web.Controllers
{
	public class LectureController : BaseController
	{
		public LectureController(INoMoreSusiData data)
			: base(data)
		{
		}

		public ActionResult All()
		{
			var viewmodel = Data.Lectures
				.All()
				.Project()
				.To<LectureViewModel>()
				.ToList();

			return View(viewmodel);
		}

		[HttpGet]
		[Authorize(Roles = GlobalConstants.AdminRoleName)]
		public ActionResult Add()
		{
			var viewmodel = new AddLectureForPageViewModel()
			{
				AddLectureViewModel = new AddLectureViewModel()
				{
				},
				Dates = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>(),
				Courses = Enum.GetValues(typeof(Course)).Cast<Course>(),
				Facultities = Enum.GetValues(typeof(Facultity)).Cast<Facultity>(),
				Rooms = Data.Rooms.All().Where(r=>r.Facultity==(Facultity)1).Project().To<RoomViewModel>().ToList()
			};

			return View(viewmodel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = GlobalConstants.AdminRoleName)]
		public ActionResult Add(AddLectureViewModel viewmodel)
		{
			if (ModelState.IsValid && viewmodel != null)
			{
				var model = Mapper.Map<Lecture>(viewmodel);

				Data.Lectures.Add(model);
				Data.SaveChanges();

				return RedirectToAction("All");
			}

			return View(viewmodel);
		}

		public JsonResult RoomsForFacultity(int id)
		{
			var viewmodel = Data.Rooms
				.All()
				.Where(r => r.Facultity == (Facultity) id)
				.Project()
				.To<RoomViewModel>()
				.ToList();

			return new JsonResult
			{
				Data = viewmodel,
				JsonRequestBehavior = JsonRequestBehavior.AllowGet
			};
		}

		[HttpGet]
		[Authorize(Roles = GlobalConstants.AdminRoleName)]
		public ActionResult Edit(int id)
		{
			var viewmodel = Mapper.Map<EditRoomViewModel>(this.Data.Rooms.GetById(id));

			return View(viewmodel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = GlobalConstants.AdminRoleName)]
		public ActionResult Edit(EditRoomViewModel viewmodel)
		{
			if (ModelState.IsValid && viewmodel != null)
			{
				var model = Mapper.Map<Room>(viewmodel);

				Data.Rooms.Update(model);
				Data.SaveChanges();

				return RedirectToAction("All");
			}

			return View(viewmodel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = GlobalConstants.AdminRoleName)]
		public ActionResult Delete(int id)
		{
			Data.Rooms.Delete(id);
			Data.SaveChanges();

			return RedirectToAction("All");
		}
	}
}