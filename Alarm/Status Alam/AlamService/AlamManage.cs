using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    class AlamManage
    {
        Control Form = null;

        public AlamManage(Control Form)
        {
            this.Form = Form;
        }

        private List<Alam> AlamList = new List<Alam>();

        public void Add(AlamStruct AlamStruct)
        {

            Form.Size = new Size(Form.Size.Width,
                                Form.Size.Height / AlamList.Count + Form.Size.Height);


            Alam alam = new Alam(AlamStruct);
            alam.LifeTimeEnd += Remove;
            AlamList.Add(alam);

            
            for (int i = 0; i < AlamList.Count; i++)
            {
                
            }

        }

        private void Remove(object sender, AlamStruct AlamStruct)
        {
            AlamList.Remove(sender as Alam);


            (sender as Control).Dispose();
        }
        
    }



    public class AlamStruct
    {
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

        public BorderStyle borderStyle { get; set; } = BorderStyle.None;

        /// <summary>
        /// 표기할 시간을 지정합니다. ( 0.0f => infinity )
        /// </summary>
        public float LifeTime { get; set; } = 0.0f;

    }
}
