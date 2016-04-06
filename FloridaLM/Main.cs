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
using App;

namespace FloridaLM
{
    public partial class Main : Form
    {
        public List<LuckyMoneyNumbers> _llmn { get; set; }
        public List<LMPicks> _llmp { get; set; }
        public Main()
        {
            InitializeComponent();
        }
        private void GetHistoryFromFL()
        {
            bool bSiteUp = CheckFloridaLotteryIsUp();

            try
            {
                if (bSiteUp)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    System.Windows.Forms.Application.DoEvents();
                    ParseFLMHistory();
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
        public bool CheckFloridaLotteryIsUp()
        {
            var _container = StructureMapConfig.Configure();

            var service = _container.GetInstance<FloridaLotteryLuckMoneyHTMLService>();

            return service.GetSiteIsUp();
        }
        private void GetHistory()
        {
            try
            {

                GetHistoryDataFromRepository();
                
                var source = new BindingSource();
                source.DataSource = _llmn;
                dgHistory.DataSource = source;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check the physical database path in the app.congig file connectionstrings section.");
                return;
            }
        }
        private void GetHistoryDataFromRepository()
        {
                var _container = StructureMapConfig.Configure();

                var service = _container.GetInstance<LuckyMoneyNumbersService>();

                _llmn = service.GetNumbers();
        }
        private void GetPicks()
        {
            try
            {
                //GetHistoryDataFromRepository();

                GenerateNumbers gn = new GenerateNumbers(_llmn);
                
                var source = new BindingSource();
                source.DataSource = gn.GetPickList();
                dgPicks.DataSource = source;
                dgPicks.Columns[6].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check the physical database path in the app.congig file connectionstrings section.");
                return;
            }
        }
        private void ParseFLMHistory()
        {
            var _container = StructureMapConfig.Configure();

            var service = _container.GetInstance<FloridaLotteryLuckMoneyHTMLService>();

            service.UpdateFloridaLuckyMoneyHistory();
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
