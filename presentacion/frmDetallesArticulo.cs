using dominio;
using negocio;
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
    public partial class frmDetallesArticulo : Form
    {
        private Articulo articulo = null;
        public frmDetallesArticulo()
        {
            InitializeComponent();
        }
        public frmDetallesArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmDetallesArticulo_Load(object sender, EventArgs e)
        {
            try
            {
                if (articulo == null) return;

                lblIdValor.Text = articulo.Id.ToString();
                if (articulo.Codigo != null)
                    lblCodigoValor.Text = articulo.Codigo.ToString();
                if (articulo.Nombre != null)
                    lblNombreValor.Text = articulo.Nombre;
                if (articulo.Descripcion != null)
                    lblDescripcionValor.Text = articulo.Descripcion;
                if (articulo.Marca.Descripcion != null)
                    lblMarcaValor.Text = articulo.Marca.Descripcion;
                if (articulo.Categoria.Descripcion != null)
                    lblCategoriaValor.Text = articulo.Categoria.Descripcion;
                if (articulo.ImagenUrl != null)
                    lblImagenUrlValor.Text = articulo.ImagenUrl;
                if (articulo.Precio != null)
                    lblPrecioValor.Text = articulo.Precio.ToString();
                Helper.cargarImg(articulo.ImagenUrl, pbxDetalles);
            }
            catch (Exception ex)
            { throw ex; }
        }

        private void lblImagenUrlValor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitarLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imposible de abrir el link.");
            }
        } 
        private void VisitarLink()
        {
            lblImagenUrlValor.LinkVisited = true;
            System.Diagnostics.Process.Start(lblImagenUrlValor.Text);
        }
    }
}
