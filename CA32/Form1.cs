using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CA32
{
    public partial class Form1 : Form
    {
        Random _r = new Random();
        byte[,,,,] _table = new byte[Param.STATE_SIZE, Param.STATE_SIZE, Param.STATE_SIZE, Param.STATE_SIZE, Param.STATE_SIZE];
        byte[,] _caCur = new byte[Param.CA_SIZE, Param.CA_SIZE];
        byte[,] _caNext = new byte[512, 512];
        bool _update = true;
        public Form1()
        {
            InitializeComponent();
            refleshCA(50, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
            refleshTable();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (_update)
            {
                progressCA();
                updateImage();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            refleshTable();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            _update = !_update;
        }
        private void refleshCA(int prb, int x0, int y0, int x1, int y1)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    if(prb == 0)
                    {
                        _caCur[i, j] = 0;
                    }
                    else
                    {
                        if((x0 <= i) && (i <= x1) && (y0 <= j) && (j <= y1))
                        {
                            _caCur[i, j] = (byte)(((prb - 1) < _r.Next(100)) ? 0 : (Param.STATE_SIZE - 1));
                        }
                        else
                        {
                            _caCur[i, j] = 0;
                        }
                    }
                }
            }
        }
        private void refleshTable()
        {
            for(int a = 0; a < Param.STATE_SIZE; a++)
            {
                for (int b = 0; b < Param.STATE_SIZE; b++)
                {
                    for (int c = 0; c < Param.STATE_SIZE; c++)
                    {
                        for (int d = 0; d < Param.STATE_SIZE; d++)
                        {
                            for (int e = 0; e < Param.STATE_SIZE; e++)
                            {
                                _table[a, b, c, d, e] = (byte)((_r.Next(2) == 0) ? 0 : Param.STATE_SIZE - 1);
                            }
                        }
                    }
                }
            }
            uint code = 0;
            int i = 0;
            for (int a = 0; a < Param.STATE_SIZE; a += (Param.STATE_SIZE - 1))
            {
                for (int b = 0; b < Param.STATE_SIZE; b += (Param.STATE_SIZE - 1))
                {
                    for (int c = 0; c < Param.STATE_SIZE; c += (Param.STATE_SIZE - 1))
                    {
                        for (int d = 0; d < Param.STATE_SIZE; d += (Param.STATE_SIZE - 1))
                        {
                            for (int e = 0; e < Param.STATE_SIZE; e += (Param.STATE_SIZE - 1))
                            {
                                if (_table[a, b, c, d, e] == Param.STATE_SIZE - 1) {
                                    code += (uint)(1 << i);
                                }
                                i++;
                            }
                        }
                    }
                }
            }
            textBox1.Text = code.ToString("X2");
        }
        private void changeTable(uint code)
        {
            int i = 0;
            for (int a = 0; a < Param.STATE_SIZE; a += (Param.STATE_SIZE - 1))
            {
                for (int b = 0; b < Param.STATE_SIZE; b += (Param.STATE_SIZE - 1))
                {
                    for (int c = 0; c < Param.STATE_SIZE; c += (Param.STATE_SIZE - 1))
                    {
                        for (int d = 0; d < Param.STATE_SIZE; d += (Param.STATE_SIZE - 1))
                        {
                            for (int e = 0; e < Param.STATE_SIZE; e += (Param.STATE_SIZE - 1))
                            {
                                _table[a, b, c, d, e] = (byte)(((code & (uint)(1 << i)) == 0) ? 0 : Param.STATE_SIZE - 1);
                                i++;
                            }
                        }
                    }
                }
            }
        }
        private void progressCA()
        {
            for(int i = 0;i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    byte a = (i == 0) ? _caCur[(Param.CA_SIZE - 1), j] : _caCur[i - 1, j];
                    byte b = (j == 0) ? _caCur[i, (Param.CA_SIZE - 1)] : _caCur[i, j - 1];
                    byte c = _caCur[i, j];
                    byte d = (i == (Param.CA_SIZE - 1)) ? _caCur[0, j] : _caCur[i + 1, j];
                    byte e = (j == (Param.CA_SIZE - 1)) ? _caCur[i, 0] : _caCur[i, j + 1];
                    _caNext[i,j] = getNext(a, b, c, d, e);
                }
            }
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    _caCur[i, j] = _caNext[i, j];
                }
            }
        }
        private byte getNext(int a, int b, int c, int d, int e)
        {
            return _table[a, b, c, d, e];
        }
        private void updateImage()
        {
            Bitmap bitmap = new Bitmap(Param.CA_SIZE, Param.CA_SIZE);

            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            byte[] buf = new byte[bitmap.Width * bitmap.Height * 4];
            Marshal.Copy(data.Scan0, buf, 0, buf.Length);

            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    int lv = _caCur[i, j] * 16;
                    buf[4 * (i * Param.CA_SIZE + j) + 0] = 0;//a
//                    buf[4 * (i * Param.CA_SIZE + j) + 1] = (byte)lv;//r
//                    buf[4 * (i * Param.CA_SIZE + j) + 2] = (byte)lv;//g
                    buf[4 * (i * Param.CA_SIZE + j) + 3] = (byte)lv;//b
                }
            }
            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bitmap.UnlockBits(data);
            pictureBox1.Image = bitmap;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            refleshCA(5, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            refleshCA(50, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            refleshCA(95, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    if (i == Param.CA_SIZE / 2)
                    {
                        _caCur[i, j] = (Param.STATE_SIZE - 1);
                    }
                    else
                    {
                        _caCur[i, j] = 0;
                    }
                }
            }
        }
        private void Button14_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    if (j == Param.CA_SIZE / 2)
                    {
                        _caCur[i, j] = (Param.STATE_SIZE - 1);
                    }
                    else
                    {
                        _caCur[i, j] = 0;
                    }
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    if ((j == Param.CA_SIZE / 2) || (i == Param.CA_SIZE / 2))
                    {
                        _caCur[i, j] = (Param.STATE_SIZE - 1);
                    }
                    else
                    {
                        _caCur[i, j] = 0;
                    }
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    if ((i == (Param.CA_SIZE / 2) - 1) || (i == (Param.CA_SIZE / 2) + 1))
                    {
                        _caCur[i, j] = (Param.STATE_SIZE - 1);
                    }
                    else
                    {
                        _caCur[i, j] = 0;
                    }
                }
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    if ((j == (Param.CA_SIZE / 2) - 1) || (i == (Param.CA_SIZE / 2) - 1) ||
                        (j == (Param.CA_SIZE / 2) + 1) || (i == (Param.CA_SIZE / 2) + 1))
                    {
                        _caCur[i, j] = (Param.STATE_SIZE - 1);
                    }
                    else
                    {
                        _caCur[i, j] = 0;
                    }
                }
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    if ((j == (Param.CA_SIZE / 2) - 1) || (j == (Param.CA_SIZE / 2) + 1))
                    {
                        _caCur[i, j] = (Param.STATE_SIZE - 1);
                    }
                    else
                    {
                        _caCur[i, j] = 0;
                    }
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            refleshCA(5, Param.CA_SIZE / 4, Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            refleshCA(50, Param.CA_SIZE / 4, Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4);
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            refleshCA(95, Param.CA_SIZE / 4, Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4, 3 * Param.CA_SIZE / 4);
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string s = textBox1.Text;
                uint code = Convert.ToUInt32(s, 16);
                changeTable(code);
            }
        }
    }
    static class Param
    {
        public const int STATE_SIZE = 16;
        public const int CA_SIZE = 512;
    }
}
