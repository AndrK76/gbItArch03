using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndrK.ZavPostav.DomainModel;
using AndrK.ZavPostav.BusinessLogic2;

namespace TestApp
{
    public class SubjectTest
    {
        public SubjectTest(IRepository rep)
        {
            sp = new SubjectProcess(rep);
        }

        SubjectProcess sp;

        public void Run()
        {
            try
            {
                Console.WriteLine("\n\nРабота с субъектами");
                #region {Заказчики}
                {
                    Console.WriteLine("Заказчики:");
                    var zakList = sp.GetZakazchikList();
                    foreach (var zak in zakList)
                        Console.WriteLine(zak);
                    Console.WriteLine("\nПоиск заказчика по Id");
                    {
                        Guid[] arr = new Guid[3] { zakList[1].Id, Guid.NewGuid(), new Guid() };
                        foreach (var id in arr)
                            Console.WriteLine($"Id={id}\t=>{sp.GetZakazchikById(id)}");
                    }
                    Console.WriteLine("\nДобавление заказчиков");
                    {
                        Zakazchik[] arr = new Zakazchik[3]
                        {
                        zakList[0],
                        new Zakazchik { Id = new Guid(), Name = "Тестовый" },
                        new Zakazchik { Id = Guid.NewGuid(), Name = "Тестовый 2" },
                        };
                        foreach (var zak in arr)
                        {
                            Console.WriteLine($"Заказчик: {zak}");
                            Console.WriteLine($"\tРезультат: {sp.AddZakazchik(zak)}");
                        }
                        Console.WriteLine("Новый список:");
                        foreach (var zak in sp.GetZakazchikList())
                            Console.WriteLine(zak);
                    }
                    Console.WriteLine("\nОбновление заказчиков");
                    {
                        Zakazchik[] arr = new Zakazchik[3]
                        {
                        zakList[0],
                        new Zakazchik { Id = new Guid(), Name = "Тестовый" },
                        new Zakazchik { Id = Guid.NewGuid(), Name = "Тестовый 2" },
                        };
                        foreach (var zak in arr)
                        {
                            Console.WriteLine($"Заказчик: {zak}");
                            Console.WriteLine($"\tРезультат: {sp.ModifyZakazchik(zak)}");
                        }
                        Console.WriteLine("Новый список:");
                        foreach (var zak in sp.GetZakazchikList())
                            Console.WriteLine(zak);
                    }
                    Console.WriteLine("\nУдаление заказчиков");
                    {
                        Zakazchik[] arr = new Zakazchik[3]
                        {
                        zakList[0],
                        new Zakazchik { Id = new Guid(), Name = "Тестовый" },
                        new Zakazchik { Id = Guid.NewGuid(), Name = "Тестовый 2" },
                        };
                        for (int i = 0; i < arr.Length; i++)
                        {
                            var zak = arr[i];
                            Console.WriteLine($"Заказчик: {zak}");
                            Console.Write($"\tРезультат: {sp.RemoveZakazchik(ref zak)}");
                            Console.WriteLine($"\t[{zak}]");
                        }
                        Console.WriteLine("Новый список:");
                        foreach (var zak in sp.GetZakazchikList())
                            Console.WriteLine(zak);
                    }
                }
                #endregion
                #region {Продавцы}
                {
                    Console.WriteLine("Продавцы:");
                    var sellerList = sp.GetSellerList();
                    foreach (var seller in sellerList)
                        Console.WriteLine(seller);
                    Console.WriteLine("\nПоиск продавца по Id");
                    {
                        Guid[] arr = new Guid[3] { sellerList[1].Id, Guid.NewGuid(), new Guid() };
                        foreach (var id in arr)
                            Console.WriteLine($"Id={id}\t=>{sp.GetSellerById(id)}");
                    }
                    Console.WriteLine("\nДобавление продавцов");
                    {
                        Seller[] arr = new Seller[3]
                        {
                        sellerList[0],
                        new Seller { Id = new Guid(), Name = "Тестовый" },
                        new Seller { Id = Guid.NewGuid(), Name = "Тестовый 2" },
                        };
                        foreach (var seller in arr)
                        {
                            Console.WriteLine($"Продавец: {seller}");
                            Console.WriteLine($"\tРезультат: {sp.AddSeller(seller)}");
                        }
                        Console.WriteLine("Новый список:");
                        foreach (var seller in sp.GetSellerList())
                            Console.WriteLine(seller);
                    }
                    Console.WriteLine("\nОбновление продавцов");
                    {
                        Seller[] arr = new Seller[3]
                        {
                        sellerList[0],
                        new Seller { Id = new Guid(), Name = "Тестовый" },
                        new Seller { Id = Guid.NewGuid(), Name = "Тестовый 2" },
                        };
                        foreach (var seller in arr)
                        {
                            Console.WriteLine($"Продавец: {seller}");
                            Console.WriteLine($"\tРезультат: {sp.ModifySeller(seller)}");
                        }
                        Console.WriteLine("Новый список:");
                        foreach (var seller in sp.GetSellerList())
                            Console.WriteLine(seller);
                    }
                    Console.WriteLine("\nУдаление продавцов");
                    {
                        Seller[] arr = new Seller[3]
                        {
                        sellerList[0],
                        new Seller { Id = new Guid(), Name = "Тестовый" },
                        new Seller { Id = Guid.NewGuid(), Name = "Тестовый 2" },
                        };
                        for (int i = 0; i < arr.Length; i++)
                        {
                            var seller = arr[i];
                            Console.WriteLine($"продавец: {seller}");
                            Console.Write($"\tРезультат: {sp.RemoveSeller(ref seller)}");
                            Console.WriteLine($"\t[{seller}]");
                        }
                        Console.WriteLine("Новый список:");
                        foreach (var seller in sp.GetSellerList())
                            Console.WriteLine(seller);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Exception showEx = ex;
                while (showEx.InnerException != null)
                    showEx = showEx.InnerException;
                Console.WriteLine($"Ошибка во время выполнения теста:\n\t{showEx.Message}\n");
            }

        }

    }
}
