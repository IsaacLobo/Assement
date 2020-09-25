using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Pais
    {
        // foto da sua bandeira, um nome e sua lista de estados
        public int PaisId { get; set; }
        public int Bandeira { get; set; }
        public string NomePais { get; set; }
        public List<Estado> Estado { get; set; }
    }
}
