using AndrK.ZavPostav.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.MokeRepository
{
    public static class SubjectsHelper
    {
        public static ISubject GetSubject<T>(Guid id)
        {
            if (typeof(T) == typeof(Zakazchik))
            {
                Zakazchik zak = Store.Zakazchiks.FirstOrDefault(r => r.Id == id);
                return zak;
            }
            else if (typeof(T) == typeof(Seller))
            {
                Seller seller = Store.Sellers.FirstOrDefault(r => r.Id == id);
                return seller;
            }
            throw new NotImplementedException($"Для типа субъекта {typeof(T).Name} пока нет реализации");
        }

        public static IList<T> GetSubjectList<T>()
        {
            if (typeof(T) == typeof(Zakazchik))
                return Store.Zakazchiks.Select(r => new Zakazchik { Id = r.Id, Name = r.Name }).ToList() as IList<T>;
            else if (typeof(T) == typeof(Seller))
                return Store.Sellers.Select(r => new Seller { Id = r.Id, Name = r.Name }).ToList() as IList<T>;
            throw new NotImplementedException($"Для типа субъекта {typeof(T).Name} пока нет реализации");
        }

        public static Guid? SaveSubject(ref ISubject subject)
        {
            ISubject currSubj = null;
            if (subject as Zakazchik != null)
                currSubj = GetSubject<Zakazchik>(subject.Id);
            else if (subject as Seller != null)
                currSubj = GetSubject<Seller>(subject.Id);
            if (currSubj != null)
                currSubj.Name = subject.Name;
            else
            {
                subject.Id = Guid.NewGuid();
                if (subject.GetType() == typeof(Zakazchik)) Store.Zakazchiks.Add(subject as Zakazchik);
                else if (subject.GetType() == typeof(Seller)) Store.Sellers.Add(subject as Seller);
            }
            return subject.Id;
        }

        public static int RemoveSubject(ref ISubject subject)
        {
            ISubject currSubj = null;
            if (subject as Zakazchik != null)
                currSubj = GetSubject<Zakazchik>(subject.Id);
            else if (subject as Seller != null)
                currSubj = GetSubject<Seller>(subject.Id);
            if (currSubj == null)
            {
                subject = null;
                return 0;
            }
            else
            {
                if (subject.GetType() == typeof(Zakazchik))
                {
                    Zakazchik zak = currSubj as Zakazchik;
                    Store.Zakazchiks.Remove(zak);
                }
                else if (subject.GetType() == typeof(Seller))
                {
                    Seller seller = currSubj as Seller;
                    Store.Sellers.Remove(seller);
                }
                subject = null;
                return 1;
            }
        }

    }
}
