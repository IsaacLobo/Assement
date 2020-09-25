using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Estado
    {
        //foto da sua bandeira e um nome
        public int EstadoId { get; set; }
        public int Bandeira { get; set; }
        public string NomeEstado { get; set; }
        public Pais Pais { get; set; }
    }
}
