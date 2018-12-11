
using QLD.ENTITY.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace QLD.SERVICE.DanhMuc
{
    public interface ISinhVienService : IService<SinhVien>
    {

    }
    public class SinhVienService : Service<SinhVien>, ISinhVienService
    {
        public SinhVienService(IRepositoryAsync<SinhVien> repository) : base(repository)
        {
        }
    }
}
