using Inlook_Core.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Inlook_Core.Interfaces.Services
{
    public interface IBaseService<T>
        where T : Base
    {
        void Create(T entity);
        T Read(Guid Id);
        void Update(T entity);
        void Delete(Guid Id);
    }
}
