﻿using System.ComponentModel.DataAnnotations;

namespace P01WebApi.Models
{
    public class tipo_equipo
    {
        [Key]

        public int id_tipo_equipo { get; set; }
        public string? descripcion { get; set; }
        public bool? estado { get; set; }
    }
}
