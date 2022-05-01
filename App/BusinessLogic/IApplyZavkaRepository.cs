using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.DomainModel;

namespace AndrK.ZavPostav.BusinessLogic
{
    /// <summary>
    /// Интерфейс репозитория для работы с процессом обработки заявок
    /// </summary>
    public interface IApplyZavkaRepository : IRepository
    {
        /// <summary>
        /// Загрузить данные для обработки заявок
        /// </summary>
        /// <param name="applyProcess"></param>
        void LoadDataForApplyZavka(ApplyZavkaProcess applyProcess);
    }
}
