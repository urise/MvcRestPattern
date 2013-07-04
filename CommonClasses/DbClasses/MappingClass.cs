using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses.DbClasses
{
    public class MappingClass
    {
        private PropertyInfo _primaryKeyProperty;
        public PropertyInfo PrimaryKeyProperty
        {
            get
            {
                if (_primaryKeyProperty == null)
                {
                    string pkName = GetType().Name + "Id";
                    _primaryKeyProperty = GetType().GetProperty(pkName);
                }
                return _primaryKeyProperty;
            }
        }

        // we suppose that ALL primary keys in our tables are integer
        public int PrimaryKeyValue
        {
            get { return (int)PrimaryKeyProperty.GetValue(this); }
        }
    }
}
