using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Заказчик
    /// </summary>
    public class Zakazchik : BaseSubject, ISubject, IStorable
    {
        public override string ToString()
        {
            return $"Заказчик: Id={Id}\tНаименование {Name}";
        }
    }
}
