﻿using System;
using AutoMapper;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;
using NoMoreSusi.Web.ViewModels.Rooms;

namespace NoMoreSusi.Web.ViewModels.Lectures
{
	public class LectureViewModel:IMapFrom<Lecture>,IHaveCustomMappings
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DayOfWeek Day { get; set; }

		public int Hour { get; set; }

		public int Length { get; set; }

		public Course Course { get; set; }

		public string Teacher { get; set; }

		public RoomViewModel Room { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Lecture, LectureViewModel>()
				.ForMember(m => m.Teacher, opt => opt.MapFrom(l => l.Teacher.Name));
		}
	}
}