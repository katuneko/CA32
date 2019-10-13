using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CA32
{
    class CA32
    {
        Random _r = new Random();
        byte[,,,,] _table = new byte[Param.STATE_SIZE, Param.STATE_SIZE, Param.STATE_SIZE, Param.STATE_SIZE, Param.STATE_SIZE];
        byte[,] _caCur = new byte[Param.CA_SIZE, Param.CA_SIZE];
        byte[,] _caNext = new byte[Param.CA_SIZE, Param.CA_SIZE];
        bool _isReduceBlink;
        int _probStop;
        int _probDead;
        public CA32(bool isReduceBlink, int prbStop, int prbDead)
        {
            refleshCA(50, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
            _isReduceBlink = isReduceBlink;
            _probStop = 100 - prbStop;
            _probDead = 100 - prbDead;
            refleshTable();
        }

        public void refleshCA(int prb, int x0, int y0, int x1, int y1)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    if (prb == 0)
                    {
                        _caCur[i, j] = 0;
                    }
                    else
                    {
                        if ((x0 <= i) && (i <= x1) && (y0 <= j) && (j <= y1))
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
        public uint refleshTable()
        {
            for (int a = 0; a < Param.STATE_SIZE; a++)
            {
                for (int b = 0; b < Param.STATE_SIZE; b++)
                {
                    for (int c = 0; c < Param.STATE_SIZE; c++)
                    {
                        for (int d = 0; d < Param.STATE_SIZE; d++)
                        {
                            for (int e = 0; e < Param.STATE_SIZE; e++)
                            {
                                bool bExec = false;
                                if (_isReduceBlink)
                                {
                                    if ((a == 0) && (b == 0) && (c == 0) && (d == 0) && (e == 0))
                                    {
                                        _table[a, b, c, d, e] = 0;
                                        bExec = true;
                                    }
                                    else if ((a == Param.STATE_SIZE - 1) &&
                                             (b == Param.STATE_SIZE - 1) &&
                                             (c == Param.STATE_SIZE - 1) &&
                                             (d == Param.STATE_SIZE - 1) &&
                                             (e == Param.STATE_SIZE - 1))
                                    {
                                        _table[a, b, c, d, e] = 0;
                                        bExec = true;
                                    }
                                }
                                if (!bExec)
                                {
                                    if (_probStop < _r.Next(100))
                                    {
                                        _table[a, b, c, d, e] = (byte)c;
                                    }
                                    else if (_probDead < _r.Next(100))
                                    {
                                        _table[a, b, c, d, e] = 0;
                                    }
                                    else if (50 < _r.Next(100))
                                    {
                                        _table[a, b, c, d, e] = (byte)((c + 1) % Param.STATE_SIZE);
                                    }
                                    else
                                    {
                                        _table[a, b, c, d, e] = (byte)((c == 0) ? Param.STATE_SIZE - 1 : c - 1);
                                    }
                                }
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
                                if (_table[a, b, c, d, e] == Param.STATE_SIZE - 1)
                                {
                                    code += (uint)(1 << i);
                                }
                                i++;
                            }
                        }
                    }
                }
            }
            return code;
        }
        public void loadTable(uint code)
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
        public void progressCA()
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
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
        public Bitmap updateImage()
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
                    int lv = _caCur[i, j] * 255 / (Param.STATE_SIZE - 1);
                    buf[4 * (i * Param.CA_SIZE + j) + 0] = 0;//a
                                                             //                    buf[4 * (i * Param.CA_SIZE + j) + 1] = (byte)lv;//r
                                                             //                    buf[4 * (i * Param.CA_SIZE + j) + 2] = (byte)lv;//g
                    buf[4 * (i * Param.CA_SIZE + j) + 3] = (byte)lv;//b
                }
            }
            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bitmap.UnlockBits(data);
            return bitmap;
        }
    }
}
