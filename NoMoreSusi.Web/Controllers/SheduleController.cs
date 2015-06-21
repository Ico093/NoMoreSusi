using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NoMoreSusi.Common;
using NoMoreSusi.Common.Helpers;
using NoMoreSusi.Data.Interfaces;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Infrastrucutre.SheduleConfigurator;

namespace NoMoreSusi.Web.Controllers
{
    public class SheduleController : BaseController
    {
        public SheduleController(INoMoreSusiData data)
            : base(data)
        {
            
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Generate()
        {
            SheduleConfigurator.GenerateShedule(Data);

            return RedirectToAction("Details");
        }

        public ActionResult Export()
        {
            var calendar = CalendarFactory.GetCalendar("PUBLISH", "2.0");

            var shedules = Data.Shedules.All().ToList();

            foreach (var shedule in shedules)
            {
                var location = String.Format("{0} Room {1}", shedule.Room.Facultity, shedule.Room.Number);
                var description = String.Format("Lecture {0} Course", shedule.Lecture.Course);

                CalendarHelper.CreateCalendarEvent(calendar, shedule.Lecture.Name, description, shedule.Hour, shedule.Day,
                    shedule.Lecture.Length, location, shedule.Lecture.Teacher.User.Email, false);
            }

            var contentType = "text/calendar";
            var bytes = CalendarHelper.ExportToByteArray(calendar);

            return File(bytes, contentType, GlobalConstants.SheduleExportFileName);
        }

        public ActionResult MyShedule()
        {
            var userId = User.Identity.GetUserId();

            var shedules = new List<Shedule>();

            if (User.IsInRole(GlobalConstants.StudentRoleName))
            {
                var student = Data.Students.All().FirstOrDefault(s => s.UserId == userId);
                shedules = Data.Shedules.All().Where(s => s.Lecture.Course == student.Course).ToList();
            }
            else if (User.IsInRole(GlobalConstants.TeacherRoleName))
            {
                var teacher = Data.Teachers.All().FirstOrDefault(s => s.UserId == userId);
                shedules = Data.Shedules.All().Where(s => s.Lecture.Teacher.Id == teacher.Id).ToList();
            }

            var calendar = CalendarFactory.GetCalendar("PUBLISH", "2.0");

            foreach (var shedule in shedules)
            {
                var location = String.Format("{0} Room {1}", shedule.Room.Facultity, shedule.Room.Number);
                var description = String.Format("Lecture {0} Course", shedule.Lecture.Course);

                CalendarHelper.CreateCalendarEvent(calendar, shedule.Lecture.Name, description, shedule.Hour, shedule.Day,
                    shedule.Lecture.Length, location, shedule.Lecture.Teacher.User.Email, false);
            }

            var contentType = "text/calendar";
            var bytes = CalendarHelper.ExportToByteArray(calendar);

            return File(bytes, contentType, GlobalConstants.SheduleExportFileName);
        }
    }
}
