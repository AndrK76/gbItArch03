using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.DomainModel;

namespace AndrK.ZavPostav.MokeRepository
{
    public static class Store
    {
        static Store()
        {
            Zakazchiks = new List<Zakazchik>
            {
                new Zakazchik{Id=Guid.NewGuid(), Name="Заказчик 1"},
                new Zakazchik{Id=Guid.NewGuid(), Name="Заказчик 2"},
            };

            Sellers = new List<Seller>
            {
                new Seller{Id=Guid.NewGuid(), Name="Продавец 1"},
                new Seller{Id=Guid.NewGuid(), Name="Продавец 2"},
            };

        }

        public static List<Zakazchik> Zakazchiks { get; private set; }
        public static List<Seller> Sellers { get; private set; }
    }
}
