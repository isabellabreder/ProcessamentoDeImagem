using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProcessamentoImagens
{
    public partial class frmPrincipal : Form
    {
        private Image image;
        private Bitmap imageBitmap;

        public frmPrincipal()
        {
            InitializeComponent();
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
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            pictBoxImg1.Image = null;
            pictBoxImg2.Image = null;
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
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Random r = new Random();
            Filtros.separaCanaisDMA(imageBitmap, imgDest, r.Next(150) % 3);
            pictBoxImg2.Image = imgDest;
        }

        private void espelhamentoVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.verticallDMA(imageBitmap, imgDest);
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
            Filtros.fatiamento(imageBitmap, imgDest, openFileDialog.FileName, 7);
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
            imageBitmap = (Bitmap)image;
            Bitmap imgh = new Bitmap(image);
            Bitmap imgs = new Bitmap(image);
            Bitmap imgi = new Bitmap(image);
            Filtros.RBGtoHSI(imageBitmap, imgDest, imgh, imgs, imgi);
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
                Color color = imageBitmap.GetPixel(e.X, e.Y);
                Color hsi = Filtros.HSI(color);
                lbValues.Location = new Point(e.X, e.Y);
                lbValues.Text = "R: " + color.R + " G: " + color.G + " B: " + color.B + 
                    "\nH: " + hsi.R + " S: " + hsi.G + " I: " + hsi.B;                
            }
            else
                lbValues.Text = "";
            

        }
    }
}
