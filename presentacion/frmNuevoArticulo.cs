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
using System.IO;
using System.Configuration;

namespace presentacion
{
    public partial class frmNuevoArticulo : Form
    {
        private Articulo articulo = null;
        private OpenFileDialog archivo = null;
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
            try
            {
                MarcaConexion marcaDatos = new MarcaConexion();
                cboMarca.DataSource = marcaDatos.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                CategoriaConexion catDatos = new CategoriaConexion();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboCategoria.DataSource = catDatos.listar();

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    txtImagenUrl.Text = articulo.ImagenUrl;
                    Helper.cargarImg(txtImagenUrl.Text, pbxNuevoArticulo);

                    nudPrecio.Value = Convert.ToDecimal(articulo.Precio);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            
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
                if(articulo.Id != 0)
                {
                    artConexion.modificar(articulo);
                    MessageBox.Show("Se ha modificado exitosamente");
                }
                else
                {
                    artConexion.agregar(articulo);
                    MessageBox.Show("Se ha agregado exitosamente");
                }
                if (archivo != null && !(txtImagenUrl.Text.ToLower().Contains("http")))
                {
                    Helper.guardarImg(archivo);
                    //cambia la direccion a tu carpeta si te da error al guardar imagenes!
                }

                Close();
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                archivo = new OpenFileDialog();
                archivo.Filter = "jpg|*.jpg|png|*.png";
                if (archivo.ShowDialog() == DialogResult.OK)
                {
                    txtImagenUrl.Text = archivo.FileName;
                    Helper.cargarImg(archivo.FileName, pbxNuevoArticulo);
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
