using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dal_sqlce;
using DomainClasses;
using dal_sqlce.Context;
using System.Linq;

namespace goszakup_plan
{
    public partial class katoForm : Form
    {
        IRepozitory<ref_kato> rep = new EFRepozitory<ref_kato>();
        BindingSource bs = new BindingSource();

        public katoForm()
        {
            InitializeComponent();
        }

        private void katoForm_Load(object sender, EventArgs e)
        {
            bs.DataSource = rep.GetList().ToList();
            dataGridViewX1.DataSource = bs;
            bindingNavigatorEx1.BindingSource = bs;
            //dataGridViewX1.Columns["id"].Visible = false;
        }
    }
}
