using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinal.Data.Models
{
    internal class Carros
    {
        public int ID { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public string Color { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public bool Disponible { get; set; }
        public DateTime FechaIngreso { get; set; }

        // Constructor sin parámetros
        public Carros() { }

        // Constructor con parámetros
        public Carros(int id, string marca, string modelo, int año, string color, decimal precio, string descripcion, bool disponible, DateTime fechaIngreso)
        {
            ID = id;
            Marca = marca;
            Modelo = modelo;
            Año = año;
            Color = color;
            Precio = precio;
            Descripcion = descripcion;
            Disponible = disponible;
            FechaIngreso = fechaIngreso;
        }

        

    }
}
