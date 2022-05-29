using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndrK.ZavPostav.DomainModel;

namespace AndrK.ZavPostav.BusinessLogic2
{
    public class SubjectProcess
    {
        public SubjectProcess(IRepository rep)
        {
            this.rep = rep;
        }

        IRepository rep;

        public IList<Zakazchik> GetZakazchikList() => rep.GetList<Zakazchik>();

        public IList<Seller> GetSellerList() => rep.GetList<Seller>();

        public Seller GetSellerById(Guid id) => rep.GetEntity<Seller>(id);

        public Zakazchik GetZakazchikById(Guid id) => rep.GetEntity<Zakazchik>(id);

        public Guid? AddZakazchik(Zakazchik subj)
        {
            if (GetZakazchikById(subj.Id) != null) return null;
            return rep.SaveEntity(ref subj);
        }

        public Guid? AddSeller(Seller subj)
        {
            if (GetSellerById(subj.Id) != null) return null;
            return rep.SaveEntity(ref subj);
        }

        public Guid? ModifyZakazchik(Zakazchik subj)
        {
            if (GetZakazchikById(subj.Id) == null) return null;
            return rep.SaveEntity(ref subj);
        }
        public Guid? ModifySeller(Seller subj)
        {
            if (GetSellerById(subj.Id) == null) return null;
            return rep.SaveEntity(ref subj);
        }
        public int RemoveZakazchik(ref Zakazchik subj)
        {
            var ent = subj;
            var ret = rep.RemoveEntity(ref ent);
            if (ret > 0) subj = null;
            return ret;
        }

        public int RemoveSeller(ref Seller subj)
        {
            var ent = subj;
            var ret = rep.RemoveEntity(ref ent);
            if (ret > 0) subj = null;
            return ret;
        }


    }
}
