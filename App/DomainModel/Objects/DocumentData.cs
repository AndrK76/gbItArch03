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
    public class DocumentData
    {
        /// <summary>
        /// Наименование документа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Данные документа
        /// </summary>
        public byte[] Content { get; set; }
    }
}
