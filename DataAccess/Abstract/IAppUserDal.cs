using Core.DataAccess.Couchbase.Abstract;
using Core.Entities.Concrete;
using System;

namespace DataAccess.Abstract
{
    public interface IAppUserDal : ICouchbaseRepository<AppUser, Guid>
    {
    }
}
