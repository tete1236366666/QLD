using QLD.ENTITY.Models;
using System.Data.Entity;

namespace QLD.ENTITY
{
    public class QLDContext: Repository.Pattern.Ef6.DataContext
    {
        public QLDContext() : base("QLDConnection")
        {
        }

        public DbSet<SinhVien> SinhViens { get; set; }
    }
}
