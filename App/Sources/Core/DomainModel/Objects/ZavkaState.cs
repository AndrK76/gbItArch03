using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Состояние заявки
    /// </summary>
    public enum ZavkaState
    {
        /// <summary>
        /// Новая
        /// </summary>
        New,
        /// <summary>
        /// Подготовлена
        /// </summary>
        Prepared,
        /// <summary>
        /// Согласована
        /// </summary>
        Accepted,
        /// <summary>
        /// Выполнена
        /// </summary>
        Completed
    }
}
