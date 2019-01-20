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
using Tag.Core.Extension;

namespace Tag
{
    public partial class Form1 : MaterialForm
    {
        readonly MaterialSkinManager materialSkinManager;
        readonly Core.CueSpliter cueSpliter = new Core.CueSpliter();
        readonly Core.Wav2Mp3Converter wav2Mp3Converter = new Core.Wav2Mp3Converter();
        readonly Core.Mp3Tagging mp3Tagging = new Core.Mp3Tagging();
        TagLib.Tag tagTemp;

        public Form1()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            Log.FilePrepare(string.Empty);
            Log.FileWrite("Run", Core.Extension.Error.None);
        }

        private void CuesplitBtnOpenDialog_Click(object sender, EventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                AddExtension = true,
                Filter = "Cue 파일|*.cue",
                CheckPathExists = true
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (fileDialog.FileNames.Length != 0)
                {
                    foreach (var path in fileDialog.FileNames)
                    {
                        CuesplitListStatus.Items.Add(new ListViewItem(new[] { Path.GetFileName(path) }));

                        cueSpliter.AddFile(path);
                        Log.FileWrite($"{path}", Error.None);
                    }
                }
            }
            Log.FileWrite("End", Error.Success);
        }

        #region Exec Program
        volatile bool isrun = false;
        private async void CuesplitBtnExecute_Click(object sender, EventArgs e)
        {
            Log.FileWrite("Start", Error.None);
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
                Log.FileWrite("End", Error.Success);
                isrun = false;
            });
        }

        private async void Mp3ConvBtnExec_Click(object sender, EventArgs e)
        {
            Log.FileWrite("Start", Error.None);
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
                Log.FileWrite("End", Error.Success);
                isrun = false;
            });
        }
        private async void TaggingBtnExec_Click(object sender, EventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            await Task.Run(() =>
            {
                if (isrun == true)
                {
                    return;
                }
                isrun = true;
                foreach (var value in mp3Tagging.Execute())
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
                Log.FileWrite("End", Error.Success);
                isrun = false;
            });
        }
        #endregion


        #region Drag Enter & Drop
        private void DragEnters(object sender, DragEventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            e.Effect = DragDropEffects.Copy;
            Log.FileWrite("End", Error.Success);
        }

        private void CuesplitListStatus_DragDrop(object sender, DragEventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path in items)
            {
                var t = Path.GetExtension(path).ToLower();
                if (t == ".cue")
                {
                    Log.FileWrite("{t}", Error.Success);
                    cueSpliter.AddFile(path
                        , Path.GetDirectoryName(path) + @"\" + Path.GetFileNameWithoutExtension(path) + ".wav"
                        , Path.GetDirectoryName(path) + @"\");
                    CuesplitListStatus.Items.Add(new ListViewItem(new[] { Path.GetFileName(path) }));
                }
            }
            Log.FileWrite("End", Error.Success);
        }
        private void Mp3ConvListStatus_DragDrop(object sender, DragEventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path in items)
            {
                var t = Path.GetExtension(path).ToLower();
                if (t == ".wav")
                {
                    Log.FileWrite("{t}", Error.Success);
                    wav2Mp3Converter.AddFile(path);
                    Mp3ConvListStatus.Items.Add(new ListViewItem(new[] { Path.GetFileName(path) }));
                }
            }
            Log.FileWrite("End", Error.Success);
        }
        private void TaggingListFile_DragDrop(object sender, DragEventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path in items)
            {
                var t = Path.GetExtension(path).ToLower();
                if (t == ".mp3")
                {
                    Log.FileWrite("{t}", Error.Success);
                    TaggingListFile.Items.Add(new ListViewItem(new[] { path }));
                }
            }
            Log.FileWrite("End", Error.Success);
        }

        private void TaggingListFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            try
            {
                tagTemp = TagLib.File.Create(TaggingListFile.SelectedItems[0].Text).Tag;
            }
            catch (Exception)
            {
                Log.FileWrite("fatal", Error.Error);
            }
            Log.FileWrite("End", Error.Success);
        }

        private void TaggingBtnTagSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TaggingListFile.SelectedIndices.Count != 0)
                {
                    var t = TaggingListFile.SelectedItems[0].Text;
                    mp3Tagging.AddFile((t, tagTemp));
                }
            }
            catch (Exception)
            {

            }
        }
        
        private void TaggingListTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (TaggingListTag.SelectedIndices[0])
                {
                    case 0:
                        TaggingTextInfo.Text = tagTemp.Title;
                        break;
                    case 1:
                        TaggingTextInfo.Text = tagTemp.Performers[0];
                        break;
                    case 2:
                        TaggingTextInfo.Text = tagTemp.Album;
                        break;
                    case 3:
                        TaggingTextInfo.Text = tagTemp.Year.ToString();
                        break;
                    case 4:
                        // tagTemp.Track = TaggingTextInfo.Text;
                        break;
                    case 5:
                        // tagTemp.TrackCount = TaggingTextInfo.Text;
                        break;
                    case 6:
                        TaggingTextInfo.Text = tagTemp.Genres[0];
                        break;
                    case 7:
                        TaggingTextInfo.Text = tagTemp.Comment;
                        break;
                    case 8:
                        TaggingTextInfo.Text = tagTemp.AlbumArtists[0];
                        break;
                    case 9:
                        TaggingTextInfo.Text = tagTemp.Composers[0];
                        break;
                    case 10:
                        TaggingTextInfo.Text = tagTemp.Disc.ToString();
                        break;
                    case 11:
                        TaggingTextInfo.Text = tagTemp.Pictures[0] == null ? "" : "존재함";
                        break;
                }
            }
            catch (Exception)
            {
                TaggingTextInfo.Text = "";
            }
        }

        private void TaggingTextInfo_KeyDown(object sender, KeyEventArgs e)
        {
            try {
                if (e.KeyCode == Keys.Enter)
                {
                    switch (TaggingListTag.SelectedIndices[0])
                    {
                        case 0:
                            tagTemp.Title = TaggingTextInfo.Text;
                            break;
                        case 1:
                            tagTemp.Performers = new string[1] { TaggingTextInfo.Text };
                            break;
                        case 2:
                            tagTemp.Album = TaggingTextInfo.Text;
                            break;
                        case 3:
                            tagTemp.Year = uint.Parse(TaggingTextInfo.Text);
                            break;
                        case 4:
                            // tagTemp.Track = TaggingTextInfo.Text;
                            break;
                        case 5:
                            // tagTemp.TrackCount = TaggingTextInfo.Text;
                            break;
                        case 6:
                            tagTemp.Genres = new string[1] { TaggingTextInfo.Text };
                            break;
                        case 7:
                            tagTemp.Comment = TaggingTextInfo.Text;
                            break;
                        case 8:
                            tagTemp.AlbumArtists = new string[1] { TaggingTextInfo.Text };
                            break;
                        case 9:
                            tagTemp.Composers = new string[1] { TaggingTextInfo.Text };
                            break;
                        case 10:
                            tagTemp.Disc = uint.Parse(TaggingTextInfo.Text);
                            break;
                        case 11:
                            tagTemp.Pictures = new TagLib.IPicture[1] { new TagLib.Picture(TaggingTextInfo.Text) };
                            break;
                    }
                    MessageBox.Show("Complete");
                }
            }catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }
        #endregion

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            if (e.KeyCode == Keys.Delete)
            {
                foreach (int value in (sender as ListView).SelectedIndices)
                {
                    (sender as ListView).Items.RemoveAt(value);
                    if ((sender as ListView).Name == "CuesplitListStatus")
                    {
                        cueSpliter.Delete(value);
                    }
                    else if ((sender as ListView).Name == "Mp3ConvListStatus")
                    {
                        wav2Mp3Converter.Delete(value);
                    }
                    else
                    {
                        mp3Tagging.Delete(value);
                    }
                    Log.FileWrite($"{value}, {(sender as ListView).Name}", Error.None);
                }
            }
            Log.FileWrite("Start", Error.None);
        }

    }
}
