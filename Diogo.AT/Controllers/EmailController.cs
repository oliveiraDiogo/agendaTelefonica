using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diogo.AT.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index(int? id)
        {
            var emails = Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == id).FirstOrDefault().Emails.ToList();
            ViewBag.IdContato = id;
            ViewBag.NomeContato = Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == id).FirstOrDefault().Nome;
            return View(emails);
        }

                // GET: Email/Create
        public ActionResult Create(int idContato)
        {
            ViewBag.IdContato = idContato;
            return View();
        }

        // POST: Email/Create
        [HttpPost]
        public ActionResult Create(Domain.Contato.EmailMO email, int idContato, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Domain.Contato.EmailMO emailMO = new Domain.Contato.EmailMO {
                    Classificacao = (Domain.Contato.ClassificacaoMO.Classificacoes)Convert.ToInt32(collection[2]),
                    Email = collection[1]
                };

                Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == idContato).FirstOrDefault().Emails.Add(emailMO);

                return RedirectToAction("Index","",idContato);
            }
            catch
            {
                return View();
            }
        }

        // GET: Email/Edit/5
        public ActionResult Edit(Guid id, int idContato)
        {
            ViewBag.IdContato = idContato;
            ViewBag.NomeContato = Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == idContato).FirstOrDefault().Nome;

            Domain.Contato.EmailMO emailMO = new Domain.Contato.EmailMO();
            foreach (var contato in Domain.Contato.ContatoMO.Contatos)
            {
                foreach (var email in contato.Emails)
                {
                    if (email.Id == id)
                    {
                        emailMO = email;
                        break;
                    }
                }
            }
            return View(emailMO);
        }

        // POST: Email/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, int idContato, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == idContato)
                                                            .First()
                                                            .Emails
                                                            .Where(t => t.Id == id)
                                                            .First().Email = collection[2];

                    Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == idContato)
                                                            .First()
                                                            .Emails
                                                            .Where(t => t.Id == id)
                                                            .First().Classificacao = (Domain.Contato.ClassificacaoMO.Classificacoes)Convert.ToInt32(collection[3]);
                    
                }

                return RedirectToAction("Index", "", idContato);
            }
            catch
            {
                return View();
            }
        }

        // GET: Email/Delete/5
        public ActionResult Delete(Guid id, int idContato)
        {
            Domain.Contato.ContatoMO.Contatos.ForEach(f => f.Emails.Remove(Domain.Contato.ContatoMO.Contatos.Select(c => c.Emails.Where(t => t.Id == id)).FirstOrDefault().FirstOrDefault()));
            return RedirectToAction("Index", "", idContato);
        }

        // POST: Email/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
