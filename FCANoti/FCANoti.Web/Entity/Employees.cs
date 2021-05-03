using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FCANoti.Web.Entity
{
    public class Employees
    {
        [Key]
        public long ID { get; set; }

        public string FullName { get; set; }

        public string MobileNo { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
