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
        private Articulo articulo = null;
        public frmNuevoArticulo()
        {
            InitializeComponent();
        }
        public frmNuevoArticulo(Articulo art)
        {
            InitializeComponent();
            articulo = art;
            Text = "Modificar Articulo";
        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            Helper.cargarImg(txtImagenUrl.Text, pbxNuevoArticulo);
        }

        private void frmNuevoArticulo_Load(object sender, EventArgs e)
        {
            MarcaConexion marcaDatos= new MarcaConexion();
            cboMarca.DataSource = marcaDatos.listar();
            CategoriaConexion catDatos = new CategoriaConexion();
            cboCategoria.DataSource = catDatos.listar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloConexion artConexion = new ArticuloConexion();
                if (articulo == null)
                    articulo = new Articulo();
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre= txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.ImagenUrl = txtImagenUrl.Text;
                articulo.Precio = nudPrecio.Value;
                artConexion.agregar(articulo);
                MessageBox.Show("Se ha agregado exitosamente");
                Close();
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
