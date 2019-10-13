using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CA32
{
    class CArgb
    {
        Random _r = new Random();
        byte[] _table;
        byte[,,] _caCur = new byte[Param.CA_SIZE, Param.CA_SIZE, 3];
        byte[,,] _caNext = new byte[Param.CA_SIZE, Param.CA_SIZE, 3];
        bool _isReduceBlink;
        public int _probStop;
        public int _probDead;
        public CArgb(bool isReduceBlink, int prbStop, int prbDead)
        {
            refleshCA(50, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
            _isReduceBlink = isReduceBlink;
            _probStop = 100 - prbStop;
            _probDead = 100 - prbDead;
            _table = new byte[Param.CA_SIZE << 19];
            refleshTable();
        }

        public void refleshCA(int prb, int x0, int y0, int x1, int y1)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (prb == 0)
                        {
                            _caCur[i, j, k] = 0;
                        }
                        else
                        {
                            if ((x0 <= i) && (i <= x1) && (y0 <= j) && (j <= y1))
                            {
                                _caCur[i, j, k] = (byte)(((prb - 1) < _r.Next(100)) ? 0 : (Param.STATE_SIZE - 1));
                            }
                            else
                            {
                                _caCur[i, j, k] = 0;
                            }
                        }
                    }
                }
            }
        }
        public void refleshLine(int mode)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        bool isWrite = false;
                        if (k == 1)
                        {
                            
                            if ((mode & 1) != 0)
                            {
                                if (i == Param.CA_SIZE / 2)
                                {
                                    _caCur[i, j, k] = (Param.STATE_SIZE - 1);
                                    isWrite = true;
                                }
                            }
                            if ((mode & 2) != 0)
                            {
                                if (j == Param.CA_SIZE / 2)
                                {
                                    _caCur[i, j, k] = (Param.STATE_SIZE - 1);
                                    isWrite = true;
                                }
                            }
                            if ((mode & 4) != 0)
                            {
                                if ((i == (Param.CA_SIZE / 2) - 1) || (i == (Param.CA_SIZE / 2) + 1))
                                {
                                    _caCur[i, j, k] = (Param.STATE_SIZE - 1);
                                    isWrite = true;
                                }
                            }
                            if ((mode & 8) != 0)
                            {
                                if ((j == (Param.CA_SIZE / 2) - 1) || (j == (Param.CA_SIZE / 2) + 1))
                                {
                                    _caCur[i, j, k] = (Param.STATE_SIZE - 1);
                                    isWrite = true;
                                }
                            }
                            if ((mode & 16) != 0)
                            {
                                if ((i == Param.CA_SIZE / 2) && (j == Param.CA_SIZE / 2))
                                {
                                    _caCur[i, j, k] = (Param.STATE_SIZE - 1);
                                    isWrite = true;
                                }
                            }
                        }
                        if (!isWrite)
                        {
                            _caCur[i, j, k] = 0;
                        }
                    }
                }
            }
        }
        public byte getVal(int code, int val)
        {
            return (byte)(((code & (1U << val)) != 0) ? 1 : 0);
        }
        public uint refleshTable()
        {
            for (int i = 0; i < (Param.STATE_SIZE << 19); i++)
            {
                bool bExec = false;
                if (_isReduceBlink)
                {
                    if (i == 0)
                    {
                        _table[i] = 0;
                        bExec = true;
                    }
                    else if (i == ((Param.STATE_SIZE << 19) - 1))
                    {
                        _table[i] = 0;
                        bExec = true;
                    }
                }
                if (!bExec)
                {
                    byte bySelf = getVal(i, 9);
                    if (_probStop < _r.Next(100))
                    {
                        _table[i] = bySelf;
                    }
                    else if (_probDead < _r.Next(100))
                    {
                        _table[i] = 0;
                    }
                    else if (50 < _r.Next(100))
                    {
                        _table[i] = (byte)((bySelf + 1) % Param.STATE_SIZE);
                    }
                    else
                    {
                        _table[i] = (byte)((bySelf == 0) ? Param.STATE_SIZE - 1 : bySelf - 1);
                    }
                }
            }
            return 0;
        }
        public void progressCA()
        {
            int[,] axisTable = new int[19,3] { 
                { -1, 0, 1 },
                { 1, 0, 1 },
                { 0, -1, 1 },
                { 0, 1, 1 },
                { 0, 0, 1 },

                { -1, 0, 0 },
                { 1, 0, 0 },
                { 0, -1, 0 },
                { 0, 1, 0 },
                { 0, 0, 0 },

                { -1, -1, 0 },
                { -1, 1, 0 },
                { 1, -1, 0 },
                { 1, 1, 0 },

                { -1, 0, -1 },
                { 1, 0, -1 },
                { 0, -1, -1 },
                { 0, 1, -1 },
                { 0, 0, -1 },
            };
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        int code = 0;
                        for (int axis = 0; axis < 19; axis++)
                        {
                            int x = (i + axisTable[axis, 0]) & (Param.CA_SIZE - 1);
                            int y = (j + axisTable[axis, 1]) & (Param.CA_SIZE - 1);
                            int z = (k + axisTable[axis, 2]);
                            if(z == -1)
                            {
                                z = 2;
                            }else if (z == 3)
                            {
                                z = 0;
                            }
                            code += _caCur[x, y, z] << axis;
                        }
                        _caNext[i, j, k] = getNext(code);
                    }
                }
            }
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        _caCur[i, j, k] = _caNext[i, j, k];
                    }
                }
            }
        }
        private byte getNext(int code)
        {
            return _table[code];
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
                    int k;
                    byte b, c, d, e, f;
                    k = 0;
                    b = (i == 0) ? _caCur[(Param.CA_SIZE - 1), j, k] : _caCur[i - 1, j, k];
                    c = (j == 0) ? _caCur[i, (Param.CA_SIZE - 1), k] : _caCur[i, j - 1, k];
                    d = _caCur[i, j, k];
                    e = (j == (Param.CA_SIZE - 1)) ? _caCur[i, 0, k] : _caCur[i, j + 1, k];
                    f = (i == (Param.CA_SIZE - 1)) ? _caCur[0, j, k] : _caCur[i + 1, j, k];
                    int R = b << 2 + c << 3 + d << 4 + e << 1 + f;

                    k = 1;
                    b = (i == 0) ? _caCur[(Param.CA_SIZE - 1), j, k] : _caCur[i - 1, j, k];
                    c = (j == 0) ? _caCur[i, (Param.CA_SIZE - 1), k] : _caCur[i, j - 1, k];
                    d = _caCur[i, j, k];
                    e = (j == (Param.CA_SIZE - 1)) ? _caCur[i, 0, k] : _caCur[i, j + 1, k];
                    f = (i == (Param.CA_SIZE - 1)) ? _caCur[0, j, k] : _caCur[i + 1, j, k];
                    int G = b << 2 + c << 3 + d << 4 + e << 1 + f;

                    k = 2;
                    b = (i == 0) ? _caCur[(Param.CA_SIZE - 1), j, k] : _caCur[i - 1, j, k];
                    c = (j == 0) ? _caCur[i, (Param.CA_SIZE - 1), k] : _caCur[i, j - 1, k];
                    d = _caCur[i, j, k];
                    e = (j == (Param.CA_SIZE - 1)) ? _caCur[i, 0, k] : _caCur[i, j + 1, k];
                    f = (i == (Param.CA_SIZE - 1)) ? _caCur[0, j, k] : _caCur[i + 1, j, k];
                    int B = b << 2 + c << 3 + d << 4 + e << 1 + f;

                    buf[4 * (i * Param.CA_SIZE + j) + 3] = 255;
                    buf[4 * (i * Param.CA_SIZE + j) + 1] = (byte)((R * 255 / 64));
                    buf[4 * (i * Param.CA_SIZE + j) + 2] = (byte)((G * 255 / 64));
                    buf[4 * (i * Param.CA_SIZE + j) + 0] = (byte)((B * 255 / 64));
                }
            }
            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bitmap.UnlockBits(data);
            
            return bitmap;
        }
    }
}
