using dominio;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("select a.Id, A.Codigo, a.Nombre, a.Descripcion, a.IdMarca, m.Descripcion Marca, a.IdCategoria, c.Descripcion Categoria, a.ImagenUrl, a.Precio from ARTICULOS a, MARCAS m, CATEGORIAS c where a.IdMarca = m.Id and a.IdCategoria = c.Id");
                datos.ejecutarLectura();
                return transformar(datos.Lector);
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
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select a.Id, A.Codigo, a.Nombre, a.Descripcion, a.IdMarca, m.Descripcion Marca, a.IdCategoria, c.Descripcion Categoria, a.ImagenUrl, a.Precio from ARTICULOS a, MARCAS m, CATEGORIAS c where a.IdMarca = m.Id and a.IdCategoria = c.Id";
                if (campo != "Marca" && campo != "Categoria")
                    consulta += " and a." + campo;
                else if (campo == "Categoria")
                    consulta += " and c.Descripcion";
                else if (campo == "Marca")
                    consulta += " and m.Descripcion";
                if (campo != "Precio")
                {
                    consulta += " like '";
                    switch (criterio)
                    {
                        case "Contiene":
                            consulta += "%" + filtro + "%'";
                            break;
                        case "Empieza por":
                            consulta += filtro + "%'";
                            break;
                        case "Termina por":
                            consulta += "%" + filtro + "'";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consulta += " = ";
                            break;
                        case "Mayor a":
                            consulta += " > ";
                            break;
                        case "Menor a":
                            consulta += " < ";
                            break;
                        default:
                            break;
                    }
                    consulta += filtro;
                }
                datos.setQuery(consulta);
                datos.ejecutarLectura();
                return transformar(datos.Lector);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private void configurarQuery()
        {

        }
        private List<Articulo> transformar(SqlDataReader lector)
        {
            List<Articulo> lista = new List<Articulo>();
            try
            {
                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)lector["Id"];
                    if (!(lector["Codigo"] is DBNull))
                        aux.Codigo = (string)lector["Codigo"];
                    if (!(lector["Nombre"] is DBNull))
                        aux.Nombre = (string)lector["Nombre"];
                    if (!(lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)lector["Descripcion"];
                    aux.Marca = new Marca();
                    if (!(lector["IdMarca"] is DBNull))
                        aux.Marca.Id = (int)lector["IdMarca"];
                    if (!(lector["Marca"] is DBNull))
                        aux.Marca.Descripcion = (string)lector["Marca"];
                    aux.Categoria = new Categoria();
                    if (!(lector["IdCategoria"] is DBNull))
                        aux.Categoria.Id = (int)lector["IdCategoria"];
                    if (!(lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)lector["Categoria"];
                    if (!(lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)lector["ImagenUrl"];
                    if (!(lector["Precio"] is DBNull))
                        aux.Precio = (decimal?)lector["Precio"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            { throw ex; }
            
            
        }// probando a ver que tal funciona para ahorrar codigo
    }
}
