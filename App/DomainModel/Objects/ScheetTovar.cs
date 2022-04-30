using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Счёт поставщика на товар
    /// </summary>
    public class ScheetTovar : BaseDocument, IDocument, IObject, IStorable
    {
        /// <summary>
        /// Поставщик
        /// </summary>
        public Seller Seller { get; set; }

        /// <summary>
        /// Номер счёта
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// Дата счёта
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма счёта
        /// </summary>
        public Decimal Sum { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public Decimal Nds { get; set; }

        /// <summary>
        /// Строки счёта
        /// </summary>
        public virtual IList<ScheetRow> Rows { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public ScheetTovar()
        {
            this.Rows = new List<ScheetRow>();
        }

        /// <summary>
        /// Счёт исполнен поставщиком
        /// </summary>
        public bool IsCompeted { get; set; }
    }
}
