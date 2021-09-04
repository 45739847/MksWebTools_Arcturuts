using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MksDecode_WebTools___Arcturus
{
    public partial class Form1 : Form
    {
        public OpenFileDialog openFile = new OpenFileDialog();

        public static string path;
        public static string proxyPath;
        public static int contProxys;
        public static int contDorks;
        public List<string> KeyList = new List<string>();
        public List<string> ProxyList = new List<string>();
        public static List<string> csList = new List<string>();
        public static string rootReturn;
        public static string nodeReturn;
        public static int agentFile;
        public static int contProxy;
        public static int contAgent;
        public static int contFor;
        public static int contWf;
        public static string[] f;
        public static List<string> Lst = new List<string>();
        public static List<string> Urls = new List<string>();
        public static List<string> Nodes = new List<string>();
        public static List<string> UserAgents = new List<string>();
        public static List<string> nodeList = new List<string>();
        public static List<string> listUrls = new List<string>();
        public static List<string> proxyL = new List<string>();
        public static List<string> UAgent = new List<string>();
        private HtmlAgilityPack.HtmlDocument doc;
        public static int rede;
        bool mouseClicked;
        Point clickedAt;

        public Form1()
        {
            InitializeComponent();

            string uPathAgent = "App/UserAgents/useragents.txt";
            Parallel
                .ForEach(File.ReadAllText(uPathAgent).Split('\n'), index => {
                    UAgent.Add(index);
                });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region "Created and verifiedl folders"
            App.FCreate FCreate = new App.FCreate("App", "App/Config/", "App/Config/key.txt");
            App.ProxyCreateFolder PCreate = new App.ProxyCreateFolder("App", "App/Proxys", "App/Proxys/Proxy.txt");
            FCreate.FolderCreate();
            PCreate.pFolderCreate();

            dPath.Text = FCreate.writePath();
            #endregion
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void openKey_Click(object sender, EventArgs e)
        {
            openFile.InitialDirectory = "App";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                path = openFile.FileName;
                dPath.Text = openFile.FileName;

                App.ListKeys listKeys = new App.ListKeys();
                listKeys.openList(path);
                foreach (var item in listKeys.KeyReturns)
                    LKeys.AppendText($"─╼ {item}");

                contDorks = listKeys.KeyReturns.Length;
                lblCont.Text = Convert.ToString(contDorks);

                FileInfo f = new FileInfo(openFile.FileName);
                lblContMb.Text = $"{f.Length.ToString("F2")} KB";
            }
        }

        private void lbl_listKeys_TextChanged(object sender, EventArgs e)
        {

        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void xuiButton4_Click(object sender, EventArgs e)
        {

        }

        private void m_move(object sender, MouseEventArgs e)
        {
            if (mouseClicked)
            {
                this.Location = new Point(Cursor.Position.X - clickedAt.X, Cursor.Position.Y - clickedAt.Y);
            }
        }

        private void m_down(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            mouseClicked = true;
            clickedAt = e.Location;
        }

        private void m_up(object sender, MouseEventArgs e)
        {
            mouseClicked = false;
        }

        private void mtop_move(object sender, MouseEventArgs e)
        {
            if (mouseClicked)
            {
                this.Location = new Point(Cursor.Position.X - clickedAt.X, Cursor.Position.Y - clickedAt.Y);
            }
        }

        private void mtop_down(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            mouseClicked = true;
            clickedAt = e.Location;
        }

        private void mtop_up(object sender, MouseEventArgs e)
        {
            mouseClicked = false;
        }

        private void xuiButton1_Click_1(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                openFile.InitialDirectory = "App/Proxys";
                proxyPath = openFile.FileName; 
                textProxyPath.Text = openFile.FileName;

                if (File.Exists(openFile.FileName))
                {
                    string[] pList = File.ReadAllText(openFile.FileName).Split('\n');
                    contProxys = Convert.ToInt32(pList.Length);
                    lblProxyCont.Text = Convert.ToString(contProxys);

                    foreach (var i in pList) ProxyList.Add(i);
                    lblType.Text = "(click to select)";
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblType_Click(object sender, EventArgs e)
        {
            SelectProxyType s = new SelectProxyType();
            s.ShowDialog();

            var selected = s.returnSelected();

            lblType.Text = selected;
        }

        private void xuiButton10_Click(object sender, EventArgs e)
        {

        }

        private void xuiButton6_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            Random c = new Random();

            int rRandom = (r.Next(10, 50) * 6) + r.Next(10, 50) * 8; rede += rRandom;
            if (rede.ToString().Length > 100000) rede = 0;
            lblContTrans.Text = $"{rRandom.ToString("F2")} KB";
            lblCript.Text = $"{c.Next(100, 999).ToString()} Hash";

            DorkSearch(csList, ProxyList, UAgent);
            SearchList();
        }

        private void xuiButton2_Click(object sender, EventArgs e)
        {
            string key = txt_sarchKey.Text;

            if (key.Length > 1)
            {
                App.ListKeys listKeys = new App.ListKeys();
                listKeys.openList(path);
                foreach (var item in listKeys.KeyReturns)
                {
                    searchList.AppendText(string.Format(item + "\r\n", txt_sarchKey.Text));
                    csList.Add(string.Format(item + "\r\n", txt_sarchKey.Text));
                }
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void consolReturn_TextChanged(object sender, EventArgs e)
        {

        }

        public void DorkSearch(List<string> l, List<string> p, List<string> u)
        {
            Lst = l;
            Urls.Add("https://www.bing.com/search?q={0}&search=&first=415&form=QBLH");
            Urls.Add("https://www.bing.com/search?q={0}&sp=-1&pq=tes&sc=9-3&qs=n&sk=&cvid=29F838C0194F4F4B983013E5087E7E84&first=9&FORM=PERE");
            Urls.Add("https://www.bing.com/search?q={0}&sp=-1&pq=tes&sc=9-3&qs=n&sk=&cvid=29F838C0194F4F4B983013E5087E7E84&first=19&FORM=PERE1");
            Urls.Add("https://www.bing.com/search?q={0}&sp=-1&pq=tes&sc=9-3&qs=n&sk=&cvid=29F838C0194F4F4B983013E5087E7E84&first=29&FORM=PERE2");
            Urls.Add("https://search.yahoo.com/search;_ylt=A2KLfSfZtydgnW0AGEJDDWVH;_ylc=X1MDMTE5NzgwNDg2NwRfcgMyBGZyAwRmcjIDc2ItdG9wLXNlYXJjaARncHJpZANHcGJwWE05S1RuYVJhcTF0VEpUaGRBBG5fcnNsdAMwBG5fc3VnZwMxMARvcmlnaW4Dc2VhcmNoLnlhaG9vLmNvbQRwb3MDMARwcXN0cgMEcHFzdHJsAzAEcXN0cmwDNgRxdWVyeQNwYXlwYWwEdF9zdG1wAzE2MTMyMTU3MTY-?p={0}&fr=sfp&iscqry=&fr2=sb-top-search");
            Urls.Add("https://search.yahoo.com/search;_ylt=A2KLfR7n7ONgEDIAheVXNyoA;_ylu=Y29sbwNiZjEEcG9zAzEEdnRpZAMEc2VjA3BhZ2luYXRpb24-?p={0}&fr=sfp&fr2=sb-top-search&b=11&pz=10&bct=0&xargs=0");
            Urls.Add("https://search.yahoo.com/search;_ylt=A2KIbNAH7eNgO9MA9P1XNyoA;_ylu=Y29sbwNiZjEEcG9zAzEEdnRpZAMEc2VjA3BhZ2luYXRpb24-?p={0}&pz=10&fr=sfp&fr2=sb-top-search&bct=0&b=21&pz=10&bct=0&xargs=0");
            Urls.Add("https://search.aol.com/aol/search;_ylt=A2KIbZwtwidgTw0AFlNpCWVH;_ylc=X1MDMTE5NzgwMzg4MQRfcgMyBGZyA25hBGdwcmlkA1JhUXdPdDJYUkRxUE5rdnBseWF5MEEEbl9yc2x0AzAEbl9zdWdnAzEwBG9yaWdpbgNzZWFyY2guYW9sLmNvbQRwb3MDMARwcXN0cgMEcHFzdHJsAzAEcXN0cmwDNgRxdWVyeQNwYXlwYWwEdF9zdG1wAzE2MTMyMTgzNjg-?q={0}&s_it=sb-top&v_t=na");
            Urls.Add("https://search.aol.com/aol/search;_ylt=AwrJ7JuX7eNgUt0A5TJpCWVH;_ylu=Y29sbwNiZjEEcG9zAzEEdnRpZAMEc2VjA3BhZ2luYXRpb24-?q={0}&fr2=sb-top-search&v_t=na&b=11&pz=10&bct=0&pstart=2");
            Urls.Add("https://yandex.com/search/?oprnd=7208367573&text={0}&lr=21221");
            Urls.Add("https://duckduckgo.com/?q={0}&t=opera&ia=web");
            Urls.Add("https://www.google.com/search?q={0}&sxsrf=ALeKk00Gc-DnoXjO1CyyH9CP5PpViko6gw%3A1625525514974&source=hp&ei=Co3jYO6dOavI1sQPh6CdoAE&iflsig=AINFCbYAAAAAYOObGrcL3JrlwkF4KXZRBsOgr0ftUaRP&oq=teste&gs_lcp=Cgdnd3Mtd2l6EAMyBAgjECcyBAgjECcyBAgjECcyCAgAELEDEIMBMgUIABCxAzIFCAAQsQMyBQgAELEDMgUIABCxAzIFCAAQsQMyBQgAELEDOgcIIxAnEJ0COgcIABCxAxBDOgQIABBDOgoIABCxAxCDARBDOhAIABCxAxCDARDHARCjAhBDOgcIIxCxAhAnOgIIADoECAAQCjoHCAAQsQMQClDWAljTDGDsD2gCcAB4AYAB1wOIAagLkgEJMC41LjEuMC4xmAEAoAEBqgEHZ3dzLXdperABAA&sclient=gws-wiz&ved=0ahUKEwjuypC3gs3xAhUrpJUCHQdQBxQQ4dUDCAY&uact=5");
            Urls.Add("https://www.metacrawler.com/serp?q={0}&sc=eRP0StJ0nRPE10");
            Urls.Add("https://www.metacrawler.com/serp?q={0}&sc=eRP0StJ0nRPE10");
            Urls.Add("https://www.metacrawler.com/serp?q={0}&sc=eRP0StJ0nRPE10");
            Urls.Add("https://www.metacrawler.com/serp?q={0}&page=2&sc=p0RHa3OsY2Xe10");
            Urls.Add("https://www.metacrawler.com/serp?q={0}&page=2&sc=p0RHa3OsY2Xe10");
            Urls.Add("https://www.metacrawler.com/serp?q={0}&page=2&sc=p0RHa3OsY2Xe10");
            Urls.Add("https://www.metacrawler.com/serp?q={0}&page=2&sc=p0RHa3OsY2Xe10");
            Urls.Add("https://www.bing.com/search?q={0}&sp=-1&pq=te&sc=8-2&qs=n&sk=&cvid=C01F259859AD4BA290BFAD5FA929FDC8&first=11&FORM=PERE");
            Urls.Add("https://www.bing.com/search?q={0}&sp=-1&pq=te&sc=8-2&qs=n&sk=&cvid=C01F259859AD4BA290BFAD5FA929FDC8&first=11&FORM=PERE");

            Nodes.Add("//*[@id='b_results']/li[1]/div[2]/div/cite");                             // bing
            Nodes.Add("//*[@id='b_results']/li[1]/div/div[1]/div/cite");                         // bing
            Nodes.Add("//*[@id='b_results']/li[1]/div/div/cite");                                // bing
            Nodes.Add("//*[@id='b_results']/li[1]/div/div/cite");                                // bing
            Nodes.Add("//*[@id='web']/ol/li[1]/div/div[1]/div/span[1]");                         // yahoo
            Nodes.Add("//*[@id='web']/ol/li[1]/div/div[1]/div/span[1]");                         // yahoo
            Nodes.Add("//*[@id='web']/ol/li[1]/div/div[1]/div/span[1]");                         // yahoo
            Nodes.Add("//*[@id='web']/ol/li[2]/div/div[1]/div/span");                            // aol
            Nodes.Add("//*[@id='yui_3_10_0_1_1625550260435_39']/li[2]/div/div[1]/div/span");     // oal
            Nodes.Add("//*[@id='search-result']/li[2]/div/div[1]/div[1]/a[@href]");              // yandex
            Nodes.Add("//*[@id='r1-0']/div/h2/a[1][@href]");                                     // duckduckgo
            Nodes.Add("//*[@id='rso']/div[1]/div/div/div[1]/a[@href]");                          // google
            Nodes.Add("/html/body/div[2]/div[2]/div[1]/div[1]/div[3]/div[1]/span[1]");           // metacrawler
            Nodes.Add("/html/body/div[2]/div[2]/div[1]/div[1]/div[3]/div[2]/span[1]");           // metacrawler
            Nodes.Add("/html/body/div[2]/div[2]/div[1]/div[1]/div[3]/div[3]/span[1]");           // metacrawler
            Nodes.Add("/html/body/div[2]/div[2]/div[1]/div[1]/div[3]/div[1]/span[1]");           // metacrawler
            Nodes.Add("/html/body/div[2]/div[2]/div[1]/div[1]/div[3]/div[1]/span[2]");           // metacrawler
            Nodes.Add("/html/body/div[2]/div[2]/div[1]/div[1]/div[3]/div[1]/span[3]");           // metacrawler
            Nodes.Add("/html/body/div[2]/div[2]/div[1]/div[1]/div[3]/div[1]/span[4]");           // metacrawler
            Nodes.Add("/html/body/div[1]/main/ol/li[3]/div/div/cite");                           // bing
            Nodes.Add("//*[@id=\"b_results\"]/li[4]/div/div/cite");                              // bing

            proxyL = p;
        }
        public async void SearchList()
        {
            int cont = 0;
            foreach (var lineList in csList)
            {
                foreach (var u in Urls)
                {
                    try
                    {
                        string n = Nodes[cont];
                        await LoadWebsitesDocumentNode(string.Format(u, lineList), n);
                    }
                    catch { cont = 0; contFor = 0; }
                    contFor++; cont++;
                }
            }
        }
        public int cAgent = 0;
        public static string randomReturn;
        public async Task<HtmlNode> LoadWebsitesDocumentNode(string url, string node)
        {
            #region "Http Requests"
            HtmlWeb htmlWeb = new HtmlWeb();
            try { doc = await htmlWeb.LoadFromWebAsync(url); } catch { }
            HtmlNode root = doc?.DocumentNode;
            HtmlNodeCollection rt = new HtmlNodeCollection(root);
            rt = root?.SelectNodes(node);
            rootReturn = root?.InnerText;
            cReturn.AppendText($"Status => {htmlWeb.StatusCode} Buffer => {htmlWeb.StreamBufferSize} Duration => {htmlWeb.RequestDuration} \r\n");
            for (var item = 0; item < rt?.Count; item++)
            {
                if (rt[item] != null)
                {
                    string returnLine;
                    try { returnLine = rt[item].InnerText.Replace(" ", ""); returnLine = returnLine.Replace('›', '/') + "\r\n"; }
                    catch { returnLine = rt[item].InnerText + "\r\n"; }

                    if (returnLine != null && returnLine != "" && returnLine != "\r\n")
                    {
                        contWf++;
                        consolReturn.AppendText(returnLine);
                        lbl_wf.Text = Convert.ToString(contWf);
                        consolReturn.Refresh();
                        listUrls.Add(returnLine);
                    }
                }
            }
            #endregion;
            return root;
        }

        private void xuiButton2_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void kryptonContextMenu2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchList.Text = "";
        }

        private void xuiButton9_Click(object sender, EventArgs e)
        {
            methodUrl("https://github.com/OkamiMks");
        }

        private void xuiButton8_Click(object sender, EventArgs e)
        {
            Donate donate = new Donate();
            donate.Show();
        }

        public static void methodUrl(string sendingUrl)
        {
            try { Process.Start(sendingUrl); }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                { sendingUrl = sendingUrl.Replace("&", "^&"); Process.Start(new ProcessStartInfo("cmd", $"/c start {sendingUrl}") { CreateNoWindow = true }); }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) { Process.Start("xdg-open", sendingUrl); }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) { _ = Process.Start("open", sendingUrl); }
                else { throw; }
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            VersionInfo versionInfo = new VersionInfo();
            versionInfo.Show();
        }
    }
}
