using CalendarScheduler.Domain.Models;
using CalendarScheduler.Repository.Implementation;
using CalendarScheduler.Repository.Interface;
using CalendarScheduler.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarScheduler.Service.Implementation
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Delete(Project p)
        {
            this._projectRepository.Delete(p);
        }

        public List<Project> GetAllProjectForUser(string UserId)
        {
            return this._projectRepository.GetAllProjectsForUser(UserId).ToList();
        }

        public Project GetProject(Guid Id)
        {
            return this._projectRepository.Get(Id);
        }

        public void Insert(Project p)
        {
            this._projectRepository.Insert(p);
        }

        public void Update(Project p)
        {
            this._projectRepository.Update(p);
        }
    }
}
