namespace ProcessamentoImagens
{
    class ColorHsi
    {
        private int h;
        private int s;
        private int i;
        public ColorHsi(int h, int s, int i)
        {
            this.h = h;
            this.s = s;
            this.i = i;
        }
        public int H { get => h; set => h = value; }
        public int S { get => s; set => s = value; }
        public int I { get => i; set => i = value; }
    }
}
