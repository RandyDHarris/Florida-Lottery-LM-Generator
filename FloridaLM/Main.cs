//AUTHOR:       Randy Harris
//Date:         4/8/2016
//Class:        Main
//Description:  This class contains all form and control code as well as object event handlers. 
//              It inherts from the form base             

#region Assemblies
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
using System.Runtime.Caching;
#endregion

namespace FloridaLM
{
    public partial class Main : Form
    {
        #region Properties
        public List<LuckyMoneyNumbers> _llmn { get; set; }
        public List<LMPicks> _llmp { get; set; }
        public Checks _chk { get; set; }
        #endregion

        #region Methods
        public Main()
        {
            InitializeComponent();
            SetupText();
        }

        public void SetupText()
        {
   
            rtReadMe.Rtf = @"{\rtf1\pc " +

            @"                               \b \ul The Florida Lottery Desktop App\ul0 \b0 \par \par" +
            @"                              \b \ul Created By Randy Harris 3/19/2016\ul0 \b0 \par \par" +
            @"                    \b \ul This is a Windows Forms C# Desktop application.\ul0 \b0 \par \par \par" +
            @"\b \ul Introduction\ul0 \b0 \par" +

            @" The application links to the Florida Lottery website and retrieves the drawing" +
            @" history for the Lucky Money game by parsing the HTML formatted data and caching it." +
            @" The application generates the top ten number sets (tickets to purchase) derived from" +
            @" an algorithm that utilizes the numbers most drawn in each position and when they" +
            @" are due to be drawn again.\par \par" + 
            @" The definition of the algorithm is as follows: \par" +
            @" Query the numbers in each draw position and order them by most occurring for the last 60 draws." +
            @" Each number is weighted by the longest period since the number was last drawn in each position." +
            @" The number possibilities are from 1 to 47 for positions 1, 2, 3, and 4. The last position is the Lucky Ball." +
            @" Since the numbers 47, 46, and 45 can never occur in position one and numbers 1, 2, and 3" +
            @" can never occur in position 4, the number of permutations are reduced which in fact" +
            @" reduces the total possibilities. Thus, there are 178,365 different ways in which 4 numbers" +
            @" can be chosen from a total of 47 unique numbers. The Lucky Ball may be between 1 and 17." +
            @" The number of permutations increases substantively when including the Lucky Ball bringing" +
            @" the chance of actually guessing the correct combination to 1 in 3,032,205." +
            @" After applying the algorithm to each position, the top ten values in each position are used" +
            @" to generate a set of 10 best tickets to purchase. \par \par" +

            @" This application was created in order to show C# programming" +
            @" examples in as close to a real world application environment as" +
            @" possible. While software code patterns are vast and can be refactored" +
            @" many times, usually a combination of many patterns ends up in the mix." +
            @" The overall concept remains the same as all Object Oriented Programming (OOP)" +
            @" efforts should overlay against the SOLID principals. \par \par" +
            @" A brief explanation of SOLID: \par" +
            @" SOLID are five basic principles which help to create good software architecture. SOLID is an acronym where: \par  \par" +
            @" \b S\b0   stands for SRP (Single responsibility principle) \par" +
            @" \b O\b0   stands for OCP (Open closed principle) \par" +
            @" \b L\b0   stands for LSP (Liskov substitution principle) \par" +
            @" \b I\b0    stands for ISP (Interface segregation principle) \par" +
            @" \b D\b0   stands for DIP (Dependency inversion principle) \par \par" +

            @" This application was coded with the SOLID principals in mind. Additionally, the ONION architecture was implemented. There are 6 projects / layers in the solution. They are: \par\par" +

            @" \ul Layers / Projects\ul0                                   \ul Dependancies\ul0   \par" +
            @" FloridaLM – Presentation                      App, Business \par" +
            @" App – Interfaces / Services                   Business \par" +
            @" Business – Data Structures \par" +
            @" Infrastructure – Repository                    App, Business \par" +
            @" Test – Unit Test                                    App, Business, Infrastructure \par" +
            @" Setup – Install shield Project   \par \par" +

            @" The most important thing to mention here is that the Infrastructure has no dependency on the Presentation nor does the Presentation have a dependency on Infrastructure. \par" +

            @"} ";
        
        
        
        
        }














        private void GetHistoryFromFL()
        {
            bool bSiteUp = CheckFloridaLotteryIsUp();

            _chk = new Checks();
            try
            {
                if (bSiteUp)
                {
                    if (!_chk.CheckIfCacheExist("LMResultsFromFLWebSite"))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        System.Windows.Forms.Application.DoEvents();
                        ParseFLMHistory();
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The history for the Florida Lottery Lucky Money game has been retrieved and stored.");
                    }

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

                var service = _container.GetInstance<FloridaLotteryLuckMoneyHTMLService>();

                _llmn = service.GetParsedHistoryResults();
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

            service.GetParsedHistoryResults();
        }
        #endregion

        #region Handlers
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
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            GetPicks();
            dgPicks.Visible = true;
            dgHistory.Visible = false;
            rtReadMe.Visible = false;
        }
        private void OnApplicationExit(object sender, EventArgs e) 
        {
            //dispose of cache here
        }
        #endregion
    }
}
