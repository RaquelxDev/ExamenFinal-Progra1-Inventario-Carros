using ExamenFinal.Data.Models;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ExamenFinal
{
    public partial class Form1 : Form
    {
        //REFERENCIA A ConexionMySql
        ConexionMySql Clscone = new ConexionMySql();

        //INSTANCIA DE CLASE CARRO Y REFERENCIA A CARRO
        Carros carro = new Carros();

        List<Carros> todos;
        ClsCursor cursor1 = new ClsCursor();

        public Form1()
        {
            InitializeComponent();
        }
                             //FUNCIONES

        //MOSTRAR TODOS LOS AUTOS DE LA BASE DE DATOS Y ACTUALIZAR LA TABLA 
        private void MostrarTodosLosCarros()
        {
            todos = Clscone.ObtenerTodosLosCarros();
            dataGridViewFechaIngreso.DataSource = todos;
            cursor1.totalRegistros = todos.Count;

            if (cursor1.totalRegistros > 0)
            {
                cursor1.current = 0; //Se reinicia el cursor al primer registro
                MostrarRegistro();
            }
            else
            {
                MessageBox.Show("No hay registros para mostrar.");
            }
        }

        //MOSTRAR DETALLES DE REGISTRO ACTUAL
        private void MostrarRegistro()
        {
            if (cursor1.current >= 0 && cursor1.current < cursor1.totalRegistros)
            {
                //c es referencia a Carro
                Carros c = todos[cursor1.current];
                textBoxID.Text = c.ID.ToString();
                textBoxMarca.Text = c.Marca;
                textBoxModelo.Text = c.Modelo;
                textBoxAño.Text = c.Año.ToString();
                textBoxColor.Text = c.Color;
                textBoxPrecio.Text = c.Precio.ToString();
                textBoxDescripcion.Text = c.Descripcion;
                checkBoxDisponible.Checked = c.Disponible;

                // Se ajusta el rango del DateTimePicker
                dateTimePickerFechaIngreso.MinDate = DateTimePicker.MinDateTime;
                dateTimePickerFechaIngreso.MaxDate = DateTimePicker.MaximumDateTime;

                if (c.FechaIngreso > DateTimePicker.MinDateTime && c.FechaIngreso < DateTimePicker.MaximumDateTime)
                {
                    dateTimePickerFechaIngreso.Value = c.FechaIngreso;
                }
                else
                {
                    dateTimePickerFechaIngreso.Value = DateTime.Now;
                }
            }
        }
        
        //GUARDAR CARRO
        private void GrabarRegistro()
        {
            try
            {
                // Se Verifica si el campo ID(codigo) está presente y si ya existia
                if (!string.IsNullOrEmpty(textBoxID.Text) && Clscone.CarroExisteID(int.Parse(textBoxID.Text)))
                {
                    MessageBox.Show("El código ya existe. Por favor, ingrese uno diferente para grabar.");
                }
                else
                {
                    Carros nuevoCarro = new Carros
                    {
                        ID = int.Parse(textBoxID.Text),
                        Marca = textBoxMarca.Text,
                        Modelo = textBoxModelo.Text,
                        Año = int.Parse(textBoxAño.Text),
                        Color = textBoxColor.Text,
                        Descripcion = textBoxDescripcion.Text,
                        Precio = decimal.Parse(textBoxPrecio.Text),
                        Disponible = checkBoxDisponible.Checked,
                        FechaIngreso = dateTimePickerFechaIngreso.Value,
                    };

                    //SE AGREGA EL NUEVO AUTO A LA BASE DE DATOS
                    Clscone.InsertarCarro(nuevoCarro);
                    MessageBox.Show("Registro grabado correctamente");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error en el formato de entrada: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al grabar el registro: " + ex.Message);
            }
        }

        //ACTUALIZAR CARRO
        private void ActualizarCarro()
        {
            try
            {
                // Se revisa que los campos no esten vacios
                if (string.IsNullOrEmpty(textBoxID.Text) ||
                    string.IsNullOrEmpty(textBoxMarca.Text) ||
                    string.IsNullOrEmpty(textBoxModelo.Text) ||
                    string.IsNullOrEmpty(textBoxAño.Text) ||
                    string.IsNullOrEmpty(textBoxColor.Text) ||
                    string.IsNullOrEmpty(textBoxPrecio.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos requeridos.");
                    return;
                }

                // Crea un nuevo objeto carro con los valores del formulario
                Carros carroActualizado = new Carros
                {
                    ID = int.Parse(textBoxID.Text),
                    Marca = textBoxMarca.Text,
                    Modelo = textBoxModelo.Text,
                    Año = int.Parse(textBoxAño.Text),
                    Color = textBoxColor.Text,
                    Descripcion = textBoxDescripcion.Text,
                    Precio = decimal.Parse(textBoxPrecio.Text),
                    Disponible = checkBoxDisponible.Checked,
                    FechaIngreso = dateTimePickerFechaIngreso.Value,

                };

                // se llamar al método para actualizar el carro en la base de datos
                Clscone.ActualizarCarro(carroActualizado);
                

                // se actualiza la lista de carros mostrados
                MostrarTodosLosCarros();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error para modificar: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al actualizar el coche: " + ex.Message);
            }
        }

        //LIMPIAR REGISTROS
        private void LimpiarCamposRegistro()
        {
            textBoxID.Clear();
            textBoxMarca.Clear();
            textBoxModelo.Clear();
            textBoxAño.Clear();
            textBoxColor.Clear();
            textBoxPrecio.Clear();
            textBoxDescripcion.Clear();
            checkBoxDisponible.Checked = false;
            dateTimePickerFechaIngreso.Value = DateTime.Now; //fecha predeterminada
        }

        //ELIMINAR REGISTROS
        private void EliminarRegistro()
        {
            //Se revisa que el campo ID no este vacio
            if (!string.IsNullOrEmpty(textBoxID.Text))
            {
                int idEliminar = int.Parse(textBoxID.Text); //Se obtiene el ID(codigo) del campo de texto
                Clscone.EliminarCarro(idEliminar);
                MessageBox.Show("Registro eliminado correctamente");
                MostrarTodosLosCarros(); //Luego de eliminar el registro se actualiza la tabla
            }
            else
            {
                MessageBox.Show("Por favor, ingrese el ID para eliminar el registro.");
            }
        }

        // BUSCAR REGISTRO POR ID(código)
        private void BuscarRegistroPorID()
        {
            if (!string.IsNullOrEmpty(textBoxID.Text))
            {
                if (int.TryParse(textBoxID.Text, out int idABuscar))
                {
                    if (Clscone.CarroExisteID(idABuscar))
                    {
                        Carros carroEncontrado = Clscone.BuscarCarroPorID(idABuscar);
                        if (carroEncontrado != null)
                        {
                            //Se actualiza el cursor a la posición del coche encontrado en la lista
                            cursor1.current = todos.FindIndex(c => c.ID == carroEncontrado.ID);
                            if (cursor1.current == -1)
                            {
                                // Si el coche no está en la lista actual, lo añadimos
                                todos.Add(carroEncontrado);
                                cursor1.current = todos.Count - 1;
                            }
                            cursor1.totalRegistros = todos.Count;
                            MostrarRegistro();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El carro con ID " + idABuscar + " no existe.");
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un ID válido.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un código para buscar.");
            }
        }

        //SALIR DEL PROGRAMA
        private void ConfirmarSalida()
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea salir?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                //Si el usuario elige que sí se cierra el programa
                Application.Exit();
            }
            // Sino, no se hace nada
        }

                                        //BOTONES

            //BOTON MOSTRAR TODOS LOS REGISTROS
            private void buttonTodos_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarTodosLosCarros();
                // Se muestra el total de registros solo si hay registros para mostrar
                if (todos.Count > 0)
                {
                    MessageBox.Show("Total de registros: " + cursor1.totalRegistros);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al intentar mostrar todos los coches: " + ex.Message);
            }
        }

        //BOTON MODIFICAR REGISTRO
        private void buttonModificar_Click(object sender, EventArgs e)
        {
            ActualizarCarro();

        }

        //BOTON GRABAR REGISTRO
        private void buttonGrabarRegistro_Click(object sender, EventArgs e)
        {
            GrabarRegistro();
            MostrarTodosLosCarros();
        }
        //BOTON SIGUIENTE
        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            // Validar que no se pase del total de registros
            if (cursor1.current < cursor1.totalRegistros - 1)
            {
                cursor1.current++;
                MostrarRegistro();
            }
            else
            {
                MessageBox.Show("Fin de los registros");
            }
        }
        
        //BOTON ANTERIOR
        private void buttonAnterior_Click(object sender, EventArgs e)
        {
            if (cursor1.current > 0)
            {
                cursor1.current--;
                MostrarRegistro();
            }
            else
            {
                MessageBox.Show("Este es el primer registro.");
            }
        }

        //BOTON BUSCAR POR ID(CODIGO)
        private void buttonBuscarID_Click(object sender, EventArgs e)
        {
            BuscarRegistroPorID();
        }

        //BOTON PARA LIMPIAR REGISTRO
        private void buttonLimpiarRegistro_Click(object sender, EventArgs e)
        {
            LimpiarCamposRegistro();
        }

        //BOTON PARA ELIMINAR UN REGISTRO
        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        //BOTON PARA SALIR DEL PROGRAMA
        private void buttonSalir_Click(object sender, EventArgs e)
        {
            ConfirmarSalida();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }


    }

}
