using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Спецификация оборудования
    /// </summary>
    public class Specification : BaseDocument, IDocument, IStorable
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="oborudovanie"></param>
        public Specification(Oborudovanie oborudovanie) : base()
        {
            this.Oborudovanie = oborudovanie;
        }

        /// <summary>
        /// Оборудование к которому относится спецификация
        /// </summary>
        public Oborudovanie Oborudovanie { get; private set; }

        /// <summary>
        /// Данные спецификации
        /// </summary>
        public DocumentData Content { get; set; }
    }
}
