﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bdd
{
    public partial class MenuVelo : Form
    {
        BDD DATABASE = new BDD();
        public MenuVelo()
        {
            InitializeComponent();
            DATABASE.Connect();
        }

        private void MenuVelo_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewVelo velo = new NewVelo();
            velo.ShowDialog();
            //Actualiser();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            DATABASE.Disconnect();
            this.Close();
            menu.Show();
        }
    }
}
