using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM = AndrK.ZavPostav.DomainModel;
using System.Data.Entity;

namespace AndrK.ZavPostav.DAL_MSSql
{
    /// <summary>
    /// Работа с оборудованием
    /// </summary>
    public class OborudovanieUtils
    {
        /// <summary>
        /// Загрузить список оборудование
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <returns>Список оборудования</returns>
        public static IList<DM.Oborudovanie> GetList(gbItArch03Entities dbContext)
        {
            return dbContext.Oborudovanie.Select(r => new DM.Oborudovanie { Id = r.id, Name = r.name, Nomenklatura = r.nomenklatura }).ToList();
        }

        /// <summary>
        /// Загрузить список спецификаций оборудования
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="obor">Оборудование</param>
        /// <returns>Список спецификаций</returns>
        public static IList<DM.Specification> GetSpecifications(gbItArch03Entities dbContext, DM.Oborudovanie obor)
        {
            return dbContext.Oborudovanie.FirstOrDefault(r => r.id == obor.Id).Specification.ToList()
                 .Select(r => new DM.Specification(obor) { Id = r.id, Name = r.name }).ToList();
        }

        public static IList<DM.DocumentData> GetSpecContent(gbItArch03Entities dbContext, DM.Specification spec)
        {
            return dbContext.DocumentData.Where(r => r.specification_id == spec.Id).Select(r => new DM.DocumentData { Id = r.id, Name = spec.Name, FileName = r.fileName }).ToList();
        }

        public static DM.Oborudovanie GetObor(gbItArch03Entities dbContext, Guid id)
        {
            return GetList(dbContext).FirstOrDefault(r => r.Id == id);
        }

        public static DM.Specification GetSpec(gbItArch03Entities dbContext, Guid id)
        {
            Specification spec = dbContext.Specification.Where(r => r.id == id).Include(r => r.Oborudovanie).FirstOrDefault();
            DM.Oborudovanie obor = GetObor(dbContext, spec.Oborudovanie.id);
            return new DM.Specification(obor) { Id = spec.id, Name = spec.name };
        }

        public static DM.DocumentData GetDocData(gbItArch03Entities dbContext, Guid id)
        {
            DocumentData ret = dbContext.DocumentData.FirstOrDefault(r => r.id == id);
            return new DM.DocumentData { Id = ret.id, Name = ret.Specification.name, FileName = ret.fileName, Content = ret.content };
        }

        public static Guid SaveSpec(gbItArch03Entities dbContext, ref DM.Specification specRef)
        {
            DM.Specification spec = specRef;
            var stored = dbContext.Specification.FirstOrDefault(r => r.id == spec.Id);
            DocumentData content = null;
            if (stored == null)
            {
                stored = new Specification();
                stored.id = spec.Id;
                dbContext.Specification.Add(stored);
            }
            else
                content = stored.DocumentData.FirstOrDefault();
            if (content == null && spec.Content != null)
            {
                content = new DocumentData();
                content.id = spec.Content.Id;
                stored.DocumentData.Add(content);
                stored.DocumentData.Add(content);
            }
            stored.name = spec.Name;
            stored.oborudovanie_id = spec.Oborudovanie.Id;
            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {

                dbContext.SaveChanges();
                spec.Id = stored.id;
                if (spec.Content != null)
                {
                    content.fileName = spec.Content.FileName;
                    content.content = spec.Content.Content;
                    content.specification_id = stored.id;
                    dbContext.SaveChanges();
                    spec.Content.Id = content.id;
                }
                dbContextTransaction.Commit();
            }

            return spec.Id;
        }
    }
}
