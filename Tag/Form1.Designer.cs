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
            this.CuesplitBtnExecute = new MaterialSkin.Controls.MaterialRaisedButton();
            this.CuesplitListStatus = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ConvMp3Tab = new System.Windows.Forms.TabPage();
            this.Mp3ConvProgressStatus = new MaterialSkin.Controls.MaterialProgressBar();
            this.Mp3ConvListStatus = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mp3ConvBtnExec = new MaterialSkin.Controls.MaterialRaisedButton();
            this.TaggingTab = new System.Windows.Forms.TabPage();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.TaggingBtnImageDelete = new MaterialSkin.Controls.MaterialFlatButton();
            this.TaggingBtnNextImage = new MaterialSkin.Controls.MaterialFlatButton();
            this.TaggingBtnPrevImage = new MaterialSkin.Controls.MaterialFlatButton();
            this.TaggingLabelIndex = new MaterialSkin.Controls.MaterialLabel();
            this.TaggingLabelFileSize = new MaterialSkin.Controls.MaterialLabel();
            this.TaggingLabelImageSize = new MaterialSkin.Controls.MaterialLabel();
            this.TaggingLabelMime = new MaterialSkin.Controls.MaterialLabel();
            this.TaggingImageList = new System.Windows.Forms.PictureBox();
            this.TaggingTextDirectory = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingTextDiscNum = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingTextComposers = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingTextAlbumArtists = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingTextComment = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingTextGenre = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingTextTrack = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingTextCreateYear = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingTextAlbum = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingTextArtists = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingTextTitle = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TaggingListFile = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TaggingProgressStatus = new MaterialSkin.Controls.MaterialProgressBar();
            this.TaggingBtnExec = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SettingTab = new System.Windows.Forms.TabPage();
            this.materialSingleLineTextField1 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.TabControl.SuspendLayout();
            this.CuesplitTab.SuspendLayout();
            this.ConvMp3Tab.SuspendLayout();
            this.TaggingTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TaggingImageList)).BeginInit();
            this.SettingTab.SuspendLayout();
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
            this.materialTabSelector1.Size = new System.Drawing.Size(1019, 23);
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
            this.TabControl.Size = new System.Drawing.Size(995, 525);
            this.TabControl.TabIndex = 1;
            // 
            // CuesplitTab
            // 
            this.CuesplitTab.BackColor = System.Drawing.SystemColors.Control;
            this.CuesplitTab.Controls.Add(this.CuesplitProgressStatus);
            this.CuesplitTab.Controls.Add(this.CuesplitBtnOpenDialog);
            this.CuesplitTab.Controls.Add(this.CuesplitTextCuePath);
            this.CuesplitTab.Controls.Add(this.CuesplitBtnExecute);
            this.CuesplitTab.Controls.Add(this.CuesplitListStatus);
            this.CuesplitTab.Location = new System.Drawing.Point(4, 22);
            this.CuesplitTab.Name = "CuesplitTab";
            this.CuesplitTab.Padding = new System.Windows.Forms.Padding(3);
            this.CuesplitTab.Size = new System.Drawing.Size(987, 499);
            this.CuesplitTab.TabIndex = 0;
            this.CuesplitTab.Text = "Cue Split";
            // 
            // CuesplitProgressStatus
            // 
            this.CuesplitProgressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CuesplitProgressStatus.Depth = 0;
            this.CuesplitProgressStatus.Location = new System.Drawing.Point(726, 487);
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
            this.CuesplitBtnOpenDialog.Location = new System.Drawing.Point(726, 49);
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
            this.CuesplitTextCuePath.Location = new System.Drawing.Point(726, 20);
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
            // CuesplitBtnExecute
            // 
            this.CuesplitBtnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CuesplitBtnExecute.AutoSize = true;
            this.CuesplitBtnExecute.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CuesplitBtnExecute.Depth = 0;
            this.CuesplitBtnExecute.Icon = null;
            this.CuesplitBtnExecute.Location = new System.Drawing.Point(902, 445);
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
            this.CuesplitListStatus.Location = new System.Drawing.Point(6, 3);
            this.CuesplitListStatus.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CuesplitListStatus.MouseState = MaterialSkin.MouseState.OUT;
            this.CuesplitListStatus.Name = "CuesplitListStatus";
            this.CuesplitListStatus.OwnerDraw = true;
            this.CuesplitListStatus.Size = new System.Drawing.Size(714, 493);
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
            this.columnHeader1.Width = 712;
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
            this.ConvMp3Tab.Size = new System.Drawing.Size(987, 499);
            this.ConvMp3Tab.TabIndex = 1;
            this.ConvMp3Tab.Text = "Mp3 Converter";
            // 
            // Mp3ConvProgressStatus
            // 
            this.Mp3ConvProgressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Mp3ConvProgressStatus.Depth = 0;
            this.Mp3ConvProgressStatus.Location = new System.Drawing.Point(729, 486);
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
            this.Mp3ConvListStatus.Location = new System.Drawing.Point(6, 3);
            this.Mp3ConvListStatus.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Mp3ConvListStatus.MouseState = MaterialSkin.MouseState.OUT;
            this.Mp3ConvListStatus.Name = "Mp3ConvListStatus";
            this.Mp3ConvListStatus.OwnerDraw = true;
            this.Mp3ConvListStatus.Size = new System.Drawing.Size(714, 490);
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
            this.Mp3ConvBtnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Mp3ConvBtnExec.AutoSize = true;
            this.Mp3ConvBtnExec.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Mp3ConvBtnExec.Depth = 0;
            this.Mp3ConvBtnExec.Icon = null;
            this.Mp3ConvBtnExec.Location = new System.Drawing.Point(905, 444);
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
            this.TaggingTab.Controls.Add(this.materialRaisedButton1);
            this.TaggingTab.Controls.Add(this.TaggingBtnImageDelete);
            this.TaggingTab.Controls.Add(this.TaggingBtnNextImage);
            this.TaggingTab.Controls.Add(this.TaggingBtnPrevImage);
            this.TaggingTab.Controls.Add(this.TaggingLabelIndex);
            this.TaggingTab.Controls.Add(this.TaggingLabelFileSize);
            this.TaggingTab.Controls.Add(this.TaggingLabelImageSize);
            this.TaggingTab.Controls.Add(this.TaggingLabelMime);
            this.TaggingTab.Controls.Add(this.TaggingImageList);
            this.TaggingTab.Controls.Add(this.TaggingTextDirectory);
            this.TaggingTab.Controls.Add(this.TaggingTextDiscNum);
            this.TaggingTab.Controls.Add(this.TaggingTextComposers);
            this.TaggingTab.Controls.Add(this.TaggingTextAlbumArtists);
            this.TaggingTab.Controls.Add(this.TaggingTextComment);
            this.TaggingTab.Controls.Add(this.TaggingTextGenre);
            this.TaggingTab.Controls.Add(this.TaggingTextTrack);
            this.TaggingTab.Controls.Add(this.TaggingTextCreateYear);
            this.TaggingTab.Controls.Add(this.TaggingTextAlbum);
            this.TaggingTab.Controls.Add(this.TaggingTextArtists);
            this.TaggingTab.Controls.Add(this.TaggingTextTitle);
            this.TaggingTab.Controls.Add(this.TaggingListFile);
            this.TaggingTab.Controls.Add(this.TaggingProgressStatus);
            this.TaggingTab.Controls.Add(this.TaggingBtnExec);
            this.TaggingTab.Location = new System.Drawing.Point(4, 22);
            this.TaggingTab.Name = "TaggingTab";
            this.TaggingTab.Size = new System.Drawing.Size(987, 499);
            this.TaggingTab.TabIndex = 2;
            this.TaggingTab.Text = "Tagging";
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.AutoSize = true;
            this.materialRaisedButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Icon = null;
            this.materialRaisedButton1.Location = new System.Drawing.Point(390, 443);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(195, 36);
            this.materialRaisedButton1.TabIndex = 28;
            this.materialRaisedButton1.Text = "materialRaisedButton1";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // TaggingBtnImageDelete
            // 
            this.TaggingBtnImageDelete.AutoSize = true;
            this.TaggingBtnImageDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TaggingBtnImageDelete.Depth = 0;
            this.TaggingBtnImageDelete.Icon = null;
            this.TaggingBtnImageDelete.Location = new System.Drawing.Point(209, 447);
            this.TaggingBtnImageDelete.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.TaggingBtnImageDelete.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingBtnImageDelete.Name = "TaggingBtnImageDelete";
            this.TaggingBtnImageDelete.Primary = false;
            this.TaggingBtnImageDelete.Size = new System.Drawing.Size(115, 36);
            this.TaggingBtnImageDelete.TabIndex = 27;
            this.TaggingBtnImageDelete.Text = "Image Delete";
            this.TaggingBtnImageDelete.UseVisualStyleBackColor = true;
            this.TaggingBtnImageDelete.Click += new System.EventHandler(this.TaggingBtnImageDelete_Click);
            // 
            // TaggingBtnNextImage
            // 
            this.TaggingBtnNextImage.AutoSize = true;
            this.TaggingBtnNextImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TaggingBtnNextImage.Depth = 0;
            this.TaggingBtnNextImage.Icon = null;
            this.TaggingBtnNextImage.Location = new System.Drawing.Point(315, 405);
            this.TaggingBtnNextImage.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.TaggingBtnNextImage.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingBtnNextImage.Name = "TaggingBtnNextImage";
            this.TaggingBtnNextImage.Primary = false;
            this.TaggingBtnNextImage.Size = new System.Drawing.Size(28, 36);
            this.TaggingBtnNextImage.TabIndex = 26;
            this.TaggingBtnNextImage.Text = ">";
            this.TaggingBtnNextImage.UseVisualStyleBackColor = true;
            this.TaggingBtnNextImage.Click += new System.EventHandler(this.TaggingBtnNextImage_Click);
            // 
            // TaggingBtnPrevImage
            // 
            this.TaggingBtnPrevImage.AutoSize = true;
            this.TaggingBtnPrevImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TaggingBtnPrevImage.Depth = 0;
            this.TaggingBtnPrevImage.Icon = null;
            this.TaggingBtnPrevImage.Location = new System.Drawing.Point(194, 405);
            this.TaggingBtnPrevImage.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.TaggingBtnPrevImage.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingBtnPrevImage.Name = "TaggingBtnPrevImage";
            this.TaggingBtnPrevImage.Primary = false;
            this.TaggingBtnPrevImage.Size = new System.Drawing.Size(28, 36);
            this.TaggingBtnPrevImage.TabIndex = 25;
            this.TaggingBtnPrevImage.Text = "<";
            this.TaggingBtnPrevImage.UseVisualStyleBackColor = true;
            this.TaggingBtnPrevImage.Click += new System.EventHandler(this.TaggingBtnPrevImage_Click);
            // 
            // TaggingLabelIndex
            // 
            this.TaggingLabelIndex.Depth = 0;
            this.TaggingLabelIndex.Font = new System.Drawing.Font("Roboto", 11F);
            this.TaggingLabelIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TaggingLabelIndex.Location = new System.Drawing.Point(229, 405);
            this.TaggingLabelIndex.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingLabelIndex.Name = "TaggingLabelIndex";
            this.TaggingLabelIndex.Size = new System.Drawing.Size(79, 36);
            this.TaggingLabelIndex.TabIndex = 24;
            this.TaggingLabelIndex.Text = "0 / 0";
            this.TaggingLabelIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TaggingLabelFileSize
            // 
            this.TaggingLabelFileSize.Depth = 0;
            this.TaggingLabelFileSize.Font = new System.Drawing.Font("Roboto", 11F);
            this.TaggingLabelFileSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TaggingLabelFileSize.Location = new System.Drawing.Point(190, 380);
            this.TaggingLabelFileSize.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingLabelFileSize.Name = "TaggingLabelFileSize";
            this.TaggingLabelFileSize.Size = new System.Drawing.Size(153, 19);
            this.TaggingLabelFileSize.TabIndex = 23;
            this.TaggingLabelFileSize.Text = "0 Kb";
            this.TaggingLabelFileSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TaggingLabelImageSize
            // 
            this.TaggingLabelImageSize.Depth = 0;
            this.TaggingLabelImageSize.Font = new System.Drawing.Font("Roboto", 11F);
            this.TaggingLabelImageSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TaggingLabelImageSize.Location = new System.Drawing.Point(190, 355);
            this.TaggingLabelImageSize.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingLabelImageSize.Name = "TaggingLabelImageSize";
            this.TaggingLabelImageSize.Size = new System.Drawing.Size(153, 19);
            this.TaggingLabelImageSize.TabIndex = 22;
            this.TaggingLabelImageSize.Text = "0 x 0";
            this.TaggingLabelImageSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TaggingLabelMime
            // 
            this.TaggingLabelMime.Depth = 0;
            this.TaggingLabelMime.Font = new System.Drawing.Font("Roboto", 11F);
            this.TaggingLabelMime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TaggingLabelMime.Location = new System.Drawing.Point(190, 330);
            this.TaggingLabelMime.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingLabelMime.Name = "TaggingLabelMime";
            this.TaggingLabelMime.Size = new System.Drawing.Size(153, 19);
            this.TaggingLabelMime.TabIndex = 21;
            this.TaggingLabelMime.Text = "Mime / Type";
            this.TaggingLabelMime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TaggingImageList
            // 
            this.TaggingImageList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TaggingImageList.Location = new System.Drawing.Point(3, 322);
            this.TaggingImageList.Name = "TaggingImageList";
            this.TaggingImageList.Size = new System.Drawing.Size(180, 180);
            this.TaggingImageList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TaggingImageList.TabIndex = 2;
            this.TaggingImageList.TabStop = false;
            this.TaggingImageList.DragDrop += new System.Windows.Forms.DragEventHandler(this.TaggingImageList_DragDrop);
            this.TaggingImageList.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnters);
            // 
            // TaggingTextDirectory
            // 
            this.TaggingTextDirectory.Depth = 0;
            this.TaggingTextDirectory.Hint = "Directory ( Not Change )";
            this.TaggingTextDirectory.Location = new System.Drawing.Point(3, 293);
            this.TaggingTextDirectory.MaxLength = 32767;
            this.TaggingTextDirectory.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextDirectory.Name = "TaggingTextDirectory";
            this.TaggingTextDirectory.PasswordChar = '\0';
            this.TaggingTextDirectory.SelectedText = "";
            this.TaggingTextDirectory.SelectionLength = 0;
            this.TaggingTextDirectory.SelectionStart = 0;
            this.TaggingTextDirectory.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextDirectory.TabIndex = 20;
            this.TaggingTextDirectory.TabStop = false;
            this.TaggingTextDirectory.UseSystemPasswordChar = false;
            this.TaggingTextDirectory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingTextDiscNum
            // 
            this.TaggingTextDiscNum.Depth = 0;
            this.TaggingTextDiscNum.Hint = "DiscNum ( Not Implementation )";
            this.TaggingTextDiscNum.Location = new System.Drawing.Point(3, 264);
            this.TaggingTextDiscNum.MaxLength = 32767;
            this.TaggingTextDiscNum.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextDiscNum.Name = "TaggingTextDiscNum";
            this.TaggingTextDiscNum.PasswordChar = '\0';
            this.TaggingTextDiscNum.SelectedText = "";
            this.TaggingTextDiscNum.SelectionLength = 0;
            this.TaggingTextDiscNum.SelectionStart = 0;
            this.TaggingTextDiscNum.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextDiscNum.TabIndex = 19;
            this.TaggingTextDiscNum.TabStop = false;
            this.TaggingTextDiscNum.UseSystemPasswordChar = false;
            this.TaggingTextDiscNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingTextComposers
            // 
            this.TaggingTextComposers.Depth = 0;
            this.TaggingTextComposers.Hint = "Composers ( Blank & Statable )";
            this.TaggingTextComposers.Location = new System.Drawing.Point(3, 235);
            this.TaggingTextComposers.MaxLength = 32767;
            this.TaggingTextComposers.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextComposers.Name = "TaggingTextComposers";
            this.TaggingTextComposers.PasswordChar = '\0';
            this.TaggingTextComposers.SelectedText = "";
            this.TaggingTextComposers.SelectionLength = 0;
            this.TaggingTextComposers.SelectionStart = 0;
            this.TaggingTextComposers.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextComposers.TabIndex = 18;
            this.TaggingTextComposers.TabStop = false;
            this.TaggingTextComposers.UseSystemPasswordChar = false;
            this.TaggingTextComposers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingTextAlbumArtists
            // 
            this.TaggingTextAlbumArtists.Depth = 0;
            this.TaggingTextAlbumArtists.Hint = "Album Artists ( Blank & Statable )";
            this.TaggingTextAlbumArtists.Location = new System.Drawing.Point(3, 206);
            this.TaggingTextAlbumArtists.MaxLength = 32767;
            this.TaggingTextAlbumArtists.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextAlbumArtists.Name = "TaggingTextAlbumArtists";
            this.TaggingTextAlbumArtists.PasswordChar = '\0';
            this.TaggingTextAlbumArtists.SelectedText = "";
            this.TaggingTextAlbumArtists.SelectionLength = 0;
            this.TaggingTextAlbumArtists.SelectionStart = 0;
            this.TaggingTextAlbumArtists.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextAlbumArtists.TabIndex = 17;
            this.TaggingTextAlbumArtists.TabStop = false;
            this.TaggingTextAlbumArtists.UseSystemPasswordChar = false;
            this.TaggingTextAlbumArtists.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingTextComment
            // 
            this.TaggingTextComment.Depth = 0;
            this.TaggingTextComment.Hint = "Comment ( Blank & Statable )";
            this.TaggingTextComment.Location = new System.Drawing.Point(3, 177);
            this.TaggingTextComment.MaxLength = 32767;
            this.TaggingTextComment.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextComment.Name = "TaggingTextComment";
            this.TaggingTextComment.PasswordChar = '\0';
            this.TaggingTextComment.SelectedText = "";
            this.TaggingTextComment.SelectionLength = 0;
            this.TaggingTextComment.SelectionStart = 0;
            this.TaggingTextComment.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextComment.TabIndex = 16;
            this.TaggingTextComment.TabStop = false;
            this.TaggingTextComment.UseSystemPasswordChar = false;
            this.TaggingTextComment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingTextGenre
            // 
            this.TaggingTextGenre.Depth = 0;
            this.TaggingTextGenre.Hint = "Genre ( Blank & Statable )";
            this.TaggingTextGenre.Location = new System.Drawing.Point(3, 148);
            this.TaggingTextGenre.MaxLength = 32767;
            this.TaggingTextGenre.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextGenre.Name = "TaggingTextGenre";
            this.TaggingTextGenre.PasswordChar = '\0';
            this.TaggingTextGenre.SelectedText = "";
            this.TaggingTextGenre.SelectionLength = 0;
            this.TaggingTextGenre.SelectionStart = 0;
            this.TaggingTextGenre.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextGenre.TabIndex = 15;
            this.TaggingTextGenre.TabStop = false;
            this.TaggingTextGenre.UseSystemPasswordChar = false;
            this.TaggingTextGenre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingTextTrack
            // 
            this.TaggingTextTrack.Depth = 0;
            this.TaggingTextTrack.Hint = "Track ( Blank & Statable )";
            this.TaggingTextTrack.Location = new System.Drawing.Point(3, 119);
            this.TaggingTextTrack.MaxLength = 32767;
            this.TaggingTextTrack.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextTrack.Name = "TaggingTextTrack";
            this.TaggingTextTrack.PasswordChar = '\0';
            this.TaggingTextTrack.SelectedText = "";
            this.TaggingTextTrack.SelectionLength = 0;
            this.TaggingTextTrack.SelectionStart = 0;
            this.TaggingTextTrack.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextTrack.TabIndex = 14;
            this.TaggingTextTrack.TabStop = false;
            this.TaggingTextTrack.UseSystemPasswordChar = false;
            this.TaggingTextTrack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingTextCreateYear
            // 
            this.TaggingTextCreateYear.Depth = 0;
            this.TaggingTextCreateYear.Hint = "Create Year ( Blank & Statable )";
            this.TaggingTextCreateYear.Location = new System.Drawing.Point(3, 90);
            this.TaggingTextCreateYear.MaxLength = 32767;
            this.TaggingTextCreateYear.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextCreateYear.Name = "TaggingTextCreateYear";
            this.TaggingTextCreateYear.PasswordChar = '\0';
            this.TaggingTextCreateYear.SelectedText = "";
            this.TaggingTextCreateYear.SelectionLength = 0;
            this.TaggingTextCreateYear.SelectionStart = 0;
            this.TaggingTextCreateYear.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextCreateYear.TabIndex = 13;
            this.TaggingTextCreateYear.TabStop = false;
            this.TaggingTextCreateYear.UseSystemPasswordChar = false;
            this.TaggingTextCreateYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingTextAlbum
            // 
            this.TaggingTextAlbum.Depth = 0;
            this.TaggingTextAlbum.Hint = "Album ( Blank & Statable )";
            this.TaggingTextAlbum.Location = new System.Drawing.Point(3, 61);
            this.TaggingTextAlbum.MaxLength = 32767;
            this.TaggingTextAlbum.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextAlbum.Name = "TaggingTextAlbum";
            this.TaggingTextAlbum.PasswordChar = '\0';
            this.TaggingTextAlbum.SelectedText = "";
            this.TaggingTextAlbum.SelectionLength = 0;
            this.TaggingTextAlbum.SelectionStart = 0;
            this.TaggingTextAlbum.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextAlbum.TabIndex = 12;
            this.TaggingTextAlbum.TabStop = false;
            this.TaggingTextAlbum.UseSystemPasswordChar = false;
            this.TaggingTextAlbum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingTextArtists
            // 
            this.TaggingTextArtists.Depth = 0;
            this.TaggingTextArtists.Hint = "Artists ( Blank & Statable )";
            this.TaggingTextArtists.Location = new System.Drawing.Point(3, 32);
            this.TaggingTextArtists.MaxLength = 32767;
            this.TaggingTextArtists.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextArtists.Name = "TaggingTextArtists";
            this.TaggingTextArtists.PasswordChar = '\0';
            this.TaggingTextArtists.SelectedText = "";
            this.TaggingTextArtists.SelectionLength = 0;
            this.TaggingTextArtists.SelectionStart = 0;
            this.TaggingTextArtists.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextArtists.TabIndex = 11;
            this.TaggingTextArtists.TabStop = false;
            this.TaggingTextArtists.UseSystemPasswordChar = false;
            this.TaggingTextArtists.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
            // 
            // TaggingTextTitle
            // 
            this.TaggingTextTitle.Depth = 0;
            this.TaggingTextTitle.Hint = "Title ( Blank & Statable )";
            this.TaggingTextTitle.Location = new System.Drawing.Point(3, 3);
            this.TaggingTextTitle.MaxLength = 32767;
            this.TaggingTextTitle.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingTextTitle.Name = "TaggingTextTitle";
            this.TaggingTextTitle.PasswordChar = '\0';
            this.TaggingTextTitle.SelectedText = "";
            this.TaggingTextTitle.SelectionLength = 0;
            this.TaggingTextTitle.SelectionStart = 0;
            this.TaggingTextTitle.Size = new System.Drawing.Size(340, 23);
            this.TaggingTextTitle.TabIndex = 10;
            this.TaggingTextTitle.TabStop = false;
            this.TaggingTextTitle.UseSystemPasswordChar = false;
            this.TaggingTextTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaggingTextInfo_KeyDown);
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
            this.TaggingListFile.Location = new System.Drawing.Point(361, 3);
            this.TaggingListFile.MouseLocation = new System.Drawing.Point(-1, -1);
            this.TaggingListFile.MouseState = MaterialSkin.MouseState.OUT;
            this.TaggingListFile.Name = "TaggingListFile";
            this.TaggingListFile.OwnerDraw = true;
            this.TaggingListFile.Size = new System.Drawing.Size(626, 434);
            this.TaggingListFile.TabIndex = 9;
            this.TaggingListFile.UseCompatibleStateImageBehavior = false;
            this.TaggingListFile.View = System.Windows.Forms.View.Details;
            this.TaggingListFile.SelectedIndexChanged += new System.EventHandler(this.TaggingListFile_SelectedIndexChanged);
            this.TaggingListFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.TaggingListFile_DragDrop);
            this.TaggingListFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnters);
            this.TaggingListFile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyDown);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "경로";
            this.columnHeader4.Width = 592;
            // 
            // TaggingProgressStatus
            // 
            this.TaggingProgressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TaggingProgressStatus.Depth = 0;
            this.TaggingProgressStatus.Location = new System.Drawing.Point(340, 485);
            this.TaggingProgressStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingProgressStatus.Name = "TaggingProgressStatus";
            this.TaggingProgressStatus.Size = new System.Drawing.Size(644, 5);
            this.TaggingProgressStatus.TabIndex = 7;
            // 
            // TaggingBtnExec
            // 
            this.TaggingBtnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TaggingBtnExec.AutoSize = true;
            this.TaggingBtnExec.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TaggingBtnExec.Depth = 0;
            this.TaggingBtnExec.Icon = null;
            this.TaggingBtnExec.Location = new System.Drawing.Point(905, 443);
            this.TaggingBtnExec.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaggingBtnExec.Name = "TaggingBtnExec";
            this.TaggingBtnExec.Primary = true;
            this.TaggingBtnExec.Size = new System.Drawing.Size(79, 36);
            this.TaggingBtnExec.TabIndex = 1;
            this.TaggingBtnExec.Text = "Execute";
            this.TaggingBtnExec.UseVisualStyleBackColor = true;
            this.TaggingBtnExec.Click += new System.EventHandler(this.TaggingBtnExec_Click);
            // 
            // SettingTab
            // 
            this.SettingTab.BackColor = System.Drawing.SystemColors.Control;
            this.SettingTab.Controls.Add(this.materialLabel1);
            this.SettingTab.Controls.Add(this.materialSingleLineTextField1);
            this.SettingTab.Location = new System.Drawing.Point(4, 22);
            this.SettingTab.Name = "SettingTab";
            this.SettingTab.Size = new System.Drawing.Size(987, 499);
            this.SettingTab.TabIndex = 3;
            this.SettingTab.Text = "Setting";
            // 
            // materialSingleLineTextField1
            // 
            this.materialSingleLineTextField1.Depth = 0;
            this.materialSingleLineTextField1.Hint = "( lame file path )";
            this.materialSingleLineTextField1.Location = new System.Drawing.Point(72, 16);
            this.materialSingleLineTextField1.MaxLength = 32767;
            this.materialSingleLineTextField1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextField1.Name = "materialSingleLineTextField1";
            this.materialSingleLineTextField1.PasswordChar = '\0';
            this.materialSingleLineTextField1.SelectedText = "";
            this.materialSingleLineTextField1.SelectionLength = 0;
            this.materialSingleLineTextField1.SelectionStart = 0;
            this.materialSingleLineTextField1.Size = new System.Drawing.Size(394, 23);
            this.materialSingleLineTextField1.TabIndex = 0;
            this.materialSingleLineTextField1.TabStop = false;
            this.materialSingleLineTextField1.UseSystemPasswordChar = false;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(12, 17);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(54, 19);
            this.materialLabel1.TabIndex = 1;
            this.materialLabel1.Text = "Lame :";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1019, 630);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.materialTabSelector1);
            this.Name = "Form1";
            this.Text = "Audio2Tag";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TabControl.ResumeLayout(false);
            this.CuesplitTab.ResumeLayout(false);
            this.CuesplitTab.PerformLayout();
            this.ConvMp3Tab.ResumeLayout(false);
            this.ConvMp3Tab.PerformLayout();
            this.TaggingTab.ResumeLayout(false);
            this.TaggingTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TaggingImageList)).EndInit();
            this.SettingTab.ResumeLayout(false);
            this.SettingTab.PerformLayout();
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
        private MaterialSkin.Controls.MaterialSingleLineTextField CuesplitTextCuePath;
        private MaterialSkin.Controls.MaterialRaisedButton CuesplitBtnOpenDialog;
        private MaterialSkin.Controls.MaterialProgressBar CuesplitProgressStatus;
        private MaterialSkin.Controls.MaterialRaisedButton Mp3ConvBtnExec;
        private MaterialSkin.Controls.MaterialListView Mp3ConvListStatus;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private MaterialSkin.Controls.MaterialProgressBar Mp3ConvProgressStatus;
        private System.Windows.Forms.TabPage SettingTab;
        private MaterialSkin.Controls.MaterialRaisedButton TaggingBtnExec;
        private MaterialSkin.Controls.MaterialProgressBar TaggingProgressStatus;
        private MaterialSkin.Controls.MaterialListView TaggingListFile;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.PictureBox TaggingImageList;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextDirectory;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextDiscNum;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextComposers;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextAlbumArtists;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextComment;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextGenre;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextTrack;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextCreateYear;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextAlbum;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextArtists;
        private MaterialSkin.Controls.MaterialSingleLineTextField TaggingTextTitle;
        private MaterialSkin.Controls.MaterialLabel TaggingLabelFileSize;
        private MaterialSkin.Controls.MaterialLabel TaggingLabelImageSize;
        private MaterialSkin.Controls.MaterialLabel TaggingLabelMime;
        private MaterialSkin.Controls.MaterialLabel TaggingLabelIndex;
        private MaterialSkin.Controls.MaterialFlatButton TaggingBtnNextImage;
        private MaterialSkin.Controls.MaterialFlatButton TaggingBtnPrevImage;
        private MaterialSkin.Controls.MaterialFlatButton TaggingBtnImageDelete;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextField1;
    }
}

