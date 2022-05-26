using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Интерфейс сохраняемого объекта
    /// </summary>
    public interface IStorable
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        Guid Id { get; }


    }
}
