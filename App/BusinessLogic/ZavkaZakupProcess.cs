using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.DomainModel;


namespace AndrK.ZavPostav.BusinessLogic
{
    /// <summary>
    /// Процесс создания заявки
    /// </summary>
    public class ZavkaZakupProcess : BaseBProcess, IBProcess
    {

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="repository"></param>
        public ZavkaZakupProcess(IRepository repository) : base(repository)
        {
            initOborudovanie();
        }

        /// <summary>
        /// Заказчик
        /// </summary>
        public Zakazchik Zakazchik
        {
            get => _zakazchik;
            set
            {
                _zakazchik = value;
                getZavkaList();
            }
        }
        Zakazchik _zakazchik = null;

        /// <summary>
        /// Заявки на закуп
        /// </summary>
        public IList<ZavkaZakup> ZavkaList { get; private set; }

        public IList<Oborudovanie> OborudovanieList { get; private set; }


        /// <summary>
        /// Получить список заявок на закуп
        /// </summary>
        void getZavkaList()
        {
            IList<ZavkaZakup> tmpList = Repository.GetList<ZavkaZakup>().Where(r => r.Zakazchik.Id == Zakazchik.Id).ToList();
            for (int i = 0; i < tmpList.Count; i++)
            {
                tmpList[i].Zakazchik = Zakazchik;
                tmpList[i].CurrBProcess = this;
            }
            this.ZavkaList = tmpList;
        }

        /// <summary>
        /// Инициализировать список оборудование
        /// </summary>
        void initOborudovanie()
        {
            OborudovanieList = Repository.GetList<Oborudovanie>();
            foreach (var obor in OborudovanieList) obor.CurrBProcess = this;
        }

        /// <summary>
        /// Отложенный инициализатор данных процесса
        /// </summary>
        /// <param name="initedObject">Инициализируемый объект</param>
        /// <param name="property">Инициализируемое свойство</param>
        /// <returns>Инициализированные данные</returns>
        public override object LazyIniter(ITwoPhaseInited initedObject, string property)
        {
            object retData;
            if (LazyIniterCommon(initedObject, property, out retData))
                return retData;
            if (initedObject.GetType() == typeof(ZavkaZakup) && property == "Oborudovanie")
            {
                ZavkaZakup currZav = initedObject as ZavkaZakup;
                Dictionary<Oborudovanie, decimal> ret = new Dictionary<Oborudovanie, decimal>();
                ZavkaZakup storZav = Repository.GetEntity<ZavkaZakup>(currZav.Id);
                foreach (var item in storZav.Oborudovanie)
                {
                    Oborudovanie obor = OborudovanieList.FirstOrDefault(r => r.Id == item.Key.Id);
                    ret[obor] = item.Value;
                }
                return ret;
            }
            else
                throw new NotImplementedException($"Для объекта типа {initedObject.GetType().Name} не реализована отложенная инициализация свойства {property} в бизнеспроцессе ZavkaZakupProcess");
        }

        /// <summary>
        /// Сохраанить заявку
        /// </summary>
        /// <param name="zavka">Сохраняемая заявка</param>
        /// <returns>Id заявки</returns>
        public Guid SaveZavka(ZavkaZakup zavka)
        {
            Repository.SaveEntity<ZavkaZakup>(ref zavka);
            return zavka.Id;
        }

        /// <summary>
        /// Сохранить единицу оборудования
        /// </summary>
        /// <param name="obor">Оборудование</param>
        /// <returns>Id оборудования</returns>
        public Guid SaveOborudovanie(Oborudovanie obor)
        {
            Repository.SaveEntity<Oborudovanie>(ref obor);
            return obor.Id;
        }

        /// <summary>
        /// Сохранить спецификацию
        /// </summary>
        /// <param name="spec">Сохраняемая спецификация</param>
        /// <returns>Id спецификации</returns>
        public Guid SaveSpecification(Specification spec)
        {
            Repository.SaveEntity<Specification>(ref spec);
            return spec.Id;
        }

        /// <summary>
        /// Создать спецификацию оборудования
        /// </summary>
        /// <param name="obor">Оборудование</param>
        /// <returns>Спецификация оборудования</returns>
        public Specification NewSpec(Oborudovanie obor)
        {
            Specification ret = new Specification(obor);
            ret.CurrBProcess = this;
            ret.Content = new DocumentData();
            obor.Specifications.Add(ret);
            return ret;
        }
    }
}
