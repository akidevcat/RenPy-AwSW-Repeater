using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace AwSW_Repeater
{
    public partial class AwSWR : Form
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

        private readonly string LabelDragInitialText;
        private readonly RepeaterCore rCore;
        private readonly SettingsForm settingsForm;

        private void CreateTooltips()
        {
            var ttClearFilesButton = new ToolTip();
            var ttLabelDrag = new ToolTip();
            ttClearFilesButton.SetToolTip(ClearFilesButton, "Clears loaded .rpy list");
            ttLabelDrag.SetToolTip(LabelDrag, "Left Mouse Click to proceed\nRight Mouse Click to open settings");
        }

        public AwSWR()
        {
            settingsForm = new SettingsForm();

            InitializeComponent();
            CreateTooltips();
            rCore = new RepeaterCore();
            LabelDragInitialText = LabelDrag.Text;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StatusBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void LabelDrag_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void LabelDrag_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var filesRead = rCore.UploadFiles(files);
            LabelDrag.Text = $"Successfully read {filesRead}\n .rpy files (total: {rCore.filesCount}).\n(｡◕‿‿◕｡)\n\nTap here to proceed.";
        }

        private void LabelDrag_DragEnter(object sender, DragEventArgs e)
        {
            LabelDrag.Text = "Catching 	ԅ(≖‿≖ԅ)";
            e.Effect = DragDropEffects.Copy;
        }

        private void LabelDrag_DragLeave(object sender, EventArgs e)
        {
            LabelDrag.Text = LabelDragInitialText;
        }

        private void ClearFilesButton_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("You're going to clear loaded .rpy list. Are you sure?", "Clear .rpy list", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                rCore.Reset();
                LabelDrag.Text = LabelDragInitialText;
            }
        }

        private void LabelDrag_Click(object sender, EventArgs e)
        {
            var me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Left)
            {
                rCore.ProcessFiles();
                var sfd = new SaveFileDialog();
                sfd.Filter = "CSV File|*.csv";
                sfd.Title = "Save output csv file";
                sfd.FileName = "AwSW-Repeating";
                sfd.ShowDialog();
                if (sfd.FileName != "")
                {
                    rCore.SaveToCsvFile(sfd.FileName);
                }
            } else if (me.Button == MouseButtons.Right)
            {
                settingsForm.Show();
            }
        }
    }
}
