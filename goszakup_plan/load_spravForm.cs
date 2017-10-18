using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using dal_sqlce;

namespace goszakup_plan
{
    public partial class load_spravForm : DevComponents.DotNetBar.Office2007Form
    {  
        int procenaj = 0;

        public load_spravForm()
        {
            InitializeComponent();            
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            OpenFileDialog f1 = new OpenFileDialog();
            f1.Filter = "Справочники *.spr|*.spr";

            if (f1.ShowDialog() == DialogResult.OK)
            {
                textBoxX1.Text = f1.FileName;
                buttonX2.Enabled = true;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {                
                progressBar1.Visible = true;
                textBoxX1.Enabled = false;
                buttonX1.Enabled = false;
                buttonX2.Enabled = false;
                // Start the asynchronous operation.                
                backgroundWorker1.RunWorkerAsync();                
            }
        }

        private void open_load(BackgroundWorker worker)
        {            
            //string[] table_sprav = new string[] {"Като", "ЭКРБ", "ктру_товары", "ктру_услуги", "ктру_работы", "opgz"};
            //for (int i = 0; i < table_sprav.Length; i++)
            //{
            //    tableLoadTable(table_sprav[i].ToString());
            //    save_table_tobase(table_sprav[i].ToString(), tableLoadTable(table_sprav[i].ToString()), worker);
            //}
            

            //// Specify null destination connection string for in-place compaction
            ////
            //engine.Compact(null);
        }

        public DataTable tableLoadTable(string table_sprav)
        {
            DataTable tbl = new DataTable();            
            //try
            //{
            //    con.Open();
            //    SqlCeCommand comand = new SqlCeCommand("SELECT * FROM " + table_sprav, con);
            //    adapter.SelectCommand = comand;
            //    adapter.Fill(tbl); 
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    con.Close();
            //}
            return tbl;
        }

        public void save_table_tobase(string table_name, DataTable tbl, BackgroundWorker worker)
        {                      
            //clear_table(table_name);
            ////dal database = new dal();
            //database.Table = table_name;
            //database.querySQL = "SELECT * FROM " + table_name;
            //BindingSource bs = database.tableLoadBS();

            //for (int i = 0; i < tbl.Rows.Count; i++)
            //{
            //    if (!worker.CancellationPending)
            //    {
            //        bs.AddNew();
            //        DataRowView drw = (DataRowView)bs.Current;
            //        for (int k = 1; k < tbl.Columns.Count; k++)
            //        {
            //            drw[k] = tbl.Rows[i][k].ToString();
            //        }                      
            //    }
            //    else
            //        break;
            //} 
            //bs.EndEdit();
            //database.updTable();
            //tbl.Clear();
            //worker.ReportProgress(procenaj += 16);
        }

        private void clear_table(string table)
        {
            //dal database = new dal();
            //database.ExecuteNonQuery_string = "DELETE FROM [" + table + "]";
            //database.tableExecuteNonQuery();            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;            
            try
            {                
                open_load(worker);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Завершено");
            this.Close();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                if (MessageBox.Show("Отменить операцию?", "Отмена", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                { 
                    backgroundWorker1.CancelAsync();
                    this.Close();
                }
            }
        }

        private void load_spravForm_Load(object sender, EventArgs e)
        {

        }
    }
}