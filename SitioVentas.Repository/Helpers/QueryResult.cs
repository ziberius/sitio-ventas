using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace SitioVentas.Repository.Helpers
{
    /// <summary>
    /// A result object with the generated sql and dynamic params.
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// The _result
        /// </summary>
        private readonly Tuple<string, IDictionary> _result;

        /// <summary>
        /// Gets the SQL.
        /// </summary>
        /// <value>
        /// The SQL.
        /// </value>
        public string Sql
        {
            get
            {
                return _result.Item1;
            }
        }

        /// <summary>
        /// Gets the param.
        /// </summary>
        /// <value>
        /// The param.
        /// </value>
        public IDictionary Param
        {
            get
            {
                return _result.Item2;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResult" /> class.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The param.</param>
        public QueryResult(string sql, IDictionary param)
        {
            _result = new Tuple<string, IDictionary>(sql, param);
        }
    }
}