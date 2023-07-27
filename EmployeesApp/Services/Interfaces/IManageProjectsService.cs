using System;
using EmployeesApp.Services.Models;

namespace EmployeesApp.Services.Interfaces
{
    public interface IManageProjectsService
    {
        Task<IEnumerable<ProjectSummary>> GetMaxWorkDaysByPair(IFormFile file);
    }
}

