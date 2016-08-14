﻿using System;
using System.Drawing;
using System.Windows.Forms;
using OSIRT.UI.Attachment;
using OSIRT.UI.AuditLog;
using OSIRT.UI.CaseNotes;
using OSIRT.VideoCapture;
using OSIRT.UI.Options;
using OSIRT.UI.SnippetTool;
using OSIRT.Helpers;
using OSIRT.UI.ViewSource;
using System.IO;
using System.Text;
using OSIRT.Loggers;
using Whois;
using System.Net;
using System.Diagnostics;
using OSIRT.Extensions;
using OSIRT.Browser;
using CefSharp;
using Tor;

namespace OSIRT.UI
{

    public partial class BrowserPanel : UserControl
    {

        public event EventHandler CaseClosing;
        public event EventHandler ResizeMainForm;
        private bool isUsingTor;
        private string userAgent;

        public BrowserPanel(bool isUsingTor, string userAgent)
        {
            this.isUsingTor = isUsingTor;
            this.userAgent = userAgent;
            CheckAdvancedOptions();
            InitializeComponent();
            uiTabbedBrowserControl.SetAddressBar(uiURLComboBox);

            if (isUsingTor)
            {
                uiURLComboBox.BackColor = Color.MediumPurple;
                uiURLComboBox.ForeColor = Color.White;
            }
               
        }

        private void BrowserPanel_Load(object sender, EventArgs e)
        {
            ConfigureUi();
            uiTabbedBrowserControl.ScreenshotComplete += UiTabbedBrowserControl_ScreenshotComplete;
            uiTabbedBrowserControl.CurrentTab.Browser.SavePageSource += Browser_SavePageSource;
            uiTabbedBrowserControl.CurrentTab.AddressChanged += CurrentTab_AddressChanged;
            OsirtVideoCapture.VideoCaptureComplete += osirtVideoCapture_VideoCaptureComplete;
            uiTabbedBrowserControl.CurrentTab.Browser.StatusMessage += Browser_StatusMessage;
        }

        private void Browser_StatusMessage(object sender, StatusMessageEventArgs e)
        {
            this.InvokeIfRequired(() =>  uiTabbedBrowserControl.SetStatusLabel(e.Value));
        }

        private void UiTabbedBrowserControl_UpdateForwardAndBackButtons(object sender, EventArgs e)
        {
            this.InvokeIfRequired(() => uiLBackButton.Enabled = uiTabbedBrowserControl.CurrentTab.Browser.CanGoBack);
            this.InvokeIfRequired(() => uiForwardButton.Enabled = uiTabbedBrowserControl.CurrentTab.Browser.CanGoForward);
        }


        private void CurrentTab_AddressChanged(object sender, EventArgs e)
        {
          
        }

        private void CurrentTab_OpenNewTab(object sender, EventArgs e)
        {
            string url = ((OnPopUpEventArgs)e).TargetUrl;
            this.InvokeIfRequired(() => uiTabbedBrowserControl.CurrentTab.Browser.Load(url));
        }


        private void Browser_NavigationComplete(object sender, EventArgs e)
        {
            Uri url = new Uri(uiTabbedBrowserControl.CurrentTab.Browser.URL);
            IPAddress[] addresses = Dns.GetHostAddresses(url.Host);

            foreach (var address in addresses)
            {
                whatsTheIPToolStripMenuItem.Text = address.ToString();
            }
        }

        private void Browser_SavePageSource(object sender, EventArgs e)
        {
            var args = ((SaveSourceEventArgs)e);
            string filename = Constants.PageSourceFileName.Replace("%%dt%%", DateTime.Now.ToString("yyyy-MM-dd_hh_mm_ss")).Replace("%%name%%", args.Domain);
            string path = Path.Combine(Constants.ContainerLocation, Constants.Directories.GetSpecifiedCaseDirectory(Enums.Actions.Source), filename);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    streamWriter.WriteLine(args.Source);
                }
            }

            Logger.Log(new WebpageActionsLog(uiTabbedBrowserControl.CurrentTab.Browser.URL, Enums.Actions.Source, OsirtHelper.GetFileHash(path), filename, "[Page source downloaded]"));
            MessageBox.Show("Page source saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void UiTabbedBrowserControl_ScreenshotComplete(object sender, EventArgs e)
        {
            //uiScreenshotButton.Enabled = true;
        }

        private void ConfigureUi()
        {
            Dock = DockStyle.Fill;
            uiBrowserToolStrip.ImageScalingSize = new Size(32, 32);
            uiURLComboBox.KeyDown += UiURLComboBox_KeyDown;

        }

        private void UiURLComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                uiTabbedBrowserControl.Navigate(uiURLComboBox.Text);
                e.SuppressKeyPress = true; //stops "ding" sound when enter is pushed
            }
        }



        private void uiScreenshotButton_Click(object sender, EventArgs e)
        {
            //uiScreenshotButton.Enabled = false;
            uiTabbedBrowserControl.FullPageScreenshot();
        }


        private void uiCaseNotesButton_Click(object sender, EventArgs e)
        {
            CaseNotesForm caseNotes = new CaseNotesForm();
            caseNotes.FormClosed += CaseNotes_FormClosed;
            caseNotes.Show();
            uiCaseNotesButton.Enabled = false;
        }

        private void CaseNotes_FormClosed(object sender, FormClosedEventArgs e)
        {
            uiCaseNotesButton.Enabled = true;
        }

        private void uiAttachmentToolStripButton_Click(object sender, EventArgs e)
        {
            using (AttachmentForm attachment = new AttachmentForm())
            {
                attachment.ShowDialog();
            }
        }

        private void uiLBackButton_Click(object sender, EventArgs e)
        {
            if (uiTabbedBrowserControl.CurrentTab.Browser.CanGoBack)
                uiTabbedBrowserControl.CurrentTab.Browser.GetBrowser().GoBack();
        }

        private void uiForwardButton_Click(object sender, EventArgs e)
        {

            if (uiTabbedBrowserControl.CurrentTab.Browser.CanGoForward)
                uiTabbedBrowserControl.CurrentTab.Browser.GetBrowser().GoForward();


        }

        private void uiRefreshButton_Click(object sender, EventArgs e)
        {
            uiTabbedBrowserControl.CurrentTab.Browser.Refresh();
        }

        private void closeCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaseClosing?.Invoke(this, e);
        }

        private MarkerWindow markerWindow;
        private void markerWindowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (markerWindow == null || markerWindow.IsDisposed)
            {
                markerWindow = new MarkerWindow();
            }
            try
            {
                markerWindow.Show();
            }
            catch { markerWindow = new MarkerWindow(); markerWindow.Show(); }
            markerWindow.TopMost = true;
        }

        private void uiVideoCaptureButton_Click(object sender, EventArgs e)
        {
            uiTabbedBrowserControl.CurrentTab.Browser.MouseTrailVisible = UserSettings.Load().ShowMouseTrail;

            ResizeMainForm?.Invoke(this, null);


            //TODO: this check is causing the user to have to click twice to stop/start recording??
            IntPtr handle;
            if (markerWindow != null && markerWindow.Visible)
                handle = markerWindow.Handle;
            else
                handle = Handle;


            if (!OsirtVideoCapture.IsRecording())
                OsirtVideoCapture.StartCapture(Width, Height, uiVideoCaptureButton, (uint)handle);
            else
                OsirtVideoCapture.StopCapture();
        }

        private void osirtVideoCapture_VideoCaptureComplete(object sender, EventArgs e)
        {
            if (markerWindow != null && markerWindow.Visible)
            {
                markerWindow.Close();
                markerWindow.Dispose();
            }

            uiTabbedBrowserControl.CurrentTab.Browser.MouseTrailVisible = false;
            VideoCaptureCompleteEventArgs ev = (VideoCaptureCompleteEventArgs)e;

            using (VideoPreviewer vidPreviewer = new VideoPreviewer(Enums.Actions.Video))
            {
                vidPreviewer.ShowDialog();
            }

            ImageDiskCache.RemoveSpecificItemFromCache(Constants.TempVideoFile);
        }

        private void uiAuditLogToolStripButton_Click(object sender, EventArgs e)
        {
            using (AuditLogForm audit = new AuditLogForm())
            {
                audit.ShowDialog();
            }
        }

        private void uiOptionsToolStripButton_Click(object sender, EventArgs e)
        {
            using (OptionsForm options = new OptionsForm())
            {
                options.ShowDialog();
            }
        }

        private void snippetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SnippingTool.Snip())
                    return;
            }
            catch
            {
                MessageBox.Show("Unable to use snipping tool. Try closing some tabs. If this continues, restart OSIRT.", "Unable to use snippet tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Previewer imagePreviewer = new ImagePreviewer(Enums.Actions.Snippet, uiTabbedBrowserControl.CurrentTab.Browser.URL))
            {
                imagePreviewer.ShowDialog();
            }
            ImageDiskCache.RemoveItemsInCache();

        }

        private void currentViewScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uiTabbedBrowserControl.CurrentViewScreenshot();
        }

        private void currentViewTimedScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SelectTimeForm timeForm = new SelectTimeForm())
            {
                if (timeForm.ShowDialog() != DialogResult.OK)
                    return;

                uiTabbedBrowserControl.TimedScreenshot(timeForm.Time);
            }


        }

        private void uiHomeButton_Click(object sender, EventArgs e)
        {
           // uiTabbedBrowserControl.CurrentTab.Browser.Navigate(UserSettings.Load().Homepage);
        }

        private void whoIsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(uiTabbedBrowserControl.CurrentTab.Browser.URL);
            try
            {
                string host = url.Host;
                if (!(host.EndsWith(".com") || host.EndsWith(".net")))
                {
                    if (host.StartsWith("www."))
                        host = host.Remove(0, 4);
                }
                var whois = new WhoisLookup().Lookup(host);
                var view = new ViewPageSource( whois.ToString(), new Tuple<string, string, string>(url.Host, host, ""));
                view.Show();
            }
            catch
            {
                MessageBox.Show("Unable to obtain Whois? information for this website.", "Error obtaining Whois?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void whatsTheIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(uiTabbedBrowserControl.CurrentTab.Browser.URL);
            IPAddress[] addresses = Dns.GetHostAddresses(url.Host);

            using (var ipForm = new IpAddressesForm(addresses))
            {
                this.InvokeIfRequired(() => ipForm.ShowDialog());
            }

                
        }

        private void uiURLComboBox_MouseEnter(object sender, EventArgs e)
        {

        }


        private void CheckAdvancedOptions()
        {
            CefSettings settings = new CefSettings();
            if (!string.IsNullOrEmpty(userAgent))
            {
                settings.UserAgent = userAgent;
            }

            if (!isUsingTor)
            {
                Cef.Initialize(settings);
                return;
            }
            settings.CefCommandLineArgs.Add("proxy-server", "127.0.0.1:8182");
            Process[] previous = Process.GetProcessesByName("tor");
            if (previous != null && previous.Length > 0)
            {
                foreach (Process process in previous)
                    process.Kill();
            }

            ClientCreateParams createParameters = new ClientCreateParams();
            createParameters.ConfigurationFile = "";
            createParameters.ControlPassword = "";
            createParameters.ControlPort = 9051;
            createParameters.DefaultConfigurationFile = "";
            createParameters.Path = @"Tor\Tor\tor.exe";

            Client client = Client.Create(createParameters);
            client.Status.BandwidthChanged += Status_BandwidthChanged;
            Cef.Initialize(settings);

        }

        private void Status_BandwidthChanged(object sender, BandwidthEventArgs e)
        {
            Invoke((Action)delegate
            {
                if (e.Downloaded.Value == 0 && e.Uploaded.Value == 0)
                    uiTabbedBrowserControl.SetStatusLabel("");
                else
                    uiTabbedBrowserControl.SetStatusLabel(string.Format("Down: {0}/s, Up: {1}/s", e.Downloaded, e.Uploaded));
            });
        }


        private void aboutOSIRTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutOSIRT().Show();
        }
      
    }
}
