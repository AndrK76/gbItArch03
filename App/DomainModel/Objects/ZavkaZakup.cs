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
    public class ZavkaZakup : BaseDocument, IDocument, IObject, IStorable, ITwoPhaseInited
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ZavkaZakup()
        {
        }


        /// <summary>
        /// Список оборудования
        /// </summary>
        public IDictionary<Oborudovanie, decimal> Oborudovanie

        {
            get
            {
                if (!isOborudovanieInited && CurrBProcess != null)
                {
                    _oborudovanie = CurrBProcess.LazyIniter(this, "Oborudovanie") as IDictionary<Oborudovanie, decimal>;
                    isOborudovanieInited = true;
                }
                return _oborudovanie;
            }
        }
        IDictionary<Oborudovanie, decimal> _oborudovanie = null;
        bool isOborudovanieInited = false;


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

        /// <summary>
        /// Текущий бизнесс-процесс, необходим для отложенных инициализаций
        /// </summary>
        public IBProcess CurrBProcess { get; set; }

    }
}
