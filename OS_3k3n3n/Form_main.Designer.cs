namespace OS_3k3n3n
{
    partial class Form_main
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
            this.components = new System.ComponentModel.Container();
            this.tb_all = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_showAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nud_from = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nud_to = new System.Windows.Forms.NumericUpDown();
            this.btn_execute = new System.Windows.Forms.Button();
            this.tb_res = new System.Windows.Forms.TextBox();
            this.rb_count = new System.Windows.Forms.RadioButton();
            this.rb_size = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_from)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_to)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_all
            // 
            this.tb_all.Location = new System.Drawing.Point(12, 36);
            this.tb_all.Multiline = true;
            this.tb_all.Name = "tb_all";
            this.tb_all.ReadOnly = true;
            this.tb_all.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_all.Size = new System.Drawing.Size(325, 253);
            this.tb_all.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btn_showAll
            // 
            this.btn_showAll.Location = new System.Drawing.Point(12, 340);
            this.btn_showAll.Name = "btn_showAll";
            this.btn_showAll.Size = new System.Drawing.Size(106, 27);
            this.btn_showAll.TabIndex = 2;
            this.btn_showAll.Text = "Показать все";
            this.btn_showAll.UseVisualStyleBackColor = true;
            this.btn_showAll.Click += new System.EventHandler(this.btn_showAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Диапазон ID:  от";
            // 
            // nud_from
            // 
            this.nud_from.Location = new System.Drawing.Point(110, 305);
            this.nud_from.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nud_from.Name = "nud_from";
            this.nud_from.Size = new System.Drawing.Size(63, 20);
            this.nud_from.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = " до: ";
            // 
            // nud_to
            // 
            this.nud_to.Location = new System.Drawing.Point(213, 307);
            this.nud_to.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nud_to.Name = "nud_to";
            this.nud_to.Size = new System.Drawing.Size(60, 20);
            this.nud_to.TabIndex = 7;
            // 
            // btn_execute
            // 
            this.btn_execute.Location = new System.Drawing.Point(160, 340);
            this.btn_execute.Name = "btn_execute";
            this.btn_execute.Size = new System.Drawing.Size(113, 27);
            this.btn_execute.TabIndex = 8;
            this.btn_execute.Text = "Выполнить";
            this.btn_execute.UseVisualStyleBackColor = true;
            this.btn_execute.Click += new System.EventHandler(this.btn_execute_Click);
            // 
            // tb_res
            // 
            this.tb_res.Location = new System.Drawing.Point(343, 36);
            this.tb_res.Multiline = true;
            this.tb_res.Name = "tb_res";
            this.tb_res.ReadOnly = true;
            this.tb_res.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_res.Size = new System.Drawing.Size(324, 253);
            this.tb_res.TabIndex = 9;
            // 
            // rb_count
            // 
            this.rb_count.AutoSize = true;
            this.rb_count.Checked = true;
            this.rb_count.Location = new System.Drawing.Point(6, 19);
            this.rb_count.Name = "rb_count";
            this.rb_count.Size = new System.Drawing.Size(130, 17);
            this.rb_count.TabIndex = 10;
            this.rb_count.TabStop = true;
            this.rb_count.Text = "Количество модулей";
            this.rb_count.UseVisualStyleBackColor = true;
            this.rb_count.CheckedChanged += new System.EventHandler(this.rb_count_CheckedChanged);
            // 
            // rb_size
            // 
            this.rb_size.AutoSize = true;
            this.rb_size.Location = new System.Drawing.Point(6, 42);
            this.rb_size.Name = "rb_size";
            this.rb_size.Size = new System.Drawing.Size(150, 17);
            this.rb_size.TabIndex = 11;
            this.rb_size.Text = "Размер памяти модулей";
            this.rb_size.UseVisualStyleBackColor = true;
            this.rb_size.CheckedChanged += new System.EventHandler(this.rb_size_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_count);
            this.groupBox1.Controls.Add(this.rb_size);
            this.groupBox1.Location = new System.Drawing.Point(288, 305);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 71);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Найти максимальное: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Все процессы:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Требуемые процессы:";
            // 
            // Form_main
            // 
            this.AcceptButton = this.btn_execute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 384);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_res);
            this.Controls.Add(this.btn_execute);
            this.Controls.Add(this.nud_to);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nud_from);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_showAll);
            this.Controls.Add(this.tb_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_main";
            this.Text = "Требуемые процессы: ";
            this.Load += new System.EventHandler(this.Form_main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_from)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_to)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_all;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btn_showAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud_from;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nud_to;
        private System.Windows.Forms.Button btn_execute;
        private System.Windows.Forms.TextBox tb_res;
        private System.Windows.Forms.RadioButton rb_count;
        private System.Windows.Forms.RadioButton rb_size;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

