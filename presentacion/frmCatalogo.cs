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
using dominio;
using System.Security.Cryptography;

namespace presentacion
{
    public partial class frmCatalogo : Form
    {
        private List<Articulo> listaArticulos = null;
        public frmCatalogo()
        {
            InitializeComponent();
        }

        private void frmCatalogo_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            try
            {
                ArticuloConexion datos = new ArticuloConexion();
                listaArticulos = datos.listar();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas();
                Helper.cargarImg(listaArticulos[0].ImagenUrl, pbxArticulo);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }
        private void ocultarColumnas()
        {
            try
            {
                hideColumn("Id");
                hideColumn("Categoria");
                hideColumn("Marca");
                hideColumn("Descripcion");
                hideColumn("ImagenUrl");
            }
            catch (Exception ex)
            { throw ex; }
        }
        private void hideColumn(string columna)
        {
            try{ dgvArticulos.Columns[columna].Visible = false; }
            catch (Exception ex) { throw ex; }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Articulo articulo = new Articulo();
                articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                Helper.cargarImg(articulo.ImagenUrl, pbxArticulo);
            }
            catch (Exception ex)
            { throw ex; }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                frmNuevoArticulo nuevo = new frmNuevoArticulo();
                nuevo.ShowDialog();
                cargar();
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
