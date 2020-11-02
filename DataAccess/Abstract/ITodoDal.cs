using Core.DataAccess.Couchbase.Abstract;
using Entities.Domain;
using System;

namespace DataAccess.Abstract
{
    public interface ITodoDal : ICouchbaseRepository<Todo, Guid>
    {
    }
}
