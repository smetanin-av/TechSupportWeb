using System;
using System.Collections.Generic;
using TechSupportWeb.Domain;
using TechSupportWeb.Services.Interfaces;

namespace TechSupportWeb.Services.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly object locker = new object();
        private readonly IDictionary<long, TEntity> data;

        public Repository()
        {
            data = new Dictionary<long, TEntity>();
        }

        public void Add(TEntity entity)
        {
            lock (locker)
            {
                data.Add(entity.ID, entity);
            }
        }

        public TResult DoFunc<TResult>(long entityId, Func<TEntity, TResult> func)
        {
            lock (locker)
            {
                var entity = FindEntity(entityId);
                return func(entity);
            }
        }

        public void DoAction(long entityId, Action<TEntity> action)
        {
            lock (locker)
            {
                var entity = FindEntity(entityId);
                action(entity);
            }
        }

        public TResult DoFunc<TResult>(Func<IDictionary<long, TEntity>, TResult> func)
        {
            lock (locker)
            {
                return func(data);
            }
        }

        public void DoAction(Action<IDictionary<long, TEntity>> action)
        {
            lock (locker)
            {
                action(data);
            }
        }

        private TEntity FindEntity(long entityId)
        {
            return data.TryGetValue(entityId, out var entity)
                ? entity
                : throw new Exception($"Запись #{entityId} не найдена в репозитории {typeof(TEntity).Name}.");
        }
    }
}