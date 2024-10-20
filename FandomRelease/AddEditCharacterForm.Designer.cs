namespace FandomRelease
{
    partial class AddEditCharacterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbImagePath = new System.Windows.Forms.TextBox();
            this.tbLink = new System.Windows.Forms.TextBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lbName = new System.Windows.Forms.Label();
            this.lbDiscription = new System.Windows.Forms.Label();
            this.lbLink = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(13, 24);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(320, 349);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // tbName
            // 
            this.tbName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbName.Location = new System.Drawing.Point(357, 20);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 1;
            // 
            // tbDescription
            // 
            this.tbDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbDescription.Location = new System.Drawing.Point(357, 59);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.Size = new System.Drawing.Size(282, 295);
            this.tbDescription.TabIndex = 2;
            // 
            // tbImagePath
            // 
            this.tbImagePath.Location = new System.Drawing.Point(12, 418);
            this.tbImagePath.Name = "tbImagePath";
            this.tbImagePath.Size = new System.Drawing.Size(10, 20);
            this.tbImagePath.TabIndex = 3;
            this.tbImagePath.Visible = false;
            // 
            // tbLink
            // 
            this.tbLink.Location = new System.Drawing.Point(357, 375);
            this.tbLink.Name = "tbLink";
            this.tbLink.ReadOnly = true;
            this.tbLink.Size = new System.Drawing.Size(282, 20);
            this.tbLink.TabIndex = 4;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(12, 379);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(321, 23);
            this.btnSelectImage.TabIndex = 5;
            this.btnSelectImage.Text = "Выбрать изображение";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Visible = false;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(645, 398);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(143, 40);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Сохранить изменения";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(354, 4);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(29, 13);
            this.lbName.TabIndex = 7;
            this.lbName.Text = "Имя";
            // 
            // lbDiscription
            // 
            this.lbDiscription.AutoSize = true;
            this.lbDiscription.Location = new System.Drawing.Point(354, 43);
            this.lbDiscription.Name = "lbDiscription";
            this.lbDiscription.Size = new System.Drawing.Size(57, 13);
            this.lbDiscription.TabIndex = 8;
            this.lbDiscription.Text = "Описание";
            // 
            // lbLink
            // 
            this.lbLink.AutoSize = true;
            this.lbLink.Location = new System.Drawing.Point(354, 356);
            this.lbLink.Name = "lbLink";
            this.lbLink.Size = new System.Drawing.Size(99, 13);
            this.lbLink.TabIndex = 9;
            this.lbLink.Text = "Ссылка на ресурс";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(645, 24);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(143, 36);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "Редактировать ";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // AddEditCharacterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lbLink);
            this.Controls.Add(this.lbDiscription);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.tbLink);
            this.Controls.Add(this.tbImagePath);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.pictureBox);
            this.Name = "AddEditCharacterForm";
            this.Text = "Информация";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbImagePath;
        private System.Windows.Forms.TextBox tbLink;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbDiscription;
        private System.Windows.Forms.Label lbLink;
        private System.Windows.Forms.Button btnEdit;
    }
}