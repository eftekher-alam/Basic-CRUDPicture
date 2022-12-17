using BasicCRUDPicture.Models;
using BasicCRUDPicture.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BasicCRUDPicture.Controllers
{
    public class PictureController : Controller
    {
        private readonly PictureDbContext _db;
        private readonly IWebHostEnvironment _environment;
        public PictureController(PictureDbContext db, IWebHostEnvironment environment) 
        {
            _db = db;
            _environment = environment;
        }
        public IActionResult Index()
        {
            IEnumerable<Picture> pictures = _db.Pictures.ToList();
            return View(pictures);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(PictureViewModel picture)
        {
            if(ModelState.IsValid)
            {
                Picture mainPicture = new Picture()
                {
                    PicName = picture.PicName
                };
                if(picture.Photo != null)
                {

                    var rootPath = _environment.WebRootPath;
                    var projFolderPath = "Pictures/" + Guid.NewGuid() + picture.Photo.FileName;
                    mainPicture.Photo = projFolderPath;
                    var fullPath = Path.Combine(rootPath, projFolderPath);
                    FileStream stream = new FileStream(fullPath, FileMode.Create);
                    picture.Photo.CopyTo(stream);
                    stream.Close();
                }
                _db.Pictures.Update(mainPicture);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(picture);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var picture = _db.Pictures.Find(id);
            ViewBag.CurrentPicture = picture.Photo;
            PictureViewModel pictureViewModel = new PictureViewModel() 
            {
                Id = picture.Id,
                PicName = picture.PicName
            };
            return View(pictureViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PictureViewModel newPicture)
        {
            var existPicture = _db.Pictures.Find(newPicture.Id);
            ViewBag.CurrentPicture = existPicture.Photo;
            if (ModelState.IsValid)
            {
                existPicture.Id = newPicture.Id;
                existPicture.PicName = newPicture.PicName;
                if (newPicture.Photo != null)
                {
                    var rootPath = _environment.WebRootPath;
                    var projFolderPath = "Pictures/" + Guid.NewGuid() + newPicture.Photo.FileName;
                    existPicture.Photo = projFolderPath;
                    var fullPath = Path.Combine(rootPath, projFolderPath);
                    FileStream stream = new FileStream(fullPath, FileMode.Create);
                    newPicture.Photo.CopyTo(stream);
                }
                _db.Pictures.Update(existPicture);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newPicture);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var existPicture = _db.Pictures.Find(id);
            return View(existPicture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var existPicture = _db.Pictures.Find(id);
            _db.Remove(existPicture);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
