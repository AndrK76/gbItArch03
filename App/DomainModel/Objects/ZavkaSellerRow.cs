using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Строка заявки поставщику
    /// </summary>
    public class ZavkaSellerRow
    {
        /// <summary>
        /// Заявка к которой относится строка
        /// </summary>
        public ZavkaSeller Zavka { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="zavka">Заявка</param>
        public ZavkaSellerRow(ZavkaSeller zavka)
        {
            this.Zavka = zavka;
        }

        /// <summary>
        /// Заявка на закуп
        /// </summary>
        public ZavkaZakup ZavkaZakup { get; set; }

        /// <summary>
        /// Оборудование
        /// </summary>
        public Oborudovanie Oborudovanie { get; set; }

        /// <summary>
        /// Строка счёта
        /// </summary>
        public ScheetRow ScheetRow { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public decimal Kol { get; set; }

        /// <summary>
        /// Данная строка сопоставлена со счётом
        /// </summary>
        public bool HaveScheet => this.ScheetRow != null;

        /// <summary>
        /// Отдано заказчику
        /// </summary>
        public bool IsCompete { get; set; }


    }
}
