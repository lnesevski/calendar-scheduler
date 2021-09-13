using CalendarScheduler.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarScheduler.Repository.Interface
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        IEnumerable<Project> GetAllProjectsForUser(string UserId);
        Project Get(Guid? id);
        void Insert(Project entity);
        void Update(Project entity);
        void Delete(Project entity);
       
    }
}
