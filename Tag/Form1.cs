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
using Tag.Core;
using Tag.Core.Conv;
using Tag.Core.Cue;
using Tag.Core.Extension;
using Tag.Core.Tagging;

namespace Tag
{
    public partial class Form1 : MaterialForm
    {
        readonly MaterialSkinManager materialSkinManager;
        readonly CueSpliter cueSpliter = new CueSpliter();
        readonly Wav2Mp3Converter wav2Mp3Converter = new Wav2Mp3Converter();
        readonly Mp3Tagging mp3Tagging = new Mp3Tagging();
        readonly AutoConverter autoConv = new AutoConverter();
        TagInfo tagTemp;

        public Form1()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            (TaggingImageList as Control).AllowDrop = true;
            Log.FilePrepare(string.Empty);
            Log.FileWrite("Run", Core.Extension.Error.None);
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
                    cueSpliter.AddFile(path);
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
                        mp3Tagging.AddFile(new TagInfo(TagLib.File.Create(path).Tag) { Path = path });
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


        private void TaggingImageList_DragDrop(object sender, DragEventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path in items)
            {
                try
                {
                    TagLib.Picture picture = new TagLib.Picture(path);
                    int now = int.Parse(TaggingLabelIndex.Text.Split('/')[0]);
                    int max = int.Parse(TaggingLabelIndex.Text.Split('/')[1]);
                    tagTemp.Image.Insert(now, picture);
                    SetCoverImage(now, tagTemp.Image.Count);
                    Log.FileWrite($"Check Vailidate", Error.Success);
                }
                catch (Exception)
                {
                    Log.FileWrite("Not Image File, Check Vailidate", Error.IOException);
                }
                    
            }
            Log.FileWrite("End", Error.Success);
        }
        #endregion

        private void TaggingListFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.FileWrite("Start", Error.None);
            try
            {
                if (TaggingListFile.SelectedIndices.Count == 0)
                {
                    Console.WriteLine("0 임");
                    return;
                }
                tagTemp = mp3Tagging.List()[TaggingListFile.SelectedIndices[0]];
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
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SetTextTagging();
                    MessageBox.Show("Complete");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

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
                if (0 > index)
                {
                    index = 0;
                }else if (index >= maxIndex)
                {
                    index = maxIndex - 1;
                }
                var bin = tagTemp.Image[index].Data.Data;
                var image = Image.FromStream(new MemoryStream(bin));
                TaggingImageList.Image = image;
                TaggingLabelImageSize.Text = $"{image.Width} x {image.Height}";
                TaggingLabelFileSize.Text = CapacityConverter.Change(bin.LongLength);
                TaggingLabelMime.Text = $"Image/{ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.FormatID == image.RawFormat.Guid).FilenameExtension.Split(';')[0]}";
                TaggingLabelIndex.Text = $"{index + 1} / {maxIndex}";
            }
            else
            {
                TaggingImageList.Image = null;
                TaggingLabelImageSize.Text = $"{0} x {0}";
                TaggingLabelFileSize.Text = CapacityConverter.Change(0);
                TaggingLabelMime.Text = $"Mime/Type";
                TaggingLabelIndex.Text = $"{0} / {0}";
            }
        }

        private void TaggingBtnImageDelete_Click(object sender, EventArgs e)
        {
            int now = int.Parse(TaggingLabelIndex.Text.Split('/')[0]);
            tagTemp.Image.RemoveAt(now - 1);
            SetCoverImage(now - 1, tagTemp.Image.Count);
        }


        void SetTextTagging()
        {
            tagTemp.Title = TaggingTextTitle.Text;
            tagTemp.Artist = TaggingTextArtists.Text.Split(';').ToList();
            tagTemp.Album = TaggingTextAlbum.Text;
            tagTemp.Year = TaggingTextCreateYear.Text;
            tagTemp.Track.Add(uint.Parse(TaggingTextTrack.Text));
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
            
            SetCoverImage(0, tagTemp.Image.Count);
            
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

        private void Form1_Load(object sender, EventArgs e)
        {



        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {

        }

        private void AutoBtnExec_Click(object sender, EventArgs e)
        {
            Core.Tagging.Library.MusicDb db = new Core.Tagging.Library.MusicDb();
            db.GetTrackInfo(new TagInfo
            {
                Identifier = "72867",
                Lang = "en"
            });



            //var list = autoConv.AutoConverting(AutoTextCuepath.Text, AutoTextWavpath.Text, AutoTextMp3path.Text, AutoTextWorkDir.Text);
            //if (list != null)
            //{
            //    foreach (var item in list)
            //    {
            //        AutoListStatus.Items.Add(
            //            new ListViewItem(
            //                new string[]
            //                {
            //                    item.Score.ToString(),
            //                    item.Title,
            //                    string.Join(", ", item.Artist),
            //                    item.Album,
            //                    string.Join(", ", item.Track),
            //                    item.Country,
            //                    string.Join(", ", item.Format),
            //                    string.Join(", ", item.Publisher),
            //                    string.Join(", ", item.CatNo)
            //                }));
                            
            //    }
            //}
        }

        private void AutoListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (sender is ListView)
            //{
            //    if ((sender as ListView).SelectedIndices.Count != 0)
            //    {
            //        autoConv.SelectBrainzIndex((sender as ListView).SelectedIndices[0]);
            //    }
            //    autoConv.Execute();
            //}
        }

        private void CuesplitListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ListView)
            {
                if ((sender as ListView).SelectedIndices.Count != 0)
                {
                    var t = (sender as ListView).SelectedIndices[0];
                    var data = cueSpliter[t];
                    CuesplitTextTitle.Text = data.Title;
                    CuesplitTextArtist.Text = data.Artist;
                    CuesplitTextGenre.Text = data.REM.Genre;
                    CuesplitTextTrackCount.Text = data.Track.Count.ToString();
                    
                    CuesplitTextBarcode.Text = data.Barcode;
                    CuesplitTextSavePath.Text = data.SavePath;
                    CuesplitTextWavPath.Text = data.WavPath;
                    CuesplitTextCuePath.Text = data.Path;
                }
            }
        }
    }
}
