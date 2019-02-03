using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag
{
    partial class Audio2Tag
    {
        void TagLoad()
        {
            TagListStatus.Font = new Font(TagListStatus.Font.FontFamily, 11.25f);

            TagLabelTitle.Text = Setting.Global.Language.Title;
            TagLabelArtist.Text = Setting.Global.Language.Artist;
            TagLabelAlbum.Text = Setting.Global.Language.Album;
            TagLabelComment.Text = Setting.Global.Language.Comment;
            TagLabelGenre.Text = Setting.Global.Language.Genre;
            TagLabelYear.Text = Setting.Global.Language.Year;
            TagLabelTrack.Text = Setting.Global.Language.Track;
            TagLabelDiscNum.Text = Setting.Global.Language.DiscNum;
            TagLabelAlbumArtist.Text = Setting.Global.Language.AlbumArtist;
            TagLabelComposer.Text = Setting.Global.Language.Composer;
            TagLabelAlbumArt.Text = Setting.Global.Language.AlbumArt;

            TagTab.Text = Setting.Global.Language.Tagging;


            TagColumnFileName.Text = Setting.Global.Language.Artist;
            TagColumnTitle.Text = Setting.Global.Language.Title;
            TagColumnPath.Text = Setting.Global.Language.Path;
            TagColumnTagType.Text = Setting.Global.Language.Tag;
            TagColumnArtist.Text = Setting.Global.Language.Artist;
            TagColumnAlbumArtist.Text = Setting.Global.Language.AlbumArtist;
            TagColumnAlbum.Text = Setting.Global.Language.Album;
            TagColumnTrack.Text = Setting.Global.Language.Track;
            TagColumnDiscNum.Text = Setting.Global.Language.DiscNum;
            TagColumnYear.Text = Setting.Global.Language.Year;
            TagColumnGenre.Text = Setting.Global.Language.Genre;
            TagColumnComment.Text = Setting.Global.Language.Comment;
            TagColumnCodec.Text = Setting.Global.Language.Codec;
            TagColumnBitrate.Text = Setting.Global.Language.Bitrate;
            TagColumnLength.Text = Setting.Global.Language.Length;
            TagColumnFixed.Text = Setting.Global.Language.Fixed;


        }


    }
}
