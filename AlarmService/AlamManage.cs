using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public class AlarmManage
    {
        public int Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
                ChangeFormSize();
                ChangeDeskLocation();
                ChangeControlLocation();
            }
        }
        private int _Size = 0;
        private List<Alarm> AlamList = new List<Alarm>();
        public Point Padding
        {
            get
            {
                return _Padding;
            }
            set
            {
                _Padding = Padding;
                ChangeDeskLocation();
            }
        }
        private Point _Padding = new Point(30, 10);
        Form Form = null;


        private void ChangeFormSize()
        {
            Form.Size = new Size(Form.Size.Width,
                                200 * (AlamList.Count + 1));
        }
        private void ChangeDeskLocation()
        {
            Rectangle pt = Screen.PrimaryScreen.WorkingArea;
            Form.SetDesktopLocation(pt.Width - this.Form.Width - _Padding.X, pt.Height - Form.Size.Height - _Padding.Y);
        }
        private void ChangeControlLocation()
        {
            int v_Y = Size;
            int v_X = Form.Size.Width;
            for (int i = 0; i < AlamList.Count; i++)
            {
                if (AlamList[i].Location.Y != Form.Size.Height - ((i + 1) * v_Y))
                    AlamList[i].Location = new Point(0, Form.Size.Height - ((i + 1) * v_Y));

                AlamList[i].Size = new Size(Form.Size.Width, v_Y);
            }
        }

        private void Remove(object sender, AlarmStruct AlamStruct)
        {
            AlamList.Remove(sender as Alarm);

            try
            {
                ChangeFormSize();
                ChangeDeskLocation();
                ChangeControlLocation();

                (sender as Control).Dispose();
            }
            catch (Exception)
            {
                Form.Invoke(new MethodInvoker(() =>
                {
                    ChangeFormSize();
                    ChangeDeskLocation();
                    ChangeControlLocation();

                    (sender as Control).Dispose();
                }));
            }

            
        }

        public AlarmManage()
        {
            Form = new Form();
            Form.Show();
            Size = 150;
            Form.BackColor = Color.FromArgb(254, 254, 254);
            Form.FormBorderStyle = FormBorderStyle.None;
            Form.TransparencyKey = Color.FromArgb(254, 254, 254);
            Form.ShowInTaskbar = false;
            Form.TopMost = true;
        }

        public void Add(AlarmStruct alarmStruct)
        {
            Alarm alarm = new Alarm(alarmStruct);
            
            alarm.LifeTimeEnd += Remove;
            AlamList.Add(alarm);
            try
            {
                Form.Controls.Add(alarm);

                ChangeFormSize();
                ChangeDeskLocation();
                ChangeControlLocation();
            }
            catch (Exception)
            {
                Form.Invoke(new MethodInvoker(() =>
                {
                    Form.Controls.Add(alarm);

                    ChangeFormSize();
                    ChangeDeskLocation();
                    ChangeControlLocation();
                }));
            }
            
        }

    }



    
}
