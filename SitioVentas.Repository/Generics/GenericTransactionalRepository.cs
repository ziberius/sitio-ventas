using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dapper;
using System.Threading.Tasks;
using System.Data;
using Dapper.Contrib.Extensions;
using System.Linq;
using SitioVentas.Repository.Helpers;
using Newtonsoft.Json.Linq;

namespace SitioVentas.Repository.Generics
{
    public class GenericTransactionalRepository<TEntity> : AbstractDapperCommon<TEntity>, IGenericTransactionalRepository<TEntity> where TEntity : class, new()
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection _c { get { return Transaction.Connection; } }

        public GenericTransactionalRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
            Inicializar();
        }

        public virtual async Task<TEntity> GetById(TEntity id)
        {
            return await GetById(id.GetType().GetProperty(this.PrimaryKeyName).GetValue(id, null).ToString()).ConfigureAwait(false);
        }

        public async Task<TEntity> GetById(dynamic Id)
        {
            TEntity id = new TEntity();
            id = this.SetPrimaryKey(Id, id);

            var idvalue = id.GetType().GetProperty(this.PrimaryKeyName).GetValue(id, null).ToString();
            var resultDapper =  await _c.GetAsync<TEntity>(idvalue, transaction: Transaction).ConfigureAwait(false);

            if (resultDapper == null)
                return null;
            else
                return resultDapper;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _c.GetAllAsync<TEntity>(transaction: Transaction).ConfigureAwait(false);
        }

        public async Task<bool> DeleteLogico(dynamic Id)
        {
            TEntity m = new TEntity();
            m = this.SetPrimaryKey(Id, m);

            string sql =
               STRING_TYPE == (typeof(TEntity).GetProperty(this.PrimaryKeyName).GetValue(m, null)).GetType().FullName ?
                   string.Format("UPDATE {0} SET AuditLastUpdateDate = CURRENT_TIMESTAMP, AuditNotDeleted = 0 WHERE {1} = '{2}'", this.TableName, this.PrimaryKeyName, m.GetType().GetProperty(this.PrimaryKeyName).GetValue(m, null).ToString()) :
                   string.Format("UPDATE {0} SET AuditLastUpdateDate = CURRENT_TIMESTAMP, AuditNotDeleted = 0 WHERE {1} = {2}", this.TableName, this.PrimaryKeyName, m.GetType().GetProperty(this.PrimaryKeyName).GetValue(m, null).ToString());

            await _c.QueryAsync(sql, transaction: Transaction).ConfigureAwait(false);

            return true;
        }

        public async Task<TEntity> Update(TEntity input)
        {
            await _c.UpdateAsync(input, transaction: Transaction).ConfigureAwait(false);
            return await GetById(input).ConfigureAwait(false);
        }

        public async Task<TEntity> Insert(TEntity input)
        {
            int identity = await _c.InsertAsync(input, transaction: Transaction).ConfigureAwait(false);

            if (identity == 0)
                return await GetById(input).ConfigureAwait(false);
            else
            {
                TEntity m = new TEntity();
                typeof(TEntity).GetProperty(this.PrimaryKeyName)
                    .SetValue(m,
                                    identity
                              );
                return await GetById(m).ConfigureAwait(false);
            }
        }

        public async Task<TEntity> InsertOrUpdate(TEntity input)
        {
            if (await this.GetById(input).ConfigureAwait(false) == null)
                //se debe crear
                return await this.Insert(input).ConfigureAwait(false);
            else
                //se debe actualizar
                return await this.Update(input).ConfigureAwait(false);
        }

        public async Task<bool> ExecuteCommand(string sql)
        {
            await _c.QueryAsync(sql, transaction: Transaction).ConfigureAwait(false);
            return true;
        }

        public async Task<IEnumerable<TEntity>> ExecutedQuery(string sql)
        {
            var resultDapper = await _c.QueryAsync<TEntity>(sql, transaction: Transaction).ConfigureAwait(false);
            if (!resultDapper.Any())
                return Enumerable.Empty<TEntity>();
            else
                return resultDapper;

        }

        public async Task<bool> ExecuteCommand(string sql, object param)
        {
            await _c.QueryAsync(sql, param, transaction: Transaction).ConfigureAwait(false);
            return true;
        }

        public async Task<IEnumerable<TEntity>> ExecutedQuery(string sql, object param)
        {
            var resultDapper = await _c.QueryAsync<TEntity>(sql, param, transaction: Transaction).ConfigureAwait(false);
            if (!resultDapper.Any())
                return Enumerable.Empty<TEntity>();
            else
                return resultDapper;
        }

        public async Task<bool> Validate(string sql, object param)
        {
            IEnumerable<TEntity> result = await ExecutedQuery(sql, param).ConfigureAwait(false);

            if (result == null || !result.Any())
                return false;

            return true;
        }

        public async Task<TEntity> ExecutedQuerySingle(string sql, object param)
        {
            IEnumerable<TEntity> result = await ExecutedQuery(sql, param).ConfigureAwait(false);

            if (result == null || !result.Any())
                return new TEntity();

            return result.FirstOrDefault();
        }

        public string GetPrimaryKeyValue(TEntity obj)
        {
            return GetColumnValueByColumnName(obj, this.PrimaryKeyName);
        }

        public async Task<IEnumerable<object>> ExecutedStoredProcedureObject(string spname, object parameters)
        {
            return await _c.QueryAsync<Object>(spname
                                                    , parameters
                                                    , commandType: CommandType.StoredProcedure
                                                    , transaction: Transaction)
                                                    .ConfigureAwait(false);


        }

        public async Task<IEnumerable<TEntity>> GetAllByExpression(Expression<Func<TEntity, bool>> predicate)
        {
            QueryResult result = DynamicQuery.GetDynamicQuery(GetTableName(), predicate);
            var resultDapper = await _c.QueryAsync<TEntity>(result.Sql, result.Param, transaction: Transaction).ConfigureAwait(false);

            if (!resultDapper.Any())
                return Enumerable.Empty<TEntity>();
            else
                return resultDapper;
        }

        public async Task<IEnumerable<TEntity>> GetAllByExpression(Expression<Func<TEntity, bool>> predicate, int offset, int limit)
        {
            QueryResult result = DynamicQuery.GetDynamicLazyQuery(GetTableName(), predicate, offset, limit);
            var resultDapper =await  _c.QueryAsync<TEntity>(result.Sql, result.Param, transaction: Transaction).ConfigureAwait(false);

            if (!resultDapper.Any())
                return Enumerable.Empty<TEntity>();
            else
                return resultDapper;
        }

        public async Task<int> GetCountByExpression(Expression<Func<TEntity, bool>> predicate)
        {
            QueryResult result = DynamicQuery.GetDynamicQuery(GetTableName(), predicate, true);
            return await _c.ExecuteScalarAsync<int>(result.Sql, result.Param,transaction: Transaction).ConfigureAwait(false);
        }

        public async Task<int> CountCommand(string sql)
        {
            var resultDapper = await _c.ExecuteScalarAsync<int>(sql,transaction: Transaction).ConfigureAwait(false);
            return resultDapper;
        }

        public async Task<JArray> ExecutedQueryJArray(string sql)
        {
            var resultDapper =  await _c.QueryAsync<Object>(sql, transaction: Transaction).ConfigureAwait(false);

            if (!resultDapper.Any())
                return new JArray();
            else
                return JArray.FromObject(resultDapper);
        }

        public async Task<JArray> ExecutedQueryJArray(string sql, object param)
        {
            var resultDapper = await _c.QueryAsync<Object>(sql, param, transaction: Transaction).ConfigureAwait(false);

            if (!resultDapper.Any())
                return new JArray();
            else
                return JArray.FromObject(resultDapper);
        }
    }
}
