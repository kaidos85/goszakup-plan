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
    public partial class kato_findForm : Form
    {
        public BindingSource bs = new BindingSource();  

        public kato_findForm()
        {
            InitializeComponent();
        }

        private void ktru_findForm_Load(object sender, EventArgs e)
        {         
            comboBox2.SelectedIndex = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rep = new EFRepozitory<ref_kato>();
            var list = rep.GetList().AsEnumerable().Where(c => c.name_ru.Contains(textBox1.Text)).ToList();
            bs.DataSource = list;
            dataGridViewX1.DataSource = bs;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }        
    }
}
