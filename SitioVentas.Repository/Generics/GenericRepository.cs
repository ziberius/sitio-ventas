using System;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SitioVentas.Repository.Helpers;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using SitioVentas.Entities.Entities;
using SitioVentas.Constants.Constants;

namespace SitioVentas.Repository.Generics
{
    public class GenericRepository<TEntity> : AbstractDapperCommon<TEntity>, IGenericRepository<TEntity> where TEntity : class, new()
    {
        #region VARIABLES Y CONSTANTES
        public IDbConnection _db;

        protected string ConString;
        public string StateName { get; set; } = "";
        #endregion


        #region Inicializacion
        public GenericRepository(IDbConnection db)
        {
            _db = db;
            SetConnString(_db.ConnectionString);
            Inicializar();
        }

        public void SetConnString(string conString)
        {
            this.ConString = conString;
        }
        #endregion


        public virtual async Task<bool> DeleteLogico(dynamic Id, string UserLastUpdate = "Sin Asignar")
        {
            TEntity m = new TEntity();
            m = this.SetPrimaryKey(Id, m);
            string sql =
               STRING_TYPE == (typeof(TEntity).GetProperty(this.PrimaryKeyName).GetValue(m, null)).GetType().FullName ?
                   string.Format("UPDATE {0} SET FechaActualizacion = getdate(), Activo = 0, UsuarioActualizador = '{3}' WHERE {1} = '{2}'", this.TableName, this.PrimaryKeyName, m.GetType().GetProperty(this.PrimaryKeyName).GetValue(m, null).ToString(), UserLastUpdate) :
                   string.Format("UPDATE {0} SET FechaActualizacion = getdate(), Activo = 0, UsuarioActualizador = '{3}' WHERE {1} = {2}", this.TableName, this.PrimaryKeyName, m.GetType().GetProperty(this.PrimaryKeyName).GetValue(m, null).ToString(), UserLastUpdate);

            await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<dynamic>>(ConString, async (_c) =>
            {
                return await _c.QueryAsync(sql).ConfigureAwait(false);
            }, GetType().FullName);

            return true;

        }

        public virtual async Task<bool> ExecuteCommand(string sql)
        {
            await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<dynamic>>(ConString, async (_c) =>
            {
                return await _c.QueryAsync(sql).ConfigureAwait(false);
            }, GetType().FullName);

            return true;

        }

        public virtual async Task<bool> ExecuteCommand(string sql, object param)
        {
            await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<dynamic>>(ConString, async (_c) =>
            {
                return await _c.QueryAsync(sql, param).ConfigureAwait(false);
            }, GetType().FullName);

            return true;

        }

        public virtual async Task<IEnumerable<TEntity>> ExecutedQuery(string sql)
        {
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<TEntity>>(ConString, async (_c) =>
            {
                return await _c.QueryAsync<TEntity>(sql).ConfigureAwait(false);
            }, GetType().FullName);

            if (!resultDapper.Any())
                return Enumerable.Empty<TEntity>();
            else
                return resultDapper;

        }

        public virtual async Task<IEnumerable<TEntity>> ExecutedQuery(string sql, object param)
        {
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<TEntity>>(ConString, async (_c) =>
            {
                return await _c.QueryAsync<TEntity>(sql, param).ConfigureAwait(false);
            }, GetType().FullName);

            if (!resultDapper.Any())
                return Enumerable.Empty<TEntity>();
            else
                return resultDapper;

        }

        public virtual async Task<TEntity> ExecutedQuerySingle(string sql, object param)
        {
            IEnumerable<TEntity> result = await ExecutedQuery(sql, param).ConfigureAwait(false);

            if (result == null || !result.Any())
                return new TEntity();

            return result.FirstOrDefault();

        }

        public virtual async Task<IEnumerable<object>> ExecutedStoredProcedureObject(string spname, object parameters)
        {
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<Object>>(ConString, async (_c) =>
            {
                return await _c.QueryAsync<Object>(spname
                                                    , parameters
                                                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }, GetType().FullName).ConfigureAwait(false);

            return resultDapper;

        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<TEntity>>(ConString, async (_c) =>
            {
                IEnumerable<TEntity> result = await _c.GetAllAsync<TEntity>().ConfigureAwait(false);
                return result;
            }, GetType().FullName);

            return resultDapper;

        }

        public virtual async Task<TEntity> GetById(dynamic Id)
        {
            TEntity id = new TEntity();
            id = this.SetPrimaryKey(Id, id);

            var idvalue = id.GetType().GetProperty(this.PrimaryKeyName).GetValue(id, null).ToString();
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<TEntity>(ConString, async (_c) =>
            {
                return await _c.GetAsync<TEntity>(idvalue).ConfigureAwait(false);
            }, GetType().FullName);

            if (resultDapper == null)
                return null;
            else
                return resultDapper;

        }
        public virtual async Task<TEntity> GetById(TEntity id)
        {
            return await GetById(id.GetType().GetProperty(this.PrimaryKeyName).GetValue(id, null).ToString()).ConfigureAwait(false);
        }

        public virtual string GetPrimaryKeyValue(TEntity obj)
        {
            return GetColumnValueByColumnName(obj, this.PrimaryKeyName);
        }

        public virtual async Task<TEntity> Insert(TEntity input)
        {
            int identity = await ExecutableWrapper.ExecuteWrapperAsynConn<int>(ConString, async (_c) =>
            {
                return await _c.InsertAsync(input).ConfigureAwait(false);
            }, GetType().FullName);

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

        public virtual async Task<TEntity> InsertOrUpdate(TEntity input)
        {
            if (await this.GetById(input).ConfigureAwait(false) == null)
                //se debe crear
                return await this.Insert(input).ConfigureAwait(false);
            else
                //se debe actualizar
                return await this.Update(input).ConfigureAwait(false);

        }

        public virtual async Task<TEntity> Update(TEntity input)
        {
            await ExecutableWrapper.ExecuteWrapperAsynConn<bool>(ConString, async (_c) =>
            {
                return await _db.UpdateAsync(input).ConfigureAwait(false);
            }, GetType().FullName);

            return await GetById(input).ConfigureAwait(false);

        }

        public async Task<bool> Validate(string sql, object param)
        {
            IEnumerable<TEntity> result = await ExecutedQuery(sql, param).ConfigureAwait(false);

            if (result == null || !result.Any())
                return false;

            return true;

        }

        public async Task<IEnumerable<TEntity>> GetAllByExpression(Expression<Func<TEntity, bool>> predicate)
        {
            QueryResult result = DynamicQuery.GetDynamicQuery(GetTableName(), predicate);
           var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<TEntity>>(ConString, async (_c) =>
            {
            return await _c.QueryAsync<TEntity>(result.Sql, result.Param).ConfigureAwait(false);
            }, GetType().FullName);

            if (!resultDapper.Any())
                return Enumerable.Empty<TEntity>();
            else
                return resultDapper;

        }

        public async Task<IEnumerable<TEntity>> GetAllByExpression(Expression<Func<TEntity, bool>> predicate, int offset, int limit)
        {
            QueryResult result = DynamicQuery.GetDynamicLazyQuery(GetTableName(), predicate, offset, limit);
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<TEntity>>(ConString, async (_c) =>
            {
                return await _c.QueryAsync<TEntity>(result.Sql, result.Param).ConfigureAwait(false);
            }, GetType().FullName);

            if (!resultDapper.Any())
                return Enumerable.Empty<TEntity>();
            else
                return resultDapper;
        }

        public virtual async Task<int> CountCommand(string sql)
        {
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<int>(ConString, async (_c) =>
            {
                return await _c.ExecuteScalarAsync<int>(sql).ConfigureAwait(false);
            }, GetType().FullName);
            return resultDapper;
        }

        public async Task<int> GetCountByExpression(Expression<Func<TEntity, bool>> predicate)
        {
            QueryResult result = DynamicQuery.GetDynamicQuery(GetTableName(), predicate, true);
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<int>(ConString, async (_c) =>
            {
                return await _c.ExecuteScalarAsync<int>(result.Sql, result.Param).ConfigureAwait(false);
            }, GetType().FullName);
            return resultDapper;
        }

        public async Task<JArray> ExecutedQueryJArray(string sql)
        {
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<Object>>(ConString, async (_c) =>
            {
                return await _c.QueryAsync<Object>(sql).ConfigureAwait(false);
            }, GetType().FullName);

            if (!resultDapper.Any())
                return new JArray();
            else
                return JArray.FromObject(resultDapper);

        }

        public async Task<JArray> ExecutedQueryJArray(string sql, object param)
        {
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<Object>>(ConString, async (_c) =>
            {
                return await _c.QueryAsync<Object>(sql, param).ConfigureAwait(false);
            }, GetType().FullName);

            if (!resultDapper.Any())
                return new JArray();
            else
                return JArray.FromObject(resultDapper);
        }
        public virtual async Task<int> GetLastOrder()
        {
            string sqlCount = string.Format("SELECT COUNT(Item) FROM {0} WHERE Item <> -1", this.TableName);
            int count = await CountCommand(sqlCount);
            if(count > 0)
            {
                string sql = string.Format("SELECT TOP(1) Item FROM {0} WHERE Item <> -1 ORDER BY Item DESC", this.TableName);
                return await CountCommand(sql);
            } 
            else
            {
                return 0;
            }           
        }

        public virtual async Task<bool> UpdateOrderAfterDelete(int idDeleted)
        {
            TEntity entity = await GetById(idDeleted);
            int item = int.Parse(GetColumnValueByColumnName(entity, "Item"));
            int languageId = int.Parse(GetColumnValueByColumnName(entity, "LanguageId"));
            string sqlUpdate = string.Format("UPDATE {0} SET Item = Item - 1 WHERE LanguageId = {1} AND Item > {2}", this.TableName, languageId, item);
            bool resultUpdate = await ExecuteCommand(sqlUpdate);
            if(resultUpdate)
            {
                string sql = string.Format("UPDATE {0} SET Item = - 1 WHERE Id = {1}", this.TableName, idDeleted);
                return await ExecuteCommand(sql);
            }
            else
            {
                return false;
            }                     
        }
    }
}
