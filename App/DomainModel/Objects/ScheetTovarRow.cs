using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Строка счёта от поставщика
    /// </summary>
    public class ScheetTovarRow
    {
        /// <summary>
        /// Счет к которому относится
        /// </summary>
        public ScheetTovar Scheet { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="scheet"></param>
        public ScheetTovarRow(ScheetTovar scheet)
        {
            this.Scheet = scheet;
        }

        /// <summary>
        /// Номер строки в счёте
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        public string Articul { get; set; }

        /// <summary>
        /// Наименование оборудования
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество оборудования
        /// </summary>
        public decimal Kol { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Оборудование
        /// </summary>
        public Oborudovanie Oborudovanie {get;set;}
    }
}
