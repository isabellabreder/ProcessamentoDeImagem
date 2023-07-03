using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProcessamentoImagens
{
    public partial class frmPrincipal : Form
    {
        private Image image;
        private Bitmap imageBitmap;
        private Color[,] mat;
        public frmPrincipal()
        {
            InitializeComponent();
            lbBrilho.Text = "0";
            lbHue.Text = "0";
        }

        private void btnAbrirImagem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.bmp;*.png)|*.jpg;*.gif;*.bmp;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                pictBoxImg1.Image = image;
                pictBoxImg1.SizeMode = PictureBoxSizeMode.Normal;

                mat = new Color[image.Height, image.Width];

                for (int i = 0; i < image.Height; i++)
                    for (int j = 0; j < image.Width; j++)
                        mat[i, j] = ((Bitmap)image).GetPixel(j, i);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            pictBoxImg1.Image = null;
            pictBoxImg2.Image = null;
            pb1.Image = null;
            pb2.Image = null;
            pb3.Image = null;
            
        }

        private void luminânciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_gray(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void negativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.negativo(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void luminânciaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void negativoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.negativoDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void espelhamentoDiagonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.espelhaDiagonalDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void espelhamentoHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.horizontalDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void separarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDestR = new Bitmap(image), imgDestG = new Bitmap(image), imgDestB = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            // Random r = new Random();
            Filtros.separaCanaisDMA(imageBitmap, imgDestR, imgDestG, imgDestB);
            pictBoxImg2.Image = imgDestR;
        }

        private void espelhamentoVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.verticalDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void rotacionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image.Height, image.Width);
            imageBitmap = (Bitmap)image;
            Filtros.rotacionaDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void pretoBrancoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.pretoBrancoDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void fatiamentoDeBitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.fatiamento(imageBitmap, imgDest, 7);
            pictBoxImg2.Image = imgDest;
        }

        private void equalizaçãoDoHistogramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.equalizacao(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void suavizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.suavizacao(imageBitmap, imgDest, 5);
            pictBoxImg2.Image = imgDest;
        }

        private void RGBToHSIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            Bitmap cinza = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Bitmap imgh = new Bitmap(image);
            Bitmap imgs = new Bitmap(image);
            Bitmap imgi = new Bitmap(image);

            Filtros.RGBtoHSI(imageBitmap, imgDest, cinza, imgh, imgs, imgi, 0, 0);
            pictBoxImg2.Image = imgDest;
            pb1.Image = imgh;
            pb2.Image = imgs;
            pb3.Image = imgi;
        }

        private void PictBoxImg1_MouseMove(object sender, MouseEventArgs e)
        {
            if (image != null && e.X < image.Width && e.Y < image.Height)
            {
                imageBitmap = (Bitmap)image;
                Color color = mat[e.Y, e.X];
                ColorHsi hsi = Filtros.HSI(color, 0, 0);
                Color cmy = Filtros.CMY(color);
                lbValues.Location = new Point(e.X, e.Y);
                lbValues.Text = "X: " + e.X + " Y: " + e.Y + 
                    "\nR: " + color.R + " G: " + color.G + " B: " + color.B + 
                    "\nH: " + hsi.H + " S: " + hsi.S + " I: " + hsi.I + 
                    "\nC: " + cmy.R + " M: " + cmy.G + " Y: " +  cmy.B;                
            }
            else
                lbValues.Text = "";
        }

        private void RGBToCMYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            // Bitmap imgh = new Bitmap(image);
            // Bitmap imgs = new Bitmap(image);
            // Bitmap imgi = new Bitmap(image);
            Filtros.RGBtoCMY(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
            // pb1.Image = imgh;
            // pb2.Image = imgs;
            // pb3.Image = imgi;
        }

        private void TrkbHue_ValueChanged(object sender, EventArgs e)
        {
            lbHue.Text = trkbHue.Value.ToString();
            Bitmap imgDest = new Bitmap(image);
            Bitmap cinza = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Bitmap imgh = new Bitmap(image);
            Bitmap imgs = new Bitmap(image);
            Bitmap imgi = new Bitmap(image);

            Filtros.RGBtoHSI(imageBitmap, imgDest, cinza, imgh, imgs, imgi, trkbHue.Value, trkbBrilho.Value);
            pictBoxImg2.Image = imgDest;
            pb1.Image = imgh;
            pb2.Image = imgs;
            pb3.Image = imgi;
        }

        private void TrackBar2_ValueChanged(object sender, EventArgs e)
        {
            lbBrilho.Text = trkbBrilho.Value.ToString();
            lbHue.Text = trkbHue.Value.ToString();
            Bitmap imgDest = new Bitmap(image);
            Bitmap cinza = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Bitmap imgh = new Bitmap(image);
            Bitmap imgs = new Bitmap(image);
            Bitmap imgi = new Bitmap(image);

            Filtros.RGBtoHSI(imageBitmap, imgDest, cinza, imgh, imgs, imgi, trkbHue.Value, trkbBrilho.Value);
            pictBoxImg2.Image = imgDest;
            pb1.Image = imgh;
            pb2.Image = imgs;
            pb3.Image = imgi;
        }
    }
}
