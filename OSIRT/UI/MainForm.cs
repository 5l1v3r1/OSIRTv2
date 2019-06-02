﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Ionic.Zip;
using Jacksonsoft;
using OSIRT.Helpers;
using OSIRT.Resources;
using OSIRT.UI.CaseClosing;
using OSIRT.Loggers;
using System.Drawing;
using OSIRT.UI.Splash;
using OSIRT.UI.BrowserOptions;

namespace OSIRT.UI
{
    public partial class MainForm : Form
    {
        private bool caseOpened;
        private bool caseClosed;
        private bool usingTor;
        private string userAgent;

        //http://stackoverflow.com/questions/2612487/how-to-fix-the-flickering-in-user-controls
        //TODO@ need to look at this for hosted controls
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }
        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
        }

        public MainForm()
        {

            SplashScreen splash = new SplashScreen();
            splash.TopMost = true;
            splash.ShowDialog();
            splash.CaseChecked += delegate
            {
                Show();
            };


            InitializeComponent();
            FormClosing += MainForm_FormClosing;
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!caseOpened || caseClosed)
            {
                e.Cancel = false;
                return;
            }

            e.Cancel = true;
            DialogResult result = MessageBox.Show("Shutdown OSIRT and close the current case?", "Close Current Case?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                caseClosed = true;
                ClosingOsirt();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            FirstLoad();
        }

        private void FirstLoad()
        {
            FirstLoadPanel firstLoadPanel = new FirstLoadPanel();
            firstLoadPanel.NewCaseClick += firstLoadPanel_NewCase_Click;
            firstLoadPanel.LoadOldCaseClick += FirstLoadPanel_LoadOldCase_Click;
            firstLoadPanel.LoadAdavancedOptions += FirstLoadPanel_LoadAdavancedOptions;
            Controls.Add(firstLoadPanel);
        }

      

        private void FirstLoadPanel_LoadAdavancedOptions(object sender, EventArgs e)
        {
            using (BrowserOptionsForm browserOptions = new BrowserOptionsForm())
            {
               DialogResult result = browserOptions.ShowDialog();
                if (result != DialogResult.OK)
                    return;

                usingTor = RuntimeSettings.IsUsingTor;
                userAgent = browserOptions.UserAgent;
            }
        }

        private void FirstLoadPanel_LoadOldCase_Click(object sender, EventArgs e)
        {
            string filenameWithPath;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "OSR Files|*.osr";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                DialogResult result = openFileDialog.ShowDialog();
                if (result != DialogResult.OK)
                    return;

                filenameWithPath = openFileDialog.FileName;
            }

            Controls.Clear();
            LoadExistingCasePanel exisitingCasePanel = new LoadExistingCasePanel(new FileInfo(filenameWithPath));
            Controls.Add(exisitingCasePanel);
            exisitingCasePanel.PasswordCheckClick += ExisitingCasePanel_PasswordCheckClick;

        }

        private void ExisitingCasePanel_PasswordCheckClick(object sender, EventArgs e)
        {
            ShowBrowserPanelAndLogOpening();
        }


        void firstLoadPanel_NewCase_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            CaseDetailsPanel2 caseDetailsPanel = new CaseDetailsPanel2();
            Controls.Add(caseDetailsPanel);
            caseDetailsPanel.NextClick += new EventHandler(caseDetailsPanel_NextClick);
        }

        protected void caseDetailsPanel_NextClick(object sender, EventArgs e)
        {
            ShowBrowserPanelAndLogOpening();
        }

        private void ShowBrowserPanelAndLogOpening()
        {
            ShowBrowserPanel();
            caseOpened = true;
            OsirtLogWriter.Write(Constants.ContainerLocation, false);
        }

        private void ShowBrowserPanel()
        {
            Controls.Clear();
            BrowserPanel browserPanel = new BrowserPanel(userAgent, this);
            Controls.Add(browserPanel);
            WindowState = FormWindowState.Maximized;
            browserPanel.CaseClosing += BrowserPanel_CaseClosing;
            
        }

        //event from menu item
        private void BrowserPanel_CaseClosing(object sender, EventArgs e)
        {
            caseClosed = true;
            ClosingOsirt();
        }

        private void ClosingOsirt()
        {
            Controls.Clear();

            //UserSettings.Load().CaseHasPassword
            bool caseHasPassword = new Database.DatabaseHandler().CaseHasPassword();
            if (caseHasPassword)
            {
                CloseCasePanel closePanel = new CloseCasePanel();
                Controls.Add(closePanel);
                closePanel.PasswordCorrect += ClosePanel_PasswordCorrect;
            }
            else
            {
                CaseClosingCleanUpPanel cleanUpPanel = new CaseClosingCleanUpPanel("", false);
                Controls.Add(cleanUpPanel);
            }
        }

        private void ClosePanel_PasswordCorrect(object sender, CaseClosingEventArgs e)
        {
            Controls.Clear();
            //add new closing panel
            CaseClosingCleanUpPanel cleanUpPanel = new CaseClosingCleanUpPanel(e.Password, false);
            Controls.Add(cleanUpPanel);

            //CloseOsirt(e.Password);
            //Close();
        }
    }
}
