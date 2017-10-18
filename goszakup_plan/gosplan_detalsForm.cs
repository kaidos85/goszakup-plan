using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dal_sqlce;
using dal_sqlce.Context;
using DomainClasses;
using System.Linq;
using dal_sqlce.DTO;

namespace goszakup_plan
{
    public partial class gosplan_detalsForm : Form
    {
        ErrorProvider error = new ErrorProvider();
        IRepozitory<UserData> rep = null;
        UserData uData = null;
        bool negu;
        int year;
        
        public gosplan_detalsForm(IRepozitory<UserData> _rep, bool _negu, int id, int _year)
        {
            InitializeComponent();
            rep = _rep;
            year = _year;
            uData = rep.Find(id) ?? new UserData();
            this.negu = _negu;
        }
        

        private void gosplan_detalsForm_Load(object sender, EventArgs e)
        {
            textBoxX15.Text = Properties.Settings.Default.koeficent;
            if (!negu)
            {
                comboBox2.DataSource = rep.GetRepozitory<r_specific>().GetList().OrderBy(t => t.code).AsEnumerable()
                                    .Select(s => new ComboboxDTO { Id = s.id, Name = $"{s.code} {s.name_ru}" }).ToList();
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "Id";
            }
            bindcontrol();
            negu_enable();

            if (negu)
            {
                comboBox3.SelectedItem = null;
            }
        }

        private void bindcontrol()
        {
            LoadCombo();
            comboBox1.SelectedItem = uData.r_point_types;
            if (!negu)
            {
                textBoxX1.Text = uData.r_abp?.code;
                textBoxX2.Text = uData.r_prg?.code;
                textBoxX3.Text = uData.r_sprg?.code;
                comboBox2.Text = $"{uData.r_specific?.code} {uData.r_specific?.name_ru}";
                comboBox3.SelectedItem = uData.r_fsource;
                if (uData.r_specific == null)
                    comboBox2.SelectedIndex = -1;
            }
            comboBox4.SelectedItem = uData.r_subj_type;
            if (uData.r_enstru != null)
                LoadKtruBox(uData.r_enstru);
            textBoxX9.Text = uData.ExtraDescription_kz;
            textBoxX10.Text = uData.ExtraDescription_ru;
            comboBox5.SelectedItem = uData.r_trade_method;
            comboBox9.SelectedItem = uData.r_trade_method_just;
            textBoxX11.Text = uData.r_enstru?.edizm;
            textBoxX12.Text = uData.Count.ToString();
            textBoxX13.Text = uData.price.ToString();
            textBoxX14.Text = uData.Summ.ToString();
            textBoxX20.Text = uData.YearSum1.ToString();
            textBoxX16.Text = uData.YearSum2.ToString();
            textBoxX17.Text = uData.YearSum3.ToString();
            comboBox6.SelectedItem = uData.r_months;
            comboBox7.SelectedItem = uData.supplyDateKz;
            comboBox8.SelectedItem = uData.supplyDateRu;
            textBoxX18.Text = uData.kato.ToString();
            textBoxX19.Text = uData.Prepayment.ToString();

            textBoxX21.Text = uData.Deliver_kz;
            textBoxX22.Text = uData.Deliver_ru;
            if (uData.DisablePerson != null)
                checkBox1.Checked = uData.DisablePerson == 1;
            label27.Text = year.ToString();
        }

        private void LoadCombo()
        {
            comboBox1.DataSource = rep.GetRepozitory<r_point_types>().GetList().ToList();
            comboBox1.DisplayMember = "NAME_RU";
            comboBox1.ValueMember = "ID";
            
            comboBox3.DataSource = rep.GetRepozitory<r_fsource>().GetList().ToList();
            comboBox3.DisplayMember = "name_ru";
            comboBox3.ValueMember = "id";

            comboBox4.DataSource = rep.GetRepozitory<r_subj_type>().GetList().ToList();
            comboBox4.DisplayMember = "name_ru";
            comboBox4.ValueMember = "code";

            comboBox5.DataSource = rep.GetRepozitory<r_trade_method>().GetList().ToList();
            comboBox5.DisplayMember = "name_ru";
            comboBox5.ValueMember = "code";
            
            comboBox6.DataSource = rep.GetRepozitory<r_months>().GetList().ToList();
            comboBox6.DisplayMember = "name_ru";
            comboBox6.ValueMember = "code";
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (checkControls(negu) & opgz_check() & (CheckPrg()))
            {
                ToData();
                rep.InsertOrUpdate(uData);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBoxX12_TextChanged(object sender, EventArgs e)
        {
            paschet();
        }

        private void textBoxX13_TextChanged(object sender, EventArgs e)
        {
            paschet();
        }

        private void paschet()
        {
            decimal colich;
            decimal cena;
            decimal summa;
            decimal koeficent;
            if (decimal.TryParse(textBoxX12.Text, out colich) & decimal.TryParse(textBoxX13.Text, out cena))
            {
                summa = Math.Round(colich * cena, 2);
                textBoxX14.Text = summa.ToString();
                if(decimal.TryParse(textBoxX15.Text, out koeficent))
                {
                    decimal summa2 = Math.Round(summa * koeficent, 2);
                    decimal summa3 = Math.Round(summa2 * koeficent, 2);
                    textBoxX20.Text = summa.ToString();
                    textBoxX16.Text = summa2.ToString();
                    textBoxX17.Text = summa3.ToString();
                } 
            }            
       }

        private void ToData()
        {
            var abp = rep.GetRepozitory<r_abp>().GetListWhere(a => a.code == textBoxX1.Text).SingleOrDefault()??null;
            var prg = abp?.r_prg?.SingleOrDefault(a => a.code == textBoxX2.Text) ?? null;
            r_sprg sprg = null;
            if(abp != null & prg != null)
            {
                long abp_prg = TryParseLong(abp.code + prg.code);
                sprg = rep.GetRepozitory<r_sprg>().GetListWhere(a => a.abp_prg == abp_prg & a.code == textBoxX3.Text).SingleOrDefault() ?? null;
            }                
            uData.Year = year;
            uData.r_point_types = comboBox1.SelectedItem as r_point_types;
            uData.r_abp = abp;
            uData.r_prg = prg;
            uData.r_sprg = sprg;
            uData.r_fsource = comboBox3.SelectedItem as r_fsource;
            uData.r_months = comboBox6.SelectedItem as r_months;
            uData.r_trade_method = comboBox5.SelectedItem as r_trade_method;
            uData.r_trade_method_just = comboBox9.SelectedItem as r_trade_method_just;
            uData.Specific = !negu ? TryParseLong(comboBox2.SelectedValue.ToString()) : 0;
            uData.r_subj_type = comboBox4.SelectedItem as r_subj_type;
            uData.ExtraDescription_kz = textBoxX9.Text;
            uData.ExtraDescription_ru = textBoxX10.Text;

            uData.Count = TryParseInt(textBoxX12.Text);
            uData.price = TryParseDecimal(textBoxX13.Text);
            uData.Summ = TryParseDecimal(textBoxX14.Text);
            uData.YearSum1 = TryParseDecimal(textBoxX20.Text);
            uData.YearSum2 = TryParseDecimal(textBoxX16.Text);
            uData.YearSum3 = TryParseDecimal(textBoxX17.Text);

            uData.supplyDateKz = comboBox7.Text;
            uData.supplyDateRu = comboBox8.Text;
            uData.Deliver_kz = textBoxX21.Text;
            uData.Deliver_ru = textBoxX22.Text;
            uData.kato = TryParseLong(textBoxX18.Text);
            uData.Prepayment = TryParseDecimal(textBoxX19.Text);
            uData.DisablePerson = checkBox1.Checked?1:0;          
        }

        private void textBoxX15_TextChanged(object sender, EventArgs e)
        {
            paschet();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            ktru_findForm find = new ktru_findForm(rep.GetRepozitory<r_enstru>());
            find.ktru = comboBox4.SelectedValue?.ToString();
            if (find.ShowDialog() == DialogResult.OK)
            {
                uData.r_enstru = find.Enstru;
                comboBox4.SelectedValue = find.ktru;
                LoadKtruBox(find.Enstru);
            }
        }

        private void LoadKtruBox(r_enstru enstr)
        {
            textBoxX4.Text = enstr.code;
            textBoxX5.Text = enstr.name_kz;
            textBoxX6.Text = enstr.name_ru;
            textBoxX7.Text = enstr.desc_kz;
            textBoxX8.Text = enstr.desc_ru;
            textBoxX11.Text = enstr.edizm;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == "Услуга" | comboBox4.Text == "Работа")
            {
                textBoxX12.Text = "1";
                textBoxX12.Enabled = false;
            }
            else
                textBoxX12.Enabled = true;
        }

        private bool checkControls(bool negu)
        {
            bool flag = true;
            Control[] ctrl;
            if(negu)
                ctrl = new Control[] { comboBox1, comboBox4, comboBox5, comboBox6, comboBox7, comboBox8, textBoxX4, textBoxX12, textBoxX13, textBoxX15, textBoxX18, textBoxX19, textBoxX21, textBoxX22};
            else
                ctrl = new Control[] { comboBox1, comboBox4, comboBox5, comboBox6, comboBox7, comboBox8, textBoxX4, textBoxX12, textBoxX13, textBoxX15, textBoxX18, textBoxX19, textBoxX1, textBoxX2, textBoxX3, comboBox2, comboBox3, textBoxX21, textBoxX22};
            for (int i = 0; i < ctrl.Length; i++)
            {
                if (ctrl[i].Text == "")
                {
                    error.SetError(ctrl[i], "Заполните пустую ячейку");
                    ctrl[i].BackColor = Color.Yellow;
                    flag = false;
                }
                else
                {
                    error.SetError(ctrl[i], String.Empty);
                    ctrl[i].BackColor = Color.White;
                }
            }
            return flag;
        }

        private void negu_enable()
        {
            Control[] ctrl = new Control[] { textBoxX1, textBoxX2, textBoxX3, comboBox2, comboBox3 };
            for (int i = 0; i < ctrl.Length; i++)
            {
                if (negu)
                {
                    ctrl[i].Enabled = false;
                }
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            kato_findForm find = new kato_findForm();
            if (find.ShowDialog() == DialogResult.OK)
            {
                if (find.bs.Count > 0)
                {
                    var drv = (ref_kato)find.bs.Current;
                    textBoxX18.Text = drv.code.ToString();                    
                }
            }
        }

        private void label30_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void baloon_metod(string text_baloon, Control ctrl)
        {
            DevComponents.DotNetBar.Balloon baloon = new DevComponents.DotNetBar.Balloon();
            baloon.Text = text_baloon;
            baloon.Style = DevComponents.DotNetBar.eBallonStyle.Alert;
            baloon.AlertAnimation = DevComponents.DotNetBar.eAlertAnimation.TopToBottom;
            baloon.AutoResize();
            baloon.AutoClose = true;
            baloon.AutoCloseTimeOut = 5;
            baloon.Owner = this;
            baloon.Show(ctrl, false);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedValue == null)
                return;
            var id = comboBox5.SelectedValue.ToString();
            comboBox9.DataSource = rep.GetRepozitory<r_trade_method_just>().GetListWhere(t=>t.tm_code== id).ToList();
            comboBox9.DisplayMember = "name_ru";
            comboBox9.ValueMember = "code";
            comboBox9.SelectedIndex = -1;
        }

        private bool opgz_check()
        {
            var source = comboBox9.DataSource as List<r_trade_method_just>;
            if ((source.Count == 0) & (comboBox9.Text == ""))
            {
                error.SetError(comboBox9, "");
                return true;
            }
            else if ((source.Count != 0) & (comboBox9.Text != ""))
            {
                error.SetError(comboBox9, "");
                return true;
            }
            else
            {
                error.SetError(comboBox9, "Заполните пустую ячейку");
                return false;
            }
        }

        private bool CheckPrg()
        {
            bool result = true;
            var abp = rep.GetRepozitory<r_abp>().GetListWhere(a => a.code == textBoxX1.Text).SingleOrDefault() ?? null;
            var prg = abp?.r_prg?.SingleOrDefault(a => a.code == textBoxX2.Text) ?? null;
            r_sprg sprg = null;
            if (abp != null & prg != null)
            {
                long abp_prg = TryParseLong(abp.code + prg.code);
                sprg = rep.GetRepozitory<r_sprg>().GetListWhere(a => a.abp_prg == abp_prg & a.code == textBoxX3.Text).SingleOrDefault() ?? null;
            }

            result = ChechPrgControl(abp, textBoxX1, "Нет Администратора") &
                     ChechPrgControl(prg, textBoxX2, "Неправильная программа") &
                     ChechPrgControl(sprg, textBoxX3, "Неправильная подпрограмма");            
            return result;
        }

        bool ChechPrgControl(object obj, Control ctrl, string text)
        {
            bool res = true;
            if (negu)
                return true;
            if(obj == null)
            {
                error.SetError(ctrl, text);
                ctrl.BackColor = Color.Yellow;
                res = false;
            }
            else
            {
                error.SetError(ctrl, "");
                ctrl.BackColor = Color.White;
            }
            return res;
        }

        long TryParseLong(string input)
        {
            long temp = 0;
            long.TryParse(input, out temp);
            return temp;
        }

        decimal TryParseDecimal(string input)
        {
            decimal temp = 0;
            decimal.TryParse(input, out temp);
            return temp;
        }

        int TryParseInt(string input)
        {
            int temp = 0;
            int.TryParse(input, out temp);
            return temp;
        }
    }
}
