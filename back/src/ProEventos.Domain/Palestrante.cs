using ProEventos.Domain.Identity;
using System.Collections.Generic;

namespace ProEventos.Domain
{
    public class Palestrante
    {
        public int Id { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemUrl { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
    }
}