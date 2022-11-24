using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace negocio
{
    public class Helper
    {
        public static void cargarImg(string imagen, PictureBox pbx)
        {
            try
            {
                pbx.Load(imagen);
            }
            catch (Exception ex)
            {
                pbx.Load("https://images.squarespace-cdn.com/content/v1/5a79de08aeb625f12ad4f85a/1527015265032-KYY1AQ4NCW6NB7BK1NDH/placeholder-image-vertical.png");
            }
        }
        public static void guardarImg(OpenFileDialog arch)
        {
            try
            {
                File.Copy(arch.FileName, ConfigurationManager.AppSettings["img-folder"] + arch.SafeFileName, true);
                //cambia la direccion a tu carpeta en app.config si te da error al guardar imagenes!
                //el true es para permitir sobreescritura de imagenes.
            }
            catch (Exception ex)
            { throw ex; }
        }
        public static bool validarFiltro(ComboBox cboCampo, ComboBox cboCriterio, TextBox txtFiltro)
        {
            try
            {
                if (cboCampo.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccione un campo.");
                    return true;
                }
                if (cboCriterio.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccione un criterio.");
                    return true;
                }
                if (cboCampo.SelectedItem.ToString() == "Precio")
                {
                    if (string.IsNullOrEmpty(txtFiltro.Text))
                    {
                        MessageBox.Show("No se puede filtro vacío para campo numérico");
                        return true;
                    }
                    if (!(EsNumero(txtFiltro.Text)))
                    {
                        MessageBox.Show("Solo números para campo numérico");
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            { throw ex; }

        }
        //Si quisiera agregar otro filtro a la busqueda normal, este método seria super útil para validar también.
        public static bool EsNumero(string cadena)
        {
            try
            {
                foreach (char letra in cadena)
                {
                    if (!(char.IsNumber(letra)))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            { throw ex; }
            
        }
        public static bool validaArt(DataGridView dgv)
        {
            try
            {
                if (!(dgv.CurrentRow != null))
                {
                    MessageBox.Show("Seleccione un Articulo", "Sin Articulo Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            { throw ex; }
            
        }
    }

}
