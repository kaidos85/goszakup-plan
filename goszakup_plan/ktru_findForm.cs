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
    public partial class ktru_findForm : Form
    {
        IRepozitory<r_enstru> rep = null;
        public string ktru;
        public r_enstru Enstru { get; set; }

        public ktru_findForm(IRepozitory<r_enstru> _rep)
        {
            InitializeComponent();
            rep = _rep;
        }

        private void ktru_findForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = rep.GetRepozitory<r_subj_type>().GetList().ToList();
            comboBox1.DisplayMember = "name_ru";
            comboBox1.ValueMember = "code";
            if(ktru != null)
                comboBox1.SelectedValue = ktru;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindGrid(textBox1.Text, comboBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridViewX1.CurrentRow != null)
            {
                var ens = dataGridViewX1.CurrentRow.DataBoundItem as r_enstru;
                Enstru = rep.GetListWhere(n => n.id == ens.id).SingleOrDefault();
                ktru = comboBox1.SelectedValue.ToString();
                this.DialogResult = DialogResult.OK;
            }
        }   
        
        private void FindGrid(string searchText, string column)
        {
            List<r_enstru> list = new List<r_enstru>();
            var typeSubj = comboBox1.SelectedValue.ToString();
            var where = $"grs = '{comboBox1.SelectedValue.ToString()}' AND {column} LIKE '%{searchText}%'";
            list = rep.Context.Database.SqlQuery<r_enstru>($"select * from r_enstru where {where}").ToList();
            dataGridViewX1.DataSource = list;
            dataGridViewX1.Columns["id"].Visible = false;
            dataGridViewX1.Columns["code"].Visible = false;
            dataGridViewX1.Columns["name_kz"].Width = 300;
            dataGridViewX1.Columns["name_ru"].Width = 300;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dataGridViewX1.DataSource = new List<r_enstru>();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}


