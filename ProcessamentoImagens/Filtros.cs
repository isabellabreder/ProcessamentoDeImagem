using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProcessamentoImagens
{
    class Filtros
    {
        //sem acesso direto a memoria
        public static void convert_to_gray(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;
            Int32 gs;

            for (int y = 0; y < height; y++)
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
            }
        }

        //sem acesso direito a memoria
        public static void negativo(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;

            for (int y = 0; y < height; y++)
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
            }
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

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src++); //está armazenado dessa forma: b g r 
                        g = *(src++);
                        r = *(src++);
                        gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);
                        *(dst++) = (byte)gs;
                        *(dst++) = (byte)gs;
                        *(dst++) = (byte)gs;
                    }
                    src += padding;
                    dst += padding;
                }
            }
            //unlock imagem origem
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        //com acesso direito a memoria
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
                byte* src1 = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src1++); //está armazenado dessa forma: b g r 
                        g = *(src1++);
                        r = *(src1++);

                        *(dst++) = (byte)(255 - b);
                        *(dst++) = (byte)(255 - g);
                        *(dst++) = (byte)(255 - r);
                    }
                    src1 += padding;
                    dst += padding;
                }
            }
            //unlock imagem origem 
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        internal static void RBGtoHSI(Bitmap imageSrc, Bitmap imgDest, Bitmap imgH, Bitmap imgS, Bitmap imgI)
        {
            double h, s, i;
            double r, g, b;
            for(int y = 0; y < imageSrc.Height; y++)
                for(int x = 0; x < imageSrc.Width; x++)
                {
                    Color hsi = HSI(imageSrc.GetPixel(x, y));

                    imgDest.SetPixel(x, y, hsi);
                    imgH.SetPixel(x, y, Color.FromArgb(hsi.R, 0, 0));
                    imgS.SetPixel(x, y, Color.FromArgb(0, hsi.G, 0));
                    imgI.SetPixel(x, y, Color.FromArgb(0, 0, hsi.B));
                }            
        }

        public static void pretoBrancoDMA(Bitmap imagebitmapSrc, Bitmap imagebitmapDest)
        {
            convert_to_grayDMA(imagebitmapSrc, imagebitmapDest);

            int width = imagebitmapSrc.Width, height = imagebitmapSrc.Height, padding, i, j;

            BitmapData bitmapDataSrc = imagebitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bitmapDataDst = imagebitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                padding = bitmapDataSrc.Stride - (3 * width);

                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int px;

                for(i = 0; i < height; i++)
                {
                    for(j = 0; j < width; j++)
                    {
                        for(int k = 0; k < 3; k++)
                        {
                            px = *(dst);

                            if (px > 127)
                                px = 255;
                            else
                                px = 0;

                            *(dst++) = (byte)px;
                        }
                    }

                    dst += padding;
                }
            }

            imagebitmapSrc.UnlockBits(bitmapDataSrc);
            imagebitmapDest.UnlockBits(bitmapDataDst);
        }
        
        public static void espelhaDiagonalDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
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
                byte* src1 = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();
                byte* aux;

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x ++)
                    {
                        b = *(src1++); //está armazenado dessa forma: b g r 
                        g = *(src1++);
                        r = *(src1++);

                        aux = dst + ((height - y - 1) * bitmapDataDst.Stride) + ((width - x) * 3 - 3);

                        *(aux++) = (byte)b;
                        *(aux++) = (byte)g;
                        *(aux++) = (byte)r;
                    }
                    src1 += padding;
                }
            }
            //unlock imagem origem 
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void inverteRBDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width, heigth = imageBitmapSrc.Height, padding, i, j ;

            BitmapData bitmapDataScr = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, heigth),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, heigth),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                int r, g, b;

                byte* scr = (byte*)bitmapDataScr.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                padding = bitmapDataScr.Stride - (width * 3);

                for (i = 0; i < heigth; i++)
                {
                    for(j = 0; j < width; j++)
                    {
                        b = *(scr++);
                        g = *(scr++);
                        r = *(scr++);

                        *(dst++) = (byte)r;
                        *(dst++) = (byte)g;
                        *(dst++) = (byte)b;
                    }

                    scr += padding;
                    dst += padding;
                }

                imageBitmapSrc.UnlockBits(bitmapDataScr);
                imageBitmapDest.UnlockBits(bitmapDataDst);
            }            
        }

        public static void separaCanaisDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest, int canal)
        {
            int width = imageBitmapSrc.Width, height = imageBitmapSrc.Height, padding, i, j;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height), 
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* scr = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                padding = bitmapDataSrc.Stride - (3 * width);
                int r, g, b;

                for(i = 0; i < height; i++)
                {
                    for(j = 0; j < width; j++)
                    {
                        b = *(scr++);
                        g = *(scr++);
                        r = *(scr++);

                        if (canal == 0)

                        {
                            *(dst++) = 0;
                            *(dst++) = 0;
                            *(dst++) = (byte)r;
                        }
                        else if (canal == 1)
                        {
                            *(dst++) = 0;
                            *(dst++) = (byte)g;
                            *(dst++) = 0;
                        }
                            
                        else
                        {
                            *(dst++) = (byte)b;
                            *(dst++) = 0;
                            *(dst++) = 0;
                        }
                    }

                    scr += padding;
                    dst += padding;
                }
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void rotacionaDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width, height = imageBitmapSrc.Height, i, j, paddingSrc, paddingDst;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, imageBitmapDest.Width, imageBitmapDest.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();
                paddingSrc = bitmapDataSrc.Stride - (3 * width);

                int r, g, b;
                for(i = 0; i < height; i++)
                {
                    for(j = 0; j < width; j++)
                    {
                        b = *(src++);
                        g = *(src++);
                        r = *(src++);

                        *(dst) = (byte)b;
                        *(dst + bitmapDataDst.Stride) = (byte)g;
                        *(dst + bitmapDataDst.Stride) = (byte)r;
                    }

                    src += paddingSrc;
                    dst += 3;
                }
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void horizontalDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width, height = imageBitmapSrc.Height, i, j, padding;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, imageBitmapDest.Width, imageBitmapDest.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();
                byte* aux;
                padding = bitmapDataSrc.Stride - (3 * width);

                int r, g, b;
                for (i = 0; i < height; i++)
                {
                    for (j = 0; j < width; j++)
                    {
                        b = *(src++);
                        g = *(src++);
                        r = *(src++);
                        
                        aux = dst + (i * bitmapDataDst.Stride) + (width * 3) - (j * 3);

                        *(aux++) = (byte)b;
                        *(aux++) = (byte)g;
                        *(aux++) = (byte)r;
                    }
                    src += padding;
                }
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void verticallDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width, height = imageBitmapSrc.Height, i, j, padding;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, imageBitmapDest.Width, imageBitmapDest.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();
                byte* aux;
                padding = bitmapDataSrc.Stride - (3 * width);

                int r, g, b;
                for (i = 0; i < height; i++)
                {
                    aux = dst + bitmapDataDst.Stride * (height - 1 - i);

                    for (j = 0; j < width; j++)
                    {
                        b = *(src++);
                        g = *(src++);
                        r = *(src++);

                        *(aux++) = (byte)b;
                        *(aux++) = (byte)g;
                        *(aux++) = (byte)r;
                    }
                    src += padding;
                }
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void fatiamento(Bitmap imageBitmapSrc, Bitmap imageBitmapDst, String name, int k)
        {
            if(k >= 0)
            {
                int width = imageBitmapSrc.Width, heigh = imageBitmapSrc.Height, padding;
                Bitmap aux = new Bitmap(width, heigh);

                convert_to_grayDMA(imageBitmapSrc, aux);

                BitmapData bitmapDataSrc = aux.LockBits(new Rectangle(0, 0, width, heigh), ImageLockMode.ReadWrite,
                    PixelFormat.Format24bppRgb);
                BitmapData bitmapDataDst = imageBitmapDst.LockBits(new Rectangle(0, 0, imageBitmapDst.Width, imageBitmapDst.Height), ImageLockMode.ReadWrite
                    , PixelFormat.Format24bppRgb);

                unsafe
                {
                    padding = bitmapDataDst.Stride - width * 3;

                    byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                    byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                    int b, a;
                    for (int i = 0; i < heigh; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            b = *(src++);
                            b = *(src++);
                            b = *(src++);

                            for(int l = 0; l < 3; l++)
                            {
                                a = 0;
                                a |= (b << k);
                                *(dst++) = (byte)a;
                            }
                        }

                        dst += padding;
                        src += padding;
                    }
                }

                aux.UnlockBits(bitmapDataSrc);
                imageBitmapDst.UnlockBits(bitmapDataDst);

                imageBitmapDst.Save(name.Replace(".jpg", "") + k + ".jpg");
                fatiamento(imageBitmapSrc, imageBitmapDst, name, k - 1);
            }
        }

        public static void equalizacao(Bitmap imageBitmapSrc, Bitmap imageBitmapDst)
        {
            int width = imageBitmapSrc.Width, heigh = imageBitmapSrc.Height, padding, i, j, p, fator = (width * heigh) / 255;
            int[] vet = new int[255];

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, heigh), ImageLockMode.ReadWrite
                , PixelFormat.Format24bppRgb);

            BitmapData bitmapDataDst = imageBitmapDst.LockBits(new Rectangle(0, 0, width, heigh), ImageLockMode.ReadWrite
                , PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();
                padding = bitmapDataDst.Stride - 3 * width;

                for (i = 0; i < heigh; i++)
                {
                    for (j = 0; j < width; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            p = *(src++);
                            vet[p]++;
                        }
                    }
                    src += padding;
                }

                src = (byte*)bitmapDataSrc.Scan0.ToPointer();

                for(i = 0; i < heigh; i++)
                {
                    for(j = 0; j < width; j++)
                    {
                        
                    }
                }
            }

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imageBitmapDst.UnlockBits(bitmapDataDst);

        }

        public static void suavizacao(Bitmap imageBitmapSrc, Bitmap imageBitmapDst, int tam)
        {
            
        }
        

        public static Color HSI(Color RGB)
        {
            double r, g, b;
            double h, s, i;

            if (RGB.R + RGB.G + RGB.B > 0)
            {
                r = ((double)RGB.R) / (RGB.R + RGB.G + RGB.B);
                g = ((double)RGB.G) / (RGB.R + RGB.G + RGB.B);
                b = ((double)RGB.B) / (RGB.R + RGB.G + RGB.B);
            }
            else
                r = g = b = 0;

            h = Math.Sqrt(Math.Pow(r - g, 2) + (r - b) * (g - b));
            h = (0.5 * ((r - g) + (r - b))) / (h == 0 ? 1 : h);
            h = Math.Acos(h);

            if (b > g)
                h = 2 * Math.PI - h;

            s = 1 - 3 * Math.Min(r, Math.Min(g, b));

            i = ((double)RGB.R + RGB.G + RGB.B) / (3 * 255);

            return Color.FromArgb((byte)(h * 180 / Math.PI), (byte)(s * 100), (byte)(i * 255));
        }
    }
}
