using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Оборудование
    /// </summary>
    public class Oborudovanie : BaseObject, IObject, IStorable
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Oborudovanie() : base()
        {
            this.Specifications = new List<Specification>();
        }

        /// <summary>
        /// Спецификации
        /// </summary>
        public virtual IList<Specification> Specifications { get; private set; }

        /// <summary>
        /// Учётные данные бухгалтерской системы
        /// </summary>
        public string Nomenklatura { get; set; }
    }
}
