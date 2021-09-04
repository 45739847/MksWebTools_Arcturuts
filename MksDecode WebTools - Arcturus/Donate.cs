using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MksDecode_WebTools___Arcturus
{
    public partial class Donate : Form
    {
        private bool mouseClicked;
        private Point clickedAt;

        public Donate()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void m_move(object sender, MouseEventArgs e)
        {
            if (mouseClicked)
            {
                this.Location = new Point(Cursor.Position.X - clickedAt.X, Cursor.Position.Y - clickedAt.Y);
            }
        }

        private void m_down(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            mouseClicked = true;
            clickedAt = e.Location;
        }

        private void m_up(object sender, MouseEventArgs e)
        {
            mouseClicked = false;
        }

        private void xuiButton3_Click(object sender, EventArgs e)
        {
            Form1.methodUrl("https://www.patreon.com/OkamiMks");
        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            Form1.methodUrl("https://www.paypal.com/donate/?hosted_button_id=NKQEXWK3RQDPQ&source=url");
        }

        private void xuiButton4_Click(object sender, EventArgs e)
        {
            Form1.methodUrl("https://ko-fi.com/okamimks");
        }

        private void xuiButton2_Click(object sender, EventArgs e)
        {
            Form1.methodUrl("https://liberapay.com/Okami/");
        }

        private void xuiButton9_Click(object sender, EventArgs e)
        {
            Form1.methodUrl("https://github.com/OkamiMks");
        }

        private void Donate_Load(object sender, EventArgs e)
        {

        }
    }
}
