using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.DomainModel;
using System.Data.Entity;

namespace AndrK.ZavPostav.MsSqlRepository
{
    public class Repository : IRepository
    {
        #region ctx - контекст
        gbItArch03Entities ctx
        {
            get
            {
                if (_ctx == null)
                    _ctx = new gbItArch03Entities();
                return _ctx;
            }
        }
        gbItArch03Entities _ctx;
        #endregion

        T IRepository.GetEntity<T>(Guid id)
        {
            Type entType = typeof(T);
            if (entType.GetInterface(nameof(ISubject)) != null)
                return SubjectsHelper.GetSubject<T>(id, ctx) as T;
            throw new NotImplementedException($"Для типа {entType.Name} пока нет реализации выбора");
        }

        public IStorable GetEntity(Guid id)
        {
            throw new NotImplementedException();
        }


        public IList<T> GetList<T>(IObject colOwner = null, string ownProp = null) where T : IStorable
        {
            Type entType = typeof(T);
            if (entType.GetInterface(nameof(ISubject)) != null)
                return SubjectsHelper.GetSubjectList<T>(ctx) as IList<T>;
            throw new NotImplementedException($"Для типа {entType.Name} пока нет реализации выбора списка");
        }

        public Guid? SaveEntity(ref IStorable entity)
        {
            Type entType = entity.GetType();
            if (entType.GetInterface(nameof(ISubject)) != null)
            {
                ISubject subj = (ISubject)entity;
                return SubjectsHelper.SaveSubject(ref subj, ctx);
            }
            throw new NotImplementedException($"Для типа {entType.Name} пока нет реализации сохранения");
        }

        public Guid? SaveEntity<T>(ref T entity)
        {
            IStorable ret = entity as IStorable;
            return SaveEntity(ref ret);
        }




        public int RemoveEntity(ref IStorable entity)
        {
            Type entType = entity.GetType();
            if (entType.GetInterface(nameof(ISubject)) != null)
            {
                ISubject subj = (ISubject)entity;
                return SubjectsHelper.RemoveSubject(ref subj, ctx);

            }
            throw new NotImplementedException($"Для типа {entType.Name} пока нет реализации удаления");
        }

        public int RemoveEntity<T>(ref T entity)
        {
            IStorable ret = entity as IStorable;
            return RemoveEntity(ref ret);
        }

    }
}
