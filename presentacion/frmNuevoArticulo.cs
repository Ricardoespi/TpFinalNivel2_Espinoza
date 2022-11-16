using negocio;
using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    public partial class frmNuevoArticulo : Form
    {
        public frmNuevoArticulo()
        {
            InitializeComponent();
        }
        public frmNuevoArticulo(Articulo art)
        {
            InitializeComponent();
            Text = "Modificar Articulo";
        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            Helper.cargarImg(txtImagenUrl.Text, pbxNuevoArticulo);
        }
    }
}
