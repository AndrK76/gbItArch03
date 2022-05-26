using AndrK.ZavPostav.BusinessLogic;
using AndrK.ZavPostav.DAL_MSSql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            try
            {
                bl = new BusinessLogic(new Repository());
                bl.InitSubjects();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        BusinessLogic bl = null;
        ZavkaZakupProcess zzp = null;


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                zzp = bl.CreateZavkaZakupProcess(bl.Zakazchiks[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var spec = zzp.ZavkaList[0].Oborudovanie.Keys.First().Specifications[0];
                //var spec = zzp.NewSpec(zzp.ZavkaList[0].Oborudovanie.Keys.First());
                //
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    if (dlg.ShowDialog()==DialogResult.OK)
                    {
                        spec.Content.FileName = Path.GetFileName(dlg.FileName);
                        spec.Content.Content = File.ReadAllBytes(dlg.FileName);
                    }
                }
                spec.Name = $"Спецификация от {DateTime.Now.ToString()}";
                zzp.SaveSpecification(spec);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }
}
