﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        private int? _instanceId;
        public int? InstanceId { get { return _instanceId; } }
        private readonly MainDbContext _context;

        public FilteredContext(int? instanceId)
        {
            _instanceId = instanceId;
            _context = new MainDbContext();
        }

        public void SetInstanceId(int? instanceId)
        {
            _instanceId = instanceId;
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

        public IQueryable<DataLog> DataLogs
        {
            get { return _context.DataLogs.Where(r => r.InstanceId == InstanceId); }
        }

        #endregion

        #region Other Methods

        public void Add<T>(T record) where T: class, IMapping
        {
            _context.GetDbSet<T>().Add(record);
        }

        public T GetById<T>(int id) where T : class, IMapping
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

        #region Transaction

        public DbTransaction BeginTransaction()
        {
            if (_context.Database.Connection.State != ConnectionState.Open)
                _context.Database.Connection.Open();
            return _context.Database.Connection.BeginTransaction();
        }

        #endregion
    }
}
