using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Интерфейс объекта
    /// </summary>
    public interface IObject
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        String Name { get; }
        
    }
}
