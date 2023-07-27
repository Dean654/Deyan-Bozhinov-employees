using System;
namespace EmployeesApp.Services.Models
{
	public class ProjectDetails
	{
		public long ProjectId { get; set; }
		public long EmployeeId { get; set; }
		public DateTime DateTo { get; set; }
		public DateTime DateFrom { get; set; }
	}
}

