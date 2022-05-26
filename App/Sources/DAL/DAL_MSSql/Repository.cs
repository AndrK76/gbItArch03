using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.BusinessLogic;
using AndrK.ZavPostav.DomainModel;
using AndrK.ZavPostav.DAL_MSSql;
using DM = AndrK.ZavPostav.DomainModel;

namespace AndrK.ZavPostav.DAL_MSSql
{
    public class Repository : IRepository, IApplyZavkaRepository, IDisposable
    {
        /// <summary>
        /// Реализация репозитория на основе SQL-сервер
        /// </summary>
        public Repository()
        {
            dbContext = new gbItArch03Entities();
        }

        /// <summary>
        /// Контекст модели БД
        /// </summary>
        gbItArch03Entities dbContext = null;

        /// <summary>
        /// Очистка ресурсов
        /// </summary>
        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        /// <summary>
        /// Получить сущность
        /// </summary>
        /// <param name="id">Id запрашиваемой сущности</param>
        /// <returns>Сущность из хранилища</returns>
        public IStorable GetEntity(Guid id)
        {
            //Не буду реализовывать данный тип.
            //Для реаляционной БД стоит создать таблицу,
            //в которой хранить GUID - Класс объекта
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить коллекцию сущностей
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="colOwner">Владелец коллекции</param>
        /// <param name="ownProp">Получаемое свойство владельца коллекции</param>
        /// <returns>Коллекция сущностей</returns>
        public IList<T> GetList<T>(IObject colOwner = null, string ownProp = null) where T : IStorable
        {
            Type entType = typeof(T);
            if (entType.GetInterface(nameof(DM.ISubject)) != null)
                return SubjectUtils.GetSubjectList<T>(dbContext) as IList<T>;
            else if (entType == typeof(DM.Oborudovanie))
                return OborudovanieUtils.GetList(dbContext) as IList<T>;
            else if (entType == typeof(DM.ZavkaZakup))
                return ZavkaZakupUtils.GetList(dbContext) as IList<T>;
            else if (entType == typeof(DM.Specification))
            {
                if (colOwner as DM.Oborudovanie != null && ownProp == "Specifications")
                    return OborudovanieUtils.GetSpecifications(dbContext, colOwner as DM.Oborudovanie) as IList<T>;
                else
                    throw new NotImplementedException($"Для типа {entType.Name} пока нет реализации выбора списка по аргументам colOwner: {colOwner}, ownProp: {ownProp}");
            }
            else if (entType == typeof(DM.DocumentData))
            {
                if (colOwner as DM.Specification != null && ownProp == "Content")
                    return OborudovanieUtils.GetSpecContent(dbContext, colOwner as DM.Specification) as IList<T>;
                else
                    throw new NotImplementedException($"Для типа {entType.Name} пока нет реализации выбора списка по аргументам colOwner: {colOwner}, ownProp: {ownProp}");
            }
            throw new NotImplementedException($"Для типа {entType.Name} пока нет реализации выбора списка");
        }

        /// <summary>
        /// Получить сущность
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="id">Id сущности</param>
        /// <returns>Сущность</returns>
        public T GetEntity<T>(Guid id) where T : class, IStorable
        {
            Type entType = typeof(T);
            if (entType.GetInterface(nameof(DM.ISubject)) != null)
                return SubjectUtils.GetSubject<T>(dbContext, id) as T;
            else if (entType == typeof(DM.Oborudovanie))
                return OborudovanieUtils.GetObor(dbContext, id) as T;
            else if (entType == typeof(DM.Specification))
                return OborudovanieUtils.GetSpec(dbContext, id) as T;
            else if (entType == typeof(DM.DocumentData))
                return OborudovanieUtils.GetDocData(dbContext, id) as T;
            else if (entType == typeof(DM.ZavkaZakup))
                return ZavkaZakupUtils.GetZavka(dbContext, id) as T;
            throw new NotImplementedException($"Для типа {entType.Name} пока нет реализации выбора");
        }

        /// <summary>
        /// Сохранить сущность
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        /// <returns>Id сохраненной сущности</returns>
        public Guid? SaveEntity<T>(ref T entity)
        {
            //Не имеет особого смысла отдельная реализация,
            //т.к. тип сущности мы и так видим
            IStorable ret = entity as IStorable;
            return SaveEntity(ref ret);
        }

        public Guid? SaveEntity(ref IStorable entity)
        {
            Type entType = entity.GetType();
            if (entType == typeof(DM.Specification))
            {
                DM.Specification spec = entity as DM.Specification;
                return OborudovanieUtils.SaveSpec(dbContext, ref spec);
            }

            throw new NotImplementedException($"Для типа {entType.Name} пока нет реализации сохранения");
        }


        public void LoadDataForApplyZavka(ApplyZavkaProcess applyProcess)
        {
            throw new NotImplementedException();
        }

        public void SaveDataForApplyZavka(ApplyZavkaProcess applyProcess)
        {
            throw new NotImplementedException();
        }



    }
}
