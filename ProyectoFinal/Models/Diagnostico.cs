using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProyectoFinal.Models
{
    public class Diagnostico
    {
        public int Id { get; set; }
        public string NombrePaciente { get; set; }
        public string Resultado { get; set; }
        public string Fecha { get; set; }
        public string Imagen { get; set; } 
    }
}