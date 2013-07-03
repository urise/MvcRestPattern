using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses.DbClasses
{
    public class TemporaryCode
    {
        public int TemporaryCodeId { get; set; }
        public int UserId { get; set; }
        [Required, MaxLength(10)]
        public string Code { get; set; }
        public DateTime ExpireDate { get; set; }

        public virtual User User { get; set; }
    }
}
