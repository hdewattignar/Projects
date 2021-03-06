﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstroidsClone
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Manager manager;
           

        public Form1()
        {
            InitializeComponent();

            Setup();
        }

        public void Setup()
        {
            BackColor = Color.Black;
            graphics = this.CreateGraphics();
            PointF canvasSize = new PointF(this.Width, this.Height);
            manager = new Manager(graphics, canvasSize);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            manager.Running = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);
            if(manager.Run() == false)
            {
                Setup();     
                timer1.Enabled = false;
            }
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                manager.RotateShip(e);
            }
            if(e.KeyCode == Keys.W)
            {
                manager.Throttle(1);
            }
            if (e.KeyCode == Keys.E)
            {
                Setup();
                timer1.Enabled = true;
            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                manager.stopRotation();
            }
            if (e.KeyCode == Keys.W)
            {
                manager.Throttle(0);
            }
        }
    }
}
