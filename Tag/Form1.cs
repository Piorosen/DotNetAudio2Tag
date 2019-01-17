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
        readonly MaterialSkinManager materialSkinManager;
        readonly Core.CueSpliter cueSpliter = new Core.CueSpliter();
        readonly Core.Wav2Mp3Converter wav2Mp3Converter = new Core.Wav2Mp3Converter();

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

                        cueSpliter.AddFile(path);
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
                    cueSpliter.AddFile(path
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
       
        private void Mp3ConvListStatus_DragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path in items)
            {
                var t = Path.GetExtension(path).ToLower();
                if (t == ".wav")
                {
                    wav2Mp3Converter.AddFile(path);
                    Mp3ConvListStatus.Items.Add(new ListViewItem(new[] { Path.GetFileName(path) }));
                }
            }
        }

        private void Mp3ConvListStatus_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private async void Mp3ConvBtnExec_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (isrun == true)
                {
                    return;
                }
                isrun = true;
                foreach (var value in wav2Mp3Converter.Execute())
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            Mp3ConvProgressStatus.Value = value;
                        }));
                    }
                    else
                    {
                        Mp3ConvProgressStatus.Value = value;
                    }
                }
                isrun = false;
            });
        }
        

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (int value in (sender as ListView).SelectedIndices)
                {
                    (sender as ListView).Items.RemoveAt(value);
                    if ((sender as ListView).Name == "CuesplitListStatus")
                    {
                        cueSpliter.Delete(value);
                    }
                    else
                    {
                        wav2Mp3Converter.Delete(value);
                    }
                    
                }
            }
        }
    }
}
