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
    public class Oborudovanie : BaseObject, IObject, IStorable, ITwoPhaseInited
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Oborudovanie() : base()
        {
        }

        /// <summary>
        /// Спецификации
        /// </summary>
        public virtual IList<Specification> Specifications
        {
            get
            {
                if (!isSpetificationsInited && CurrBProcess != null)
                {
                    this._specifications = CurrBProcess.LazyIniter(this, "Specifications") as IList<Specification>;
                    isSpetificationsInited = true;
                }
                return _specifications;
            }
        }
        bool isSpetificationsInited = false;
        IList<Specification> _specifications = null;


        /// <summary>
        /// Учётные данные бухгалтерской системы
        /// </summary>
        public string Nomenklatura { get; set; }

        /// <summary>
        /// Текущий бизнесс-процесс, необходим для отложенных инициализаций
        /// </summary>
        public IBProcess CurrBProcess { get; set; }
    }
}
