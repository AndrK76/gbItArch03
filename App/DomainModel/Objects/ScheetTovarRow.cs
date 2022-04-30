using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel.Objects
{
    /// <summary>
    /// Строка товара
    /// </summary>
    public class ScheetTovarRow
    {
        /// <summary>
        /// Счёт к которому относится строка
        /// </summary>
        public ScheetTovar ScheetTovar { get; private set; }

        public ScheetTovarRow(ScheetTovar scheetTovar)
        {
            this.ScheetTovar = scheetTovar;
        }

        /// <summary>
        /// Заявка по которой отрабатываем
        /// </summary>
        public ZavkaZakup ZavkaZakup { get; set; }

        /// <summary>
        /// Оборудование для закупа
        /// </summary>
        public Oborudovanie Oborudovanie { get; set; }

        /// <summary>
        /// Количество оборудования
        /// </summary>
        public int Kol { get; set; }



    }
}
