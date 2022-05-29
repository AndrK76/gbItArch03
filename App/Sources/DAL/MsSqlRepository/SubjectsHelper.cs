using AndrK.ZavPostav.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.MsSqlRepository
{
    public static class SubjectsHelper
    {
        public static ISubject GetSubject<T>(Guid id, gbItArch03Entities ctx)
        {
            if (typeof(T) == typeof(Zakazchik))
            {
                ZAKAZCHIK zak = ctx.ZAKAZCHIKS.FirstOrDefault(r => r.id == id);
                if (zak != null) return new Zakazchik { Id = zak.id, Name = zak.name };
                return null;
            }
            else if (typeof(T) == typeof(Seller))
            {
                SELLER seller = ctx.SELLERS.FirstOrDefault(r => r.id == id);
                if (seller != null) return new Seller { Id = seller.id, Name = seller.name };
                return null;
            }
            throw new NotImplementedException($"Для типа субъекта {typeof(T).Name} пока нет реализации");
        }

        public static IList<T> GetSubjectList<T>(gbItArch03Entities ctx)
        {
            if (typeof(T) == typeof(Zakazchik))
                return ctx.ZAKAZCHIKS.Select(r => new Zakazchik { Id = r.id, Name = r.name }).ToList() as IList<T>;
            else if (typeof(T) == typeof(Seller))
                return ctx.SELLERS.Select(r => new Seller { Id = r.id, Name = r.name }).ToList() as IList<T>;
            throw new NotImplementedException($"Для типа субъекта {typeof(T).Name} пока нет реализации");
        }

        public static Guid? SaveSubject(ref ISubject subject, gbItArch03Entities ctx)
        {
            ISubject subj = subject;
            if (subject as Zakazchik != null)
            {
                var stored = ctx.ZAKAZCHIKS.FirstOrDefault(r => r.id == subj.Id);
                if (stored != null)
                {
                    stored.name = subj.Name;
                    ctx.Entry(stored).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    stored = new ZAKAZCHIK { name = subj.Name, id = Guid.NewGuid() };
                    ctx.ZAKAZCHIKS.Add(stored);
                }
                ctx.SaveChanges();
                subject.Id = stored.id;
            }
            else if (subject as Seller != null)
            {
                var stored = ctx.SELLERS.FirstOrDefault(r => r.id == subj.Id);
                if (stored != null)
                {
                    stored.name = subj.Name;
                    ctx.Entry(stored).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    stored = new SELLER { name = subj.Name , id = Guid.NewGuid()};
                    ctx.SELLERS.Add(stored);
                }
                ctx.SaveChanges();
                subject.Id = stored.id;
            }
            else
                throw new NotImplementedException($"Для типа субъекта {subj.GetType().Name} пока нет реализации");
            return subject.Id;
        }

        public static int RemoveSubject(ref ISubject subject, gbItArch03Entities ctx)
        {
            ISubject currSubj = null;
            if (subject as Zakazchik != null)
                currSubj = GetSubject<Zakazchik>(subject.Id, ctx);
            else if (subject as Seller != null)
                currSubj = GetSubject<Seller>(subject.Id, ctx);
            else
                throw new NotImplementedException($"Для типа субъекта {subject.GetType().Name} пока нет реализации");
            if (currSubj == null)
            {
                subject = null;
                return 0;
            }
            else
            {
                ISubject subj = subject;
                if (subject.GetType() == typeof(Zakazchik))
                {
                    ZAKAZCHIK zak = ctx.ZAKAZCHIKS.FirstOrDefault(r => r.id == subj.Id);
                    ctx.ZAKAZCHIKS.Remove(zak);
                }
                else if (subject.GetType() == typeof(Seller))
                {
                    SELLER seller = ctx.SELLERS.FirstOrDefault(r => r.id == subj.Id);
                    ctx.SELLERS.Remove(seller);
                }
                ctx.SaveChanges();
                subject = null;
                return 1;
            }
        }



    }
}
