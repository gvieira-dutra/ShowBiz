using GVD2247A5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GVD2247A5.Controllers
{
    public class ShowController : Controller
    {
        Manager m = new Manager();
        // GET: Show
        public ActionResult Index()
        {
            return View(m.ShowGetAll());
        }

        // GET: Show/Details/5
        public ActionResult Details(int id)
        {
            return View(m.ShowGetOne(id));
        }

        // GET: Show/Create
        [Authorize (Roles ="Clerk")]
        [Route("Shows/{id}/AddEpisode")]
        public ActionResult EpisodeAddNew(int id)
        {
            // Attempt to get the associated object
            var show = m.ShowGetOne(id);

            if (show == null)
            {
                return HttpNotFound();
            }
            else
            {
               
                // Create and configure a form object
                var formModel = new EpisodeAddFormViewModel();

                formModel.ShowName = show.Name;

                formModel.ShowId = show.Id;

                formModel.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

                return View(formModel);
            }
        }

        // POST: Show/Create
        [HttpPost]
        [Authorize (Roles ="Clerk")]
        [Route("Shows/{id}/AddEpisode")]
        [ValidateInput(false)]
        public ActionResult EpisodeAddNew(EpisodeAddViewModel newEpisode)
        {
            if (!ModelState.IsValid)
            {
                return View(newEpisode);
            }

            //Process the input
           var addedItem = m.EpisodeAdd(newEpisode);

            if (addedItem == null)
            {
                return View(newEpisode);
            }
            else
            {
                // TODO: 20 - Must redirect to the Vehicles controller
                return RedirectToAction("details", "Episode", new { id = addedItem.Id });
            }
        }

        // GET: Show/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Show/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Show/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Show/Delete/5
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
