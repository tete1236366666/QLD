using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using BTS.SP.TOOLS.BuildQuery.Result.Types;
using BTS.SP.TOOLS.BuildQuery.Types;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using System.Linq.Dynamic;

namespace Service.Pattern
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class, IObjectState
    {
        #region Private Fields
        private readonly IRepositoryAsync<TEntity> _repository;
        #endregion Private Fields

        #region Constructor
        protected Service(IRepositoryAsync<TEntity> repository) { _repository = repository; }
        #endregion Constructor

        public virtual TEntity Find(params object[] keyValues) { return _repository.Find(keyValues); }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters) { return _repository.SelectQuery(query, parameters).AsQueryable(); }

        public virtual void Insert(TEntity entity) { _repository.Insert(entity); }

        public virtual void InsertRange(IEnumerable<TEntity> entities) { _repository.InsertRange(entities); }

        public virtual void InsertOrUpdateGraph(TEntity entity) { _repository.InsertOrUpdateGraph(entity); }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities) { _repository.InsertGraphRange(entities); }

        public virtual void Update(TEntity entity) { _repository.Update(entity); }

        public virtual void Delete(object id) { _repository.Delete(id); }

        public virtual void Delete(TEntity entity) { _repository.Delete(entity); }

        public IQueryFluent<TEntity> Query() { return _repository.Query(); }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject) { return _repository.Query(queryObject); }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query) { return _repository.Query(query); }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues) { return await _repository.FindAsync(keyValues); }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues) { return await _repository.FindAsync(cancellationToken, keyValues); }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues) { return await DeleteAsync(CancellationToken.None, keyValues); }

        //IF 04/08/2014 - Before: return await DeleteAsync(cancellationToken, keyValues);
        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues) { return await _repository.DeleteAsync(cancellationToken, keyValues); }

        public IQueryable<TEntity> Queryable() { return _repository.Queryable(); }

        public ResultObj<PagedObj<TEntity>> Filter<TSearch>(FilterObj<TSearch> filtered, IQueryBuilder query = null) where TSearch : IDataSearch
        {
            query = query ?? new QueryBuilder();
            var advanceData = filtered.AdvanceData;
            if (!filtered.IsAdvance)
            {
                advanceData.LoadGeneralParam(filtered.Summary);
            }
            var filters = advanceData.GetFilters();
            if (filters.Count > 0)
            {
                var newQuery = new QueryFilterLinQ
                {
                    Method = filtered.IsAdvance ? FilterMethod.And : FilterMethod.Or,
                    SubFilters = filters,
                };
                if (query.Filter == null)
                {
                    query.Filter = newQuery;
                }
                else
                {
                    query.Filter.MergeFilter(newQuery);
                }
            }
            var quickFilters = advanceData.GetQuickFilters();
            if (quickFilters != null && quickFilters.Any())
            {
                var newQuery = new QueryFilterLinQ
                {
                    Method = FilterMethod.And,
                    SubFilters = quickFilters,
                };
                if (query.Filter == null)
                {
                    query.Filter = newQuery;
                }
                else
                {
                    query.Filter.MergeFilter(newQuery);
                }
            }
            // load order 
            if (!string.IsNullOrEmpty(filtered.OrderBy))
            {
                query.OrderBy(new QueryOrder
                {
                    Field = filtered.OrderBy,
                    MethodName = filtered.OrderType
                });
            }
            // at lease one order for paging
            if (query.Orders.Count == 0)
            {
                query.OrderBy(new QueryOrder { Field = advanceData.DefaultOrder });
            }
            // query
            var result = new ResultObj<PagedObj<TEntity>>();
            try
            {
                var data = QueryPaged(query);
                result.Value = data;
                result.State = ResultState.Success;
            }
            catch (Exception ex)
            {
                result.SetException = ex;
            }
            return result;
        }

        public async Task<ResultObj<PagedObj<TEntity>>> FilterAsync<TSearch>(FilterObj<TSearch> filtered, IQueryBuilder query = null) where TSearch : IDataSearch
        {
            query = query ?? new QueryBuilder();
            var advanceData = filtered.AdvanceData;
            if (!filtered.IsAdvance)
            {
                advanceData.LoadGeneralParam(filtered.Summary);
            }
            var filters = advanceData.GetFilters();
            if (filters.Count > 0)
            {
                var newQuery = new QueryFilterLinQ
                {
                    Method = filtered.IsAdvance ? FilterMethod.And : FilterMethod.Or,
                    SubFilters = filters,
                };
                if (query.Filter == null)
                {
                    query.Filter = newQuery;
                }
                else
                {
                    query.Filter.MergeFilter(newQuery);
                }
            }
            var quickFilters = advanceData.GetQuickFilters();
            if (quickFilters != null && quickFilters.Any())
            {
                var newQuery = new QueryFilterLinQ
                {
                    Method = FilterMethod.And,
                    SubFilters = quickFilters,
                };
                if (query.Filter == null)
                {
                    query.Filter = newQuery;
                }
                else
                {
                    query.Filter.MergeFilter(newQuery);
                }
            }
            // load order 
            if (!string.IsNullOrEmpty(filtered.OrderBy))
            {
                query.OrderBy(new QueryOrder
                {
                    Field = filtered.OrderBy,
                    MethodName = filtered.OrderType
                });
            }
            // at lease one order for paging
            if (query.Orders.Count == 0)
            {
                query.OrderBy(new QueryOrder { Field = advanceData.DefaultOrder });
            }
            // query
            var result = new ResultObj<PagedObj<TEntity>>();
            try
            {
                var data = await QueryPagedAsync(query);
                result.Value = data;
                result.State = ResultState.Success;
            }
            catch (Exception ex)
            {
                result.SetException = ex;
            }
            return result;
        }

        private PagedObj<TEntity> QueryPaged(IQueryBuilder query)
        {
            var result = new PagedObj<TEntity>();
            IQueryable<TEntity> data = _repository.Queryable();
            var filterString = query.BuildFilter();
            var orderString = query.BuildOrder();
            if (!string.IsNullOrEmpty(filterString))
                data = data.Where(filterString, query.Filter.QueryStringParams.Params.ToArray());
            if (!string.IsNullOrEmpty(orderString))
                data = data.OrderBy(orderString);
            result.totalItems = data.Count();
            if (query.Skip > 0)
            {
                data = data.Skip(query.Skip);
            }
            if (query.Take > 0)
            {
                data = data.Take(query.Take);
            }
            result.Data.AddRange(data.ToList());
            return result;
        }

        private async Task<PagedObj<TEntity>> QueryPagedAsync(IQueryBuilder query)
        {
            var result = new PagedObj<TEntity>();
            IQueryable<TEntity> data = _repository.Queryable();
            var filterString = query.BuildFilter();
            var orderString = query.BuildOrder();
            if (!string.IsNullOrEmpty(filterString))
                data = data.Where(filterString, query.Filter.QueryStringParams.Params.ToArray());
            if (!string.IsNullOrEmpty(orderString))
                data = data.OrderBy(orderString);
            result.totalItems = await data.CountAsync();
            if (query.Skip > 0)
            {
                data = data.Skip(query.Skip);
            }
            if (query.Take > 0)
            {
                data = data.Take(query.Take);
            }
            result.Data.AddRange(await data.ToListAsync());
            return result;
        }
    }
}
