//Form1.cs
//biuld by Visaul studio 2019

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Word
{
    public partial class Form1 : Form
    {
        public const string V = "字符数:";
        public const string VV = "选中:";
        public const string VVV = "行数:";
        public StringReader lineReader = null;
        public string[] strs = { "你知道吗...\n查找高亮格式可以自己挑", "你知道吗...\n你可以按地球图标的按钮,来查看每日提示", "你知道吗...\n能功藏隐发触框本文击双以可你", "你知道吗...\n按ctrl加滚轮可以缩放哟", "你知道吗...\n按红叉按钮可以退出" };
        public string[] vs = { "新界面", "新功能", "更人性化" };
        public bool flag = false;
        public bool fg = false;
        public bool ff = false;
        public bool fff = false;
        public bool ffff = true;
        public Font font;
        public string flm = "";
        public Color color;

        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int WM_CLOSE = 0x10;

        public bool Flfl { get; set; } = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void SaveToDoc()
        {
            byte[] fileBytes = System.Text.Encoding.Default.GetBytes(s: richTextBox1.Text.Trim());
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string saveFileName = saveFileDialog1.FileName;
                    FileStream fs = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    BinaryWriter br = new BinaryWriter(fs);
                    br.Write(fileBytes, 0, fileBytes.Length);
                    br.Close();
                    fs.Close();
                }
                catch
                {
                    MessageBox.Show("无法保存此文件!", "错误", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                }
            }
        }

        private void Killmessagebox()
        {
            IntPtr ptr = FindWindow(null, "#提示#");
            if (ptr != IntPtr.Zero)
            {
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        private void Showlcno()
        {
            //当前光标所在位置
            //当前行的索引
            int index = richTextBox1.GetFirstCharIndexOfCurrentLine();
            //共有行数
            _ = richTextBox1.GetLineFromCharIndex(richTextBox1.TextLength) + 1;
            //得到光标的行号
            int line = richTextBox1.GetLineFromCharIndex(index) + 1;
            //得到光标列的索引  
            /*SelectionStart得到光标所在位置的索引 
                再减去 
                当前行第一个字符的索引 
                = 光标所在的列数(从0开始）
            */
            int column = richTextBox1.SelectionStart - index + 1;
            //this.richTxt.Paste(Clipboard.GetDataObject());
            label11.Text = string.Format("{0}-{1}", line.ToString(), column.ToString());
        }
        private void ShowLineNo()
        {
            richTextBox2.Text = "";
            richTextBox2.Font = richTextBox1.Font;
            richTextBox2.ReadOnly = true;
            int ls = richTextBox1.GetLineFromCharIndex(richTextBox1.TextLength);
            for (int i = 1; i <= ls; ++i)
            {
                richTextBox2.Text += $"{i}\n";
            }
            richTextBox2.Text += richTextBox1.GetLineFromCharIndex(richTextBox1.TextLength) + 1;
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            ShowLineNo();
            Showlcno();
            flag = false;
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
            ShowLineNo();
            Form2 form2 = new Form2();
            form2.Close();
            backgroundWorker1.WorkerReportsProgress = true;
            printPreviewControl1.Visible = false;
            label1.BringToFront();
            Visible = false;
            label1.Visible = true;
            label2.Text = "字符数:0";
            label3.Visible = true;
            label7.Visible = false;
            button21.Visible = false;
            timer1.Enabled = true;
            Form1 form1 = new Form1();
            form1.AllowDrop = true;
            timer1.Start();
            richTextBox1.Text = "亲爱的用户:\r\n  感谢您使用弟弟记事本，祝您使用愉快！\r\n本项目使用C#开发，VisaulStudio作为IDE。注:项目开源地址https://github.com/Solt-hub/ccc/tree/main/Word\r\nsolt(作者)\r\n2021年1月24日";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "保存";
            saveFileDialog1.DefaultExt = "*.rtf";
            saveFileDialog1.Filter = "RTF 文档|*.rtf|文本文件|*.txt|Word文档|*.doc";
            if (ff == false)
            {
                MessageBox.Show("保存的格式是RTF|TXT|DOC哟!", "提示", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                ff = true;
            }
            DialogResult dr = saveFileDialog1.ShowDialog();
            string filename = saveFileDialog1.FileName;
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(filename))
            {
                if (string.IsNullOrEmpty(filename))
                {_ = MessageBox.Show("文件名不能为空,请重新输入!", "警告", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
                }
                else
                {
                   flag = true;
                   if (saveFileDialog1.FilterIndex == 1)
                    {
                        try
                        {
                           richTextBox1.SaveFile(filename,RichTextBoxStreamType.RichText);
                         }
                        catch
                        {
                           MessageBox.Show("无法保存此文件!", "错误", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                         }
                    }
                else if (saveFileDialog1.FilterIndex == 2)
                {
                    try
                    {
                        richTextBox1.SaveFile(filename,RichTextBoxStreamType.PlainText);
                    }
                    catch
                    {
                        MessageBox.Show("无法保存此文件!", "错误", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    }
                }
                    else
                    {
                        SaveToDoc();
                    }
                }
                
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
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "RTF 文档|*.rtf|所有文件|*.*";
            openFileDialog1.Title = "打开";
            saveFileDialog1.DefaultExt = "*.rtf";
            Showlcno();
            if (fff == false)
            {
                _ = MessageBox.Show("打开的文件格式是RTF|TXT哟!", "提示", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
            }
            DialogResult dr = openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(filename))
            {
                flag = true;
                if (string.IsNullOrEmpty(filename))
                {
                    _ = MessageBox.Show("文件名不能为空,请重新输入!", "警告", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
                }
                else
                {
                    if (Path.GetExtension(filename) == ".rtf")
                    {
                        try
                        {
                            ShowLineNo();
                            richTextBox1.LoadFile(filename, RichTextBoxStreamType.RichText);
                        }
                        catch
                        {
                            MessageBox.Show("无法打开此文件!", "错误", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                        }
                    }
                    else if (Path.GetExtension(filename) == ".doc")
                    {
                        try
                        {
                            ShowLineNo();
                            richTextBox1.LoadFile(filename, RichTextBoxStreamType.UnicodePlainText);
                        }
                        catch
                        {
                            MessageBox.Show("无法打开此文件!", "错误", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        try
                        {
                            ShowLineNo();
                            richTextBox1.LoadFile(filename, RichTextBoxStreamType.PlainText);
                        }
                        catch
                        {
                            MessageBox.Show("无法打开此文件!", "错误", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                        }
                    }
                }
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
版本号:v1.0.7DEV", caption: "关于",
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

        private void Button12_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                openFileDialog1.Filter = " 图片文件|*.jpg";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Clipboard.SetDataObject(Image.FromFile(openFileDialog1.FileName), false);
                    richTextBox1.Paste();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

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
            _ = richTextBox1.Find(textBox1.Text, RichTextBoxFinds.MatchCase);
            if (fg == false)
            {
                DialogResult result = fontDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    richTextBox1.SelectionFont = fontDialog1.Font;
                    font = fontDialog1.Font;
                }
                DialogResult result1 = colorDialog1.ShowDialog();
                if (result1 == DialogResult.OK)
                {
                    richTextBox1.SelectionColor = colorDialog1.Color;
                    color = colorDialog1.Color;
                }
                fg = true;
            }
            else
            {
                richTextBox1.SelectionColor = color;
                richTextBox1.SelectionFont = font;
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("如果查找后并没有高亮显示\n,就表明未找到!(查找区分大小写)\ntrips:请不要把字体与样式设定为\n与查找的高亮显示一样", "帮助", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
            _ = dialogResult;
        }

        private void Button14_KeyDown(object sender, KeyEventArgs e)
        {
            _ = richTextBox1.Find(textBox1.Text, RichTextBoxFinds.MatchCase);
            if (fg == false)
            {
                DialogResult result = fontDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    richTextBox1.SelectionFont = fontDialog1.Font;
                    font = fontDialog1.Font;
                }
                DialogResult result1 = colorDialog1.ShowDialog();
                if (result1 == DialogResult.OK)
                {
                    richTextBox1.SelectionColor = colorDialog1.Color;
                    color = colorDialog1.Color;
                }
                fg = true;
            }
            else
            {
                richTextBox1.SelectionColor = color;
                richTextBox1.SelectionFont = font;
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            _ = richTextBox1.Find(textBox1.Text, RichTextBoxFinds.MatchCase);
            if (fg == false)
            {
                DialogResult result = fontDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    richTextBox1.SelectionFont = fontDialog1.Font;
                    font = fontDialog1.Font;
                }
                DialogResult result1 = colorDialog1.ShowDialog();
                if (result1 == DialogResult.OK)
                {
                    richTextBox1.SelectionColor = colorDialog1.Color;
                    color = colorDialog1.Color;
                }
                fg = true;
            }
            else
            {
                richTextBox1.SelectionColor = color;
                richTextBox1.SelectionFont = font;
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            SendKeys.Send("^c");
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            _ = richTextBox1.Focus();
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
                string str2 = $"{VV}{richTextBox1.SelectionLength}";
                label3.Text = str2;
            }
        }

        private void RichTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            Showlcno();
            string str2 = $"{VV}{richTextBox1.SelectionLength}";
            label3.Text = str2;
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            _ = random.Next(0, 3);
            int g = (int)(DateTime.Now.Ticks % 5);
            label7.Text = strs[g];
            label7.Visible = true;
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
            B = (B > 255) ? 255 : B;
            label7.ForeColor = Color.FromArgb(R, G, B);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            SoundPlayer soundPlayer = new SoundPlayer
            {
                SoundLocation = @"C:\Users\Administrator\source\repos\Word\Word\江南皮革厂原版音频mp3.wav"
            };
            soundPlayer.Play();
            for (int i = 1; i <= 1000; i++)
            {
                richTextBox1.Text += "ha";
            }
        }

        private void RichTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button21.Visible = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (flag == false)
            {
                DialogResult result = MessageBox.Show("您的文档未保存!", "#提示#", buttons: MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                        DialogResult dr = saveFileDialog1.ShowDialog();
                        string filename = saveFileDialog1.FileName;
                        if (dr == DialogResult.OK && !string.IsNullOrEmpty(filename))
                        {
                            if (string.IsNullOrEmpty(filename))
                            {
                                _ = MessageBox.Show("文件名不能为空,请重新输入!", "警告", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
                            }
                            else
                            {
                                flag = true;
                                if (saveFileDialog1.FilterIndex == 1)
                                {
                                    try
                                    {
                                        richTextBox1.SaveFile(filename, RichTextBoxStreamType.RichText);
                                    }
                                    catch
                                    {
                                        MessageBox.Show("无法保存此文件!", "错误", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                    }
                                }
                                else if (saveFileDialog1.FilterIndex == 2)
                                {
                                    try
                                    {
                                        richTextBox1.SaveFile(filename, RichTextBoxStreamType.PlainText);
                                    }
                                    catch
                                    {
                                        MessageBox.Show("无法保存此文件!", "错误", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    SaveToDoc();
                                }
                            }
                        }
                }
                else if (result == DialogResult.No)
                {
                    Refresh();
                }
                else if (result == DialogResult.Cancel)
                {
                    Killmessagebox();
                }
            }
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            ShowLineNo();
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, emSize: trackBar1.Value, richTextBox1.Font.Style);
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value == 5)
            {
                MessageBox.Show("已缩放到了最小!", "警告", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
            }
            else
            {
                trackBar1.Value -= 2;
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, emSize: trackBar1.Value, richTextBox1.Font.Style);
            }
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value == 72)
            {
                MessageBox.Show("已缩放到了最大!", "警告", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
            }
            else
            {
                trackBar1.Value += 2;
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, emSize: trackBar1.Value, richTextBox1.Font.Style);
            }
        }

        private void Pd1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics; //获得绘图对象
            int count = 0; //行计数器
            float leftMargin = 1; //左边距
            float topMargin = 1; //上边距
            string line = ""; //行字符串
            Font printFont = this.textBox1.Font; //当前的打印字体
            SolidBrush myBrush = new SolidBrush(Color.Black);//刷子
            float linesPerPage = e.MarginBounds.Height / printFont.GetHeight(g);
            //逐行的循环打印一页
            while (count < linesPerPage && ((line = lineReader.ReadLine()) != null))
            {
                float yPosition = topMargin + (count * printFont.GetHeight(g));
                g.DrawString(line, printFont, myBrush, leftMargin, yPosition, new StringFormat());
                count++;
            }
            // 注意：使用本段代码前，要在该窗体的类中定义lineReader对象：
            //       StringReader lineReader = null;
            //如果本页打印完成而line不为空,说明还有没完成的页面,这将触发下一次的打印事件。在下一次的打印中lineReader会
            //自动读取上次没有打印完的内容，因为lineReader是这个打印方法外的类的成员，它可以记录当前读取的位置
            if (line != null)
                e.HasMorePages = true;
            else
            {
                e.HasMorePages = false;
                // 重新初始化lineReader对象，不然使用打印预览中的打印按钮打印出来是空白页
                lineReader = new StringReader(textBox1.Text); // textBox是你要打印的文本框的内容
            }
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            DialogResult result = printDialog1.ShowDialog();
            printPreviewControl1.Show();
            printPreviewControl1.Visible = true;
            if (result == DialogResult.OK)
            {

                lineReader = new StringReader(richTextBox1.Rtf);   // 获取要打印的字符串
                pd1.Print();      //执行打印方法
                printPreviewControl1.Visible = false;
            }
            else
            {
                printPreviewControl1.Visible = false;
            }
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {


        }

        private void BackgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }

        private void Button26_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionIndent = (int)numericUpDown1.Value;
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBullet = ffff;
            ffff = !ffff;
        }

        private void RichTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", e.LinkText);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length < 0)
            {
                string str2 = $"{VV}{Convert.ToString(Convert.ToDecimal(value: richTextBox1.SelectedText.Length))}";
                label3.Text = str2;
            }
        }

        private void RichTextBox1_VScroll(object sender, EventArgs e)
        {
            ShowLineNo();
        }

        private void Panel1_DoubleClick(object sender, EventArgs e)
        {
            ShowLineNo();
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            _ = richTextBox1.Find(textBox1.Text,RichTextBoxFinds.MatchCase);
            richTextBox1.SelectedText = textBox2.Text;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _ = richTextBox1.Find(textBox1.Text,RichTextBoxFinds.MatchCase);
                richTextBox1.SelectedText = textBox2.Text;
            }
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void TabPage3_Click(object sender, EventArgs e)
        {

        }

        private void Button30_Click(object sender, EventArgs e)
        {

        }

        private void Button29_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
               richTextBox2.ForeColor = colorDialog1.Color;
            }
        }

        private void Button30_Click_1(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                richTextBox2.BackColor = colorDialog1.Color;
            }
        }

        private void Button1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("保存", button1,0,-button1.Height);
        }

        private void Button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("打开", button2, 0, -button2.Height);
        }

        private void Button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("文本颜色", button3, 0, -button3.Height);
        }

        private void Button8_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("背景颜色", button8, 0, -button8.Height);
        }

        private void Button5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("插入时间", button5, 0, -button5.Height);
        }

        private void Button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("字体", button4, 0, -button4.Height);
        }

        private void Button7_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("居中对齐", button7, 0, -button7.Height);
        }

        private void Button9_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("左对齐", button9, 0, -button9.Height);
        }

        private void Button10_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("右对齐", button10, 0, -button10.Height);
        }

        private void Button16_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("复制", button16, 0, -button16.Height);
        }

        private void Button19_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("剪切", button19, 0, -button19.Height);
        }

        private void Button17_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("粘贴", button17, 0, -button17.Height);
        }

        private void Button18_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("全选", button18, 0, -button18.Height);
        }

        private void Button11_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("撤销", button11, 0, -button11.Height);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button12_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("重做", button12, 0, -button12.Height);
        }

        private void Button13_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("插入图片", button13, 0, -button13.Height);
        }

        private void Button14_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("查找", button14, 0, -button14.Height);
        }

        private void Button28_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("查找并替换", button28, 0, -button28.Height);
        }

        private void Button20_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("每日提示", button20, 0, -button20.Height);
        }

        private void Button26_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("缩进对齐", button26, 0, -button26.Height);
        }

        private void Button25_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("关闭", button25, 0, -button25.Height);
        }

        private void Button24_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("打印", button24, 0, -button24.Height);
        }

        private void Button27_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("段落格式", button27, 0, -button27.Height);
        }

        private void Button29_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("行号字体颜色", button29, 0, -button29.Height);
        }

        private void Button30_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("行号背景颜色", button30, 0, -button30.Height);
        }

        private void Button21_MouseHover(object sender, EventArgs e)
        {
            if (button21.Visible == true)
            {
                toolTip1.Show("不要按", button21, 0, -button21.Height);
            }
        }

        private void Button6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("关于", button6, 0, -button6.Height);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button31_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                richTextBox1.Select(0, richTextBox1.MaxLength);
                richTextBox1.SelectionBackColor = colorDialog1.Color;
                richTextBox1.Select(0, 0);
            }
        }

        private void Button31_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("背景色",button31,0,-button31.Height);
        }

        private void Button32_Click(object sender, EventArgs e)
        {
            int g = (int)(DateTime.Now.Ticks % 3);
            label13.Text = vs[g];
            label13.Visible = true;
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
            B = (B > 255) ? 255 : B;
            label13.ForeColor = Color.FromArgb(R, G, B);
        }

        private void Button32_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("希望作者下个版本更新什么?",button32,0,-button32.Height);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            flm = (string)e.Data.GetData(DataFormats.FileDrop);
            string hz = Path.GetExtension(flm);
            if (hz == ".rtf")
            {
                richTextBox1.LoadFile(flm, RichTextBoxStreamType.RichText);
            }
            else
            {
                richTextBox1.LoadFile(flm, RichTextBoxStreamType.PlainText);
            }
        }
    }
}
//Coding by Solt 
//edit it now!