using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank
{
    public partial class Form1 : Form
    {
        int speed;
        int score = 0;
        public Form1()
        {
            InitializeComponent();

            

        }

        void Fire()
        {
            PictureBox bullet = new PictureBox();
            bullet.Image = Properties.Resources.Bullet3;
            bullet.Size = new Size(5, 20);
            bullet.SizeMode = PictureBoxSizeMode.Zoom;
            bullet.Tag = "Bullet";
            bullet.Location = new Point(Tank90.Left + Tank90.Width / 2, Tank90.Top - 20);
            Controls.Add(bullet);

            if (Levelspeed() == 0) { Controls.Remove(bullet); }
            



        }
        private int Levelspeed()
        {
            int s;
            if (label1.BackColor == System.Drawing.Color.Green )
            {
                s =  5;
         
            }
            else if (label2.BackColor == System.Drawing.Color.Green)
            {
                s =  15;
            }
            else if (label3.BackColor == System.Drawing.Color.Green)
            {

                s = 30;
            }
            else
            {
                s = 0;
            }
           
            return s;

        }



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
                
                if (e.KeyCode == Keys.Left) speed = -Levelspeed(); //LevelSpeed()
                else if (e.KeyCode == Keys.Right) speed = Levelspeed();
            


        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            Tank90.Left += speed;
            if (Tank90.Left <= 0) Tank90.Left = 0;
            else if (Tank90.Right >= Width) { Tank90.Left = Width - Tank90.Width; }






            bool flag = false;
            foreach (PictureBox p in Controls.OfType<PictureBox>())
                if (p.Location.X < 145)
                {
                    flag = true;
                }


            if (!flag)
            {
                PictureBox Al = new PictureBox();
                Al.Image = Properties.Resources.Alien2;
                Al.Size = new Size(115, 106);
                Al.SizeMode = PictureBoxSizeMode.Zoom;
                Al.Tag = "Alien";
                Al.Location = new Point(24, 42);
                Controls.Add(Al);


            }




                foreach (PictureBox  pb in Controls.OfType<PictureBox>())
                {
                if (pb.Tag == "Alien")
                {
                    pb.Left += Levelspeed();
                    
                    if (pb.Right >= Width)
                    {
                        pb.Left = -pb.Width / 2;

                        pb.Left += Levelspeed();
                        pb.Top += 150;
                    }
                    if (pb.Bounds.IntersectsWith(Tank90.Bounds))
                    {
                        timer1.Stop();
                        DialogResult result = MessageBox.Show("Your Score: " + score.ToString() + "   Restart?", "Game over! " , MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {

                            System.Windows.Forms.Application.Restart();
                        }
                        else if (result == DialogResult.No )
                        {

                            System.Environment.Exit(1);

                        }

                    }
                        
                   
                }


                if (pb.Tag == "Bullet")
                {

                    pb.Top -= 20; 
                    

                    if (pb.Top < 0)
                    {
                        Controls.Remove(pb);
                    }
                }


                foreach (PictureBox x in Controls.OfType<PictureBox>())
                    foreach (PictureBox y in Controls.OfType<PictureBox>())
                        if (x.Tag == "Alien" && y.Tag == "Bullet" && x.Bounds.IntersectsWith(y.Bounds))
                        {
                            Controls.Remove(x);
                            Controls.Remove(y);

                            score += 1;
                            Scorelbl.Text = "Score: " + score.ToString();




                        }





                if (score == 100)
                {
                    timer1.Stop();
                    MessageBox.Show("ПОБЕДА!");
                    System.Environment.Exit(1);
                }
                        

            }










        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad1)
            {
                label1.BackColor = System.Drawing.Color.Green;
                label2.BackColor = System.Drawing.Color.White;
                label3.BackColor = System.Drawing.Color.White;

            }

            if (e.KeyCode == Keys.NumPad2)
            {
                label1.BackColor = System.Drawing.Color.White;
                label2.BackColor = System.Drawing.Color.Green;
                label3.BackColor = System.Drawing.Color.White;

            }
            if (e.KeyCode == Keys.NumPad3)
            {
                label1.BackColor = System.Drawing.Color.White;
                label2.BackColor = System.Drawing.Color.White;
                label3.BackColor = System.Drawing.Color.Green;

            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) speed = 0;
            else if (e.KeyCode == Keys.Space) Fire();
            else if (e.KeyCode == Keys.Escape ) System.Threading.Thread.Sleep(5000); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
