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
            ArticuloConexion datos = new ArticuloConexion();
            cargar();
            foreach (var propiedad in typeof(Articulo).GetProperties())//por cada prop en las props de mi clase articulo
            {
                if (propiedad.Name != "ImagenUrl" && propiedad.Name != "Id")
                    cboCampo.Items.Add(propiedad.Name);//estoy usando esto para probar codigos dinamicos!
            }
            
            //cboCampo.Items.Add("Código");
            //cboCampo.Items.Add("Nombre");
            //cboCampo.Items.Add("Descripción");
            //cboCampo.Items.Add("Marca");
            //cboCampo.Items.Add("Categoría");
            //cboCampo.Items.Add("Precio");
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
                dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "N2";
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo aMod = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmNuevoArticulo mod = new frmNuevoArticulo(aMod);
                mod.ShowDialog();
                cargar();
            }
            catch (Exception  ex)
            { throw ex; }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloConexion datos = new ArticuloConexion();
                if (MessageBox.Show("No podrá recuperarlo, ¿está seguro de que lo quiere eliminar?", "Eliminar Artículo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Articulo art = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    datos.eliminar(art.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            { throw ex; }
            
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo aDeta = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmDetallesArticulo deta = new frmDetallesArticulo(aDeta);
                deta.ShowDialog();
            }
            catch (Exception ex)
            { throw ex; }
            
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCampo.SelectedItem.ToString() == "Número" || cboCampo.SelectedItem.ToString() == "Precio")
            {

            }
            else
            {

            }
        }
    }
}
