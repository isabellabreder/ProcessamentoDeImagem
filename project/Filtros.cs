using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ProcessamentoImagens
{
    class Filtros
    {
        //sem acesso direto a memória
        public static void convert_to_gray(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;
            Int32 gs;

            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = imageBitmapSrc.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;
                    gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);

                    //nova cor
                    Color newcolor = Color.FromArgb(gs, gs, gs);

                    imageBitmapDest.SetPixel(x, y, newcolor);
                }
            });
        }

        //sem acesso direito a memória
        public static void negativo(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;

            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = imageBitmapSrc.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;

                    //nova cor
                    Color newcolor = Color.FromArgb(255 - r, 255 - g, 255 - b);

                    imageBitmapDest.SetPixel(x, y, newcolor);
                }
            });
        }

        //com acesso direto a memória
        public static void convert_to_grayDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;
            Int32 gs;

            //lock dados bitmap origem
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + (x * pixelSize);

                        int b = srcPixel[0];
                        int g = srcPixel[1];
                        int r = srcPixel[2];
                        gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);

                        dstPixel[0] = (byte)gs;
                        dstPixel[1] = (byte)gs;
                        dstPixel[2] = (byte)gs;
                    }
                });
            }

            //unlock dados bitmap origem
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock dados bitmap destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        //com acesso direto a memória
        public static void negativoDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            //lock dados bitmap origem
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + (x * pixelSize);

                        dstPixel[0] = (byte)(255 - srcPixel[0]);
                        dstPixel[1] = (byte)(255 - srcPixel[1]);
                        dstPixel[2] = (byte)(255 - srcPixel[2]);
                    }
                });
            }

            //unlock dados bitmap origem
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock dados bitmap destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void RGBtoHSI(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;

            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    Color cor = imageBitmapSrc.GetPixel(x, y);
                    double r = cor.R / 255.0;
                    double g = cor.G / 255.0;
                    double b = cor.B / 255.0;

                    double h = 0.0;
                    double s = 0.0;
                    double i = 0.0;

                    double minRGB = Math.Min(Math.Min(r, g), b);
                    double maxRGB = Math.Max(Math.Max(r, g), b);
                    double delta = maxRGB - minRGB;

                    // Calcula o valor da intensidade (I)
                    i = (r + g + b) / 3.0;

                    // Caso especial: se a intensidade for zero, então a cor é preta (H = 0 e S = 0)
                    if (i != 0.0)
                    {
                        // Calcula a saturação (S)
                        s = 1 - (minRGB / i);

                        // Calcula o matiz (H)
                        if (delta == 0)
                        {
                            h = 0;
                        }
                        else if (r == maxRGB)
                        {
                            h = (g - b) / delta;
                        }
                        else if (g == maxRGB)
                        {
                            h = 2 + (b - r) / delta;
                        }
                        else if (b == maxRGB)
                        {
                            h = 4 + (r - g) / delta;
                        }

                        h *= 60;
                        if (h < 0)
                        {
                            h += 360;
                        }
                    }

                    int newHue = (int)Math.Round(h);
                    int newSaturation = (int)Math.Round(s * 255);
                    int newIntensity = (int)Math.Round(i * 255);

                    Color newColor = Color.FromArgb(newHue, newSaturation, newIntensity);
                    imageBitmapDest.SetPixel(x, y, newColor);
                }
            });
        }

        public static void RGBtoCMY(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;

            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    Color cor = imageBitmapSrc.GetPixel(x, y);
                    int c = 255 - cor.R;
                    int m = 255 - cor.G;
                    int y = 255 - cor.B;

                    Color newColor = Color.FromArgb(c, m, y);
                    imageBitmapDest.SetPixel(x, y, newColor);
                }
            });
        }

        public static void pretoBrancoDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + (x * pixelSize);

                        int gs = (int)(srcPixel[2] * 0.2990 + srcPixel[1] * 0.5870 + srcPixel[0] * 0.1140);

                        if (gs > 127)
                        {
                            dstPixel[0] = 255;
                            dstPixel[1] = 255;
                            dstPixel[2] = 255;
                        }
                        else
                        {
                            dstPixel[0] = 0;
                            dstPixel[1] = 0;
                            dstPixel[2] = 0;
                        }
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void espelhaDiagonalDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + ((width - x - 1) * pixelSize);

                        dstPixel[0] = srcPixel[0];
                        dstPixel[1] = srcPixel[1];
                        dstPixel[2] = srcPixel[2];
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        // sem acesso direto a memória
        public static void inverteRBDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + (x * pixelSize);

                        dstPixel[0] = (byte)(255 - srcPixel[0]);
                        dstPixel[1] = srcPixel[1];
                        dstPixel[2] = (byte)(255 - srcPixel[2]);
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void espelhaDiagonalDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + ((width - x - 1) * pixelSize);

                        dstPixel[0] = srcPixel[0];
                        dstPixel[1] = srcPixel[1];
                        dstPixel[2] = srcPixel[2];
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void inverteRBDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + (x * pixelSize);

                        dstPixel[0] = (byte)(255 - srcPixel[0]);
                        dstPixel[1] = srcPixel[1];
                        dstPixel[2] = (byte)(255 - srcPixel[2]);
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void separaCanaisDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDestR, Bitmap imageBitmapDestG, Bitmap imageBitmapDestB)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDstR = imageBitmapDestR.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDstG = imageBitmapDestG.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDstB = imageBitmapDestB.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dstR = (byte*)bitmapDataDstR.Scan0.ToPointer();
                byte* dstG = (byte*)bitmapDataDstG.Scan0.ToPointer();
                byte* dstB = (byte*)bitmapDataDstB.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLineR = dstR + (y * bitmapDataDstR.Stride);
                    byte* dstLineG = dstG + (y * bitmapDataDstG.Stride);
                    byte* dstLineB = dstB + (y * bitmapDataDstB.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixelR = dstLineR + (x * pixelSize);
                        byte* dstPixelG = dstLineG + (x * pixelSize);
                        byte* dstPixelB = dstLineB + (x * pixelSize);

                        dstPixelR[0] = srcPixel[0];
                        dstPixelR[1] = 0;
                        dstPixelR[2] = 0;

                        dstPixelG[0] = 0;
                        dstPixelG[1] = srcPixel[1];
                        dstPixelG[2] = 0;

                        dstPixelB[0] = 0;
                        dstPixelB[1] = 0;
                        dstPixelB[2] = srcPixel[2];
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDestR.UnlockBits(bitmapDataDstR);
            imageBitmapDestG.UnlockBits(bitmapDataDstG);
            imageBitmapDestB.UnlockBits(bitmapDataDstB);
        }

        public static void rotacionaDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, height, width),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int paddingSrc = bitmapDataSrc.Stride - (width * pixelSize);
            int paddingDst = bitmapDataDst.Stride - (height * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * pixelSize);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + ((height - y - 1) * bitmapDataDst.Stride) + (x * pixelSize);

                        dstPixel[0] = srcPixel[0];
                        dstPixel[1] = srcPixel[1];
                        dstPixel[2] = srcPixel[2];
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void horizontalDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + ((width - x - 1) * pixelSize);

                        dstPixel[0] = srcPixel[0];
                        dstPixel[1] = srcPixel[1];
                        dstPixel[2] = srcPixel[2];
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void verticalDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + ((height - y - 1) * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + (x * pixelSize);

                        dstPixel[0] = srcPixel[0];
                        dstPixel[1] = srcPixel[1];
                        dstPixel[2] = srcPixel[2];
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void fatiamento(Bitmap imageBitmapSrc, Bitmap imageBitmapDest, int threshold)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + (x * pixelSize);

                        byte gray = (byte)((srcPixel[0] + srcPixel[1] + srcPixel[2]) / 3);
                        byte newColor = gray < threshold ? (byte)0 : (byte)255;

                        dstPixel[0] = newColor;
                        dstPixel[1] = newColor;
                        dstPixel[2] = newColor;
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        // sem acesso direto a memória
        public static void equalizacao(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            // Cria um histograma de tons de cinza
            int[] histogram = new int[256];

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + (x * pixelSize);

                        byte gray = (byte)((srcPixel[0] + srcPixel[1] + srcPixel[2]) / 3);
                        histogram[gray]++;
                    }
                });
            }

            // Calcula a função de distribuição acumulada
            int[] cumulativeHistogram = new int[256];
            cumulativeHistogram[0] = histogram[0];
            for (int i = 1; i < 256; i++)
            {
                cumulativeHistogram[i] = cumulativeHistogram[i - 1] + histogram[i];
            }

            // Equaliza a imagem
            double scalingFactor = 255.0 / (width * height);
            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstLine = dst + (y * bitmapDataDst.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + (x * pixelSize);

                        byte gray = (byte)((srcPixel[0] + srcPixel[1] + srcPixel[2]) / 3);
                        byte newColor = (byte)(cumulativeHistogram[gray] * scalingFactor);

                        dstPixel[0] = newColor;
                        dstPixel[1] = newColor;
                        dstPixel[2] = newColor;
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void suavizacao(Bitmap imageBitmapSrc, Bitmap imageBitmapDst, int tam)
        {
            
        }
        public static ColorHsi HSI(Color RGB, int hue, int intensidade)
        {
            double r, g, b;
            double h, s, i;

            if (RGB.R + RGB.G + RGB.B == 255 && RGB.R == 197)
                r = 1;

            if (RGB.R + RGB.G + RGB.B > 0)
            {
                r = ((double)RGB.R) / (RGB.R + RGB.G + RGB.B);
                g = ((double)RGB.G) / (RGB.R + RGB.G + RGB.B);
                b = ((double)RGB.B) / (RGB.R + RGB.G + RGB.B);
            }
            else
                r = g = b = 0;
            
            h = Math.Sqrt(Math.Pow(r - g, 2) + (r - b) * (g - b));
            if(h != 0)
                h = (0.5 * ((r - g) + (r - b))) / h;
            h = Math.Acos(h);

            if (b > g)
                h = 360 - h;

            h += (hue * 0.1);
            h = h > 360 ? 360 : h;

            s = 1.0 - 3.0 * Math.Min(r, Math.Min(g, b));

            i = (RGB.R + RGB.G + RGB.B) / (3.0 * 255.0);

            i += i * intensidade / 100;
            return new ColorHsi((int)(h * 180.0 / Math.PI), (int)(s * 100), (int)(i * 255));
        }

        public static Color CMY(Color RGB)
        {
            return Color.FromArgb(255 - RGB.R, 255 - RGB.G, 255 - RGB.B);
        }

        public static Color RGB(ColorHsi HSI)
        {
            double h, s, i, x, y, z;
            h = HSI.H * Math.PI / 180.0;
            s = HSI.S / 100.0;
            i = HSI.I / 255.0;

            x = i * (1.0 - s);            
            
            Color rgb;
            if (h < 2.0 * Math.PI / 3.0)
            {
                y = i * (1.0 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3.0 - h)));
                z = 3.0 * i - (x + y);

                x *= 255; x = x > 255 ? 255 : x; x = x < 0 ? 0 : x;
                y *= 255; y = y > 255 ? 255 : y; y = y < 0 ? 0 : y;
                z *= 255; z = z > 255 ? 255 : z; z = z < 0 ? 0 : z;

                rgb = Color.FromArgb((int)y, (int)z, (int)x);
            }
            else if(h < 4.0 * Math.PI / 3.0)
            {
                h = h - 2.0 * Math.PI / 3.0;
                y = i * (1.0 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3.0 - h)));
                z = 3.0 * i - (x + y);

                x *= 255;  x = x > 255 ? 255 : x; x = x < 0 ? 0 : x;
                y *= 255;  y = y > 255 ? 255 : y; y = y < 0 ? 0 : y;
                z *= 255;  z = z > 255 ? 255 : z; z = z < 0 ? 0 : z;                

                rgb = Color.FromArgb((int)x , (int)y, (int)z);
            }
            else
            {
                h = h - 4 * Math.PI / 3;
                y = i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3.0 - h)));
                z = 3.0 * i - (x + y);

                x *= 255; x = x > 255 ? 255 : x; x = x < 0 ? 0 : x;
                y *= 255; y = y > 255 ? 255 : y; y = y < 0 ? 0 : y;
                z *= 255; z = z > 255 ? 255 : z; z = z < 0 ? 0 : z;

                rgb = Color.FromArgb((int)z, (int)x, (int)y);
            }

            return rgb;
        }
    }
}