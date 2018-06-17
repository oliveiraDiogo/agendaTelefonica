using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diogo.AT.Domain.Contato
{
    public class TelefoneMO
    {        
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Numero { get; set; }
        [Required]
        [MaxLength(2,ErrorMessage = "DDD deve possuir apenas 2 dígitos")]
        public string DDD { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma classificação")]
        public ClassificacaoMO.Classificacoes Classificacao { get; set; }

        
    }
}