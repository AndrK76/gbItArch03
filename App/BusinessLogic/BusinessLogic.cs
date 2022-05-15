using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.DomainModel;

namespace AndrK.ZavPostav.BusinessLogic
{
    /// <summary>
    /// Фабрика по созданию бизнесс-процессов
    /// </summary>
    public class BusinessLogic
    {
        #region Общая часть
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="repository">Пкпозиторий по умолчанию</param>
        public BusinessLogic(IRepository repository)
        {
            _defaultRepository = repository;
        }
        /// <summary>
        /// Репозиторий по умолчанию
        /// </summary>
        IRepository _defaultRepository;

        /// <summary>
        /// Создать бизнесс-процесс
        /// </summary>
        /// <typeparam name="T">Тип бизнесс-процесса</typeparam>
        /// <param name="repository">Репозиторий процесса</param>
        /// <returns>Новый бизнесс-процесс</returns>
        T Create<T>(IRepository repository = null) where T : IBProcess
        {
            try
            {
                return (T)Activator.CreateInstance(typeof(T), repository ?? _defaultRepository);
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
        #endregion

        #region Работа с субъектами
        /// <summary>
        /// Инициализировать субъектов
        /// </summary>
        /// <param name="repository">Репозиторий с субъектами</param>
        public void InitSubjects(IRepository repository = null)
        {
            subjectRepository = repository ?? _defaultRepository;
            Zakazchiks = subjectRepository.GetList<Zakazchik>();
            Sellers = subjectRepository.GetList<Seller>();
        }
        IRepository subjectRepository = null;

        /// <summary>
        /// Заказчики
        /// </summary>
        public IList<Zakazchik> Zakazchiks { get; private set; }

        /// <summary>
        /// Поставщики
        /// </summary>
        public IList<Seller> Sellers { get; private set; }

        /// <summary>
        /// Сохранить субъекта
        /// </summary>
        /// <typeparam name="T">Тип субъекта</typeparam>
        /// <param name="subject">Субъект</param>
        public void SaveSubject<T>(T subject) where T : class, ISubject
        {
            subjectRepository.SaveEntity<T>(ref subject);
        }
        #endregion

        #region  Процесс заявки на закуп
        /// <summary>
        /// Процесс заявки на закуп
        /// </summary>
        public ZavkaZakupProcess ZavkaZakupProcess { get; private set; }

        /// <summary>
        /// Создать процесс заявки на закуп
        /// </summary>
        /// <param name="zakazchik">Заказчик</param>
        /// <param name="repository">Репозиторий заявок</param>
        /// <returns>Процесс заявки на закуп</returns>
        public ZavkaZakupProcess CreateZavkaZakupProcess(Zakazchik zakazchik, IRepository repository = null)
        {
            ZavkaZakupProcess ret = null;
            if (zakazchik == null)
                throw new ArgumentNullException("Не определена служба заказчика");
            ret = Create<ZavkaZakupProcess>(repository);
            ret.Zakazchik = zakazchik;
            this.ZavkaZakupProcess = ret;
            return ret;
        }
        #endregion

        #region  Процесс обработки заявки
        /// <summary>
        /// Процесс обработки заявки
        /// </summary>
        public ApplyZavkaProcess ApplyProcess { get; private set; }

        /// <summary>
        /// Создать процесс обработки заявки
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        /// <returns>Процесс обработки заявки</returns>
        public ApplyZavkaProcess CreateApplyZavkaProcess(IRepository repository = null)
        {
            ApplyZavkaProcess ret = null;
            if (repository as IApplyZavkaRepository == null)
                throw new ArgumentException($"Репозиторий типа {repository.GetType().Name} не реализует интерфейс IApplyZavkaRepository");
            ret = Create<ApplyZavkaProcess>(repository);
            this.ApplyProcess = ret;
            return ret;
        }
        #endregion

    }
}
