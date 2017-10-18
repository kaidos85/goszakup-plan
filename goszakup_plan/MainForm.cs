using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dal_sqlce;
using reporter;
using dal_sqlce.DTO;
using dal_sqlce.Context;
using DomainClasses;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace goszakup_plan
{
    public partial class MainForm : Form
    {

        IRepozitory<UserData> rep = new EFRepozitory<UserData>();
        //BindingList<PlanDTO> bs = new BindingList<PlanDTO>();
        BindingSource bs_1 = new BindingSource();        
        bool negu = false;
        string name_orgs;
        decimal label_summ;        

        public MainForm()
        {
            InitializeComponent();
            int currentYear = int.Parse(Properties.Settings.Default.god);
            LoadData(currentYear);
            metod_negu();
        }

        private void metod_negu()
        {
            var org = new EFRepozitory<Orgs>().GetList().FirstOrDefault();
            negu = org?.Negu ?? false;
            name_orgs = org?.name_ru;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "План госзакупок - " + name_orgs;
            //bs_1.DataSource = bs;            
            dataGridViewX1.DataSource = bs_1;            
            //bs.Filter = "god LIKE '" + comboBox1.Text + "'";
            visible_columns();
            comboBox1.ComboBoxEx.DataSource = Properties.Settings.Default.gody.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            comboBox1.Text = Properties.Settings.Default.god;
            unVisibleColumns();
            //CompactNo(50);
            //dataGridViewX1.Columns["Вид предмета закупок"].Width = 50;

        }

        private void visible_columns()
        {            
            if (negu)
            {
                dataGridViewX1.Columns["Администратор_бюджетной_программы"].Visible = false;
                dataGridViewX1.Columns["Программа"].Visible = false;
                dataGridViewX1.Columns["Подпрограмма"].Visible = false;
                dataGridViewX1.Columns["Специфика"].Visible = false;
                dataGridViewX1.Columns["Источник_финансирования"].Visible = false;
            }
            else
            {
                dataGridViewX1.Columns["Администратор_бюджетной_программы"].Visible = true;
                dataGridViewX1.Columns["Программа"].Visible = true;
                dataGridViewX1.Columns["Подпрограмма"].Visible = true;
                dataGridViewX1.Columns["Специфика"].Visible = true;
                dataGridViewX1.Columns["Источник_финансирования"].Visible = true;
            }
        }

        private void unVisibleColumns()
        {
            string[] cols = { "Id", "abp", "stru", "monthId", "Year", "DisablePerson",
                               "TradeMethod", "tradeMethodJust", "Program", "SubProgram"
                                , "Specific", "pointTypeCode", "FinSource"};
            foreach (var item in cols)
            {
                dataGridViewX1.Columns[item].Visible = false;
            }
        }

        private void LoadData(int year)
        {
            var includes = new[] { "r_abp", "r_enstru", "r_fsource", "r_months", "r_point_types",
                "r_prg", "r_specific", "r_sprg", "r_subj_type", "r_trade_method", "r_trade_method_just" };
            var data = rep.GetListWhere(includes, u => u.Year == year).AsEnumerable()
                .Select(u => MapToPlan(u)).ToList();
            bs_1.DataSource = data;
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            new sprav_Form().ShowDialog();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            avtozapolnenie(checkBoxItem1.Checked);
            int selectedYear = int.Parse(comboBox1.Text);
            gosplan_detalsForm form = new gosplan_detalsForm(rep, negu, 0, selectedYear);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData(selectedYear);
            }         
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            EditRow();
        }

        private void EditRow()
        {
            if (dataGridViewX1.CurrentRow == null)
                return;
            int selectedYear = int.Parse(comboBox1.Text);
            int id = (int)(dataGridViewX1.CurrentRow.DataBoundItem as PlanDTO).Id;
            gosplan_detalsForm form = new gosplan_detalsForm(rep, negu, id, selectedYear);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData(selectedYear);
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.CurrentRow == null)
                return;
            if (MessageBox.Show("Удаление записи", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var ud = rep.Find((int)(dataGridViewX1.CurrentRow.DataBoundItem as PlanDTO).Id);
                if(ud!=null)
                    rep.Delete(ud);
                LoadData(int.Parse(comboBox1.Text));       
            }            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(int.Parse(comboBox1.Text));
        }

        private void avtozapolnenie(bool check)
        {
            if (check & dataGridViewX1.RowCount > 0)
            {
                //drw_new["Тип пункта плана"] = drw_old["Тип пункта плана"];
                //drw_new["Администратор бюджетной программы"] = drw_old["Администратор бюджетной программы"];
                //drw_new["Программа"] = drw_old["Программа"];
                //drw_new["Подпрограмма"] = drw_old["Подпрограмма"];
                //drw_new["Источник финансирования"] = drw_old["Источник финансирования"];
                //drw_new["Способ закупок"] = drw_old["Способ закупок"];
                //drw_new["КАТО"] = drw_old["КАТО"];
                //drw_new["Аванс"] = drw_old["Аванс"];
                //drw_new["МестоПоставки_каз"] = drw_old["МестоПоставки_каз"];
                //drw_new["МестоПоставки_рус"] = drw_old["МестоПоставки_рус"];
            }
            //else
                
        }

        private void dataGridViewX1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            label_summa();
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridViewX1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridViewX1.Rows[index].HeaderCell.Value = indexStr;
        }

        private void label_summa()
        {
            decimal label_summa = 0, temp;
            if (dataGridViewX1.RowCount > 0)
            {
                for (int i = 0; i < dataGridViewX1.RowCount; i++)
                {
                    if (decimal.TryParse(dataGridViewX1.Rows[i].Cells["Сумма"].Value.ToString(), out temp))
                    {
                        label_summa += temp;
                    }
                }
            }
            labelItem2.Text = "Итого сумма: " + Math.Round(label_summa, 2).ToString();
            label_summ = Math.Round(label_summa, 2);            
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount > 0)
            {
                var save = new SaveFileDialog();
                save.Filter = "XML File|*.xml";
                if(save.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Akt));
                    var org = new EFRepozitory<Orgs>().GetList().FirstOrDefault();
                    Akt akt = GetAkt(org);
                    // получаем поток, куда будем записывать сериализованный объект
                    using (var writer = new MemoryStream())
                    {
                        formatter.Serialize(writer, akt);
                        var text = Encoding.UTF8.GetString(writer.ToArray());
                        using (var stw = new StreamWriter(save.FileName, false, Encoding.UTF8))
                        {
                            var mass = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None );
                            foreach (var item in mass)
                            {
                                if (!item.Contains("yearPlans1"))
                                    stw.WriteLine(item);
                            }
                            stw.Close();
                        }
                    }
                }
            }
        }

        private Akt GetAkt(Orgs org)
        {
            int selectedYear = int.Parse(comboBox1.Text);
            return new Akt()
            {
                bin = org?.BIN?.ToString(),
                finYear = comboBox1.Text,
                guCodes = new AktGuCodes
                {
                    abpCode = org?.KodGU?.ToString().Substring(0, 3),
                    budTypeCode = org?.r_bud_type.code,
                    code = org?.KodGU.ToString()
                },
                orgNameKz = org?.name_kz?.ToString(),
                orgNameRu = org?.name_ru?.ToString(),
                yearPlans1 = rep.GetListWhere(r => r.Year == selectedYear).AsEnumerable()
                            .Select(p => MapToYearPlan(p, (int)org?.KodGU)).ToList()
            };
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {            
            new load_spravForm().ShowDialog();            
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount > 0)
            {
                //new check_planForm(bs_1, null, comboBox1.Text).ShowDialog();
            }
        }

        private void dataGridViewX1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditRow();
        }       

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }
        
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonItem15_Click(object sender, EventArgs e)
        {
            new settings_Form().ShowDialog();
        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount > 0)
            {
                var excel = new ExportToExcel(bs_1.DataSource as List<PlanDTO>);
                var save = new SaveFileDialog();
                save.Filter = "Excel files|*.xlsx";
                if(save.ShowDialog()== DialogResult.OK)
                    excel.Export(comboBox1.Text, save.FileName);
            }
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount > 0)
            {
                //new genReport().rep1(bs_orgs, label_summ, comboBox1.Text);
            }
        }

        private void addtocopy_Click(object sender, EventArgs e)
        {
            //DataRowView drv1 = (DataRowView)bs.Current;
            //bs.AddNew();
            //DataRowView drv2 = (DataRowView)bs.Current;
            //for (int i = 1; i < drv1.Row.ItemArray.Length; i++)
            //    drv2[i] = drv1[i];
            //bs.EndEdit();
            //database.updTable();
        }

        private void buttonItem18_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Будут удалены все записи. Вы Уверены?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var allData = rep.GetList().ToList();
                foreach (var item in allData)
                {
                    rep.Delete(item);
                }
                LoadData(int.Parse(comboBox1.Text));
            }  
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\Goszakup plan.chm");
        }
                
        private void CompactNo(int width)
        {
            var col = new List<string>() {
                "Тип_пункта_плана",
                "Администратор_бюджетной_программы",
                "Программа",
                "Подпрограмма",
                "Специфика",
                "Прогнозная_сумма1",
                "Прогнозная_сумма2",
                "Прогнозная_сумма3"
            };
            foreach (var item in col)
            {
                dataGridViewX1.Columns[item].Width = width;
            }
            
        }

        private void checkBoxItem2_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            if (checkBoxItem2.Checked)
                CompactNo(50);
            else
                CompactNo(100);
        }

        PlanDTO MapToPlan(UserData uData)
        {
            var plan = new PlanDTO();
            plan.Id = uData.Id;
            plan.Администратор_бюджетной_программы = uData.r_abp?.code;
            plan.Источник_финансирования = uData.r_fsource?.name_ru;
            plan.Вид_предмета_закупок = uData.r_subj_type?.name_ru;
            plan.Дополнительная_каз = uData.ExtraDescription_kz;
            plan.Дополнительная_рус = uData.ExtraDescription_ru;
            plan.Краткая_каз = uData.r_enstru?.desc_kz;
            plan.Краткая_рус = uData.r_enstru?.desc_ru;
            plan.КТРУ = uData.r_enstru?.code;
            plan.Наименование_каз = uData.r_enstru?.name_kz;
            plan.Наименование_рус = uData.r_enstru?.name_ru;
            plan.Единица_измерения = uData.r_enstru?.edizm;
            plan.Количество = uData.Count;
            plan.Цена = uData.price;
            plan.Сумма = uData.Summ??0;
            plan.Прогнозная_сумма1 = uData.YearSum1;
            plan.Прогнозная_сумма2 = uData.YearSum2;
            plan.Прогнозная_сумма3 = uData.YearSum3;
            plan.Аванс = uData.Prepayment;
            plan.Тип_пункта_плана = uData.r_point_types?.NAME_RU;
            plan.Программа = uData.r_prg?.code;
            plan.Подпрограмма = uData.r_sprg?.code;
            plan.Планируемый_срок = uData.r_months?.name_ru;
            plan.СрокПоставки_каз = uData.supplyDateKz;
            plan.СрокПоставки_рус = uData.supplyDateRu;
            plan.МестоПоставки_каз = uData.Deliver_kz;
            plan.МестоПоставки_рус = uData.Deliver_ru;
            plan.Обоснование = uData.r_trade_method_just?.name_ru;
            plan.Способ_закупки = uData.r_trade_method?.name_ru;
            plan.Специфика = uData.r_specific?.code;
            plan.КАТО = uData.kato;
            plan.Сумма = uData.Summ??0;
            plan.ПризнакПоставщика = uData.DisablePerson.ToString();
            return plan;
        }

        yearPlans MapToYearPlan(UserData uData, int guCode)
        {
            var yearPlan = new yearPlans();
            yearPlan.abpId = (int)uData.r_abp?.id;
            yearPlan.id = (int)uData.Id;
            yearPlan.aditDeliveryKz = uData.Deliver_kz;
            yearPlan.aditDeliveryRu = uData.Deliver_ru;
            yearPlan.count = uData.Count;
            yearPlan.descriptionKz = uData.r_enstru?.desc_kz;
            yearPlan.descriptionRu = uData.r_enstru?.desc_ru;
            yearPlan.disablePerson = (int)uData.DisablePerson;
            yearPlan.enstruId = (int)uData.r_enstru?.id;
            yearPlan.extraDescriptionKz = uData.ExtraDescription_kz;
            yearPlan.extraDescriptionRu = uData.ExtraDescription_ru;
            yearPlan.finSourceId = (int)uData.r_fsource?.id;
            yearPlan.firdYearSum = uData.YearSum3;
            yearPlan.firstYearSum = uData.YearSum1;
            yearPlan.guCode = guCode;
            yearPlan.kato = (int)uData.kato;
            yearPlan.mkeiId = uData.r_enstru?.edizm;
            yearPlan.monthCode = (int)uData.r_months?.code;
            yearPlan.pointTypeCode = (int)uData.r_point_types?.ID;
            yearPlan.prepayment = uData.Prepayment;
            yearPlan.price = uData.price;
            yearPlan.programId = (int)uData.r_prg?.id;
            yearPlan.secondYearSum = uData.YearSum3;
            yearPlan.specificId = (int)uData.r_specific?.id;
            yearPlan.subjectTypeCode = uData.r_subj_type?.code;
            yearPlan.subProgramId = (int)uData.r_sprg?.id;
            yearPlan.supplyDateKz = uData.supplyDateKz;
            yearPlan.supplyDateRu = uData.supplyDateRu;
            yearPlan.tradeMethodId = (int)uData.r_trade_method?.code;
            yearPlan.tradeMethodJistiId = (int)uData.r_trade_method_just?.id;
            return yearPlan;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int selectedYear = int.Parse(comboBox1.Text);
            LoadData(selectedYear);
        }

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            var form = new orgs_editForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                metod_negu();
                this.Text = "План госзакупок - " + name_orgs;
                visible_columns();
            }
        }
    }
}
