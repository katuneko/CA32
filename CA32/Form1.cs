#define TABLE4
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CA32
{
    public partial class Form1 : Form
    {
        dynamic _ca;
        bool _update = true;
        public Form1()
        {
            InitializeComponent();
            _ca = new CArgb(true, 50, 95);
            StayProb.Text = "50";
            DeathProb.Text = "95";
            //_ca = new CA32(false, 50, 0);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (_update)
            {
                _ca.progressCA();
                pictureBox1.Image = _ca.updateImage();
            }
        }
        private void updateImage()
        {
            pictureBox1.Image = _ca.updateImage();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            _ca.refleshCA(95, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            _ca.refleshLine(1);
        }
        private void Button14_Click(object sender, EventArgs e)
        {
            _ca.refleshLine(2);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            _ca.refleshLine(3);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            _ca.refleshLine(4);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            _ca.refleshLine(12);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            _ca.refleshLine(8);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            _ca.refleshCA(1, Param.CA_SIZE / 4, Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            _ca.refleshCA(50, Param.CA_SIZE / 4, Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4);
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            _ca.refleshCA(98, Param.CA_SIZE / 4, Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4);
        }

        private void RefleshButton_Click(object sender, EventArgs e)
        {
            _ca.refleshTable();
            //_ca.refleshCA(50, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
        }

        private void RunStop_Click(object sender, EventArgs e)
        {
            _update = !_update;
        }

        private void Fmin_Click(object sender, EventArgs e)
        {
            _ca.refleshCA(5, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
        }

        private void Fmid_Click(object sender, EventArgs e)
        {
            _ca.refleshCA(50, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
        }

        private void StayProb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                try
                {
                    _ca._probStop = 100 - Convert.ToInt32(StayProb.Text);
                    _ca._probDead = 100 - Convert.ToInt32(DeathProb.Text);
                    _ca.refleshTable();
                }
                catch
                {
                    
                }
            }
        }

        private void DeathProb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {
                    _ca._probStop = 100 - Convert.ToInt32(StayProb.Text);
                    _ca._probDead = 100 - Convert.ToInt32(DeathProb.Text);
                    _ca.refleshTable();
                }
                catch
                {

                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _ca.refleshLine(16);
        }
    }
    static class Param
    {
        public const int STATE_SIZE = 2;
        public const int CA_SIZE = 256;
    }
}
