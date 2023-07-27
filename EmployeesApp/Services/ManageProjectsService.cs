using System;
using System.Globalization;
using EmployeesApp.Services.Interfaces;
using EmployeesApp.Services.Models;

namespace EmployeesApp.Services
{
    public class ManageProjectsService : IManageProjectsService
    {
        public ManageProjectsService()
        {
        }
        #region private
        private static string[] dateFormats = new string[]
        {
            "yyyy-MM-dd",
            "yyyy-MM-d",
            "yyyy-M-d",
            "yyyy-M-dd",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-ddTHH:mm:ssZ",
            "yyyy-MM-ddTHH:mm:ss.fffffff",
            "MM/dd/yyyy",
            "dd/MM/yyyy",
            "dddd, MMMM dd, yyyy",
            "ddd, dd MMM yyyy HH:mm:ss GMT",
            // Add more formats here as needed
        };

        private static DateTime ParseDateWithFormats(string input)
        {
            DateTime parsedDateTime;
            input = input.Trim();

            foreach (string format in dateFormats)
            {
                if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
                {
                    // Parsing successful
                    return parsedDateTime;
                }
            }

            // Parsing failed for all formats 
            throw new NotImplementedException();
        }
        #endregion

        public async Task<IEnumerable<ProjectSummary>> GetMaxWorkDaysByPair(IFormFile file)
        {
            List<ProjectDetails> projectDetails = new List<ProjectDetails>();

            try
            {
                using (StreamReader reader = new StreamReader(file.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        string? dataLine = await reader.ReadLineAsync();
                        string[] values = dataLine!.Split(',');

                        // CSV structure is: EmpID, ProjectID, DateFrom, DateTo
                        long empId = long.Parse(values[0]);
                        long projectId = long.Parse(values[1]);

                        DateTime dateFrom = DateTime.Now;
                        values[2] = values[2].Trim();
                        if (!string.IsNullOrEmpty(values[2]) && values[2] != "NULL")
                        {
                            dateFrom = ParseDateWithFormats(values[2]);
                        }

                        values[3] = values[3].Trim();
                        DateTime dateTo = DateTime.Now;
                        if (!string.IsNullOrEmpty(values[3]) && values[3] != "NULL")
                        {
                            dateTo = ParseDateWithFormats(values[3]);
                        }

                        // Create a ProjectDetails object and add it to the list
                        projectDetails.Add(new ProjectDetails { EmployeeId = empId, ProjectId = projectId, DateFrom = dateFrom, DateTo = dateTo });
                    }
                }

                var groupedProjects = projectDetails.GroupBy(c => new { c.ProjectId, c.EmployeeId })
                                                .Select(group => new
                                                {
                                                    group.Key.ProjectId,
                                                    group.Key.EmployeeId,
                                                    TotalDays = group.Sum(c => (c.DateTo - c.DateFrom).TotalDays)
                                                })
                                                .GroupBy(c => c.ProjectId)
                                                .SelectMany(group => group.OrderByDescending(c => c.TotalDays).Take(2))
                                                .GroupBy(c => c.ProjectId)
                                                .Select(group => new ProjectSummary
                                                {
                                                    ProjectId = group.Key,
                                                    FirstEmployeeId = group.OrderByDescending(c => c.TotalDays).Select(c => c.EmployeeId).FirstOrDefault(),
                                                    SecondEmployeeId = group.OrderByDescending(c => c.TotalDays).Skip(1).Select(c => c.EmployeeId).FirstOrDefault(),
                                                    TotalDays = (long)group.Sum(c => c.TotalDays)
                                                })
                                                .ToList();

                return groupedProjects;

            }
            catch (Exception ex)
            {
                throw new Exception("Something goes wrong!");
            }
        }
    }
}

