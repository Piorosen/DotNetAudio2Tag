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
    }
}
