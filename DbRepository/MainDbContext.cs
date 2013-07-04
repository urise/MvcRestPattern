﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.DbClasses;

namespace DbLayer
{
    public class MainDbContext: DbContext
    {
        public MainDbContext(): base("Name=MainDbContext") {}

        public DbSet<User> Users { get; set; }
        public DbSet<Instance> Instances { get; set; }
        public DbSet<UserInstance> UserInstances { get; set; }
        public DbSet<InstanceUsage> InstanceUsages { get; set; }
        public DbSet<TemporaryCode> TemporaryCodes { get; set; }

        #region Auxilliary Properties and Methods

        private Dictionary<Type, object> _dict;
        protected Dictionary<Type, object> Dict
        {
            get
            {
                if (_dict == null) InitializeDict();
                return _dict;
            }
        }

        private void InitializeDict()
        {
            _dict = new Dictionary<Type, object>();
            var propertyInfos = GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType.FullName.StartsWith("System.Data.Entity.DbSet"))
                {
                    _dict.Add(propertyInfo.PropertyType.GenericTypeArguments[0], propertyInfo.GetValue(this));
                }
            }
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            object result;
            if (!Dict.TryGetValue(typeof(T), out result))
                throw new Exception("There is no DbSet for class " + typeof(T).Name);
            return (DbSet<T>) result;
        }

        #endregion
    }
}
