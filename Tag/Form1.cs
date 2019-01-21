using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        Tag.Core.TagInfo tagTemp;

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
                            TaggingProgressStatus.Value = value;
                        }));
                    }
                    else
                    {
                        TaggingProgressStatus.Value = value;
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
                    Log.FileWrite($"{t}", Error.Success);
                    try
                    {
                        mp3Tagging.AddFile(new Core.TagInfo(TagLib.File.Create(path).Tag) { Path = path });
                        TaggingListFile.Items.Add(new ListViewItem(new[] { path }));
                    }
                    catch (Exception)
                    {
                        Log.FileWrite("path fatal", Error.IOException);
                    }
                }
            }
            Log.FileWrite("End", Error.Success);
        }

        private void TaggingListFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            try
            {   if (TaggingListFile.SelectedIndices.Count == 0)
                {
                    Console.WriteLine("0 임");
                    return;
                }
                tagTemp = mp3Tagging[TaggingListFile.SelectedIndices[0]];
                GetTextTagging();
            }
            catch (Exception)
            {
                Log.FileWrite("fatal", Error.Error);
            }
            Log.FileWrite("End", Error.Success);
        }

        private void TaggingTextInfo_KeyDown(object sender, KeyEventArgs e)
        {
            try {
                if (e.KeyCode == Keys.Enter)
                {
                    SetTextTagging();
                    MessageBox.Show("Complete");
                }
            }catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }
        #endregion

        private void TaggingBtnPrevImage_Click(object sender, EventArgs e)
        {
            int now, max;
            now = int.Parse(TaggingLabelIndex.Text.Split('/')[0]);
            max = int.Parse(TaggingLabelIndex.Text.Split('/')[1]);
            now -= 1;
            if (now <= 0)
            {
                return;
            }
            SetCoverImage(now - 1, max);
            
        }
        private void TaggingBtnNextImage_Click(object sender, EventArgs e)
        {
            int now, max;
            now = int.Parse(TaggingLabelIndex.Text.Split('/')[0]);
            max = int.Parse(TaggingLabelIndex.Text.Split('/')[1]);
            now += 1;
            if (now > max)
            {
                return;
            }
            SetCoverImage(now - 1, max);
        }

        void SetCoverImage(int index, int maxIndex)
        {
            if (tagTemp.Image.Count > 0)
            {
                var bin = (byte[])(tagTemp.Image[index].Data.Data);
                var image = Image.FromStream(new MemoryStream(bin));
                TaggingImageList.Image = image;
                TaggingLabelImageSize.Text = $"{image.Width} x {image.Height}";
                TaggingLabelFileSize.Text = CapacityConverter.Change(bin.LongLength);
                TaggingLabelMime.Text = $"Image/{ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.FormatID == image.RawFormat.Guid).FilenameExtension.Split(';')[0]}";
                TaggingLabelIndex.Text = $"{index + 1} / {maxIndex}";
            }
        }


        void SetTextTagging()
        {
            tagTemp.Title = TaggingTextTitle.Text;
            tagTemp.Artist = TaggingTextArtists.Text.Split(';').ToList();
            tagTemp.Album = TaggingTextAlbum.Text;
            tagTemp.Year = uint.Parse(TaggingTextCreateYear.Text);
            tagTemp.Track = uint.Parse(TaggingTextTrack.Text);
            tagTemp.Genre = TaggingTextGenre.Text.Split(';').ToList();
            tagTemp.Comment = TaggingTextComment.Text;
            tagTemp.AlbumArtist = TaggingTextAlbumArtists.Text.Split(';').ToList();
            tagTemp.Composer = TaggingTextComposers.Text.Split(';').ToList();
            tagTemp.DiscNum = TaggingTextDiscNum.Text;
            // tagTemp.Path = TaggingTextDirectory.Text;
        }

        void GetTextTagging()
        {
            TaggingTextTitle.Text = tagTemp.Title;
            TaggingTextArtists.Text = string.Join(";", tagTemp.Artist);
            TaggingTextAlbum.Text = tagTemp.Album;
            TaggingTextCreateYear.Text = tagTemp.Year.ToString();
            TaggingTextTrack.Text = tagTemp.Track.ToString();
            TaggingTextGenre.Text = string.Join(";", tagTemp.Genre);
            TaggingTextComment.Text = tagTemp.Comment;
            TaggingTextAlbumArtists.Text = string.Join(";", tagTemp.AlbumArtist);
            TaggingTextComposers.Text = string.Join(";", tagTemp.Composer);
            TaggingTextDiscNum.Text = tagTemp.DiscNum;
            TaggingTextDirectory.Text = tagTemp.Path;

            if (tagTemp.Image.Count > 0)
            {
                SetCoverImage(0, tagTemp.Image.Count);
            }
            
        }

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
