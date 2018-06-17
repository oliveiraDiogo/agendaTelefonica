using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diogo.AT.Controllers
{
    public class TelefoneController : Controller
    {
        // GET: Telefone
        public ActionResult Index(int? id)
        {
            var telefones = Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == id).FirstOrDefault().Telefones.ToList();
            ViewBag.IdContato = id;
            ViewBag.NomeContato = Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == id).FirstOrDefault().Nome;
            return View(telefones);
        }

        // GET: Telefone/Create
        public ActionResult Create(int idContato)
        {
            ViewBag.IdContato = idContato;
            return View();
        }

        // POST: Telefone/Create
        [HttpPost]
        public ActionResult Create(int idContato, Domain.Contato.TelefoneMO telefone)
        {
            try
            {
                Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == idContato).FirstOrDefault().Telefones.Add(telefone);
                return RedirectToAction("Index","",idContato);
            }
            catch
            {
                return View();
            }
        }

        // GET: Telefone/Edit/5
        public ActionResult Edit(Guid id, int idContato)
        {
            ViewBag.IdContato = idContato;
            ViewBag.NomeContato = Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == idContato).FirstOrDefault().Nome;

            Domain.Contato.TelefoneMO telefoneMO = new Domain.Contato.TelefoneMO();
            foreach (var contato in Domain.Contato.ContatoMO.Contatos)
            {
                foreach (var telefone in contato.Telefones)
                {
                    if (telefone.Id == id)
                    {
                        telefoneMO = telefone;
                        break;
                    }
                }
            }
            return View(telefoneMO);
        }

        // POST: Telefone/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, int idContato, Domain.Contato.TelefoneMO telefone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == idContato)
                                                            .First()
                                                            .Telefones
                                                            .Where(t => t.Id == id)
                                                            .First().DDD = telefone.DDD;

                    Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == idContato)
                                                            .First()
                                                            .Telefones
                                                            .Where(t => t.Id == id)
                                                            .First().Numero = telefone.Numero;

                    Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == idContato)
                                                            .First()
                                                            .Telefones
                                                            .Where(t => t.Id == id)
                                                            .First().Classificacao = telefone.Classificacao;
                }

                return RedirectToAction("Index","",idContato);
            }
            catch
            {
                return View();
            }
        }

        // GET: Telefone/Delete/5
        public ActionResult Delete(Guid id, int idContato)
        {
            try
            {
                Domain.Contato.ContatoMO.Contatos.ForEach(f => f.Telefones.Remove(Domain.Contato.ContatoMO.Contatos.Select(c => c.Telefones.Where(t => t.Id == id)).FirstOrDefault().FirstOrDefault()));
                return RedirectToAction("Index", "", idContato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Telefone/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
