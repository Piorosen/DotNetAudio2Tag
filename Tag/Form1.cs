using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tag
{
    public partial class Form1 : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        Core.CueSpliter cueSpliter = new Core.CueSpliter();

        public Form1()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void CuesplitBtnOpenDialog_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.AddExtension = true;
            fileDialog.Filter = "Cue 파일|*.cue";
            fileDialog.CheckPathExists = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (fileDialog.FileNames.Length != 0)
                {
                    foreach (var path in fileDialog.FileNames)
                    {
                        CuesplitListStatus.Items.Add(new ListViewItem(new[] { Path.GetFileName(path)}));

                        cueSpliter.AddCueFile(path
                            , Path.GetDirectoryName(path) + @"\" + Path.GetFileNameWithoutExtension(path) + ".wav"
                            , Path.GetDirectoryName(path) + @"\");
                    }
                }
            }
        }
        
        private void CuesplitBtnImport_Click(object sender, EventArgs e)
        {

        }
        volatile bool isrun = false;
        private async void CuesplitBtnExecute_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (isrun == true)
                {
                    return;
                }
                isrun = true;
                foreach (var value in cueSpliter.Execute())
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            CuesplitProgressStatus.Value = value;
                        }));
                    }
                    else
                    {
                        CuesplitProgressStatus.Value = value;
                    }
                }
                isrun = false;
            });
            
        }

        private void CuesplitListStatus_DragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path in items)
            {
                var t = Path.GetExtension(path).ToLower();
                if (t == ".cue")
                {
                    cueSpliter.AddCueFile(path
                        , Path.GetDirectoryName(path) + @"\" + Path.GetFileNameWithoutExtension(path) + ".wav"
                        , Path.GetDirectoryName(path) + @"\");
                    CuesplitListStatus.Items.Add(new ListViewItem(new[] { Path.GetFileName(path) }));
                }
            }
        }

        private void CuesplitListStatus_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            Core.Comparer b = new Core.Comparer();
            b.IsEqual(@"D:\data\1.mp3", @"D:\data\2.mp3");

        }
    }
}
