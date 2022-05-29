using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Интерфейс субьекта
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        String Name { get; set; }
        
    }
}
