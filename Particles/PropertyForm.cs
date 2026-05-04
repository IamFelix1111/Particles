using System;
using System.Windows.Forms;

namespace Particles
{
    public partial class PropertyForm : Form
    {
        private static PropertyForm instance;

        public PropertyForm()
        {
            InitializeComponent();
        }

        internal static PropertyForm Instance 
        { 
            get 
            {
                if (instance == null || instance.IsDisposed) instance = new PropertyForm();
                return instance;
            } 
        }

        private void PropertyForm_Load(object sender, EventArgs e)
        {
            
        }

        private void openDevButton_Click(object sender, EventArgs e)
        {
            MainForm.Instance.webView2.CoreWebView2.OpenDevToolsWindow();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {

        }

        private void PropertyForm_Activated(object sender, EventArgs e)
        {
            MainForm.SetFormBehindWindow(MainForm.Instance, this);
        }
    }
}
