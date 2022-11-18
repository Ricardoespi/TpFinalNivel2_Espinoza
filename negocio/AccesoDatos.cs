using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector 
        { 
            get{ return lector; }
        }
        public AccesoDatos() 
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true;");
            comando = new SqlCommand();
        }
        public void setQuery(string consulta)
        {
            try
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
            }
            catch (Exception ex)
            { throw ex; }
        }
        public void ejecutarLectura()
        {
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            { throw ex; }
        }
        public void ejecutarAccion()
        {
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            { throw ex; }
        }
        public void cerrarConexion()
        {
            try
            {
                if (lector != null)
                    lector.Close();
                conexion.Close();
            }
            catch (Exception ex)
            { throw ex; }
        }
        public void setParametro(string nombre, object valor)
        {
            try
            {
                comando.Parameters.AddWithValue(nombre, valor);
            }
            catch (Exception ex)
            { throw ex;
            }
        }

    }
}
