using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProyectoLenguajes1137818
{
    public partial class DFA : Form
    {
        public DFA()
        {
            InitializeComponent();
        }

        private void RG_Click(object sender, EventArgs e)
        {
            this.Close();
            Proyecto proyecto = new Proyecto();
            proyecto.Show();
        }
    }
}
