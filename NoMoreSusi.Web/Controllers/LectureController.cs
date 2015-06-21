using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
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

		[Authorize(Roles = GlobalConstants.AdminRoleName + "," + GlobalConstants.TeacherRoleName + "," + GlobalConstants.StudentRoleName)]
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
		[Authorize(Roles = GlobalConstants.TeacherRoleName)]
		public ActionResult Add()
		{
			var viewmodel = new AddLectureForPageViewModel()
			{
				AddLectureViewModel = new AddLectureViewModel(),
				Dates = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>(),
				Courses = Enum.GetValues(typeof(Course)).Cast<Course>(),
				Facultities = Enum.GetValues(typeof(Facultity)).Cast<Facultity>(),
				Rooms = Data.Rooms.All().Where(r => r.Facultity == (Facultity)1).Project().To<RoomViewModel>().ToList()
			};

			return View(viewmodel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = GlobalConstants.TeacherRoleName)]
		public ActionResult Add(AddLectureViewModel viewmodel)
		{
			if (ModelState.IsValid && viewmodel != null)
			{
				var model = Mapper.Map<Lecture>(viewmodel);

				var userId = User.Identity.GetUserId();
				var teacher = Data.Teachers.All().FirstOrDefault(t => t.UserId == userId);

				model.TeacherId = teacher.Id;
				var students = Data.Students.All().Where(s => s.Course == model.Course).ToList();

				foreach (var student in students)
				{
					model.Students.Add(student);
				}

				Data.Lectures.Add(model);
				Data.SaveChanges();

				return RedirectToAction("All");
			}

			var viewmodelForPage = new AddLectureForPageViewModel()
			{
				AddLectureViewModel = viewmodel,
				Dates = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>(),
				Courses = Enum.GetValues(typeof(Course)).Cast<Course>(),
				Facultities = Enum.GetValues(typeof(Facultity)).Cast<Facultity>(),
				Rooms = Data.Rooms.All().Where(r => r.Facultity == (Facultity)1).Project().To<RoomViewModel>().ToList()
			};

			return View(viewmodelForPage);
		}

		[Authorize(Roles = GlobalConstants.TeacherRoleName)]
		public JsonResult RoomsForFacultity(int id)
		{
			var viewmodel = Data.Rooms
				.All()
				.Where(r => r.Facultity == (Facultity)id)
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
		[Authorize(Roles = GlobalConstants.AdminRoleName + ", " + GlobalConstants.TeacherRoleName)]
		public ActionResult Edit(int id)
		{
			var viewmodel = new EditLectureForPageViewModel()
			{
				EditLectureViewModel = Mapper.Map<EditLectureViewModel>(this.Data.Lectures.GetById(id)),
				Dates = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>(),
				Facultities = Enum.GetValues(typeof(Facultity)).Cast<Facultity>(),
				Rooms = Data.Rooms.All().Where(r => r.Facultity == (Facultity)1).Project().To<RoomViewModel>().ToList()
			};

			return View(viewmodel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = GlobalConstants.AdminRoleName + ", " + GlobalConstants.TeacherRoleName)]
		public ActionResult Edit(EditLectureViewModel viewmodel)
		{
			if (ModelState.IsValid && viewmodel != null)
			{
				var model = Data.Lectures.GetById(viewmodel.Id);

				model.Name = viewmodel.Name;
				model.Day = viewmodel.Day;
				model.Hour = viewmodel.Hour;
				model.Length = viewmodel.Length;
				model.RoomId = viewmodel.RoomId;

				Data.Lectures.Update(model);
				Data.SaveChanges();

				return RedirectToAction("All");
			}

			var viemodelForPage = new EditLectureForPageViewModel()
			{
				EditLectureViewModel = viewmodel,
				Dates = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>(),
				Facultities = Enum.GetValues(typeof(Facultity)).Cast<Facultity>(),
				Rooms = Data.Rooms.All().Where(r => r.Facultity == (Facultity)1).Project().To<RoomViewModel>().ToList()
			};

			return View(viemodelForPage);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = GlobalConstants.AdminRoleName)]
		public ActionResult Delete(int id)
		{
			Data.Lectures.Delete(id);
			Data.SaveChanges();

			return RedirectToAction("All");
		}
	}
}