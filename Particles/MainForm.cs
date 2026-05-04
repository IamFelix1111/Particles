using Particles.Properties;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Particles
{
    public partial class MainForm : Form
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, uint uFlags, uint uIDNewItem, string lpNewItem);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy,
            uint uFlags);

        private const uint MENU_PROPERTY = 1000;
        private const uint WM_SYSCOMMAND = 0x0112;
        private const uint MF_STRING = 0x0;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
        private static MainForm instance;

        internal static void SetFormBehindWindow(Form form, Form window)
        {
            SetWindowPos(
                form.Handle,
                window.Handle,
                0, 0, 0, 0,
                SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE
            );
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public static MainForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed) instance = new MainForm();
                return instance;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            AppendMenu(hSysMenu, MF_STRING, MENU_PROPERTY, Resources.Menu_Property);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                uint cmd = (uint)(m.WParam.ToInt64() & 0xFFFF);
                switch (cmd)
                {
                    case MENU_PROPERTY:
                        PropertyForm.Instance.Show();
                        PropertyForm.Instance.Activate();
                        return;
                }
            }
            base.WndProc(ref m);
        }

        private string GetHtml()
        {
            string html = Encoding.UTF8.GetString(Resources.Particles_HTML);
            html = html.Replace("particles.json",
                "data:text/json;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(Settings.Default.Particles_JSON)));
            return html;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string finalHtml = GetHtml();
            webView2.Source = new Uri("data:text/html;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(finalHtml)));
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (PropertyForm.Instance != null && !PropertyForm.Instance.IsDisposed)
            {
                SetFormBehindWindow(PropertyForm.Instance, this);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            PropertyForm.Instance?.Close();
        }
    }
}
