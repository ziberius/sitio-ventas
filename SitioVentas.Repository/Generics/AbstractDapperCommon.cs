using System;
using Dapper.Contrib.Extensions;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace SitioVentas.Repository.Generics
{
    public abstract class AbstractDapperCommon<TEntity>
    {
        protected string TableName;
        protected string PrimaryKeyName;
        protected string PrimaryKeyType;
        protected const string STRING_TYPE = "String";

        public void Inicializar()
        {
            this.SetPrimaryKeyName();
            this.SetPrimaryKeyType();
            this.SetTableName();
        }

        #region FUNCIONES DE APOYO
        /// <summary>
        /// Setea el valor del nombre de la tabla
        /// </summary>
        /// <returns>string</returns>
        protected string GetTableName()
        {
            return ((TableAttribute)typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), true)[0]).Name;
        }

        protected void SetTableName()
        {
            this.TableName = GetTableName();
        }

        /// <summary>
        /// Sete el nombre de la columna de clave primaria
        /// </summary>
        /// <returns>string</returns>
        protected string GetPrimaryKeyName()
        {

            foreach (PropertyInfo pi in typeof(TEntity).GetProperties())
            {
                ExplicitKeyAttribute attribute =
               (ExplicitKeyAttribute)
               typeof(TEntity)
                  .GetProperty(pi.Name)
                  .GetCustomAttributes(typeof(ExplicitKeyAttribute), false).FirstOrDefault();

                if (attribute != null)
                    return pi.Name;

            }

            foreach (PropertyInfo pi in typeof(TEntity).GetProperties())
            {
                KeyAttribute attribute =
               (KeyAttribute)
               typeof(TEntity)
                  .GetProperty(pi.Name)
                  .GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();

                if (attribute != null)
                    return pi.Name;
            }

            throw new AmbiguousMatchException("No se ha establecido nombre de atributo identificador");
        }

        /// <summary>
        /// En base a la estructura del objeto, identifica si existe o no dicho atributo
        /// </summary>
        /// <param name="AttributeName">Nombre del atributo</param>
        /// <returns>true: Existe; false: No existe</returns>
        protected bool ExistAttributtName(string AttributeName)
        {
            foreach (PropertyInfo pi in typeof(TEntity).GetProperties())
                if (pi.Name.Equals(AttributeName))
                    return true;

            return false;
        }

        /// <summary>
        /// Obtiene el tipo de dato de la clave primaria, string o alguno numerico
        /// </summary>
        /// <returns>string con el nombre correspondiente</returns>
        protected string GetPrimaryKeyType()
        {
            foreach (PropertyInfo pi in typeof(TEntity).GetProperties())
            {
                ExplicitKeyAttribute attribute =
               (ExplicitKeyAttribute)
               typeof(TEntity)
                  .GetProperty(pi.Name)
                  .GetCustomAttributes(typeof(ExplicitKeyAttribute), false).FirstOrDefault();

                if (attribute != null)
                    return pi.PropertyType.Name;

            }

            foreach (PropertyInfo pi in typeof(TEntity).GetProperties())
            {
                KeyAttribute attribute =
               (KeyAttribute)
               typeof(TEntity)
                  .GetProperty(pi.Name)
                  .GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();

                if (attribute != null)
                    return pi.PropertyType.Name;
            }

            throw new AmbiguousMatchException("No se ha establecido nombre de atributo identificador");
        }

        /// <summary>
        /// Se establece el nombre del atributo asociado a la clave primaria
        /// </summary>
        protected void SetPrimaryKeyName()
        {
            this.PrimaryKeyName = GetPrimaryKeyName();
        }

        /// <summary>
        /// Se establece el tipo de dato asociado a la clave primaria
        /// </summary>
        protected void SetPrimaryKeyType()
        {
            this.PrimaryKeyType = GetPrimaryKeyType();
        }

        /// <summary>
        /// Se setea el valor de la clave primaria
        /// </summary>
        /// <param name="input">valor</param>
        /// <param name="obj">entidad a ser seteada</param>
        /// <returns></returns>
        protected TEntity SetPrimaryKey(dynamic input, TEntity obj)
        {
            string _input = Convert.ToString(input);
            if (STRING_TYPE == this.PrimaryKeyType)
                typeof(TEntity).GetProperty(this.PrimaryKeyName).SetValue(obj, _input);
            else
                typeof(TEntity).GetProperty(this.PrimaryKeyName).SetValue(obj, Int32.Parse(_input));

            return obj;
        }

        protected string GetColumnValueByColumnName(TEntity obj, string ColumnName)
        {
            return obj.GetType().GetProperty(ColumnName).GetValue(obj, null).ToString();
        }

       

        #endregion

    }
}
