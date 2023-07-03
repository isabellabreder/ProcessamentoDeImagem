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

        public static void RGBtoHSI(Bitmap imageSrc, Bitmap imgDest, Bitmap cinza, Bitmap imgH, Bitmap imgS, Bitmap imgI, int hue, int intensidade)
        {

            int width = imageSrc.Width, height = imageSrc.Height, pixelSize = 3;

            convert_to_grayDMA(imageSrc, cinza);

            BitmapData bitmapDataSrcC = imageSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDstC = imgDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataCinza = cinza.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataHue = imgH.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataSat = imgS.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataInt = imgI.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrcC.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDstC.Scan0.ToPointer();
                byte* cin = (byte*)bitmapDataCinza.Scan0.ToPointer();
                byte* bh = (byte*)bitmapDataHue.Scan0.ToPointer();
                byte* bs = (byte*)bitmapDataSat.Scan0.ToPointer();
                byte* bi = (byte*)bitmapDataInt.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrcC.Stride);
                    byte* dstLine = dst + (y * bitmapDataDstC.Stride);
                    byte* bhLine = bh + (y * bitmapDataHue.Stride);
                    byte* bsLine = bs + (y * bitmapDataSat.Stride);
                    byte* biLine = bi + (y * bitmapDataInt.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        ColorHsi hsi;

                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstLine + (x * pixelSize);
                        byte* bhPixel = bhLine + (x * pixelSize);
                        byte* bsPixel = bsLine + (x * pixelSize);
                        byte* biPixel = biLine + (x * pixelSize);


                        int b = srcPixel[0];
                        int g = srcPixel[1];
                        int r = srcPixel[2];

                        Color cor = Color.FromArgb(r, g, b);
                        hsi = HSI(cor, hue, intensidade);
                        cor = RGB(hsi);

                        dstPixel[0] = (byte)cor.B;
                        dstPixel[1] = (byte)cor.G;
                        dstPixel[2] = (byte)cor.R;

                        hsi.H = hsi.H > 255 ? 255 : hsi.H;
                        hsi.S = hsi.S > 255 ? 255 : hsi.S;
                        hsi.I = hsi.I > 255 ? 255 : hsi.I;

                        bhPixel[0] = (byte)hsi.H;
                        bhPixel[1] = (byte)hsi.H;
                        bhPixel[2] = (byte)hsi.H;

                        bsPixel[0] = (byte)hsi.S;
                        bsPixel[1] = (byte)hsi.S;
                        bsPixel[2] = (byte)hsi.S;
                        
                        biPixel[0] = (byte)hsi.I;
                        biPixel[1] = (byte)hsi.I;
                        biPixel[2] = (byte)hsi.I;
                        
                    }
                });

            }

            imageSrc.UnlockBits(bitmapDataSrcC);
            imgDest.UnlockBits(bitmapDataDstC);
            cinza.UnlockBits(bitmapDataCinza);
            imgH.UnlockBits(bitmapDataHue);
            imgS.UnlockBits(bitmapDataSat);
            imgI.UnlockBits(bitmapDataInt);

        }

        public static void RGBtoCMY(Bitmap imageBitmapSrc, Bitmap imageBitmapDest, Bitmap imgH, Bitmap imgS, Bitmap imgI)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

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

                        Color cor = CMY(Color.FromArgb(r, g, b));

                        dstPixel[0] = (byte)cor.B;
                        dstPixel[1] = (byte)cor.G;
                        dstPixel[2] = (byte)cor.R;
                        
                    }
                });

            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);

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

        public static void separaCanaisDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest, int canal)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

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

                        if(canal == 0)
                        {
                            dstPixel[0] = 0;
                            dstPixel[1] = 0;
                            dstPixel[2] = srcPixel[2];
                        }
                        else if(canal == 1)
                        {
                            dstPixel[0] = 0;
                            dstPixel[1] = srcPixel[1];
                            dstPixel[2] = 0;
                        }
                        else
                        {
                            dstPixel[0] = srcPixel[0];
                            dstPixel[1] = 0;
                            dstPixel[2] = 0;
                        }
                    }
                });
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
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

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                Parallel.For(0, height, y =>
                {
                    byte* srcLine = src + (y * bitmapDataSrc.Stride);
                    byte* dstCol = dst + ((height - y - 1) * pixelSize);

                    for (int x = 0; x < width; x++)
                    {
                        byte* srcPixel = srcLine + (x * pixelSize);
                        byte* dstPixel = dstCol + (x * bitmapDataDst.Stride);

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