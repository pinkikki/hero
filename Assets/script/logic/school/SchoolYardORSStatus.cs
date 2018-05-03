using System;

namespace script.logic.school
{
	public class SchoolYardOrsStatus
	{
		private static String classmateOName = "classmateO";
		private static String classmateRName = "classmateR";
		private static String classmateSName = "classmateS";

		public static string ClassmateOName 
		{
			get { return classmateOName; }
			set { classmateOName = value; }
		}

		public static string ClassmateRName
		{
			get { return classmateRName; }
			set { classmateRName = value; }
		}

		public static string ClassmateSName
		{
			get { return classmateSName; }
			set { classmateSName = value; }
		}
	}
}
