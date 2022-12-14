using System;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;
using System.IO;
namespace Trees
{
    public partial class Form1 : Form
    {
        Tree tree;
        Random rand = new Random();
        int move_x = 0, move_y = 0, size = 32;
        bool redraw;
        int[] mm = new int[1000]; 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Top = 0;
            pictureBox1.Left = 0;
            pictureBox1.Width = Width - 16;
            pictureBox1.Height = Height - pictureBox1.Location.Y - 39;
            if (redraw)
            {
                SolidBrush white = new SolidBrush(Color.White);
                Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                g.FillRectangle(white, 0, 0, pictureBox1.Width, pictureBox1.Height);
                white.Dispose();
                tree.recalc();
                tree.Draw(g, move_x + pictureBox1.Width / 2, 4 + move_y);
                redraw = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tree = new Tree(rand.Next(-10, 10), 0, size);
            tree.root = tree;
            WindowState = FormWindowState.Maximized;
        }

      public int fibc(Tree treel)
        {
            if (treel == null) { return 0; }
            if (treel.fic(treel.x)==1) { return 1; }
            return fibc(treel.left) + fibc(treel.right);
        }
        public void mc(Tree tr)
        {
            if (tr != null)
            {
                if (tr.x < 0) { tr.x = Math.Abs(tr.x); }
            }
            if(tr.left!=null) mc(tr.left);
            if (tr.right != null) mc(tr.right);

        }
        private void hl(Tree tr, int i)
        {
            if ((tr.left == null) && (tr.right == null)) { mm[i] += 1; }
            int a = i + 1, b = i + 1;
            if (tr.left != null) { hl(tr.left, a); }
            if (tr.right != null) { hl(tr.right, b); }
        }
        private int fm()
        {
            int m = 0,mi=0;
            for(int i = 0; i < 1000; i++)
            {
                if (mm[i] > m) { m = mm[i];mi = i+1; }
            }
            return mi;
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'f')
            {
                move_x = 0;
                move_y = 0;
                tree = new Tree(rand.Next(-10, 10), 0, size);
                tree.root = tree;
                for (int i = 0; i < 100; i++) {
                    tree.Add(rand.Next(-100, 100));
                                       
                };
                tree.recalc();
                move_x = -tree.dx;
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'q')
            {
                
                move_x = 0;
                move_y = 0;
                tree = new Tree(rand.Next(-10, 10), 0, size);
                tree.root = tree;

                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'v')
            {
                tree = tree.root;
                if (fibc(tree) > 0)
                {
                    tree = tree.root;
                    mc(tree);
                }
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'b')
            {
                tree = tree.root;
                hl(tree, 0);
                label2.Text= "Уровень с максимальным количеством листьев: "+Convert.ToString(fm());
                FileInfo f = new FileInfo("s.txt");
                StreamWriter fsw = f.CreateText();
                foreach(int m in mm) { fsw.Write(" {0} ", m); }
                for(int i = 0; i < 100; i++) { mm[i] = 0; }
                fsw.Close();
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'e')
            {
                tree.Add(rand.Next(-100, 100));
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'c')
            {
                for (int i = 0; i < 100; i++) tree.Add(rand.Next(-100, 100));
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'r')
            {
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'w')
            {
                move_y -= tree.node_size * 5;
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 's')
            {
                move_y += tree.node_size * 5;
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'a')
            {
                move_x -= tree.node_size * 5;
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'd')
            {
                move_x += tree.node_size * 5;
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'z')
            {
                if (size > 1) size--;
                tree.chsz(size);
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'x')
            {
                size++;
                tree.chsz(size);
                redraw = true;
                Invalidate();
            }
        }
    }
}