using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsClient.FirstServiceReference;

namespace WebFormsClient
{
    public partial class TestForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FirstServiceSoapClient cli = new FirstServiceSoapClient();

            ArrayOfInt vals2 = new ArrayOfInt { 5, 6, 7 };
            Label1.Text = cli.Add(vals2).ToString();
        }
    }
}