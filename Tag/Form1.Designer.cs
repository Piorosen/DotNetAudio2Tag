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
            System.Windows.Forms.ListViewItem listViewItem37 = new System.Windows.Forms.ListViewItem("Title");
            System.Windows.Forms.ListViewItem listViewItem38 = new System.Windows.Forms.ListViewItem("Artist");
            System.Windows.Forms.ListViewItem listViewItem39 = new System.Windows.Forms.ListViewItem("Album");
            System.Windows.Forms.ListViewItem listViewItem40 = new System.Windows.Forms.ListViewItem("Year");
            System.Windows.Forms.ListViewItem listViewItem41 = new System.Windows.Forms.ListViewItem("Track");
            System.Windows.Forms.ListViewItem listViewItem42 = new System.Windows.Forms.ListViewItem("TrackNum");
            System.Windows.Forms.ListViewItem listViewItem43 = new System.Windows.Forms.ListViewItem("Genre");
            System.Windows.Forms.ListViewItem listViewItem44 = new System.Windows.Forms.ListViewItem("Comments");
            System.Windows.Forms.ListViewItem listViewItem45 = new System.Windows.Forms.ListViewItem("Album Artist");
            System.Windows.Forms.ListViewItem listViewItem46 = new System.Windows.Forms.ListViewItem("Composer");
            System.Windows.Forms.ListViewItem listViewItem47 = new System.Windows.Forms.ListViewItem("DiscNum");
            System.Windows.Forms.ListViewItem listViewItem48 = new System.Windows.Forms.ListViewItem("PictureImagePath");
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
            this.TaggingBtnTagSave = new MaterialSkin.Controls.MaterialRaisedButton();
            this.TaggingLabelValue = new MaterialSkin.Controls.MaterialLabel();
            this.TaggingListTagHttp = new System.Windows.Forms.ListView();
            this.TaggingTextHttp = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingListFile = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TaggingTextInfo = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingProgressStatus = new MaterialSkin.Controls.MaterialProgressBar();
            this.TaggingBtnExec = new MaterialSkin.Controls.MaterialRaisedButton();
            this.TaggingListTag = new System.Windows.Forms.ListView();
            this.SettingTab = new System.Windows.Forms.TabPage();
            this.TabControl.SuspendLayout();
            this.CuesplitTab.SuspendLayout();
            this.ConvMp3Tab.SuspendLayout();
            this.TaggingTab.SuspendLayout();
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
            this.TabControl.Controls.Add(this.SettingTab);
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
            this.CuesplitListStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnters);
            this.CuesplitListStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyDown);
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
            this.Mp3ConvListStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnters);
            this.Mp3ConvListStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyDown);
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
            this.TaggingTab.BackColor = System.Drawing.SystemColors.Control;
            this.TaggingTab.Controls.Add(this.TaggingBtnTagSave);
            this.TaggingTab.Controls.Add(this.TaggingLabelValue);
            this.TaggingTab.Controls.Add(this.TaggingListTagHttp);
            this.TaggingTab.Controls.Add(this.TaggingTextHttp);
            this.TaggingTab.Controls.Add(this.TaggingListFile);
            this.TaggingTab.Controls.Add(this.TaggingTextInfo);
            this.TaggingTab.Controls.Add(this.TaggingProgressStatus);
            this.TaggingTab.Controls.Add(this.TaggingBtnExec);
            this.TaggingTab.Controls.Add(this.TaggingListTag);
            this.TaggingTab.Location = new System.Drawing.Point(4, 22);
            this.TaggingTab.Name = "TaggingTab";
            this.TaggingTab.Size = new System.Drawing.Size(768, 319);
            this.TaggingTab.TabIndex = 2;
            this.TaggingTab.Text = "Tagging";
            // 
            // TaggingBtnTagSave
            // 
            this.TaggingBtnTagSave.AutoSize = true;
            this.TaggingBtnTagSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TaggingBtnTagSave.Depth = 0;
            this.TaggingBtnTagSave.Icon = null;
            this.TaggingBtnTagSave.Location = new System.Drawing.Point(510, 263);
            this.TaggingBtnTagSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingBtnTagSave.Name = "TaggingBtnTagSave";
            this.TaggingBtnTagSave.Primary = true;
            this.TaggingBtnTagSave.Size = new System.Drawing.Size(82, 36);
            this.TaggingBtnTagSave.TabIndex = 13;
            this.TaggingBtnTagSave.Text = "TagSave";
            this.TaggingBtnTagSave.UseVisualStyleBackColor = true;
            this.TaggingBtnTagSave.Click += new System.EventHandler(this.TaggingBtnTagSave_Click);
            // 
            // TaggingLabelValue
            // 
            this.TaggingLabelValue.AutoSize = true;
            this.TaggingLabelValue.Depth = 0;
            this.TaggingLabelValue.Font = new System.Drawing.Font("Roboto", 11F);
            this.TaggingLabelValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TaggingLabelValue.Location = new System.Drawing.Point(510, 237);
            this.TaggingLabelValue.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingLabelValue.Name = "TaggingLabelValue";
            this.TaggingLabelValue.Size = new System.Drawing.Size(59, 19);
            this.TaggingLabelValue.TabIndex = 12;
            this.TaggingLabelValue.Text = "Value : ";
            // 
            // TaggingListTagHttp
            // 
            this.TaggingListTagHttp.Location = new System.Drawing.Point(510, 34);
            this.TaggingListTagHttp.Name = "TaggingListTagHttp";
            this.TaggingListTagHttp.Size = new System.Drawing.Size(255, 200);
            this.TaggingListTagHttp.TabIndex = 11;
            this.TaggingListTagHttp.UseCompatibleStateImageBehavior = false;
            // 
            // TaggingTextHttp
            // 
            this.TaggingTextHttp.Depth = 0;
            this.TaggingTextHttp.Hint = "Http Adress";
            this.TaggingTextHttp.Location = new System.Drawing.Point(510, 4);
            this.TaggingTextHttp.MaxLength = 32767;
            this.TaggingTextHttp.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextHttp.Name = "TaggingTextHttp";
            this.TaggingTextHttp.PasswordChar = '\0';
            this.TaggingTextHttp.SelectedText = "";
            this.TaggingTextHttp.SelectionLength = 0;
            this.TaggingTextHttp.SelectionStart = 0;
            this.TaggingTextHttp.Size = new System.Drawing.Size(255, 23);
            this.TaggingTextHttp.TabIndex = 10;
            this.TaggingTextHttp.TabStop = false;
            this.TaggingTextHttp.UseSystemPasswordChar = false;
            // 
            // TaggingListFile
            // 
            this.TaggingListFile.AllowDrop = true;
            this.TaggingListFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TaggingListFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TaggingListFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.TaggingListFile.Depth = 0;
            this.TaggingListFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.TaggingListFile.FullRowSelect = true;
            this.TaggingListFile.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.TaggingListFile.LabelEdit = true;
            this.TaggingListFile.Location = new System.Drawing.Point(242, 3);
            this.TaggingListFile.MouseLocation = new System.Drawing.Point(-1, -1);
            this.TaggingListFile.MouseState = MaterialSkin.MouseState.OUT;
            this.TaggingListFile.Name = "TaggingListFile";
            this.TaggingListFile.OwnerDraw = true;
            this.TaggingListFile.Size = new System.Drawing.Size(262, 306);
            this.TaggingListFile.TabIndex = 9;
            this.TaggingListFile.UseCompatibleStateImageBehavior = false;
            this.TaggingListFile.View = System.Windows.Forms.View.Details;
            this.TaggingListFile.SelectedIndexChanged += new System.EventHandler(this.TaggingListFile_SelectedIndexChanged);
            this.TaggingListFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.TaggingListFile_DragDrop);
            this.TaggingListFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnters);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "경로";
            this.columnHeader4.Width = 227;
            // 
            // TaggingTextInfo
            // 
            this.TaggingTextInfo.Depth = 0;
            this.TaggingTextInfo.Hint = "Set Value";
            this.TaggingTextInfo.Location = new System.Drawing.Point(3, 286);
            this.TaggingTextInfo.MaxLength = 32767;
            this.TaggingTextInfo.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextInfo.Name = "TaggingTextInfo";
            this.TaggingTextInfo.PasswordChar = '\0';
            this.TaggingTextInfo.SelectedText = "";
            this.TaggingTextInfo.SelectionLength = 0;
            this.TaggingTextInfo.SelectionStart = 0;
            this.TaggingTextInfo.Size = new System.Drawing.Size(233, 23);
            this.TaggingTextInfo.TabIndex = 8;
            this.TaggingTextInfo.TabStop = false;
            this.TaggingTextInfo.UseSystemPasswordChar = false;
            this.TaggingTextInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingProgressStatus
            // 
            this.TaggingProgressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TaggingProgressStatus.Depth = 0;
            this.TaggingProgressStatus.Location = new System.Drawing.Point(510, 305);
            this.TaggingProgressStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingProgressStatus.Name = "TaggingProgressStatus";
            this.TaggingProgressStatus.Size = new System.Drawing.Size(255, 5);
            this.TaggingProgressStatus.TabIndex = 7;
            // 
            // TaggingBtnExec
            // 
            this.TaggingBtnExec.AutoSize = true;
            this.TaggingBtnExec.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TaggingBtnExec.Depth = 0;
            this.TaggingBtnExec.Icon = null;
            this.TaggingBtnExec.Location = new System.Drawing.Point(686, 263);
            this.TaggingBtnExec.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingBtnExec.Name = "TaggingBtnExec";
            this.TaggingBtnExec.Primary = true;
            this.TaggingBtnExec.Size = new System.Drawing.Size(79, 36);
            this.TaggingBtnExec.TabIndex = 1;
            this.TaggingBtnExec.Text = "Execute";
            this.TaggingBtnExec.UseVisualStyleBackColor = true;
            this.TaggingBtnExec.Click += new System.EventHandler(this.TaggingBtnExec_Click);
            // 
            // TaggingListTag
            // 
            this.TaggingListTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TaggingListTag.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem37,
            listViewItem38,
            listViewItem39,
            listViewItem40,
            listViewItem41,
            listViewItem42,
            listViewItem43,
            listViewItem44,
            listViewItem45,
            listViewItem46,
            listViewItem47,
            listViewItem48});
            this.TaggingListTag.Location = new System.Drawing.Point(3, 3);
            this.TaggingListTag.Name = "TaggingListTag";
            this.TaggingListTag.Size = new System.Drawing.Size(232, 272);
            this.TaggingListTag.TabIndex = 0;
            this.TaggingListTag.UseCompatibleStateImageBehavior = false;
            this.TaggingListTag.View = System.Windows.Forms.View.List;
            this.TaggingListTag.SelectedIndexChanged += new System.EventHandler(this.TaggingListTag_SelectedIndexChanged);
            // 
            // SettingTab
            // 
            this.SettingTab.Location = new System.Drawing.Point(4, 22);
            this.SettingTab.Name = "SettingTab";
            this.SettingTab.Size = new System.Drawing.Size(768, 319);
            this.SettingTab.TabIndex = 3;
            this.SettingTab.Text = "Setting";
            this.SettingTab.UseVisualStyleBackColor = true;
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
            this.TaggingTab.ResumeLayout(false);
            this.TaggingTab.PerformLayout();
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
        private System.Windows.Forms.TabPage SettingTab;
        private System.Windows.Forms.ListView TaggingListTag;
        private MaterialSkin.Controls.MaterialRaisedButton TaggingBtnExec;
        private MaterialSkin.Controls.MaterialProgressBar TaggingProgressStatus;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextInfo;
        private MaterialSkin.Controls.MaterialListView TaggingListFile;
        private MaterialSkin.Controls.MaterialLabel TaggingLabelValue;
        private System.Windows.Forms.ListView TaggingListTagHttp;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextHttp;
        private MaterialSkin.Controls.MaterialRaisedButton TaggingBtnTagSave;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}

