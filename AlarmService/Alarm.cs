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
    delegate void LifeTimeEvent(object sender, AlarmStruct alarmStruct);
    partial class Alarm : UserControl
    {
        public event LifeTimeEvent LifeTimeEnd;
        private void OnLifeTimeEnd(AlarmStruct alarmStruct)
        {
            LifeTimeEnd?.Invoke(this, alarmStruct);
        }

        AlarmStruct alarmStruct;

        public Alarm(AlarmStruct alarm)
        {
            InitializeComponent();

            alarmStruct = alarm;

            alarmStruct = alarm;
            Label_Title.Text = alarmStruct.Title;
            Label_Body.Text = alarmStruct.Body;

            if (alarm.BackColor != Color.Empty)
                Panel_Main.BackColor = alarmStruct.BackColor;

            if (alarm.ImagePath != string.Empty && new FileInfo(alarm.ImagePath).Exists)
            {
                try
                {
                    Picture_Image.Image = Image.FromFile(alarmStruct.ImagePath);
                    Label_Title.Size = new Size(Label_Title.Size.Width - Picture_Image.Size.Width - 10, Label_Title.Size.Height);
                    Label_Body.Size = new Size(Label_Body.Size.Width - Picture_Image.Size.Width - 10, Label_Body.Size.Height);
                }
                catch (Exception)
                {
                }
            }

            if (alarm.BorderStyle != Picture_Image.BorderStyle)
                Picture_Image.BorderStyle = alarmStruct.BorderStyle;

            Timer.Start();
        }

        private void Panel_Main_Paint(object sender, PaintEventArgs e)
        {
            var g = (sender as Control);
            float Size = 5;
            g.CreateGraphics().DrawRectangle(new Pen(Color.Chartreuse, Size), Size / 2, Size / 2, g.Size.Width - Size, g.Size.Height - Size);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (alarmStruct.LifeTime == -1) return;
            alarmStruct.LifeTime -= Timer.Interval / 1000.0f;
            if (alarmStruct.LifeTime <= 0)
            {
                Timer.Stop();
                OnLifeTimeEnd(alarmStruct);
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            OnLifeTimeEnd(alarmStruct);
        }
    }
}
