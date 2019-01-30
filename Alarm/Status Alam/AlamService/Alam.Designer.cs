namespace Library
{
    partial class Alam
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.Panel_Main = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Picture_Image = new System.Windows.Forms.PictureBox();
            this.Label_Body = new System.Windows.Forms.Label();
            this.Label_Title = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Panel_Main.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_Main
            // 
            this.Panel_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Main.BackColor = System.Drawing.Color.OldLace;
            this.Panel_Main.Controls.Add(this.panel1);
            this.Panel_Main.Controls.Add(this.button1);
            this.Panel_Main.Location = new System.Drawing.Point(10, 10);
            this.Panel_Main.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Main.Name = "Panel_Main";
            this.Panel_Main.Size = new System.Drawing.Size(491, 223);
            this.Panel_Main.TabIndex = 0;
            this.Panel_Main.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Main_Paint);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.Picture_Image);
            this.panel1.Controls.Add(this.Label_Body);
            this.panel1.Controls.Add(this.Label_Title);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 199);
            this.panel1.TabIndex = 2;
            // 
            // Picture_Image
            // 
            this.Picture_Image.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Picture_Image.Location = new System.Drawing.Point(300, 8);
            this.Picture_Image.Name = "Picture_Image";
            this.Picture_Image.Size = new System.Drawing.Size(127, 182);
            this.Picture_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Picture_Image.TabIndex = 2;
            this.Picture_Image.TabStop = false;
            // 
            // Label_Body
            // 
            this.Label_Body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Body.Location = new System.Drawing.Point(11, 33);
            this.Label_Body.Name = "Label_Body";
            this.Label_Body.Size = new System.Drawing.Size(278, 152);
            this.Label_Body.TabIndex = 1;
            this.Label_Body.Text = "내용 : ";
            // 
            // Label_Title
            // 
            this.Label_Title.Location = new System.Drawing.Point(11, 11);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(41, 12);
            this.Label_Title.TabIndex = 0;
            this.Label_Title.Text = "제목 : ";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.OldLace;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.OldLace;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(453, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Alam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.Panel_Main);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Alam";
            this.Size = new System.Drawing.Size(511, 243);
            this.Panel_Main.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Main;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Picture_Image;
        private System.Windows.Forms.Label Label_Body;
        private System.Windows.Forms.Label Label_Title;
    }
}
