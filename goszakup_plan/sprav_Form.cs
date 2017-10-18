using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace goszakup_plan
{
    public partial class sprav_Form : Form
    {
        public sprav_Form()
        {
            InitializeComponent();
        }

        private bool OnlyOneForm(string form)
        {
            if (this.MdiChildren.Length == 0)
            {
                return true;
            }
            else
            {
                bool open = false;
                for (int w = 0; w < this.MdiChildren.Length; w++)
                {
                    if (this.MdiChildren[w].Text.Equals(form))
                    {
                        this.MdiChildren[w].Activate();
                        open = true;
                    }
                }
                if (!open)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (OnlyOneForm("KATO"))
            {
                katoForm form = new katoForm();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            if (OnlyOneForm("КТРУ товары"))
            {
                ktru_tovar_Form form = new ktru_tovar_Form();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            if (OnlyOneForm("КТРУ услуги"))
            {
                ktru_uslugi_Form form = new ktru_uslugi_Form();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            if (OnlyOneForm("КТРУ работы"))
            {
                ktru_rabot_Form form = new ktru_rabot_Form();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }        

        private void buttonItem11_Click(object sender, EventArgs e)
        {
            if (OnlyOneForm("ЭКРБ специфики"))
            {
                ekrb_Form form = new ekrb_Form();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            if (OnlyOneForm("ОПГЗ"))
            {
                opgz_Form form = new opgz_Form();
                form.Text = "ОПГЗ";
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }
    }
}
