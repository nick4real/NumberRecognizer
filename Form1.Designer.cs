namespace NumberRecognizer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            buttonTrainCount = new Button();
            label1 = new Label();
            listBox1 = new ListBox();
            label2 = new Label();
            buttonTestCount = new Button();
            buttonAllTest = new Button();
            buttonNextTest = new Button();
            buttonAllTrain = new Button();
            buttonNextTrain = new Button();
            button9 = new Button();
            trackBar1 = new TrackBar();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            menuStrip1 = new MenuStrip();
            programToolStripMenuItem = new ToolStripMenuItem();
            clearLogsToolStripMenuItem = new ToolStripMenuItem();
            neuralNetworkToolStripMenuItem = new ToolStripMenuItem();
            deleteSavedWeightsToolStripMenuItem = new ToolStripMenuItem();
            newWeightsToolStripMenuItem = new ToolStripMenuItem();
            saveWeightsToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(12, 74);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(140, 140);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // buttonTrainCount
            // 
            buttonTrainCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonTrainCount.BackColor = SystemColors.ControlDarkDark;
            buttonTrainCount.FlatStyle = FlatStyle.Flat;
            buttonTrainCount.ForeColor = SystemColors.ControlLight;
            buttonTrainCount.Location = new Point(378, 74);
            buttonTrainCount.Name = "buttonTrainCount";
            buttonTrainCount.Size = new Size(194, 23);
            buttonTrainCount.TabIndex = 1;
            buttonTrainCount.Text = "Random train count";
            buttonTrainCount.UseVisualStyleBackColor = false;
            buttonTrainCount.Click += buttonTrainCount_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(12, 33);
            label1.Name = "label1";
            label1.Size = new Size(93, 30);
            label1.TabIndex = 2;
            label1.Text = "Number";
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBox1.BackColor = SystemColors.ControlDarkDark;
            listBox1.ForeColor = SystemColors.ControlLight;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 256);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(560, 319);
            listBox1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16F);
            label2.ForeColor = Color.WhiteSmoke;
            label2.Location = new Point(12, 217);
            label2.Name = "label2";
            label2.Size = new Size(58, 30);
            label2.TabIndex = 4;
            label2.Text = "Logs";
            // 
            // buttonTestCount
            // 
            buttonTestCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonTestCount.BackColor = SystemColors.ControlDarkDark;
            buttonTestCount.FlatStyle = FlatStyle.Flat;
            buttonTestCount.ForeColor = SystemColors.ControlLight;
            buttonTestCount.Location = new Point(378, 132);
            buttonTestCount.Name = "buttonTestCount";
            buttonTestCount.Size = new Size(194, 23);
            buttonTestCount.TabIndex = 6;
            buttonTestCount.Text = "Random test count";
            buttonTestCount.UseVisualStyleBackColor = false;
            buttonTestCount.Click += buttonTestCount_Click;
            // 
            // buttonAllTest
            // 
            buttonAllTest.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAllTest.BackColor = SystemColors.ControlDarkDark;
            buttonAllTest.FlatStyle = FlatStyle.Flat;
            buttonAllTest.ForeColor = SystemColors.ControlLight;
            buttonAllTest.Location = new Point(178, 161);
            buttonAllTest.Name = "buttonAllTest";
            buttonAllTest.Size = new Size(194, 23);
            buttonAllTest.TabIndex = 11;
            buttonAllTest.Text = "All test";
            buttonAllTest.UseVisualStyleBackColor = false;
            buttonAllTest.Click += buttonAllTest_Click;
            // 
            // buttonNextTest
            // 
            buttonNextTest.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonNextTest.BackColor = SystemColors.ControlDarkDark;
            buttonNextTest.FlatStyle = FlatStyle.Flat;
            buttonNextTest.ForeColor = SystemColors.ControlLight;
            buttonNextTest.Location = new Point(178, 132);
            buttonNextTest.Name = "buttonNextTest";
            buttonNextTest.Size = new Size(194, 23);
            buttonNextTest.TabIndex = 10;
            buttonNextTest.Text = "Next random test";
            buttonNextTest.UseVisualStyleBackColor = false;
            buttonNextTest.Click += buttonNextTest_Click;
            // 
            // buttonAllTrain
            // 
            buttonAllTrain.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAllTrain.BackColor = SystemColors.ControlDarkDark;
            buttonAllTrain.FlatStyle = FlatStyle.Flat;
            buttonAllTrain.ForeColor = SystemColors.ControlLight;
            buttonAllTrain.Location = new Point(178, 103);
            buttonAllTrain.Name = "buttonAllTrain";
            buttonAllTrain.Size = new Size(194, 23);
            buttonAllTrain.TabIndex = 9;
            buttonAllTrain.Text = "All train";
            buttonAllTrain.UseVisualStyleBackColor = false;
            buttonAllTrain.Click += buttonAllTrain_Click;
            // 
            // buttonNextTrain
            // 
            buttonNextTrain.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonNextTrain.BackColor = SystemColors.ControlDarkDark;
            buttonNextTrain.FlatStyle = FlatStyle.Flat;
            buttonNextTrain.ForeColor = SystemColors.ControlLight;
            buttonNextTrain.Location = new Point(178, 74);
            buttonNextTrain.Name = "buttonNextTrain";
            buttonNextTrain.Size = new Size(194, 23);
            buttonNextTrain.TabIndex = 8;
            buttonNextTrain.Text = "Next random train";
            buttonNextTrain.UseVisualStyleBackColor = false;
            buttonNextTrain.Click += buttonNextTrain_Click;
            // 
            // button9
            // 
            button9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button9.BackColor = SystemColors.ControlDarkDark;
            button9.Enabled = false;
            button9.FlatStyle = FlatStyle.Flat;
            button9.ForeColor = SystemColors.ControlLight;
            button9.Location = new Point(178, 191);
            button9.Name = "button9";
            button9.Size = new Size(194, 23);
            button9.TabIndex = 12;
            button9.Text = "Change learning speed";
            button9.UseVisualStyleBackColor = false;
            // 
            // trackBar1
            // 
            trackBar1.Enabled = false;
            trackBar1.Location = new Point(378, 190);
            trackBar1.Maximum = 100;
            trackBar1.Minimum = 1;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(194, 45);
            trackBar1.TabIndex = 13;
            trackBar1.TickFrequency = 10;
            trackBar1.Value = 10;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ControlDarkDark;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.ForeColor = SystemColors.ControlLight;
            textBox1.Location = new Point(378, 103);
            textBox1.MaxLength = 5;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(194, 23);
            textBox1.TabIndex = 14;
            textBox1.KeyPress += keyPressEvent;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.ControlDarkDark;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.ForeColor = SystemColors.ControlLight;
            textBox2.Location = new Point(378, 162);
            textBox2.MaxLength = 5;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(194, 23);
            textBox2.TabIndex = 15;
            textBox2.KeyPress += keyPressEvent;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 16F);
            label3.ForeColor = Color.WhiteSmoke;
            label3.Location = new Point(178, 33);
            label3.Name = "label3";
            label3.Size = new Size(89, 30);
            label3.TabIndex = 16;
            label3.Text = "Options";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { programToolStripMenuItem, neuralNetworkToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(584, 24);
            menuStrip1.TabIndex = 17;
            menuStrip1.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            programToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clearLogsToolStripMenuItem });
            programToolStripMenuItem.Name = "programToolStripMenuItem";
            programToolStripMenuItem.Size = new Size(65, 20);
            programToolStripMenuItem.Text = "Program";
            // 
            // clearLogsToolStripMenuItem
            // 
            clearLogsToolStripMenuItem.Name = "clearLogsToolStripMenuItem";
            clearLogsToolStripMenuItem.Size = new Size(126, 22);
            clearLogsToolStripMenuItem.Text = "Clear logs";
            // 
            // neuralNetworkToolStripMenuItem
            // 
            neuralNetworkToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { deleteSavedWeightsToolStripMenuItem, newWeightsToolStripMenuItem, saveWeightsToolStripMenuItem });
            neuralNetworkToolStripMenuItem.Name = "neuralNetworkToolStripMenuItem";
            neuralNetworkToolStripMenuItem.Size = new Size(102, 20);
            neuralNetworkToolStripMenuItem.Text = "Neural Network";
            // 
            // deleteSavedWeightsToolStripMenuItem
            // 
            deleteSavedWeightsToolStripMenuItem.Name = "deleteSavedWeightsToolStripMenuItem";
            deleteSavedWeightsToolStripMenuItem.Size = new Size(152, 22);
            deleteSavedWeightsToolStripMenuItem.Text = "Delete save file";
            // 
            // newWeightsToolStripMenuItem
            // 
            newWeightsToolStripMenuItem.Name = "newWeightsToolStripMenuItem";
            newWeightsToolStripMenuItem.Size = new Size(152, 22);
            newWeightsToolStripMenuItem.Text = "New weights";
            // 
            // saveWeightsToolStripMenuItem
            // 
            saveWeightsToolStripMenuItem.Name = "saveWeightsToolStripMenuItem";
            saveWeightsToolStripMenuItem.Size = new Size(152, 22);
            saveWeightsToolStripMenuItem.Text = "Save weights";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(72, 72, 72);
            ClientSize = new Size(584, 598);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(trackBar1);
            Controls.Add(button9);
            Controls.Add(buttonAllTest);
            Controls.Add(buttonNextTest);
            Controls.Add(buttonAllTrain);
            Controls.Add(buttonNextTrain);
            Controls.Add(buttonTestCount);
            Controls.Add(label2);
            Controls.Add(listBox1);
            Controls.Add(label1);
            Controls.Add(buttonTrainCount);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(600, 1000);
            MinimumSize = new Size(600, 400);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button buttonTrainCount;
        private Label label1;
        private ListBox listBox1;
        private Label label2;
        private Button buttonTestCount;
        private Button buttonAllTest;
        private Button buttonNextTest;
        private Button buttonAllTrain;
        private Button buttonNextTrain;
        private Button button9;
        private TrackBar trackBar1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem neuralNetworkToolStripMenuItem;
        private ToolStripMenuItem deleteSavedWeightsToolStripMenuItem;
        private ToolStripMenuItem newWeightsToolStripMenuItem;
        private ToolStripMenuItem saveWeightsToolStripMenuItem;
        private ToolStripMenuItem programToolStripMenuItem;
        private ToolStripMenuItem clearLogsToolStripMenuItem;
    }
}
