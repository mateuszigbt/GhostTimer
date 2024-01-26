using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GhostTimer
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int MYACTION_HOTKEY_ID_F1 = 1;
        private const int MYACTION_HOTKEY_ID_F10 = 2;
        private const int MYACTION_HOTKEY_ID_F2 = 3;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const int HWND_TOPMOST = -1;
        private int count = 0;
        private DateTime startTime;
        public Form1()
        {
            Form form = new Form();
            SetWindowPos(this.Handle, (IntPtr)HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            this.Left = 0;
            this.Top = 0;
            form.TopMost = true;
            InitializeComponent();
            label1.ForeColor = Color.White;
            label4.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID_F1, 0, Keys.F1);
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID_F10, 0, Keys.F10);
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID_F2, 0, Keys.F2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                if (id == MYACTION_HOTKEY_ID_F1)
                {
                    // Obsługa skrótu dla F1
                    count++;
                    if (count == 1)
                    {
                        startTime = DateTime.Now;
                        timer1.Start();
                        label2.ForeColor = Color.Green;
                        label2.Text = "START";
                    }
                    if (count == 2)
                    {
                        timer1.Stop();
                        label2.ForeColor = Color.DarkRed;
                        label2.Text = "STOP";
                        count = 0;
                    }
                }
                if (id == MYACTION_HOTKEY_ID_F2)
                {
                    timer1.Stop();
                    label1.Text = string.Format("00:00:000");
                    label2.ForeColor = Color.Orange;
                    label2.Text = "RESET";
                    label4.Text = "";
                    count = 0;
                }
                if (id == MYACTION_HOTKEY_ID_F10)
                {
                    this.Close();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            label1.Text = string.Format("{0:00}:{1:00}:{2:00}", elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds);
            /*
            if (elapsedTime.Seconds >= 20 && elapsedTime.Seconds <= 25)
            {
                label4.Text = "Posibility hunt by Demon";
            }
            */
            if (elapsedTime.Minutes == 1 && elapsedTime.Seconds <= 10)
            {
                label4.Text = "Posibility hunt by Demon after incense";
            }
            else if (elapsedTime.Minutes == 3 && elapsedTime.Seconds <= 10)
            {
                label4.Text = "Posibility hunt by Spirit after incense";
            }
            else
            {
                label4.Text = "";
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
