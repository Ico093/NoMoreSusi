using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NoMoreSusi.Common;
using NoMoreSusi.Data.Interfaces;
using NoMoreSusi.Models;
using NoMoreSusi.Web.ViewModels.Students;
using NoMoreSusi.Web.ViewModels.Teachers;

namespace NoMoreSusi.Web.Controllers
{
    public class TeacherController : BaseController
    {
        public TeacherController(INoMoreSusiData data)
            : base(data)
        {

        }

        [HttpGet]
        public ActionResult All()
        {
            var teachersViewModel = Data.Teachers
                .All()
                .Project()
                .To<TeachersViewModel>()
                .ToList();

            return View(teachersViewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public ActionResult Add()
        {
            var teacherViewModel = new TeacherViewModel();

            return View(teacherViewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public ActionResult Add(TeacherViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var teacherModel = Mapper.Map<Teacher>(viewModel);

                var userStore = new UserStore<User>(Data.Context);
                var userManager = new UserManager<User>(userStore);

                var user = new User
                {
                    UserName = viewModel.UserName,
                    Email = viewModel.Email
                };

                userManager.Create(user, viewModel.Password);
                userManager.AddToRole(user.Id, GlobalConstants.TeacherRoleName);

                teacherModel.User = user;

                Data.Teachers.Add(teacherModel);
                Data.SaveChanges();

                return RedirectToAction("All");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var teacherModel = Data.Teachers.GetById(id);
            var viewModel = Mapper.Map<EditTeacherViewModel>(teacherModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditTeacherViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var teacherModel = Mapper.Map<Teacher>(viewModel);

                Data.Teachers.Update(teacherModel);
                Data.SaveChanges();

                return RedirectToAction("All");
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Data.Teachers.Delete(id);
            Data.SaveChanges();

            return RedirectToAction("All");
        }
    }
}