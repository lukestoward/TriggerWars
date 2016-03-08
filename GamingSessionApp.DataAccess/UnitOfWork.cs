﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamingSessionApp.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        //Single Instace of UoW. This is so we can have a single instance
        //of the ApplicationDbContext
        private static UnitOfWork _instance;
        public static UnitOfWork Instance
        {
            get { return _instance ?? (_instance = new UnitOfWork()); }
            set { _instance = value; }
        }

        private readonly ApplicationDbContext _context;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        private UnitOfWork()
        {
            _context = new ApplicationDbContext();
        }

        public GenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (GenericRepository<T>)_repositories[type];
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<bool> SaveAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
