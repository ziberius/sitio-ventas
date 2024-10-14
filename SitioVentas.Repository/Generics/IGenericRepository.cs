using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SitioVentas.Repository.Generics
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(dynamic Id);
        Task<bool> DeleteLogico(dynamic Id); 
        Task<TEntity> Update(TEntity input);
        Task<TEntity> Insert(TEntity input);
        Task<TEntity> InsertOrUpdate(TEntity input);
        Task<bool> ExecuteCommand(string sql);
        Task<IEnumerable<TEntity>> ExecutedQuery(string sql);
        Task<bool> ExecuteCommand(string sql, object param);
        Task<IEnumerable<TEntity>> ExecutedQuery(string sql, object param);
        Task<bool> Validate(string sql, Object param);
        Task<TEntity> ExecutedQuerySingle(string sql, Object param);
        string GetPrimaryKeyValue(TEntity obj);
        Task<IEnumerable<Object>> ExecutedStoredProcedureObject(string spname, object parameters);
        Task<IEnumerable<TEntity>> GetAllByExpression(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllByExpression(Expression<Func<TEntity, bool>> predicate, int offset, int limit);
        Task<int> GetCountByExpression(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountCommand(string sql);
        Task<JArray> ExecutedQueryJArray(string sql);
        Task<JArray> ExecutedQueryJArray(string sql, object param);
    }
}
