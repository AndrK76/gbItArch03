using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Информация о документе
    /// </summary>
    public class DocumentData : BaseDocument, IStorable, IObject, ITwoPhaseInited
    {

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }

        byte[] _content = null;
        bool isContentInited = false;

        /// <summary>
        /// Данные документа
        /// </summary>
        public byte[] Content
        {
            get
            {
                if (!isContentInited && CurrBProcess != null)
                {
                    this._content = CurrBProcess.LazyIniter(this, "Content") as byte[];
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

        /// <summary>
        /// Текущий бизнесс-процесс, необходим для отложенных инициализаций
        /// </summary>
        public IBProcess CurrBProcess { get; set; }
    }
}
