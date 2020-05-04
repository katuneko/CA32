using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Runtime.InteropServices;

namespace CA32
{
    class CAWave
    {
        Random _r = new Random();
        byte[,] _caPrv = new byte[Param.CA_SIZE, Param.CA_SIZE];
        byte[,] _caCur = new byte[Param.CA_SIZE, Param.CA_SIZE];
        byte[,] _caNext = new byte[Param.CA_SIZE, Param.CA_SIZE];

        public double _neighbor = 0.2;
        public double _today = 1.0;
        public double _yesterday = 0.2;
        public CAWave(bool isReduceBlink, int prbStop, int prbDead)
        {
            refleshCA(50, 0, 0, Param.CA_SIZE, Param.CA_SIZE);
        }

        public void refleshCA(int prb, int x0, int y0, int x1, int y1)
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    _caCur[i, j] = (byte)_r.Next(Param.STATE_SIZE - 1);
                    _caPrv[i, j] = _caCur[i, j];
                }
            }
        }
        public void progressCA()
        {
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    byte LU = getStateLU(i, j);
                    byte L = getStateL(i, j);
                    byte LB = getStateLB(i, j);
                    byte U = getStateU(i, j);
                    byte ME = getStateME(i, j);
                    byte B = getStateB(i, j);
                    byte RU = getStateRU(i, j);
                    byte R = getStateR(i, j);
                    byte RB = getStateRB(i, j);
                    _caNext[i, j] = getNext(LU, L, LB, U, ME, B, RU, R, RB, _caPrv[i, j]);
                }
            }
            for (int i = 0; i < Param.CA_SIZE; i++)
            {
                for (int j = 0; j < Param.CA_SIZE; j++)
                {
                    _caPrv[i, j] = _caCur[i, j];
                    _caCur[i, j] = _caNext[i, j];
                }
            }
        }
        /*
        private byte getStateLU(int x, int y)
        {
            int i, j;
            i = (x == 0) ? (Param.CA_SIZE - 1) : (x - 1);
            j = (y == 0) ? (Param.CA_SIZE - 1) : (y - 1);
            int ret = _caCur[i, j] - _caPrv[i, j];
            if (ret > 255) { ret = 0; }
            if (ret < 0) { ret = 255; }
            return (byte)ret;
        }
        private byte getStateL(int x, int y)
        {
            int i, j;
            i = (x == 0) ? (Param.CA_SIZE - 1) : (x - 1);
            j = y;
            int ret = _caCur[i, j] - _caPrv[i, j];
            if (ret > 255) { ret = 0; }
            if (ret < 0) { ret = 255; }
            return (byte)ret;
        }
        private byte getStateLB(int x, int y)
        {
            int i, j;
            i = (x == 0) ? (Param.CA_SIZE - 1) : (x - 1);
            j = (y == (Param.CA_SIZE - 1)) ? 0 : (y + 1);
            int ret = _caCur[i, j] - _caPrv[i, j];
            if (ret > 255) { ret = 0; }
            if (ret < 0) { ret = 255; }
            return (byte)ret;
        }
        private byte getStateU(int x, int y)
        {
            int i, j;
            i = x;
            j = (y == 0) ? (Param.CA_SIZE - 1) : (y - 1);
            int ret = _caCur[i, j] - _caPrv[i, j];
            if (ret > 255) { ret = 0; }
            if (ret < 0) { ret = 255; }
            return (byte)ret;
        }
        private byte getStateME(int x, int y)
        {
            int i, j;
            i = x;
            j = y;
            return _caCur[i, j];
        }
        private byte getStateB(int x, int y)
        {
            int i, j;
            i = x;
            j = (y == (Param.CA_SIZE - 1)) ? 0 : (y + 1);
            int ret = _caCur[i, j] - _caPrv[i, j];
            if (ret > 255) { ret = 0; }
            if (ret < 0) { ret = 255; }
            return (byte)ret;
        }
        private byte getStateRU(int x, int y)
        {
            int i, j;
            i = (x == (Param.CA_SIZE - 1)) ? 0 : (x + 1);
            j = (y == 0) ? (Param.CA_SIZE - 1) : (y - 1);
            int ret = _caCur[i, j] - _caPrv[i, j];
            if (ret > 255) { ret = 0; }
            if (ret < 0) { ret = 255; }
            return (byte)ret;
        }
        private byte getStateR(int x, int y)
        {
            int i, j;
            i = (x == (Param.CA_SIZE - 1)) ? 0 : (x + 1);
            j = y;
            int ret = _caCur[i, j] - _caPrv[i, j];
            if (ret > 255) { ret = 0; }
            if (ret < 0) { ret = 255; }
            return (byte)ret;
        }
        private byte getStateRB(int x, int y)
        {
            int i, j;
            i = (x == (Param.CA_SIZE - 1)) ? 0 : (x + 1);
            j = (y == (Param.CA_SIZE - 1)) ? 0 : (y + 1);
            int ret = _caCur[i, j] - _caPrv[i, j];
            if (ret > 255) { ret = 0; }
            if (ret < 0) { ret = 255; }
            return (byte)ret;
        }
        */
        private byte getStateLU(int x, int y)
        {
            int i, j;
            i = (x == 0) ? (Param.CA_SIZE - 1) : (x - 1);
            j = (y == 0) ? (Param.CA_SIZE - 1) : (y - 1);
            return _caCur[i, j];
        }
        private byte getStateL(int x, int y)
        {
            int i, j;
            i = (x == 0) ? (Param.CA_SIZE - 1) : (x - 1);
            j = y;
            return _caCur[i, j];
        }
        private byte getStateLB(int x, int y)
        {
            int i, j;
            i = (x == 0) ? (Param.CA_SIZE - 1) : (x - 1);
            j = (y == (Param.CA_SIZE - 1)) ? 0 : (y + 1);
            return _caCur[i, j];
        }
        private byte getStateU(int x, int y)
        {
            int i, j;
            i = x;
            j = (y == 0) ? (Param.CA_SIZE - 1) : (y - 1);
            return _caCur[i, j];
        }
        private byte getStateME(int x, int y)
        {
            int i, j;
            i = x;
            j = y;
            return _caCur[i, j];
        }
        private byte getStateB(int x, int y)
        {
            int i, j;
            i = x;
            j = (y == (Param.CA_SIZE - 1)) ? 0 : (y + 1);
            return _caCur[i, j];
        }
        private byte getStateRU(int x, int y)
        {
            int i, j;
            i = (x == (Param.CA_SIZE - 1)) ? 0 : (x + 1);
            j = (y == 0) ? (Param.CA_SIZE - 1) : (y - 1);
            return _caCur[i, j];
        }
        private byte getStateR(int x, int y)
        {
            int i, j;
            i = (x == (Param.CA_SIZE - 1)) ? 0 : (x + 1);
            j = y;
            return _caCur[i, j];
        }
        private byte getStateRB(int x, int y)
        {
            int i, j;
            i = (x == (Param.CA_SIZE - 1)) ? 0 : (x + 1);
            j = (y == (Param.CA_SIZE - 1)) ? 0 : (y + 1);
            return _caCur[i, j];
        }
        private byte getNext(int LU, int L, int LB, int U, int ME, int B, int RU, int R, int RB, int PV)
        {
            double[,] table = new double[3, 3]
            {
                {0,-_neighbor/4,0},
                {-_neighbor/4,_today,_neighbor},
                {0,_neighbor,0},
            };
            int ave = (int)((LU* table[0,0] + L * table[0, 1] + LB * table[0, 2] + U * table[1, 0] + B * table[1, 2] + RU * table[2, 0] + R * table[2, 1] + RB * table[2, 2]) / 8);
//            int ave = (LU+ L+ LB+ U+ B + RU + R + RB) / 8;
            int next;
            if (ave == 255)
            {
                next = 0;
            }else if(ave == 0)
            {
                next = 255;
            }
            else
            {
                next = ((int)(ME * _today) - (int)(PV * _yesterday)) + (int)(ave);
                if (next > 255)
                {
                    next = 255;
                }
                else if(next < 0)
                {
                    next = 0;
                }
            }
            return (byte)next;
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
                    buf[4 * (i * Param.CA_SIZE + j) + 0] = 0;
                    buf[4 * (i * Param.CA_SIZE + j) + 3] = (byte)(255 - lv);
                }
            }
            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bitmap.UnlockBits(data);
            return bitmap;
        }
    }
}
