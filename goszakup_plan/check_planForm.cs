using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dal_sqlce;

namespace goszakup_plan
{
    public partial class check_planForm : Form
    {
        BindingSource bs = new BindingSource();
        BindingSource bs_orgs = new BindingSource();
        string god;
        public check_planForm(BindingSource _bs, BindingSource _bs_orgs, string _god)
        {
            InitializeComponent();
            this.bs = _bs;
            this.god = _god;
            this.bs_orgs = _bs_orgs;
        }

        private void check_planForm_Load(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = bs;
            dataGridViewX1.Columns["Id"].Visible = false;            
            //dataGridViewX1.Columns["god"].Visible = false;
            for (int i = 1; i < dataGridViewX1.Columns.Count; i++)
            {
                dataGridViewX1.Columns[i].ReadOnly = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!check_columns())
            {
                MessageBox.Show("Нечего не выбрано");
                return;
            } 
        }

        private bool check_columns()
        {
            bool check = false;
            for (int i = 0; i < dataGridViewX1.RowCount; i++)
            {
                if (Convert.ToBoolean(dataGridViewX1.Rows[i].Cells["V"].Value))
                {
                    check = true;
                }
            }                
            return check;
        }
          
        private void filter_bs()
        {
            bs.Filter = "[Способ_закупки] LIKE '%" + comboBox1.Text + "%' AND [Планируемый_срок] LIKE '%" + comboBox2.Text + "%' AND [Вид_предмета_закупок] LIKE '%" + comboBox3.Text + "%'";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter_bs();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter_bs();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter_bs();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bs.Filter = "god = '" + god + "'";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount > 0)
            {
                for (int i = 0; i < dataGridViewX1.RowCount; i++)
                {
                    dataGridViewX1.Rows[i].Cells["V"].Value = true;
                }
            }             
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount > 0)
            {
                for (int i = 0; i < dataGridViewX1.RowCount; i++)
                {
                    dataGridViewX1.Rows[i].Cells["V"].Value = false;
                }
            }  
        }

        private void check_planForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            bs.Filter = "god = '" + god + "'";
        }
    }
}
