using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Diogo.AT.Controllers
{
    public class ContatoController : Controller
    {
        
        // GET: Contato
        public ActionResult Index(string busca)
        {
            Domain.Contato.ContatoMO contato = new Domain.Contato.ContatoMO();
            List<Domain.Contato.ContatoMO> contatos;
            if (!string.IsNullOrEmpty(busca))
                contatos = contato.GetContatos(busca);
            else
                contatos = contato.GetContatos();
            if (contatos.Count > 0)
                return View(contatos);
            else
                return View("SemContato");
        }
        
        // GET: Contato/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //GET: Contato/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contato/Create
        [HttpPost]
        public ActionResult Create(Domain.Contato.TelefoneMO telefone, Domain.Contato.ContatoMO contato)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Domain.Contato.ContatoMO c = new Domain.Contato.ContatoMO();
                    Domain.Contato.ContatoMO.IdCount++;
                    contato.Id = Domain.Contato.ContatoMO.IdCount;
                    contato.Telefones.Add(telefone);
                    Domain.Contato.ContatoMO.Contatos.Add(contato);
                    //CreateEmail(Domain.Contato.ContatoMO.Contatos.Last().Id);
                    return RedirectToAction("Create", "Email", new { idContato = contato.Id });
                    //return RedirectToAction("Index");
                }

            }
            catch
            {
                ModelState.AddModelError("", "Não foi salvar");
            }

            return View();
        }               

        // GET: Contato/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.TelefonesMO = Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == id).FirstOrDefault().Telefones;
            return View(Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == id).FirstOrDefault());
        }

        // POST: Contato/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Domain.Contato.ContatoMO contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == id).FirstOrDefault().Nome = contato.Nome;
                    Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == id).FirstOrDefault().Empresa = contato.Empresa;
                    Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == id).FirstOrDefault().Endereco = contato.Endereco;
                }

                return RedirectToAction("Index","Telefone", new { id = id});
            }
            catch
            {
                return View();
            }
        }

        // GET: Contato/Delete/5
        public ActionResult Delete(int id)
        {
            Domain.Contato.ContatoMO.Contatos.Remove(
                Domain.Contato.ContatoMO.Contatos.Where(c => c.Id == id).First());

            return RedirectToAction("Index");
        }

        // POST: Contato/Delete/5
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
