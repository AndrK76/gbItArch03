using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.DomainModel;

namespace AndrK.ZavPostav.BusinessLogic
{
    /// <summary>
    /// Базовый бизнесс-процесс
    /// </summary>
    public abstract class BaseBProcess : IBProcess
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        internal BaseBProcess(IRepository repository)
        {
            this._repository = repository;
        }

        public IRepository Repository => _repository;
        IRepository _repository = null;


        /// <summary>
        /// Отложенный инициализатор данных процесса
        /// </summary>
        /// <param name="initedObject">Инициализируемый объект</param>
        /// <param name="property">Инициализируемое свойство</param>
        /// <returns>Инициализированные данные</returns>
        public abstract object LazyIniter(ITwoPhaseInited initedObject, string property);


        /// <summary>
        /// Отложенный инициализатор данных процесса (общая часть)
        /// </summary>
        /// <param name="initedObject">Инициализируемый объект</param>
        /// <param name="property">Инициализируемое свойство</param>
        /// <param name="retData">Возвращаемые данные</param>
        /// <returns>Данные обработаны</returns>
        public bool LazyIniterCommon(ITwoPhaseInited initedObject, string property, out object retData)
        {
            if (initedObject.GetType() == typeof(Oborudovanie) && property == "Specifications")
            {
                Oborudovanie obor = initedObject as Oborudovanie;
                var ret = Repository.GetList<Specification>(obor, property).ToList();
                ret.ForEach(r => r.CurrBProcess = this);
                retData = ret;
                return true;
            }
            else if (initedObject.GetType() == typeof(Specification) && property == "Content")
            {
                Specification spec = initedObject as Specification;
                DocumentData ret = Repository.GetList<DocumentData>(spec, property).FirstOrDefault();
                if (ret != null) ret.CurrBProcess = this;
                retData = ret;
                return true;
            }
            else if (initedObject.GetType() == typeof(DocumentData) && property == "Content")
            {
                DocumentData currData = initedObject as DocumentData;
                DocumentData storData = Repository.GetEntity<DocumentData>(currData.Id);
                retData = storData.Content;
                return true;
            }
            retData = null;
            return false;
        }
    }
}
