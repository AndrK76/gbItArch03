using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AndrK.ZavPostav.TestService
{
    /// <summary>
    /// Сводное описание для FirstService
    /// </summary>
    [WebService(Namespace = "http://TestService.ZavPostav.AndrK/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class FirstService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Привет всем!";
        }

        [WebMethod]
        public int Add(List<int> listInt)
        {
            int result = 0;
            for (int i = 0; i < listInt.Count; i++)
            {
                result = result + listInt[i];
            }

            return result;
        }
    }
}
