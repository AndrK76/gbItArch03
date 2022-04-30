using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Заявка на закуп
    /// </summary>
    public class ZavkaZakup : BaseDocument, IDocument, IObject, IStorable
    {
        public ZavkaZakup()
        {
            this.Oborudovanie = new Dictionary<Oborudovanie, decimal>();
        }
        
        /// <summary>
        /// Список оборудования
        /// </summary>
        public IDictionary<Oborudovanie,decimal> Oborudovanie { get; private set; }

        /// <summary>
        /// Заявка подготовлена
        /// </summary>
        public bool IsPrepared { get; set; }

        /// <summary>
        /// Заявка принята в работу
        /// </summary>
        public bool IsAccepted { get; set; }

        /// <summary>
        /// Заявка выполнена
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Статус заявки
        /// </summary>
        public ZavkaState State
        {
            get
            {
                if (this.IsCompleted) return ZavkaState.Completed;
                if (this.IsAccepted) return ZavkaState.Accepted;
                if (this.IsPrepared) return ZavkaState.Prepared;
                return ZavkaState.New;
            }
        }

        /// <summary>
        /// Заказчик
        /// </summary>
        public Zakazchik Zakazchik { get; set; }

    }
}
