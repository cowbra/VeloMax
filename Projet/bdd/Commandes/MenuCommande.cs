﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace bdd
{
    public partial class MenuCommande : Form
    {
        BDD DATABASE = new BDD();
        public MenuCommande()
        {
            InitializeComponent();
            DATABASE.Connect();
            //Actualiser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewCommande commande = new NewCommande();
            commande.ShowDialog();
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
