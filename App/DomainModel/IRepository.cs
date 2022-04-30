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
    }
}
