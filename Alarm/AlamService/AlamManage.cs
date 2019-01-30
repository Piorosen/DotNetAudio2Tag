using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public class AlamManage
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
        private List<Alam> AlamList = new List<Alam>();
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

        private void Remove(object sender, AlamStruct AlamStruct)
        {
            AlamList.Remove(sender as Alam);

            try
            {
                Form.Invoke(new MethodInvoker(() =>
                {
                    ChangeFormSize();
                    ChangeDeskLocation();
                    ChangeControlLocation();

                    (sender as Control).Dispose();
                }));
            }
            catch (Exception)
            {
                ChangeFormSize();
                ChangeDeskLocation();
                ChangeControlLocation();

                (sender as Control).Dispose();
            }

            
        }

        public AlamManage(Form _Form)
        {
            Form = _Form;
            Size = 200;
            Form.BackColor = Color.FromArgb(254, 254, 254);
            Form.FormBorderStyle = FormBorderStyle.None;
            Form.TransparencyKey = Color.FromArgb(254, 254, 254);
            Form.ShowInTaskbar = false;
            Form.TopMost = true;
        }

        public void Add(AlamStruct AlamStruct)
        {
            Alam alam = new Alam(AlamStruct);

            alam.LifeTimeEnd += Remove;
            AlamList.Add(alam);
            try
            {
                Form.Invoke(new MethodInvoker(() =>
                {
                    Form.Controls.Add(alam);

                    ChangeFormSize();
                    ChangeDeskLocation();
                    ChangeControlLocation();
                }));
            }
            catch (Exception)
            {
                Form.Controls.Add(alam);

                ChangeFormSize();
                ChangeDeskLocation();
                ChangeControlLocation();
            }
            
        }

    }



    public class AlamStruct
    {
        public AlamStruct(string Title, string Body, float LifeTime = -1.0f)
        {
            this.Title = Title;
            this.Body = Body;
            this.LifeTime = LifeTime;
        }

        /// <summary>
        /// 알람의 제목을 설정합니다.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 알람의 주 내용을 설정합니다.
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// 이미지 경로설정.
        /// </summary>
        public string ImagePath { get; set; } = string.Empty;

        /// <summary>
        /// 백컬러 지정.
        /// </summary>
        public Color BackColor { get; set; } = Color.Empty;

        /// <summary>
        /// 이미지 스타일 지정.
        /// </summary>
        public BorderStyle BorderStyle { get; set; } = BorderStyle.None;

        /// <summary>
        /// 표기할 시간을 지정합니다. ( -1.0f => infinity )
        /// </summary>
        public float LifeTime { get; set; } = 0.0f;

    }
}
