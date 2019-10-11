using System;
using System.Collections.Generic;

namespace TechSupportWeb.Services.Interfaces
{
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Добавление записи
        /// </summary>
        void Add(TEntity entity);

        /// <summary>
        /// Выполнение операции с получением данных для одной записи
        /// </summary>
        TResult DoFunc<TResult>(long entityId, Func<TEntity, TResult> func);

        /// <summary>
        /// Выполнение операции без получения данных для одной записи
        /// </summary>
        void DoAction(long entityId, Action<TEntity> action);

        /// <summary>
        /// Выполнение операции с получением данных для всех записей
        /// </summary>
        TResult DoFunc<TResult>(Func<IDictionary<long, TEntity>, TResult> func);

        /// <summary>
        /// Выполнение операции без получения данных для всех записей
        /// </summary>
        void DoAction(Action<IDictionary<long, TEntity>> action);
    }
}