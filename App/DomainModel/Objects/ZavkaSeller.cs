using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Запрос поставщику
    /// </summary>
    public class ZavkaSeller : BaseDocument, IDocument, IObject, IStorable
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ZavkaSeller()
        {
            this.Rows = new List<ZavkaSellerRow>();
        }

        /// <summary>
        /// Строки заявки
        /// </summary>
        public virtual IList<ZavkaSellerRow> Rows { get; private set; }
    }
}
