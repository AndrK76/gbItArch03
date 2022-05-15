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
    public class Specification : BaseDocument, IDocument, IObject, IStorable, ITwoPhaseInited
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
        public DocumentData Content
        {
            get
            {
                if (!isContentInited && CurrBProcess != null)
                {
                    this._content = CurrBProcess.LazyIniter(this, "Content") as DocumentData;
                    isContentInited = true;
                }
                return _content;
            }
            set
            {
                _content = value;
                isContentInited = true;
            }
        }
        DocumentData _content;
        bool isContentInited = false;

        /// <summary>
        /// Текущий бизнесс-процесс, необходим для отложенных инициализаций
        /// </summary>
        public IBProcess CurrBProcess { get; set; }

        /// <summary>
        /// Получить строковое представление
        /// </summary>
        /// <returns>Текстовое представление</returns>
        public override string ToString()
        {
            return $"Object: Type={this.GetType().Name}: Name={Name}";
        }

    }
}
