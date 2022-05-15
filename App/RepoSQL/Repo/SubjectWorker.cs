using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM = AndrK.ZavPostav.DomainModel;
using System.Data.Entity;

namespace AndrK.ZavPostav.RepoSQL.Repo
{
    public class SubjectWorker
    {
        /// <summary>
        /// Загрузить список субъектов
        /// </summary>
        /// <typeparam name="T">Тип субъекта</typeparam>
        /// <param name="dbContext">Контекст БД</param>
        /// <returns>Список субъектов</returns>
        public static IList<T> GetSubjectList<T>(gbItArch03Entities dbContext)
        {
            if (typeof(T) == typeof(DM.Zakazchik))
                return dbContext.Zakazchik.Select(r => new DM.Zakazchik { Id = r.id, Name = r.name }).ToList() as IList<T>;
            else if (typeof(T) == typeof(DM.Seller))
                return dbContext.Seller.Select(r => new DM.Seller { Id = r.id, Name = r.name }).ToList() as IList<T>;
            throw new NotImplementedException($"Для типа субъекта {typeof(T).Name} пока нет реализации");
        }

        /// <summary>
        /// Получить все данные субъекта
        /// </summary>
        /// <typeparam name="T">Тип субъекта</typeparam>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="id">Id субъекта</param>
        /// <returns>Субъект</returns>
        public static DM.ISubject GetSubject<T>(gbItArch03Entities dbContext, Guid id)
        {
            if (typeof(T) == typeof(DM.Zakazchik))
            {
                Zakazchik zak = dbContext.Zakazchik.FirstOrDefault(r => r.id == id);
                if (zak != null) return new DM.Zakazchik { Id = zak.id, Name = zak.name };
                return null;
            }
            else if (typeof(T) == typeof(DM.Seller))
            {
                Seller zak = dbContext.Seller.FirstOrDefault(r => r.id == id);
                if (zak != null) return new DM.Seller { Id = zak.id, Name = zak.name };
                return null;
            }
            throw new NotImplementedException($"Для типа субъекта {typeof(T).Name} пока нет реализации");
        }

    }
}
