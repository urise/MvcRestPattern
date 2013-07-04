﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.DbClasses;
using System.Linq.Expressions;

namespace DbLayer
{
    public class FilteredContext: IDisposable
    {
        #region Properties and Constructors

        private int _instanceId;
        public int InstanceId { get { return _instanceId; } }
        private readonly MainDbContext _context;

        public FilteredContext(int instanceId)
        {
            _instanceId = instanceId;
            _context = new MainDbContext();
        }

        public void SetInstanceId(int companyId)
        {
            _instanceId = companyId;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion

        #region Tables

        public IQueryable<User> Users
        {
            get { return _context.Users; }
        }

        public IQueryable<Instance> Instances
        {
            get { return _context.Instances; }
        }

        public IQueryable<UserInstance> UserInstances
        {
            get { return _context.UserInstances; }
        }

        public IQueryable<InstanceUsage> InstanceUsages
        {
            get { return _context.InstanceUsages; }
        }

        public IQueryable<TemporaryCode> TemporaryCodes
        {
            get { return _context.TemporaryCodes; }
        }
        #endregion

        #region Other Methods

        public void Add<T>(T record) where T: MappingClass
        {
            _context.GetDbSet<T>().Add(record);
        }

        public T GetById<T>(int id) where T : MappingClass
        {
            var param = Expression.Parameter(typeof(T), "e");
            var predicate =
                Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(param, typeof(T).Name + "Id"),
                                                                  Expression.Constant(id)), param);
            var dbSet = _context.GetDbSet<T>();
            return dbSet.SingleOrDefault(predicate);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}
