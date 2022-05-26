using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Объект инициализируемый в 2 фазы
    /// </summary>
    public interface ITwoPhaseInited
    {
        /// <summary>
        /// Текущий бизнесс-процесс, необходим для отложенных инициализаций
        /// </summary>
        IBProcess CurrBProcess { get; set; }
    }
}
