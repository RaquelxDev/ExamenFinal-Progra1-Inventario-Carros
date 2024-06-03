using ExamenFinal.Data.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenFinal
{
    //CONEXIÓN A LA BASE DE DATOS
    internal class ConexionMySql
    {

        string connectionString = "server=localhost; database=examenfinal; user=root; password=123456";
        private MySqlConnection connection;

        //CONSTRUCTOR
        public ConexionMySql()
        {
            connection = new MySqlConnection(connectionString);
        }

            //INSERTAR NUEVO AUTO
            public void InsertarCarro(Carros carro)
        {
            try
            {
                string query = "INSERT INTO carros (ID, Marca, Modelo, Año, Color, Precio, Descripcion, Disponible, FechaIngreso) VALUES (@ID, @Marca, @Modelo, @Año, @Color, @Precio, @Descripcion, @Disponible, @FechaIngreso)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", carro.ID);
                cmd.Parameters.AddWithValue("@Marca", carro.Marca);
                cmd.Parameters.AddWithValue("@Modelo", carro.Modelo);
                cmd.Parameters.AddWithValue("@Año", carro    .Año);
                cmd.Parameters.AddWithValue("@Color", carro.Color);
                cmd.Parameters.AddWithValue("@Precio", carro.Precio);
                cmd.Parameters.AddWithValue("@Descripcion", carro.Descripcion);
                cmd.Parameters.AddWithValue("@Disponible", carro.Disponible);
                cmd.Parameters.AddWithValue("@FechaIngreso", carro.FechaIngreso);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el auto: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //ACTUALIZAR CARRO
        public void ActualizarCarro(Carros carro)
        {
            try
            {
                string query = "UPDATE carros SET Marca = @Marca, Modelo = @Modelo, Año = @Año, Color = @Color, Precio = @Precio, Descripcion = @Descripcion, Disponible = @Disponible, FechaIngreso = @FechaIngreso WHERE ID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", carro.ID);
                cmd.Parameters.AddWithValue("@Marca", carro.Marca);
                cmd.Parameters.AddWithValue("@Modelo", carro.Modelo);
                cmd.Parameters.AddWithValue("@Año", carro.Año);
                cmd.Parameters.AddWithValue("@Color", carro.Color);
                cmd.Parameters.AddWithValue("@Precio", carro.Precio);
                cmd.Parameters.AddWithValue("@Descripcion", carro.Descripcion);
                cmd.Parameters.AddWithValue("@Disponible", carro.Disponible);
                cmd.Parameters.AddWithValue("@FechaIngreso", carro.FechaIngreso);


                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message);
            }

                finally
                {
                    connection.Close();
                }

        }

        //ELIMINAR CARRO
        public void EliminarCarro(int id)
        {
            try
            {
                string query = "DELETE FROM carros WHERE ID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //BUSCAR CARRO POR CÓDIGO
        public Carros BuscarCarroPorID(int id)
        {
            Carros carroEncontrado = null;
            try
            {
                string query = "SELECT * FROM carros WHERE ID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", id);
                connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        carroEncontrado = new Carros
                        {
                            ID = reader.GetInt32("ID"),
                            Marca = reader.IsDBNull(reader.GetOrdinal("Marca")) ? string.Empty : reader.GetString("Marca"),
                            Modelo = reader.IsDBNull(reader.GetOrdinal("Modelo")) ? string.Empty : reader.GetString("Modelo"),
                            Año = reader.IsDBNull(reader.GetOrdinal("Año")) ? 0 : reader.GetInt32("Año"),
                            Color = reader.IsDBNull(reader.GetOrdinal("Color")) ? string.Empty : reader.GetString("Color"),
                            Precio = reader.IsDBNull(reader.GetOrdinal("Precio")) ? 0 : reader.GetDecimal("Precio"),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? string.Empty : reader.GetString("Descripcion"),
                            Disponible = reader.IsDBNull(reader.GetOrdinal("Disponible")) ? false : reader.GetBoolean("Disponible"),
                            FechaIngreso = reader.IsDBNull(reader.GetOrdinal("FechaIngreso")) ? DateTime.Now : reader.GetDateTime("FechaIngreso")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return carroEncontrado;
        }


        //VERIFICAR QUE EXISTE EL CÓDIGO DEL CARRO
        public bool CarroExisteID(int id)
        {
            bool existe = false;
            try
            {
                string query = "SELECT COUNT(1) FROM carros WHERE ID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", id);
                connection.Open();
                // Si el conteo es mayor que 0, el id existe
                existe = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar el Código: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return existe;
        }

        //LISTA PARA OBTENER LOS REGISTROS DE LA TABLA CARROS
        public List<Carros> ObtenerTodosLosCarros()
        {
            List<Carros> carros = new List<Carros>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM carros";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Carros carro = new Carros
                        {
                            ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt32("ID"),
                            Marca = reader.IsDBNull(reader.GetOrdinal("Marca")) ? string.Empty : reader.GetString("Marca"),
                            Modelo = reader.IsDBNull(reader.GetOrdinal("Modelo")) ? string.Empty : reader.GetString("Modelo"),
                            Año = reader.IsDBNull(reader.GetOrdinal("Año")) ? 0 : reader.GetInt32("Año"),
                            Color = reader.IsDBNull(reader.GetOrdinal("Color")) ? string.Empty : reader.GetString("Color"),
                            Precio = reader.IsDBNull(reader.GetOrdinal("Precio")) ? 0 : reader.GetDecimal("Precio"),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? string.Empty : reader.GetString("Descripcion"),
                            Disponible = reader.IsDBNull(reader.GetOrdinal("Disponible")) ? false : reader.GetBoolean("Disponible"),
                            FechaIngreso = reader.IsDBNull(reader.GetOrdinal("FechaIngreso")) ? DateTime.MinValue : reader.GetDateTime("FechaIngreso")
                        };

                        carros.Add(carro);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener los coches: " + ex.Message);
                }
            }
            return carros;
        }











    }














}


