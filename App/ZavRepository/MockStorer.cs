using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.DomainModel;
using AndrK.ZavPostav.BusinessLogic;

namespace AndrK.ZavPostav.ZavRepository
{
    public class MockStorer : IRepository, IApplyZavkaRepository
    {
        public MockStorer(IDbConnection connection)
        {
            this._connection = connection;
        }

        IDbConnection _connection = null;

        public IStorable GetEntity(Guid id)
        {
            //Реализация этого метода возможна при уникальном ID по всем объектам
            throw new NotImplementedException();
        }


        public T GetEntity<T>(Guid id) where T : class, IStorable
        {
            if (typeof(T) == typeof(Specification))
            {
                //В хранилище находим по Id спецификации нужное оборудование
                Oborudovanie obor = GetEntity<Oborudovanie>(Guid.Empty);
                Specification spec = new Specification(obor);
                //Загружаем спецификацию
                return obor as T;

            }
            else if (typeof(T) == typeof(ZavkaZakup))
            {
                //Загружаем заявку
                return null;
            }
            else
            {
                throw new NotImplementedException();
            }
        }


        public IList<T> GetList<T>(IObject colOwner = null, string ownProp = null) where T : IStorable
        {
            throw new NotImplementedException();
        }

        public void LoadDataForApplyZavka(ApplyZavkaProcess applyProcess)
        {
            //В этом методе последовательно загрузим строки для всех объектов applyProcess после
            //ZakazchikList = Repository.GetList<Zakazchik>();
            //SellerList = Repository.GetList<Seller>();
            //OborudovanieList = Repository.GetList<Oborudovanie>();
            //ZavkaList = new List<ZavkaZakup>();
            //WorkList = new List<ZavkaSeller>();
            //ScheetList = new List<ScheetTovar>();

            throw new NotImplementedException();
        }

        public void SaveDataForApplyZavka(ApplyZavkaProcess applyProcess)
        {
            throw new NotImplementedException();
        }

        public Guid? SaveEntity(ref IStorable entity)
        {
            //Логику работы этого и следующего метода можно совместить
            if (entity.GetType() == typeof(Seller))
            {
                Seller seller = entity as Seller;
                return SaveEntity<Seller>(ref seller);
            }
            throw new NotImplementedException();
        }

        public Guid? SaveEntity<T>(ref T entity)
        {
            throw new NotImplementedException();
        }

    }
}
