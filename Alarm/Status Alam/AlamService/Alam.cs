using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Library
{
    public delegate void LifeTimeEvent(object sender, AlamStruct AlamStruct);
    public partial class Alam : UserControl
    {
        public event LifeTimeEvent LifeTimeEnd;
        private void OnLifeTimeEnd(AlamStruct alamStruct)
        {
            LifeTimeEnd?.Invoke(this, alamStruct);
        }

        AlamStruct AlamStruct = new AlamStruct();

        public Alam(AlamStruct alam)
        {
            InitializeComponent();

            AlamStruct = alam;

            AlamStruct = alam;
            Label_Title.Text = AlamStruct.Title;
            Label_Body.Text = AlamStruct.Body;

            if (alam.BackColor != Color.Empty)
                Panel_Main.BackColor = AlamStruct.BackColor;

            if (alam.ImagePath != string.Empty && !(new FileInfo(alam.ImagePath).Exists))
                Picture_Image.Image = Image.FromFile(AlamStruct.ImagePath);

            if (alam.borderStyle != Picture_Image.BorderStyle)
                Picture_Image.BorderStyle = AlamStruct.borderStyle;
        }

        private void Panel_Main_Paint(object sender, PaintEventArgs e)
        {
            var g = (sender as Control);
            float Size = 5;
            g.CreateGraphics().DrawRectangle(new Pen(Color.Beige, Size), Size / 2, Size / 2, g.Size.Width - Size, g.Size.Height - Size);
        }
    }
}
