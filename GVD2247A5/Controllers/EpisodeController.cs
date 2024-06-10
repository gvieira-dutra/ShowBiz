using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GVD2247A5.Controllers
{
    public class EpisodeController : Controller
    {
        Manager m = new Manager();
        // GET: Episode
        public ActionResult Index()
        {
            return View(m.EpisodesGetAll());
        }

        // GET: Episode/Details/5
        public ActionResult Details(int id)
        {
            return View(m.EpisodeGetOne(id));
        }

        [Route("Episodes/Video/{id}")]
        public ActionResult GetEpisodeVideo(int? id)
        {
            var myVid = m.EpisodeVideoGetById(id.GetValueOrDefault());

            if(myVid == null)
            {
                return HttpNotFound();
            } 
            else
            {
                return File(myVid.Video, myVid.VideoContentType);
            }
        }


        // GET: Episode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Episode/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Episode/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Episode/Edit/5
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

        // GET: Episode/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Episode/Delete/5
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
