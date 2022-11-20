using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace negocio
{
    public class ArticuloConexion
    {
        public List<Articulo> listar()
        {
            List<Articulo> articulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("select a.Id, A.Codigo, a.Nombre, a.Descripcion, a.IdMarca, m.Descripcion Marca, a.IdCategoria, c.Descripcion Categoria, a.ImagenUrl, a.Precio from ARTICULOS a, MARCAS m, CATEGORIAS c where a.IdMarca = m.Id and a.IdCategoria = c.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    if (!(datos.Lector["Codigo"] is DBNull))
                        aux.Codigo = (string)datos.Lector["Codigo"];
                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    if (!(datos.Lector["Marca"] is DBNull))
                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal?)datos.Lector["Precio"];
                    articulos.Add(aux);
                }

                return articulos;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                datos.cerrarConexion();
            }


        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@cod, @nombre, @desc, @marca, @cat, @img, @precio)");
                datos.setParametro("@cod", nuevo.Codigo);
                datos.setParametro("@nombre", nuevo.Nombre);
                datos.setParametro("@desc", nuevo.Descripcion);
                datos.setParametro("@marca", nuevo.Marca.Id);
                datos.setParametro("@cat", nuevo.Categoria.Id);
                datos.setParametro("@img", nuevo.ImagenUrl);
                datos.setParametro("@precio", nuevo.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificar(Articulo mod)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("update ARTICULOS set Codigo = @cod, Nombre = @nombre, Descripcion = @desc, IdMarca=@idMarca, IdCategoria=@idCat, ImagenUrl=@img, Precio=@precio where id = @id");
                datos.setParametro("@id", mod.Id);
                datos.setParametro("@cod", mod.Codigo);
                datos.setParametro("@nombre", mod.Nombre);
                datos.setParametro("@desc", mod.Descripcion);
                datos.setParametro("@idMarca", mod.Marca.Id);
                datos.setParametro("@idCat", mod.Categoria.Id);
                datos.setParametro("@img", mod.ImagenUrl);
                datos.setParametro("@precio", mod.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("delete from articulos where id = @id");
                datos.setParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
