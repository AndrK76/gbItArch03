using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AndrK.ZavPostav.DomainModel.IRepository rep = new AndrK.ZavPostav.MsSqlRepository.Repository();
            Console.WriteLine("Работа с клиентами");
            SubjectTest st = new SubjectTest(rep);
            st.Run();

            Console.ReadLine();
        }
    }
}
