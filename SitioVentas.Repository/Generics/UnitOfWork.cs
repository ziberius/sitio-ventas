using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SitioVentas.Repository.Generics
{
    public class UnitOfWork : IUnitOfWork
    {
        protected List<dynamic> _transactionalRepositories = null;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if(_transactionalRepositories == null) throw new ArgumentException();


                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                //ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _transactionalRepositories = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        public void SetRepository(Type tipo)
        {
            RevisarInstanciaRepos();

            foreach (var go in _transactionalRepositories)
            {
                if (go.GetType() == tipo) return;
            }

            var type = typeof(IGenericTransactionalRepository<>).MakeGenericType(tipo);
            dynamic a = Activator.CreateInstance(tipo, _transaction);
            _transactionalRepositories.Add(a);
        }

        public T GetRepository<T>()
        {
            RevisarInstanciaRepos();

            foreach (var go in _transactionalRepositories)
            {
                if (go.GetType() == typeof(T)) return (T)go;
            }
            throw new ArgumentException();
        }

        private void RevisarInstanciaRepos()
        {
            if (_transactionalRepositories == null)
            {
                _transactionalRepositories = new List<dynamic>();
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
