using CalendarScheduler.Domain.Models;
using CalendarScheduler.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarScheduler.Repository.Implementation
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Project> entities;
        string errorMessage = string.Empty;

        public ProjectRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Project>();
        }
        public IEnumerable<Project> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<Project> GetAllProjectsForUser(string UserId)
        {
            return entities.AsEnumerable().Where(z => z.UserId == UserId);
        }

        public Project Get(Guid? id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
        public void Insert(Project entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Project entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Project entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
