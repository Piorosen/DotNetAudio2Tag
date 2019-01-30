using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public class AlarmStruct
    {
        public AlarmStruct(string Title, string Body, float LifeTime = -1.0f)
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
