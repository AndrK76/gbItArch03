using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.DomainModel;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Интерфейс бизнесс-процесса
    /// </summary>
    public interface IBProcess
    {
        /// <summary>
        /// Репозиторий, работающий с хранилищем
        /// </summary>
        IRepository Repository { get; }

        /// <summary>
        /// Отложенный инициализатор
        /// </summary>
        /// <param name="initedObject">Инициализируемый объект</param>
        /// <param name="property">Инициализируемое свойство</param>
        /// <returns>Инициализированные данные</returns>
        object LazyIniter(ITwoPhaseInited initedObject, string property);

    }
}
