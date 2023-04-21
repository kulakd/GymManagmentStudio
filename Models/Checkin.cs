using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Checkin: BaseEntity
    {
        public int MemberId { get; set; }
        [NotMapped]
        public string? Status { get; set; }
        [NotMapped]
        public DateTime? EndDate { get; set; }
        public virtual Member? Member { get; set; }
    }
}
