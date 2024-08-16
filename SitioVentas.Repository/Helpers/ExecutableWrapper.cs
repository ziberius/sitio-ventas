using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;


namespace SitioVentas.Repository.Helpers
{
    public static class ExecutableWrapper
    {
        /// <summary>
        /// Encapsula las llamadas a la base de datos
        /// </summary>
        /// <typeparam name="TResult">Resultado de la llamada</typeparam>
        /// <param name="conn">SqlConnection</param>
        /// <param name="func">Función a ejecutar</param>
        /// <param name="MetFullName">Nombre del método desde donde se esta llamando</param>
        /// <returns>Resultado de la llamada a la base de datos</returns>
        public static async Task<TResult> ExecuteWrapper<TResult>(IDbConnection conn, Func<IDbConnection, Task<TResult>> func, string MetFullName)
        {
            TResult result;
            try
            {
                conn.Open();
                result = await func(conn);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(String.Format("{0}.ExecuteWrapper() TimeOut: " + ex.InnerException, MetFullName), ex);
            }
            catch (MySqlException ex)
            {
                throw new EvaluateException(String.Format("{0}.ExecuteWrapper() Problemas con la consulta: " + ex.InnerException, MetFullName));
            }
            catch (Exception ex)
            {
                throw new DataException(String.Format("{0}.ExecuteWrapper() Problemas con la consulta: " + ex.InnerException, MetFullName), ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Encapsula las llamadas a la base de datos, generando una conexión asincrona a la misma
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="conn"></param>
        /// <param name="func"></param>
        /// <param name="MetFullName"></param>
        /// <returns></returns>
        public static async Task<TResult> ExecuteWrapperAsynConn<TResult>(string conn, Func<IDbConnection, Task<TResult>> func, string MetFullName)
        {
            TResult result;
            try
            {
                using (var connection = new MySqlConnection(conn))
                {
                    await connection.OpenAsync();
                    result = await func(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(String.Format("{0}.ExecuteWrapper() TimeOut: {1}", MetFullName, ex.Message), ex);
            }
            catch (MySqlException ex)
            {
                throw new EvaluateException(String.Format("{0}.ExecuteWrapper() Problemas con la consulta: {1}", MetFullName, ex.Message), ex);
            }
            catch (Exception ex)
            {
                throw new DataException(String.Format("{0}.ExecuteWrapper() Problemas con la consulta: {1}", MetFullName, ex.Message), ex);
            }

            return result;
        }

    }
}
