using Core.DataAccess.Couchbase.Abstract;
using DataAccess.Abstract;
using Entities.Domain;
using System;

namespace DataAccess.Concrete.Couchbase
{
    public class TodoDal : CouchbaseRepositoryBase<Todo, Guid>, ITodoDal
    {
        public TodoDal() : base()
        {

        }
    }
}
