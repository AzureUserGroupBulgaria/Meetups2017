using PhotoOfTheDay.WebApp.Entities;
using PhotoOfTheDay.WebApp.Models;
using PhotoOfTheDay.WebApp.Services;
using System.Web.Mvc;

namespace PhotoOfTheDay.WebApp.Controllers
{
    public class PhotosController : Controller
    {
        public ActionResult Index()
        {
            var photoService = new PhotoEntryService(this.Server);
            var photoEntries = photoService.GetPhotoEntryViews();

            return View(photoEntries);
        }

        [HttpGet]
        public ActionResult Submit()
        {
            return View();
        }

        [ActionName("Submit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitPost(SubmitPhotoEntryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var entryService = new PhotoEntryService(this.Server);
            var newEntry = PhotoEntry.New(model.PhotoTitle, model.PhotoDescription);
            entryService.SaveNewEntry(newEntry);

            if (model.Image != null)
            {
                var imageService = new ImageProcessingService(this.Server);
                imageService.ProcessImage(model.Image.InputStream, newEntry);
            }

            return this.RedirectToAction("Index", "Photos");
        }
        
    }
}