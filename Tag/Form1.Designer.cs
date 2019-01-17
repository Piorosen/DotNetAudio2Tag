namespace Tag
{
    partial class Form1
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.TabControl = new MaterialSkin.Controls.MaterialTabControl();
            this.CuesplitTab = new System.Windows.Forms.TabPage();
            this.CuesplitProgressStatus = new MaterialSkin.Controls.MaterialProgressBar();
            this.CuesplitBtnOpenDialog = new MaterialSkin.Controls.MaterialRaisedButton();
            this.CuesplitTextCuePath = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.CuesplitBtnImport = new MaterialSkin.Controls.MaterialRaisedButton();
            this.CuesplitBtnExecute = new MaterialSkin.Controls.MaterialRaisedButton();
            this.CuesplitListStatus = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ConvMp3Tab = new System.Windows.Forms.TabPage();
            this.Mp3ConvProgressStatus = new MaterialSkin.Controls.MaterialProgressBar();
            this.Mp3ConvListStatus = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mp3ConvBtnExec = new MaterialSkin.Controls.MaterialRaisedButton();
            this.TaggingTab = new System.Windows.Forms.TabPage();
            this.TabControl.SuspendLayout();
            this.CuesplitTab.SuspendLayout();
            this.ConvMp3Tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabSelector1.BaseTabControl = this.TabControl;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 64);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(800, 23);
            this.materialTabSelector1.TabIndex = 0;
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.CuesplitTab);
            this.TabControl.Controls.Add(this.ConvMp3Tab);
            this.TabControl.Controls.Add(this.TaggingTab);
            this.TabControl.Depth = 0;
            this.TabControl.Location = new System.Drawing.Point(12, 93);
            this.TabControl.MouseState = MaterialSkin.MouseState.HOVER;
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(776, 345);
            this.TabControl.TabIndex = 1;
            // 
            // CuesplitTab
            // 
            this.CuesplitTab.BackColor = System.Drawing.SystemColors.Control;
            this.CuesplitTab.Controls.Add(this.CuesplitProgressStatus);
            this.CuesplitTab.Controls.Add(this.CuesplitBtnOpenDialog);
            this.CuesplitTab.Controls.Add(this.CuesplitTextCuePath);
            this.CuesplitTab.Controls.Add(this.CuesplitBtnImport);
            this.CuesplitTab.Controls.Add(this.CuesplitBtnExecute);
            this.CuesplitTab.Controls.Add(this.CuesplitListStatus);
            this.CuesplitTab.Location = new System.Drawing.Point(4, 22);
            this.CuesplitTab.Name = "CuesplitTab";
            this.CuesplitTab.Padding = new System.Windows.Forms.Padding(3);
            this.CuesplitTab.Size = new System.Drawing.Size(768, 319);
            this.CuesplitTab.TabIndex = 0;
            this.CuesplitTab.Text = "Cue Split";
            // 
            // CuesplitProgressStatus
            // 
            this.CuesplitProgressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CuesplitProgressStatus.Depth = 0;
            this.CuesplitProgressStatus.Location = new System.Drawing.Point(507, 133);
            this.CuesplitProgressStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.CuesplitProgressStatus.Name = "CuesplitProgressStatus";
            this.CuesplitProgressStatus.Size = new System.Drawing.Size(255, 5);
            this.CuesplitProgressStatus.TabIndex = 5;
            // 
            // CuesplitBtnOpenDialog
            // 
            this.CuesplitBtnOpenDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CuesplitBtnOpenDialog.AutoSize = true;
            this.CuesplitBtnOpenDialog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CuesplitBtnOpenDialog.Depth = 0;
            this.CuesplitBtnOpenDialog.Icon = null;
            this.CuesplitBtnOpenDialog.Location = new System.Drawing.Point(507, 49);
            this.CuesplitBtnOpenDialog.MouseState = MaterialSkin.MouseState.HOVER;
            this.CuesplitBtnOpenDialog.Name = "CuesplitBtnOpenDialog";
            this.CuesplitBtnOpenDialog.Primary = true;
            this.CuesplitBtnOpenDialog.Size = new System.Drawing.Size(108, 36);
            this.CuesplitBtnOpenDialog.TabIndex = 4;
            this.CuesplitBtnOpenDialog.Text = "Open Dialog";
            this.CuesplitBtnOpenDialog.UseVisualStyleBackColor = true;
            this.CuesplitBtnOpenDialog.Click += new System.EventHandler(this.CuesplitBtnOpenDialog_Click);
            // 
            // CuesplitTextCuePath
            // 
            this.CuesplitTextCuePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CuesplitTextCuePath.Depth = 0;
            this.CuesplitTextCuePath.Hint = "( Cue Path )";
            this.CuesplitTextCuePath.Location = new System.Drawing.Point(507, 20);
            this.CuesplitTextCuePath.MaxLength = 32767;
            this.CuesplitTextCuePath.MouseState = MaterialSkin.MouseState.HOVER;
            this.CuesplitTextCuePath.Name = "CuesplitTextCuePath";
            this.CuesplitTextCuePath.PasswordChar = '\0';
            this.CuesplitTextCuePath.SelectedText = "";
            this.CuesplitTextCuePath.SelectionLength = 0;
            this.CuesplitTextCuePath.SelectionStart = 0;
            this.CuesplitTextCuePath.Size = new System.Drawing.Size(255, 23);
            this.CuesplitTextCuePath.TabIndex = 3;
            this.CuesplitTextCuePath.TabStop = false;
            this.CuesplitTextCuePath.UseSystemPasswordChar = false;
            // 
            // CuesplitBtnImport
            // 
            this.CuesplitBtnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CuesplitBtnImport.AutoSize = true;
            this.CuesplitBtnImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CuesplitBtnImport.Depth = 0;
            this.CuesplitBtnImport.Icon = null;
            this.CuesplitBtnImport.Location = new System.Drawing.Point(690, 49);
            this.CuesplitBtnImport.MouseState = MaterialSkin.MouseState.HOVER;
            this.CuesplitBtnImport.Name = "CuesplitBtnImport";
            this.CuesplitBtnImport.Primary = true;
            this.CuesplitBtnImport.Size = new System.Drawing.Size(72, 36);
            this.CuesplitBtnImport.TabIndex = 2;
            this.CuesplitBtnImport.Text = "Import";
            this.CuesplitBtnImport.UseVisualStyleBackColor = true;
            this.CuesplitBtnImport.Click += new System.EventHandler(this.CuesplitBtnImport_Click);
            // 
            // CuesplitBtnExecute
            // 
            this.CuesplitBtnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CuesplitBtnExecute.AutoSize = true;
            this.CuesplitBtnExecute.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CuesplitBtnExecute.Depth = 0;
            this.CuesplitBtnExecute.Icon = null;
            this.CuesplitBtnExecute.Location = new System.Drawing.Point(683, 91);
            this.CuesplitBtnExecute.MouseState = MaterialSkin.MouseState.HOVER;
            this.CuesplitBtnExecute.Name = "CuesplitBtnExecute";
            this.CuesplitBtnExecute.Primary = true;
            this.CuesplitBtnExecute.Size = new System.Drawing.Size(79, 36);
            this.CuesplitBtnExecute.TabIndex = 1;
            this.CuesplitBtnExecute.Text = "Execute";
            this.CuesplitBtnExecute.UseVisualStyleBackColor = true;
            this.CuesplitBtnExecute.Click += new System.EventHandler(this.CuesplitBtnExecute_Click);
            // 
            // CuesplitListStatus
            // 
            this.CuesplitListStatus.AllowDrop = true;
            this.CuesplitListStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CuesplitListStatus.BackColor = System.Drawing.SystemColors.Control;
            this.CuesplitListStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CuesplitListStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.CuesplitListStatus.Depth = 0;
            this.CuesplitListStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.CuesplitListStatus.FullRowSelect = true;
            this.CuesplitListStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.CuesplitListStatus.Location = new System.Drawing.Point(6, 6);
            this.CuesplitListStatus.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CuesplitListStatus.MouseState = MaterialSkin.MouseState.OUT;
            this.CuesplitListStatus.Name = "CuesplitListStatus";
            this.CuesplitListStatus.OwnerDraw = true;
            this.CuesplitListStatus.Size = new System.Drawing.Size(495, 304);
            this.CuesplitListStatus.TabIndex = 0;
            this.CuesplitListStatus.UseCompatibleStateImageBehavior = false;
            this.CuesplitListStatus.View = System.Windows.Forms.View.Details;
            this.CuesplitListStatus.DragDrop += new System.Windows.Forms.DragEventHandler(this.CuesplitListStatus_DragDrop);
            this.CuesplitListStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.CuesplitListStatus_DragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Cue 이름";
            this.columnHeader1.Width = 495;
            // 
            // ConvMp3Tab
            // 
            this.ConvMp3Tab.BackColor = System.Drawing.SystemColors.Control;
            this.ConvMp3Tab.Controls.Add(this.Mp3ConvProgressStatus);
            this.ConvMp3Tab.Controls.Add(this.Mp3ConvListStatus);
            this.ConvMp3Tab.Controls.Add(this.Mp3ConvBtnExec);
            this.ConvMp3Tab.Location = new System.Drawing.Point(4, 22);
            this.ConvMp3Tab.Name = "ConvMp3Tab";
            this.ConvMp3Tab.Padding = new System.Windows.Forms.Padding(3);
            this.ConvMp3Tab.Size = new System.Drawing.Size(768, 319);
            this.ConvMp3Tab.TabIndex = 1;
            this.ConvMp3Tab.Text = "Mp3 Converter";
            // 
            // Mp3ConvProgressStatus
            // 
            this.Mp3ConvProgressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Mp3ConvProgressStatus.Depth = 0;
            this.Mp3ConvProgressStatus.Location = new System.Drawing.Point(507, 111);
            this.Mp3ConvProgressStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.Mp3ConvProgressStatus.Name = "Mp3ConvProgressStatus";
            this.Mp3ConvProgressStatus.Size = new System.Drawing.Size(255, 5);
            this.Mp3ConvProgressStatus.TabIndex = 6;
            // 
            // Mp3ConvListStatus
            // 
            this.Mp3ConvListStatus.AllowDrop = true;
            this.Mp3ConvListStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Mp3ConvListStatus.BackColor = System.Drawing.SystemColors.Control;
            this.Mp3ConvListStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Mp3ConvListStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.Mp3ConvListStatus.Depth = 0;
            this.Mp3ConvListStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Mp3ConvListStatus.FullRowSelect = true;
            this.Mp3ConvListStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.Mp3ConvListStatus.Location = new System.Drawing.Point(6, 6);
            this.Mp3ConvListStatus.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Mp3ConvListStatus.MouseState = MaterialSkin.MouseState.OUT;
            this.Mp3ConvListStatus.Name = "Mp3ConvListStatus";
            this.Mp3ConvListStatus.OwnerDraw = true;
            this.Mp3ConvListStatus.Size = new System.Drawing.Size(495, 307);
            this.Mp3ConvListStatus.TabIndex = 1;
            this.Mp3ConvListStatus.UseCompatibleStateImageBehavior = false;
            this.Mp3ConvListStatus.View = System.Windows.Forms.View.Details;
            this.Mp3ConvListStatus.DragDrop += new System.Windows.Forms.DragEventHandler(this.Mp3ConvListStatus_DragDrop);
            this.Mp3ConvListStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.Mp3ConvListStatus_DragEnter);
            this.Mp3ConvListStatus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Mp3ConvListStatus_KeyPress);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "wav 경로";
            this.columnHeader2.Width = 495;
            // 
            // Mp3ConvBtnExec
            // 
            this.Mp3ConvBtnExec.AutoSize = true;
            this.Mp3ConvBtnExec.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Mp3ConvBtnExec.Depth = 0;
            this.Mp3ConvBtnExec.Icon = null;
            this.Mp3ConvBtnExec.Location = new System.Drawing.Point(683, 69);
            this.Mp3ConvBtnExec.MouseState = MaterialSkin.MouseState.HOVER;
            this.Mp3ConvBtnExec.Name = "Mp3ConvBtnExec";
            this.Mp3ConvBtnExec.Primary = true;
            this.Mp3ConvBtnExec.Size = new System.Drawing.Size(79, 36);
            this.Mp3ConvBtnExec.TabIndex = 0;
            this.Mp3ConvBtnExec.Text = "Execute";
            this.Mp3ConvBtnExec.UseVisualStyleBackColor = true;
            this.Mp3ConvBtnExec.Click += new System.EventHandler(this.Mp3ConvBtnExec_Click);
            // 
            // TaggingTab
            // 
            this.TaggingTab.Location = new System.Drawing.Point(4, 22);
            this.TaggingTab.Name = "TaggingTab";
            this.TaggingTab.Size = new System.Drawing.Size(768, 319);
            this.TaggingTab.TabIndex = 2;
            this.TaggingTab.Text = "Tagging";
            this.TaggingTab.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.materialTabSelector1);
            this.Name = "Form1";
            this.Text = "Audio2Tag";
            this.TabControl.ResumeLayout(false);
            this.CuesplitTab.ResumeLayout(false);
            this.CuesplitTab.PerformLayout();
            this.ConvMp3Tab.ResumeLayout(false);
            this.ConvMp3Tab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialTabControl TabControl;
        private System.Windows.Forms.TabPage CuesplitTab;
        private System.Windows.Forms.TabPage ConvMp3Tab;
        private System.Windows.Forms.TabPage TaggingTab;
        private MaterialSkin.Controls.MaterialListView CuesplitListStatus;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private MaterialSkin.Controls.MaterialRaisedButton CuesplitBtnExecute;
        private MaterialSkin.Controls.MaterialRaisedButton CuesplitBtnImport;
        private MaterialSkin.Controls.MaterialSingleLineTextField CuesplitTextCuePath;
        private MaterialSkin.Controls.MaterialRaisedButton CuesplitBtnOpenDialog;
        private MaterialSkin.Controls.MaterialProgressBar CuesplitProgressStatus;
        private MaterialSkin.Controls.MaterialRaisedButton Mp3ConvBtnExec;
        private MaterialSkin.Controls.MaterialListView Mp3ConvListStatus;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private MaterialSkin.Controls.MaterialProgressBar Mp3ConvProgressStatus;
    }
}

