using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM = AndrK.ZavPostav.DomainModel;
using System.Data.Entity;

namespace AndrK.ZavPostav.RepoSQL.Repo
{
    public class ZavkaZakupWorker
    {
        public static DM.ZavkaZakup Create(ZavkaZakup stored, bool createObor = false)
        {
            if (stored == null) return null;
            return new DM.ZavkaZakup(createObor)
            {
                Id = stored.id,
                Name = stored.name,
                IsAccepted = stored.isAccepted,
                IsPrepared = stored.isPrepared,
                IsCompleted = stored.isCompleted,
                Zakazchik = new DM.Zakazchik { Id = stored.zakazchik_id }
            };
        }

        /// <summary>
        /// Загрузить список заявок на закуп
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <returns>Список заявок</returns>
        public static IList<DM.ZavkaZakup> GetList(gbItArch03Entities dbContext)
        {
            List<DM.ZavkaZakup> ret = new List<DM.ZavkaZakup>();
            dbContext.ZavkaZakup.ToList().ForEach(r => ret.Add(ZavkaZakupWorker.Create(r)));
            return ret;
        }

        public static DM.ZavkaZakup GetZavka(gbItArch03Entities dbContext, Guid id)
        {
            var stored = dbContext.ZavkaZakup.FirstOrDefault(r => r.id == id);
            var ret = Create(stored, true);
            foreach (var obor in stored.ZavkaZakup_Oborudovanie.ToDictionary(k => new DM.Oborudovanie { Id = k.oborudovanie_id }, v => v.kol))
                ret.Oborudovanie.Add(obor);
            return ret;
        }
    }
}
