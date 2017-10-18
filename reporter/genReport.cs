using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using FastReport;
using dal_sqlce;


namespace reporter
{
    public class genReport
    {
        public genReport()
        {
            
        }

        //public void rep1(BindingSource bs, decimal label_summa, string god)
        //{
        //    //dal database = new workDB().plan();
        //    //database.Zapros_column1 = " WHERE god LIKE '";
        //    //database.Zapros_column2 = "'";
        //    //DataTable tbl_plan = database.tableLoadTable(god);
        //    //DataRowView drv = (DataRowView)bs.Current;
        //    //FastReport.Report rep = new Report();
        //    //rep.Load(Application.StartupPath + @"\Reports\gz.frx");
        //    //rep.RegisterData(tbl_plan, "plan_gz");            
        //    //(rep.FindObject("Text_orgsname") as TextObject).Text = drv["Наименование заказчика_рус"].ToString();
        //    //(rep.FindObject("Text_god") as TextObject).Text = god;
        //    //(rep.FindObject("Text_punkt") as TextObject).Text = tbl_plan.Rows.Count.ToString();
        //    //(rep.FindObject("Text_summa") as TextObject).Text = label_summa.ToString("# ### ##0.00") + " тг";
        //    //rep.Show();
        //}       

    }
}
