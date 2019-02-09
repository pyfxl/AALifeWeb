﻿using AALife.Core;
using AALife.Core.Domain.Media;
using AALife.Core.Services.Media;
using AALife.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class PictureController : BaseController
    {
        private readonly IPictureService _pictureService;
        private readonly IWorkContext _workContext;

        public PictureController(IPictureService pictureService,
            IWorkContext workContext)
        {
            this._pictureService = pictureService;
            this._workContext = workContext;
        }

        // GET: Picture
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult AsyncUpload()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.UploadPictures))
            //    return Json(new { success = false, error = "You do not have required permissions" }, "text/plain");

            //we process it distinct ways based on a browser
            //find more info here http://stackoverflow.com/questions/4884920/mvc3-valums-ajax-file-upload
            Stream stream = null;
            var fileName = "";
            var contentType = "";
            if (String.IsNullOrEmpty(Request["qqfile"]))
            {
                // IE
                HttpPostedFileBase httpPostedFile = Request.Files[0];
                if (httpPostedFile == null)
                    throw new ArgumentException("No file uploaded");
                stream = httpPostedFile.InputStream;
                fileName = Path.GetFileName(httpPostedFile.FileName);
                contentType = httpPostedFile.ContentType;
            }
            else
            {
                //Webkit, Mozilla
                stream = Request.InputStream;
                fileName = Request["qqfile"];
            }

            var fileBinary = new byte[stream.Length];
            stream.Read(fileBinary, 0, fileBinary.Length);

            var fileExtension = Path.GetExtension(fileName);
            if (!String.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            //contentType is not always available 
            //that's why we manually update it here
            //http://www.sfsu.edu/training/mimetype.htm
            if (String.IsNullOrEmpty(contentType))
            {
                switch (fileExtension)
                {
                    case ".bmp":
                        contentType = MimeTypes.ImageBmp;
                        break;
                    case ".gif":
                        contentType = MimeTypes.ImageGif;
                        break;
                    case ".jpeg":
                    case ".jpg":
                    case ".jpe":
                    case ".jfif":
                    case ".pjpeg":
                    case ".pjp":
                        contentType = MimeTypes.ImageJpeg;
                        break;
                    case ".png":
                        contentType = MimeTypes.ImagePng;
                        break;
                    case ".tiff":
                    case ".tif":
                        contentType = MimeTypes.ImageTiff;
                        break;
                    default:
                        break;
                }
            }

            var picture = _pictureService.InsertPicture(fileBinary, contentType, fileName, fileExtension, fileBinary.Length, PictureType.bg, true, _workContext.CurrentUser.Id);
            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return Json(new
            {
                success = true,
                pictureId = picture.Id,
                thumbUrl = _pictureService.GetPictureUrl(picture, 100, true, null, PictureType.bg),
                imageUrl = _pictureService.GetPictureUrl(picture.Id, null, PictureType.bg)
            },
                MimeTypes.TextPlain);
        }
    }
}