using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Базовый класс документа
    /// </summary>
    public class BaseDocument : BaseObject, IDocument, IObject, IStorable
    {
                    /// <summary>
                    /// Строковое представление
                    /// </summary>
                    /// <returns></returns>
        public override string ToString()
        {
            return $"Document: Type={MethodBase.GetCurrentMethod().DeclaringType}: Name={Name}";
        }
    }
}
