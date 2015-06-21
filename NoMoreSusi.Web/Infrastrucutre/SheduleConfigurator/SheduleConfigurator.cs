using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NoMoreSusi.Common.Helpers;
using NoMoreSusi.Data.Interfaces;
using NoMoreSusi.Models;

namespace NoMoreSusi.Web.Infrastrucutre.SheduleConfigurator
{
	public static class SheduleConfigurator
	{
		private static int min_hour = 9;
		private static int max_hour = 18;

		private static INoMoreSusiData _data;

		private static IList<Lecture> _lecturesForDistribution = new List<Lecture>();

		public static void GenerateShedule(INoMoreSusiData data)
		{
			_data = data;

			RemoveDataFromShedules();

			DistributeLectures();

			DistributeRemainingLectures();
		}

		private static void RemoveDataFromShedules()
		{
			var shedules = _data.Shedules.All().ToList();

			foreach (var shedule in shedules)
			{
				_data.Shedules.Delete(shedule);
			}

			_data.SaveChanges();
		}

		private static void DistributeLectures()
		{
			var teachers = _data.Teachers.All().ToList();

			foreach (var teacher in teachers)
			{
				foreach (var lecture in teacher.Lectures)
				{
					if (!_data.Shedules.All()
						.Any(s =>
								s.RoomId == lecture.RoomId && s.Day == lecture.Day &&
								(s.Hour >= lecture.Hour && s.Hour <= (lecture.Hour + lecture.Length))) && lecture.Room.PeopleCount >= lecture.Students.Count)
					{
						SaveLectureToHour(lecture, lecture.Hour, lecture.Room, lecture.Day);
					}
					else
					{
						_lecturesForDistribution.Add(lecture);
					}
				}
			}
		}

		private static void DistributeRemainingLectures()
		{
			foreach (var lecture in _lecturesForDistribution)
			{
				if (!FindRoomAndHourAndDay(lecture))
				{
					_data.Unsheduleds.Add(new Unsheduled()
					{
						Lecture = lecture
					});
				}
			}
		}

		private static bool FindRoomAndHourAndDay(Lecture lecture)
		{
			if (FindRoomAndHour(lecture, lecture.Day))
			{
				return true;
			}

			var daysOfWeek = EnumToListHelper.TransformDaysOfWeek().Where(d => d != lecture.Day);

			foreach (var day in daysOfWeek)
			{
				if (FindRoomAndHour(lecture, day))
				{
					return true;
				}
			}

			return false;
		}

		private static bool FindRoomAndHour(Lecture lecture, DayOfWeek day)
		{
			if (FindRoom(lecture, lecture.Hour, day))
			{
				return true;
			}

			var lectures = _data.Shedules.All().Where(s => s.RoomId == lecture.RoomId && s.Day == day).ToList();

			for (int currentHour = lecture.Hour; currentHour >= min_hour; currentHour--)
			{
				if (!lectures.Any(l => l.Hour >= currentHour && l.Hour <= (currentHour + lecture.Length)) && lecture.Room.PeopleCount >= lecture.Students.Count)
				{
					if (FindRoom(lecture, currentHour, day))
					{
						return true;
					}
				}
			}

			for (int currentHour = lecture.Hour; currentHour <= max_hour; currentHour++)
			{
				if (!lectures.Any(l => l.Hour >= currentHour && l.Hour <= (currentHour + lecture.Length)) && lecture.Room.PeopleCount >= lecture.Students.Count)
				{
					if (FindRoom(lecture, currentHour, day))
					{
						return true;
					}
				}
			}

			return false;
		}

		private static bool FindRoom(Lecture lecture, int hour, DayOfWeek day)
		{
			if (FindExact(lecture, hour, lecture.Room, day))
			{
				return true;
			}

			var rooms = _data.Rooms.All().Where(r => r.PeopleCount >= lecture.Students.Count).ToList();

			foreach (var lectureRoom in rooms)
			{
				if (FindExact(lecture, hour, lectureRoom, day))
				{
					return true;
				}
			}

			return false;
		}

		private static bool FindExact(Lecture lecture, int hour, Room room, DayOfWeek day)
		{
			if (!_data.Shedules.All()
					.Any(s => s.Day == day && s.Hour >= hour && s.Hour <= (hour + lecture.Length) && s.RoomId == room.Id))
			{
				SaveLectureToHour(lecture, hour, room, day);
				return true;
			}

			return false;
		}


		private static void SaveLectureToHour(Lecture lecture, int currentHour, Room room, DayOfWeek day)
		{
			_data.Shedules.Add(new Shedule()
			{
				Hour = currentHour,
				Day = day,
				Lecture = lecture,
				Room = room
			});

			_data.SaveChanges();
		}
	}
}