using System;

namespace Model
{
    public class Amigo
    {
        //nome, sobrenome, e-mail, telefone, data de aniversário
        public int AmigoId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime Aniversario { get; set; }
    }
}
