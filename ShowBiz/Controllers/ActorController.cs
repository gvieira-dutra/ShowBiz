using GVD2247A5.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GVD2247A5.Controllers
{
    public class ActorController : Controller
    {
        Manager m = new Manager();
        // GET: Actor
        public ActionResult Index()
        {
            return View(m.ActorGetAll());
        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            var myView = m.ActorWithShowInfoGetById(id.GetValueOrDefault());
           
            foreach (var item in myView.ActorMediaItems)
            {
                if (item.ContentType.Equals("image/jpg") || item.ContentType.Equals("image/png") || item.ContentType.Equals("image/jpeg"))
                {
                    myView.Photos.Add(item);
                }
                else if (item.ContentType.Equals("audio/mpeg"))
                {
                    myView.AudioClips.Add(item);
                }
                else if (item.ContentType.Equals("video/mp4"))
                {
                    myView.VideoClips.Add(item);
                }
                else if (item.ContentType.Equals("application/pdf"))
                {
                    myView.Documents.Add(item);
                }
            }
            myView.Photos.Sort((i1, i2) => string.Compare
                             (i1.Caption, i2.Caption, StringComparison.Ordinal));
            myView.AudioClips.Sort((i1, i2) => string.Compare
                             (i1.Caption, i2.Caption, StringComparison.Ordinal));
            myView.VideoClips.Sort((i1, i2) => string.Compare
                             (i1.Caption, i2.Caption, StringComparison.Ordinal));
            myView.Documents.Sort((i1, i2) => string.Compare
                             (i1.Caption, i2.Caption, StringComparison.Ordinal));
           
            return View(myView);
        }

        // GET: Actor/Create
        [Authorize (Roles = "Executive")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        [HttpPost]
        [Authorize(Roles = "Executive")]
        [ValidateInput(false)]
        public ActionResult Create(ActorAddViewModel newActor)
        {
            //var user = m.
            if (!ModelState.IsValid) return View(newActor);

            try
            {
                //Attempt to add a new actor to the database
                var addedItem = m.ActorAdd(newActor);

                //If not added, redirect to Create page,
                //Or to the list if it was added successfully 
                if (addedItem == null) return View(m.ActorGetAll());

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: Show/Create
        [Route("Actor/{id}/AddShow")]
        [Authorize (Roles ="Coordinator")]
        public ActionResult AddShow(int id)
        {
            // Attempt to get the associated object
            var actor = m.ActorWithShowInfoGetById(id);

            if (actor == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure a form object
                var formModel = new ShowAddFormViewModel();
                formModel.ActorId = actor.Id;

                formModel.ActorName= actor.Name;

            formModel.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

            formModel.ActorList = new SelectList(m.ActorGetAll(), "Id", "Name");

                return View(formModel);
            }
        }

        [HttpPost]
        [Authorize (Roles="Coordinator")]
        [Route("Actor/{id}/AddShow")]
        [ValidateInput(false)]
        public ActionResult AddShow(ShowAddViewModel newShow)
        {
            if (!ModelState.IsValid)
            {
                return View(newShow);
            }

            var addedItem = m.ShowAdd(newShow);

            if (addedItem == null)
            {
                return View(newShow);
            }
            else
            {
                return RedirectToAction("details", "Show", new { id = addedItem.Id });
            }
        }

        //// GET: Actor/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Actor/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Actor/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Actor/Delete/5

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

        [Authorize(Roles = "Executive")]
        [Route("Actor/{id}/AddMediaItem")]
        public ActionResult AddMediaItem(int id)
        {
            var actor = m.ActorWithShowInfoGetById(id);
            if (actor == null) return HttpNotFound();

            var form = new ActorMediaItemAddFormViewModel();
            
            form.ActorName = actor.Name;
            form.ActorId = actor.Id;

            return View(form);
        }

        [HttpPost]
        [Authorize(Roles = "Executive")]
        [Route("Actor/{id}/AddMediaItem")]
        public ActionResult AddMediaItem(ActorMediaItemAddViewModel newMedia)
        {
            if (!ModelState.IsValid)
            {
                return View(newMedia);
            }

            // Process the input
            var addedItem = m.ActorMediaItemAdd(newMedia);

            if (addedItem == null)
            {
                return View(newMedia);
            }
            else
            {
                return RedirectToAction("details", "Actor", new { id = addedItem.Id });
            }
        }
   
        [Route("Actors/MediaItem/{id}")]
        public ActionResult MediaItemDownload(int id)
        {
            var actor = m.ActorMediaItemGetById(id);

            if (actor == null) return HttpNotFound();

            return File(actor.Content, actor.ContentType);
        }

        [Route("Actors/{Id}/download")]
        public ActionResult DetailsDownload(int Id)
        {
            // Attempt to get the matching object
            var o = m.ActorMediaItemGetById(Id);

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Get file extension, assumes the web server is Microsoft IIS based
                // Must get the extension from the Registry (which is a key-value storage structure for configuration settings, for the Windows operating system and apps that opt to use the Registry)

                // Working variables
                string extension;
                RegistryKey key;
                object value;

                // Open the Registry, attempt to locate the key
                key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + o.ContentType, false);
                // Attempt to read the value of the key
                value = (key == null) ? null : key.GetValue("Extension", null);
                // Build/create the file extension string
                extension = (value == null) ? string.Empty : value.ToString();

                // Create a new Content-Disposition header
                var cd = new System.Net.Mime.ContentDisposition
                {
                    // Assemble the file name + extension
                    FileName = $"{o.Caption}{extension}",
                    // Force the media item to be saved (not viewed)
                    Inline = false
                };
                // Add the header to the response
                Response.AppendHeader("Content-Disposition", cd.ToString());

                return File(o.Content, o.ContentType);
            }
        }
    }
}
