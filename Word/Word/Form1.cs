using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Word
{
    public partial class Form1 : Form
    {
        public const string V = "字符数:";
        public const string VV = "选中:";
        public const string VVV = "行数:";
        public StringReader lineReader = null;
        public bool flag = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            string tl = richTextBox1.Text;
            if (tl.Length != 0)
            {
                label1.Visible = false;
            }
            else if (tl.Length == 0)
            {
                label1.Visible = true;
            }
            string str = $"{V}{Convert.ToString(value: Convert.ToDecimal(tl.Length))}";
            label2.Text = str;
            label4.Text = $"{VVV}{Convert.ToString(Convert.ToDecimal(richTextBox1.GetLineFromCharIndex(richTextBox1.TextLength) + 1))}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.BringToFront();
            label1.Visible = true;
            label2.Text = "字符数:0";
            label3.Visible = true;
            richTextBox1.Text = "亲爱的用户:\r\n  感谢您使用弟弟记事本，祝您使用愉快！\r\n本项目使用C#开发，ViSaulStudio作为IDE。\r\nsolt(作者)\r\n2021年1月24日";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = saveFileDialog1.ShowDialog();
            string filename = saveFileDialog1.FileName;
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(filename))
            {
                flag = true;
                richTextBox1.SaveFile(filename, RichTextBoxStreamType.RichText);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(filename))
            {
                richTextBox1.LoadFile(filename, fileType: RichTextBoxStreamType.PlainText);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DialogResult result = fontDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += Convert.ToString(DateTime.Now);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            _ = MessageBox.Show(
                text: @"开发者:solt(sotl工作室)
注:本项目只有工作室solt一人开发
如果你想参与开发,请进入sotl工作室,谢谢
版本号:v1.0.3", caption: "关于",
                buttons: MessageBoxButtons.OKCancel, icon: MessageBoxIcon.Information);
        }

        private void Button7_Click(object sender, EventArgs e)
        {

        }

        private void Button8_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                richTextBox1.SelectionBackColor = colorDialog1.Color;
            }
        }

        private void Button7_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                openFileDialog1.Filter = " 图片文件|*.jpg|所有文件|*.* ";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Clipboard.SetDataObject(Image.FromFile(openFileDialog1.FileName), false);
                    richTextBox1.Paste();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag == false)
            {
                DialogResult result = MessageBox.Show("您的文档未保存,是否保存？", "提示", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DialogResult dr = saveFileDialog1.ShowDialog();
                    string filename = saveFileDialog1.FileName;
                    if (dr == DialogResult.OK && !string.IsNullOrEmpty(filename))
                    {
                        flag = true;
                        richTextBox1.SaveFile(filename, fileType: RichTextBoxStreamType.PlainText);
                    }
                }
                else if (result == DialogResult.No)
                {
                    Close();
                }
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {

        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void PrintDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {


        }

        private void Button14_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == false)
            {
                _ = richTextBox1.Find(textBox1.Text, RichTextBoxFinds.MatchCase);
                richTextBox1.SelectionFont = new Font("微软雅黑", 12, FontStyle.Bold);
                richTextBox1.SelectionColor = Color.Red;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("如果查找后并没有红色,粗体的高亮显示\n,就表明未找到!(查找区分大小写)\ntrips:请不要把字体与样式设定为\n与查找的高亮显示一样", "帮助", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
            _ = dialogResult;
        }

        private void Button14_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _ = richTextBox1.Find(textBox1.Text, RichTextBoxFinds.MatchCase);
                richTextBox1.SelectionFont = new Font("微软雅黑", 12, FontStyle.Bold);
                richTextBox1.SelectionColor = Color.Red;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            SendKeys.Send("^c");
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            SendKeys.Send("^v");
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            richTextBox1.SelectAll();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            SendKeys.Send("^x");
        }

        private void RichTextBox1_Enter(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length < 0)
            {
                string str2 = $"{VV}{Convert.ToString(Convert.ToDecimal(value: richTextBox1.SelectedText.Length))}";
                label3.Text = str2;
            }
        }

        private void RichTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length < 0)
            {
                string str2 = $"{VV}{Convert.ToString(Convert.ToDecimal(value: richTextBox1.SelectedText.Length))}";
                label3.Text = str2;
            }
        }
    }
}
