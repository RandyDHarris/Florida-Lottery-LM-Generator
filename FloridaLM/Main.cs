using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;

namespace FloridaLM
{
    public partial class Main : Form
    {

        public List<LuckyMoneyNumbers> llmp { get; set; }
        public ServiceAccess sa { get; set; }
        public Main()
        {
            InitializeComponent();
            sa = new ServiceAccess();
        }

        private void GetHistoryFromFL()
        {
            try
            {
                if (sa.SiteAvailable())
                {
                    

                    Cursor.Current = Cursors.WaitCursor;
                    System.Windows.Forms.Application.DoEvents();
                    sa.ParseHistoryResults();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("The history for the Florida Lottery Lucky Money game has been retrieved and stored.");
                    GetHistory();
                    dgPicks.Visible = false;
                    dgHistory.Visible = true;
                    rtReadMe.Visible = false;
                }
                else
                {
                    MessageBox.Show("The Florida Lottery website http://www.floridaylottery.com is unavailable at this time.");
                }
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("attach"))
                {
                    MessageBox.Show("The history for the Florida Lottery Lucky Money game has been retrieved but was not stored in the database. Please check the physical database path in the app.congig file connectionstrings section.");
                }
                else
                {
                    MessageBox.Show("You currently do not have internet connectivity or acess to the site http://www.floridaylottery.com has been blocked by an administrator");
                }
                return;
            }
        }
       
        
        private void GetHistory()
        {
            try
            {
                var source = new BindingSource();

                source.DataSource = llmp;
                dgHistory.DataSource = source;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check the physical database path in the app.congig file connectionstrings section.");
                return;
            }
        }
        

        private void GetPicks()
        {
            try
            {
                var source = new BindingSource();
                source.DataSource = llmp;
                dgPicks.DataSource = source;
                dgPicks.Columns[6].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check the physical database path in the app.congig file connectionstrings section.");
                return;
            }
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rtReadMe.Visible = true;
            dgPicks.Visible = false;
            dgHistory.Visible = false;
        }


        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GetHistoryFromFL();
        }


        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            dgPicks.Visible = false;
            dgHistory.Visible = true;
            rtReadMe.Visible = false;
            GetHistory();
        }


        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            GetPicks();
            dgPicks.Visible = true;
            dgHistory.Visible = false;
            rtReadMe.Visible = false;
        }


    }
}
