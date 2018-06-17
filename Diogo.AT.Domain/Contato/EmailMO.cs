using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diogo.AT.Domain.Contato
{
    public class EmailMO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage ="Insira um email válido")]
        public string Email { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma classificação")]
        public ClassificacaoMO.Classificacoes Classificacao { get; set; }
    }
}