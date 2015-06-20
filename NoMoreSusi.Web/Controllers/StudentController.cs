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

namespace NoMoreSusi.Web.Controllers
{
    public class StudentController : BaseController
    {
        public StudentController(INoMoreSusiData data)
            : base(data)
        {

        }

        [HttpGet]
        public ActionResult All()
        {
            var studentsViewModel = Data.Students
                .All()
                .Project()
                .To<StudentsViewModel>()
                .ToList();

            return View(studentsViewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public ActionResult Add()
        {
            var studentViewModel = new StudentViewModel();

            return View(studentViewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public ActionResult Add(StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var studentModel = Mapper.Map<Student>(viewModel);

                var userStore = new UserStore<User>(Data.Context);
                var userManager = new UserManager<User>(userStore);

                var user = new User
                {
                    UserName = viewModel.UserName,
                    Email = viewModel.Email
                };

                userManager.Create(user, viewModel.Password);
                userManager.AddToRole(user.Id, GlobalConstants.StudentRoleName);

                studentModel.User = user;

                Data.Students.Add(studentModel);
                Data.SaveChanges();

                return RedirectToAction("All");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var studentModel = Data.Students.GetById(id);
            var viewModel = Mapper.Map<EditStudentViewModel>(studentModel);

            viewModel.Courses[2].Selected = true;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var studentModel = Mapper.Map<Student>(viewModel);

                Data.Students.Update(studentModel);
                Data.SaveChanges();

                return RedirectToAction("All");
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Data.Students.Delete(id);
            Data.SaveChanges();

            return RedirectToAction("All");
        }
    }
}