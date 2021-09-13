using CalendarScheduler.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarScheduler.Service.Interface
{
    public interface IProjectService
    {
        List<Project> GetAllProjectForUser(string UserId);
        Project GetProject(Guid Id);
        void Insert(Project p);
        void Update(Project p);
        void Delete(Project p);
    }

    

}
