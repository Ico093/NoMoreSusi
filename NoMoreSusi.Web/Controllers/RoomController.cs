using System;
using System.Web.Mvc;
using AutoMapper;
using NoMoreSusi.Data.Interfaces;
using NoMoreSusi.Models;
using NoMoreSusi.Web.ViewModels.Rooms;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace NoMoreSusi.Web.Controllers
{
	[Authorize(Roles="Administrator")]
	public class RoomController : BaseController
	{
		public RoomController(INoMoreSusiData data)
			: base(data)
		{

		}

		public ActionResult All()
		{
			var viewmodel = Data.Rooms
				.All()
				.Project()
				.To<RoomViewModel>()
				.ToList();

			return View(viewmodel);
		}

		[HttpGet]
		public ActionResult Add()
		{
			var viewmodel = new AddRoomViewModel();

			return View(viewmodel);
		}

		[HttpPost]
		[ValidateAntiForgeryTokenAttribute]
		public ActionResult Add(AddRoomViewModel viewmodel)
		{
			if (ModelState.IsValid && viewmodel != null)
			{
				var model = Mapper.Map<Room>(viewmodel);

				Data.Rooms.Add(model);
				Data.SaveChanges();

				return RedirectToAction("All");
			}

			return View(viewmodel);
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var viewmodel = Mapper.Map<EditRoomViewModel>(this.Data.Rooms.GetById(id));

			return View(viewmodel);
		}

		[HttpPost]
		[ValidateAntiForgeryTokenAttribute]
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
		[ValidateAntiForgeryTokenAttribute]
		public ActionResult Delete(int id)
		{
			Data.Rooms.Delete(id);
			Data.SaveChanges();

			return RedirectToAction("All");
		}
	}
}