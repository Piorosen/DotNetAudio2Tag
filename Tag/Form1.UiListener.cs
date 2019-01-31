using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tag.Core.Extension;

namespace Tag
{
	/// <summary>
    /// UI Relative
    /// </summary>
    partial class Audio2Tag
    {

        private void pp(object sender, EventArgs e)
        {
            materialSkinManager.Theme = (sender as CheckBox).Checked
                                            ? MaterialSkin.MaterialSkinManager.Themes.DARK
                                            : MaterialSkin.MaterialSkinManager.Themes.LIGHT; 
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
    }
}
