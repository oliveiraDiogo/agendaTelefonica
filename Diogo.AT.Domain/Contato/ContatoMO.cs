using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Diogo.AT.Domain.Contato
{
    public class ContatoMO
    {
        
        public static List<ContatoMO> Contatos = new List<ContatoMO>();
        
        public static int IdCount = 1;
        public static bool Excluir { get; set; }

        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [Required]
        public List<TelefoneMO> Telefones { get; set; } = new List<TelefoneMO>();

        public List<EmailMO> Emails { get; set; } = new List<EmailMO>();
        
        [DataType(DataType.Text)]
        public string Empresa { get; set; }

        [DataType(DataType.Text)]
        public string Endereco { get; set; }

        public List<ContatoMO> GetContatos(string busca)
        {
            try
            {
                if (Contatos.Count == 0 && !Excluir)
                {
                    Excluir = true;
                    for (int i = 1; i < 2; i++)
                    {
                        IdCount++;

                        List<TelefoneMO> t = new List<TelefoneMO>();
                        t.Add(new TelefoneMO
                        {
                            DDD = "11",
                            Numero = "985274149",
                            Classificacao = ClassificacaoMO.Classificacoes.Trabalho
                        });
                        t.Add(new TelefoneMO
                        {
                            DDD = "11",
                            Numero = "23054772",
                            Classificacao = ClassificacaoMO.Classificacoes.Casa
                        });

                        List<EmailMO> e = new List<EmailMO>();
                        e.Add(new EmailMO {
                            Email = "oliveira.dop@gmail.com",
                            Classificacao = ClassificacaoMO.Classificacoes.Trabalho
                        });
                        e.Add(new EmailMO {
                            Email = "oliveira_dop@hotmail.com",
                            Classificacao = ClassificacaoMO.Classificacoes.Outro
                        });

                        Contatos.Add(
                            new ContatoMO
                            {
                                Id = IdCount,
                                Nome = "Diogo Oliveira",
                                Telefones = t,
                                Emails = e,
                                Empresa = string.Format("{0}-{1}", "Empresa", IdCount),
                                Endereco = string.Format("{0}-{1}", "Rua Butantã, ", IdCount)
                            });
                    }                    
                }
                else if (!string.IsNullOrEmpty(busca))
                {
                    List<ContatoMO> contatoBusca = new List<ContatoMO>();

                    if (busca.Contains("@"))
                    {
                        foreach (var item in Contatos)
                        {
                            foreach (var email in item.Emails)
                            {
                                if (email.Email.Trim().ToUpper() == busca.Trim().ToUpper())
                                {
                                    contatoBusca.Add(item);
                                    return contatoBusca;
                                }   
                            }
                        }
                    }                        
                    else
                    {
                        if (Contatos.Where(x => x.Nome.ToUpper().Trim() == busca.ToUpper().Trim()).ToList().Count > 0)
                        {
                            contatoBusca.Add(Contatos.Where(x => x.Nome.ToUpper().Trim() == busca.ToUpper().Trim()).FirstOrDefault());
                            return contatoBusca;
                        }
                        else
                        {
                            foreach (var item in Contatos)
                            {
                                foreach (var telefone in item.Telefones)
                                {
                                    if (telefone.Numero.Trim() == busca.Trim())
                                    {
                                        contatoBusca.Add(item);
                                        return contatoBusca;
                                    }
                                }
                            }
                        }                       
                    }   
                }
                    return Contatos.OrderBy(x => x.Nome).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ContatoMO> GetContatos() => GetContatos(string.Empty);
                
    }
}