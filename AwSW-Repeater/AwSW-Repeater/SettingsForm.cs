using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwSW_Repeater
{
    public partial class SettingsForm : Form
    {
        #region Form Shadows
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        #region DllImport
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion


        public SettingsForm()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void StatusBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Output_OnlyOnce = checkBox1.Checked;
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Filters_OldNewOnly = checkBox2.Checked;
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Filters_CheckCharacter = checkBox3.Checked;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int value))
                Settings.Filters_CheckFirstWords = value;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out int value))
                Settings.Filters_MinWordCount = value;
        }
    }
}
