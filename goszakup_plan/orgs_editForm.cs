using dal_sqlce.Context;
using DomainClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace goszakup_plan
{
    public partial class orgs_editForm : Form
    {

        IRepozitory<Orgs> rep = new EFRepozitory<Orgs>();
        Orgs org = null;
        public orgs_editForm()
        {
            InitializeComponent();
            org = rep.GetList().FirstOrDefault()??new Orgs();
        }        

        private void orgs_editForm_Load(object sender, EventArgs e)
        {
            textBoxX1.Text = org.name_ru;
            textBoxX2.Text = org.name_kz;
            textBoxX3.Text = "";
            textBoxX4.Text = org.BIN.ToString();
            textBoxX5.Text = org.KodGU.ToString();
            comboBox1.DataSource = rep.GetRepozitory<r_bud_type>().GetList().ToList();
            comboBox1.DisplayMember = "name_ru";
            comboBox1.SelectedItem = org.r_bud_type;
            checkBoxX1.Checked = org.Negu??false;
            negu_metod();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            org.name_ru = textBoxX1.Text;
            org.name_kz = textBoxX2.Text;            
            org.BIN = TryParseLong(textBoxX4.Text);
            org.KodGU = TryParseLong(textBoxX5.Text);
            org.r_bud_type = (r_bud_type)comboBox1.SelectedItem;
            org.Negu = checkBoxX1.Checked;
            rep.InsertOrUpdate(org);
            this.DialogResult = DialogResult.OK;
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            negu_metod();
        }

        long TryParseLong(string input)
        {
            long temp = 0;
            long.TryParse(input, out temp);
            return temp;
        }

        private void negu_metod()
        {
            if (checkBoxX1.Checked)
            {
                textBoxX5.Enabled = false;
                comboBox1.Enabled = false;
                textBoxX5.Text = "";
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                textBoxX5.Enabled = true;
                comboBox1.Enabled = true;
            }
        }        
    }
}
