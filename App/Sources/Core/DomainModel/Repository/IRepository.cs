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
        /// <returns>Сущность из хранилища</returns>
        IStorable GetEntity(Guid id);

        /// <summary>
        /// Получить сущность
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="id">Id искомой сущности</param>
        /// <returns>Сущность из хранилища</returns>
        T GetEntity<T>(Guid id) where T : class, IStorable;


        /// <summary>
        /// Сохранить сущность
        /// </summary>
        /// <param name="entity">Сохраняемая сущность</param>
        /// <returns>Идентификатор сущности</returns>
        Guid? SaveEntity(ref IStorable entity);

        /// <summary>
        /// Сохранить сущность
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="entity">Сохраняемая сущность</param>
        /// <returns>Id сущности</returns>
        Guid? SaveEntity<T>(ref T entity);


        /// <summary>
        /// Получить список сущностей
        /// </summary>
        /// <typeparam name="T">Тип получаемой сущности</typeparam>
        /// <param name="colOwner">Только данные из коллекции объекта</param>
        /// <param name="ownProp">Свойсторво коллекции</param>
        /// <returns>Список сущностей</returns>
        /// <remarks>В данном методе необходимо получать
        /// список сущностей без детализации (Id, Name, внешние связи
        /// , детальная информация
        /// получается методом GetEntity</remarks>
        IList<T> GetList<T>(IObject colOwner=null, string ownProp = null) where T : IStorable;

    }
}
