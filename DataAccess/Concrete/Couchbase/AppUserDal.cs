using Core.DataAccess.Couchbase.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;

namespace DataAccess.Concrete.EntityFramework
{
    public class AppUserDal : CouchbaseRepositoryBase<AppUser, Guid>, IAppUserDal
    {
        public AppUserDal() : base()
        {

        }
    }
}
