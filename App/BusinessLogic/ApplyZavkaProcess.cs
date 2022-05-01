using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.DomainModel;

namespace AndrK.ZavPostav.BusinessLogic
{
    public class ApplyZavkaProcess : BaseBProcess, IBProcess
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="repository"></param>
        internal ApplyZavkaProcess(IApplyZavkaRepository repository) : base(repository)
        {
            thisRepository = repository;
            initData();
        }

        /// <summary>
        /// Список заказчиков
        /// </summary>
        public IList<Zakazchik> ZakazchikList { get; private set; }

        /// <summary>
        /// Список поставщиков
        /// </summary>
        public IList<Seller> SellerList { get; private set; }

        /// <summary>
        /// Список оборудования
        /// </summary>
        public IList<Oborudovanie> OborudovanieList { get; private set; }

        /// <summary>
        /// Список заявок
        /// </summary>
        public IList<ZavkaZakup> ZavkaList { get; private set; }

        /// <summary>
        /// Заявки поставщику
        /// </summary>
        public IList<ZavkaSeller> WorkList { get; private set; }

        /// <summary>
        /// Счета поставщику
        /// </summary>
        public IList<ScheetTovar> ScheetList { get; private set; }

        List<ZavkaSellerRow> allRows = null;
        //List<ScheetTovarRow> allSchRows = null;


        IApplyZavkaRepository thisRepository = null;

        void initData()
        {
            ZakazchikList = Repository.GetList<Zakazchik>();
            SellerList = Repository.GetList<Seller>();
            OborudovanieList = Repository.GetList<Oborudovanie>();
            ZavkaList = new List<ZavkaZakup>();
            WorkList = new List<ZavkaSeller>();
            ScheetList = new List<ScheetTovar>();
            thisRepository.LoadDataForApplyZavka(this);

            allRows = new List<ZavkaSellerRow>();
            foreach (var zav in WorkList)
                allRows.AddRange(zav.Rows);

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
            /*if false
                ;
            else*/
            throw new NotImplementedException($"Для объекта типа {initedObject.GetType().Name} не реализована отложенная инициализация свойства {property} в бизнеспроцессе ApplyZavkaProcess");

        }

        /// <summary>
        /// Список заявок нуждающихся во включении в заявки на закуп
        /// </summary>
        /// <returns>Список заявок</returns>
        public List<ZavkaZakup> GetZavkaListNeedApply()
        {
            List<ZavkaZakup> ret = new List<ZavkaZakup>();
            foreach (var zav in ZavkaList)
            {
                foreach (var item in zav.Oborudovanie)
                {
                    var couOst = item.Value - allRows.Where(r => r.ZavkaZakup == zav && r.Oborudovanie == item.Key).Sum(r => r.Kol);
                    if (couOst > 0)
                    {
                        ret.Add(zav);
                        break;
                    }
                }
            }
            return ret;
        }


        /// <summary>
        /// Добавить данные из заявок на закуп в заявку поставщику
        /// </summary>
        /// <param name="currZavka">Заявка поставщику</param>
        /// <param name="zavka">Обрабатываемая заявка на закуп</param>
        /// <param name="oborudovanie">Список добавляемого оборудования</param>
        public void AddZayavkaData(ZavkaSeller currZavka, ZavkaZakup zavka, IList<Oborudovanie> oborudovanie = null)
        {
            foreach (var obor in zavka.Oborudovanie.Keys)
            {
                if (oborudovanie != null && !oborudovanie.Contains(obor))
                    continue;
                var applyKol = allRows.Where(r => r.ZavkaZakup == zavka && r.Oborudovanie == obor).Sum(r => r.Kol);
                if (zavka.Oborudovanie[obor] > applyKol)
                {
                    ZavkaSellerRow ins = new ZavkaSellerRow(currZavka);
                    ins.ZavkaZakup = zavka;
                    ins.Oborudovanie = obor;
                    ins.Kol = zavka.Oborudovanie[obor] - applyKol;
                    currZavka.Rows.Add(ins);
                    allRows.Add(ins);
                }
            }
        }

        /// <summary>
        /// Разнести счёт поставщика по заявкам поставщику
        /// </summary>
        /// <param name="scheet">Счёт на закуп</param>
        public void ApplyScheetToZavkaSeller(ScheetTovar scheet)
        {
            foreach (var sch in scheet.Rows)
            {
                if (sch.Oborudovanie == null) continue;
                decimal couOst = sch.Kol - allRows.Where(r => r.ScheetRow == sch).Sum(r => r.KolScheet);
                if (couOst == 0) continue;
                foreach (var zSelRow in allRows.Where(r => r.Zavka.Seller == scheet.Seller && r.Oborudovanie == sch.Oborudovanie && r.ScheetRow == null))
                {
                    if (zSelRow.Kol > couOst) { zSelRow.KolScheet = couOst; couOst = 0; }
                    else { zSelRow.KolScheet = zSelRow.Kol; couOst -= zSelRow.KolScheet; }
                    if (couOst == 0) break;
                }

            }
        }

        /// <summary>
        /// Получить список строк заявок на поставку нуждающихся в корректировке
        /// </summary>
        /// <returns>Строки нуждающиеся в корректировке</returns>
        public List<ZavkaSellerRow> GetRowsToNeedCorrectAfterApllyScheet()
        {
            return allRows.Where(r => r.ScheetRow != null && r.Kol > r.KolScheet).ToList();
        }

        /// <summary>
        /// Откорректировать строку заявки поставщику
        /// </summary>
        /// <param name="row">Корректируемая строка</param>
        /// <param name="newZavka">Новая заявка</param>
        public void ApplyZavkaSellerRowCorrect(ZavkaSellerRow row,ZavkaSeller newZavka)
        {
            if (newZavka == null)
                row.Kol = row.KolScheet;
            else
            {
                decimal newKol = row.Kol - row.KolScheet;
                ZavkaSellerRow ins = new ZavkaSellerRow(newZavka);
                ins.ZavkaZakup = row.ZavkaZakup;
                ins.Oborudovanie = row.Oborudovanie;
                ins.Kol = newKol;
                newZavka.Rows.Add(ins);
                allRows.Add(ins);
            }
        }

    }
}
