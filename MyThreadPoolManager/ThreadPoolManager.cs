using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ThreadPoolManager
{
    public partial class ThreadPoolManager : Form, IDisposable
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (pool != null))
            {
                PoolThreadProcessor.Stop(pool);

            }

            PoolThreadProcessor.Stop(pool);
            base.Dispose(disposing);
        }

        MyThreadPool pool = null;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Panel panel1;
        private NumericUpDown threadsNumber;
        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown minTime;
        private NumericUpDown maxTime;
        private Button button1;

        public ThreadPoolManager()
        {
            InitializeComponent();
        }
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ThreadPoolManager());
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.threadsNumber = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.minTime = new System.Windows.Forms.NumericUpDown();
            this.maxTime = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threadsNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxTime)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 59);
            this.button1.TabIndex = 0;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 62);
            this.button2.TabIndex = 1;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(5, 96);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 60);
            this.button3.TabIndex = 2;
            this.button3.Text = "Pause";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(7, 170);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(151, 60);
            this.button4.TabIndex = 3;
            this.button4.Text = "Stop";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(26, 365);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(148, 62);
            this.button5.TabIndex = 4;
            this.button5.Text = "Stop Enque";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(18, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 252);
            this.panel1.TabIndex = 5;
            // 
            // threadsNumber
            // 
            this.threadsNumber.Location = new System.Drawing.Point(289, 59);
            this.threadsNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadsNumber.Name = "threadsNumber";
            this.threadsNumber.Size = new System.Drawing.Size(185, 26);
            this.threadsNumber.TabIndex = 6;
            this.threadsNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Number task creators";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(286, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Task create min time (s)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(295, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(272, 30);
            this.label3.TabIndex = 9;
            this.label3.Text = "Task create max time (s)";
            // 
            // minTime
            // 
            this.minTime.Location = new System.Drawing.Point(288, 158);
            this.minTime.Name = "minTime";
            this.minTime.Size = new System.Drawing.Size(185, 26);
            this.minTime.TabIndex = 10;
            // 
            // maxTime
            // 
            this.maxTime.Location = new System.Drawing.Point(289, 250);
            this.maxTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxTime.Name = "maxTime";
            this.maxTime.Size = new System.Drawing.Size(184, 26);
            this.maxTime.TabIndex = 11;
            this.maxTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ThreadPoolManager
            // 
            this.ClientSize = new System.Drawing.Size(511, 475);
            this.Controls.Add(this.maxTime);
            this.Controls.Add(this.minTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.threadsNumber);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Name = "ThreadPoolManager";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.threadsNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            pool = PoolThreadProcessor.Run((int)threadsNumber.Value, (int)minTime.Value, (int)maxTime.Value);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pool != null)
            {
                PoolThreadProcessor.Stop(pool);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pool != null)
            {
                pool.Resume();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pool != null)
            {
                pool.Pause();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pool != null)
            {
                pool.Stop();
            }
        }
    }
}
