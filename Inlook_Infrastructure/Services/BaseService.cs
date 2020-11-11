using System;
using System.Collections.Generic;
using System.Text;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Inlook_Infrastructure.Services
{
    public class BaseService<T> : IBaseService<T>
        where T : Base
    {
        private readonly Inlook_Context context;
        private readonly DbSet<T> dbSet;

        public BaseService(Inlook_Context context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();

        }
        public void Create(T entity)
        {
            entity.LastModifiedDate = entity.CreatedDate = DateTime.Now;
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = this.dbSet.Find(id);
            this.dbSet.Remove(entity);
            this.context.SaveChanges();
        }

        public T Read(Guid Id)
        {
            var entity = dbSet.Find(Id);
            return entity;
        }

        public void Update(T entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            this.dbSet.Update(entity);
            this.context.SaveChanges();
        }
    }
}
