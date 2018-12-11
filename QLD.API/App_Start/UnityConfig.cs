using QLD.ENTITY;
using QLD.ENTITY.Models;
using QLD.SERVICE.DanhMuc;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace QLD.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IDataContextAsync, QLDContext>(new HierarchicalLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new HierarchicalLifetimeManager())

                .RegisterType<IRepositoryAsync<SinhVien>, Repository<SinhVien>>()
                .RegisterType<ISinhVienService, SinhVienService>()
                ;

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}