using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLD.ENTITY.Models
{
    [Table("SINHVIEN")]
    public class SinhVien : Repository.Pattern.Ef6.Entity
    {
        [Key]
        public int Id { get; set; }

        [Column("HOTEN")]
        [StringLength(100)]
        public string HoTen { get; set; }
    }
}
