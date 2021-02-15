using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Word
{
    public partial class Form2 : Form
    {
        public string[] vs = { ".", "..", "..." };
        public string lo = "loading";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            Form1 form1 = new Form1();
            form1.Visible = false;
            Visible = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 584)
            {
                timer1.Stop();
                label1.ForeColor = Color.FromArgb(0, 0, 0);
                label4.ForeColor = Color.FromArgb(0, 0, 0);
                _ = MessageBox.Show("加载完成!", "提示", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                Form1 form1 = new Form1();
                form1.ShowDialog();
                Visible = false;
            }
            else
            {
                progressBar1.Value += 8;
                label2.Text = $"{lo}{vs[progressBar1.Value % 3]}";
                _ = new Random(10);
                long tick = DateTime.Now.Ticks;
                Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                int R = ran.Next(255);
                int G = ran.Next(255);
                int B = ran.Next(255);
                B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
                B = (B > 255) ? 255 : B;
                label1.ForeColor = Color.FromArgb(R, G, B);
                label2.ForeColor = Color.FromArgb(B, G, R);
                label4.ForeColor = Color.FromArgb(R, G, B);
                _ = new Form2
                {
                    BackColor = Color.FromArgb(B, R, G)
                };
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }
    }
}
