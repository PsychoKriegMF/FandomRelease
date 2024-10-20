namespace FandomRelease
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvCharacters = new System.Windows.Forms.DataGridView();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnDellPerson = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCharacters)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCharacters
            // 
            this.dgvCharacters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCharacters.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCharacters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCharacters.ColumnHeadersVisible = false;
            this.dgvCharacters.Location = new System.Drawing.Point(12, 12);
            this.dgvCharacters.Name = "dgvCharacters";
            this.dgvCharacters.RowHeadersVisible = false;
            this.dgvCharacters.Size = new System.Drawing.Size(597, 643);
            this.dgvCharacters.TabIndex = 0;
            this.dgvCharacters.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCharacters_CellDoubleClick);
            this.dgvCharacters.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCharacters_CellDoubleClick);
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(676, 12);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(173, 23);
            this.btnAddPerson.TabIndex = 1;
            this.btnAddPerson.Text = "Добавить персонажа";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDellPerson
            // 
            this.btnDellPerson.Location = new System.Drawing.Point(676, 53);
            this.btnDellPerson.Name = "btnDellPerson";
            this.btnDellPerson.Size = new System.Drawing.Size(173, 23);
            this.btnDellPerson.TabIndex = 2;
            this.btnDellPerson.Text = "Удалить персонажа";
            this.btnDellPerson.UseVisualStyleBackColor = true;
            this.btnDellPerson.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(676, 632);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(173, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Выгрузить в файл";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 667);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnDellPerson);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.dgvCharacters);
            this.Name = "MainForm";
            this.Text = "Fandom";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCharacters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCharacters;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnDellPerson;
        private System.Windows.Forms.Button btnExport;
    }
}

