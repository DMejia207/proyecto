using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace proyecto.Models
{
    public class Sitios
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(100)]
        public string nombre { get; set; }

        [MaxLength(100)]
        public string pais { get; set; }

        [MaxLength(100)]
        public string latitud { get; set; }

        [MaxLength(100)]
        public string longitud { get; set; }

        [MaxLength(100)]
        public string nota { get; set; }

        public string foto { get; set;}
    }
}
