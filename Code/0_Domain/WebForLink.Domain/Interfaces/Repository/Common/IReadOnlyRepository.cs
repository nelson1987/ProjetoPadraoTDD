﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebForLink.Domain.Interfaces.Repository.Common
{
    public interface IReadOnlyRepository<TEntity>
        where TEntity : class
    {
        TEntity Get(int id);
        TEntity GetAllReferences(int id);
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}