using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Интерфейс репозитория для сохранения объектов
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Получить / обновить сущность
        /// </summary>
        /// <param name="id">Id запрашиваемой сущности</param>
        /// <param name="entity">Сущность</param>
        /// <returns>Сущность из хранилища</returns>
        IStorable GetEntity(Guid id, IStorable entity);

        /// <summary>
        /// Сохранить сущность
        /// </summary>
        /// <param name="entity">Сохраняемая сущность</param>
        /// <returns>Идентификатор сущности</returns>
        Guid? SaveEntity(ref IStorable entity);

        /// <summary>
        /// Получить список сущностей
        /// </summary>
        /// <typeparam name="T">Тип получаемой сущности</typeparam>
        /// <returns>Список сущностей</returns>
        /// <remarks>В данном методе необходимо получать
        /// список сущностей без детализации, детальная информация
        /// получается методом GetEntity</remarks>
        IList<T> GetList<T>() where T : IStorable;

    }
}
