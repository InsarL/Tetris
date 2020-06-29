namespace Tetris
{
    partial class Tetris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tetris));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelScore = new System.Windows.Forms.Label();
            this.labelNextFigure = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(412, 513);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 600;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Location = new System.Drawing.Point(302, 174);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(42, 13);
            this.labelScore.TabIndex = 1;
            this.labelScore.Text = "Счёт: 0";
            // 
            // labelNextFigure
            // 
            this.labelNextFigure.AutoSize = true;
            this.labelNextFigure.Location = new System.Drawing.Point(280, 9);
            this.labelNextFigure.Name = "labelNextFigure";
            this.labelNextFigure.Size = new System.Drawing.Size(108, 13);
            this.labelNextFigure.TabIndex = 2;
            this.labelNextFigure.Text = "Следующая фигура:";
            // 
            // Tetris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 534);
            this.Controls.Add(this.labelNextFigure);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Tetris";
            this.Text = "Tetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelNextFigure;
    }
}

