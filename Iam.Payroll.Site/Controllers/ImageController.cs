using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using Iam.Payroll.Common;
using Iam.Payroll.Data;
using Iam.Payroll.Services;
using Iam.Payroll.Data.Models;
using System.IO;

namespace Iam.Payroll.Site.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        //
        // GET: /Item/
        readonly ImagesService svcImage;
        public ImageController(ImagesService svcImage)
        {
            this.svcImage = svcImage;
        }

        public JsonResult List(string OwnerId, string OwnerType = "Item")
        {
            return Json(svcImage.GetAll("OwnerId == \"" + OwnerId + "\" AND OwnerType == \"" + OwnerType + "\""), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string OwnerId, string OwnerType, int FileId)
        {
            ImageEx oImage = svcImage.Get(FileId);
            if (oImage.OwnerId == OwnerId && oImage.OwnerType == OwnerType)
            {
                svcImage.Remove(oImage);
                //If Directory Exists
                if (System.IO.Directory.Exists(WebConfigurationManager.AppSettings["ImageFolder"] + "\\" + OwnerType + "\\" + OwnerId))
                {
                    //delete directory to refresh all pictures.
                    System.IO.Directory.Delete(WebConfigurationManager.AppSettings["ImageFolder"] + "\\" + OwnerType + "\\" + OwnerId);
                }
                return Json(new { success = true, message = "File successfully deleted." });
            }
            else
            {
                return Json(new { success = false, message = "File not deleted." });
            }
        }

        [HttpPost]
        public JsonResult Upload(string OwnerId, string OwnerType, HttpPostedFileBase file)
        {
            ImageEx oImage = new ImageEx();
            oImage.Name = "Untitled";
            oImage.OwnerId = OwnerId;
            oImage.OwnerType = OwnerType;
            oImage.FileName = "";
            oImage.Description = "";
            ImageEx oReturn = svcImage.Add(oImage);

            string sPath = WebConfigurationManager.AppSettings["ImageSourceFolder"] + "\\" + oReturn.Id + ".jpg";
            file.SaveAs(sPath);

            return Json(oReturn);
        }

        /// <summary>
        /// Get each image from Source Location. 
        /// </summary>
        [AcceptVerbs(HttpVerbs.Get)]
        public FileResult Images(string OwnerId, int Width, int Height, int ImageId, string type = "Item")
        {
            // Checking if File doesn't exists Create directory in Temp folder.
            string sFile = WebConfigurationManager.AppSettings["ImageFolder"] + "\\" + type + "\\" + OwnerId + "\\" + Width + "x" + Height + "\\" + ImageId + ".jpg";
            if (!System.IO.File.Exists(sFile))
            {
                if (!System.IO.Directory.Exists(WebConfigurationManager.AppSettings["ImageFolder"] + "\\" + type + "\\" + OwnerId + "\\" + Width + "x" + Height))
                {
                    System.IO.Directory.CreateDirectory(WebConfigurationManager.AppSettings["ImageFolder"] + "\\" + type + "\\" + OwnerId + "\\" + Width + "x" + Height);
                }
                switch (type.ToLower())
                {
                    case "customer":
                    case "vendor":
                    case "item":
                        List<System.Drawing.Image> aImage = svcImage.Images(ImageId, Width, Height);
                        int iIndex = 1;
                        foreach (System.Drawing.Image oImage in aImage)
                        {
                            string sThisFile = WebConfigurationManager.AppSettings["ImageFolder"] + "\\" + type + "\\" + OwnerId + "\\" + Width + "x" + Height + "\\" + ImageId + ".jpg";
                            oImage.Save(sThisFile);
                            oImage.Dispose();
                            iIndex++;
                        }
                        break;
                }
            }
            // If exists then return file.
            if (System.IO.File.Exists(sFile))
            {
                return new FileStreamResult(new System.IO.FileStream(sFile, System.IO.FileMode.Open), "image/jpeg");
            }
            else
            {
                return new FileStreamResult(new System.IO.FileStream(Server.MapPath("/content/assets/images/image.png"), System.IO.FileMode.Open), "image/jpeg");
            }
        }
        /// <summary>
        /// To Show Images on Items Page
        /// </summary>
        [AcceptVerbs(HttpVerbs.Get)]
        public FileResult DefaultImage(string OwnerId, int Width, int Height, string type = "Item")
        {

            string sFile = WebConfigurationManager.AppSettings["ImageFolder"] + "\\" + type + "\\" + OwnerId + "\\" + Width + "x" + Height + "\\default.jpg";
            if (!System.IO.File.Exists(sFile))
            {
                if (!System.IO.Directory.Exists(WebConfigurationManager.AppSettings["ImageFolder"] + "\\" + type + "\\" + OwnerId + "\\" + Width + "x" + Height))
                {
                    System.IO.Directory.CreateDirectory(WebConfigurationManager.AppSettings["ImageFolder"] + "\\" + type + "\\" + OwnerId + "\\" + Width + "x" + Height);
                }
                switch (type.ToLower())
                {
                    case "customer":
                    case "vendor":
                    case "item":
                        ImageEx oImageEx = svcImage.GetAll("OwnerType = \"" + type + "\" and OwnerId = \"" + OwnerId + "\"").OrderByDescending(m => m.IsDefault).FirstOrDefault();
                        if (oImageEx != null)
                        {
                            List<System.Drawing.Image> aImage = svcImage.Images(oImageEx.Id, Width, Height);
                            int iIndex = 1;
                            foreach (System.Drawing.Image oImage in aImage)
                            {
                                string sThisFile = WebConfigurationManager.AppSettings["ImageFolder"] + "\\" + type + "\\" + OwnerId + "\\" + Width + "x" + Height + "\\default.jpg";
                                oImage.Save(sThisFile);
                                oImage.Dispose();
                                iIndex++;
                            }
                        }
                        break;
                }
            }
            if (System.IO.File.Exists(sFile))
            {
                return new FileStreamResult(new System.IO.FileStream(sFile, System.IO.FileMode.Open), "image/jpeg");
            }
            else
            {
                return new FileStreamResult(new System.IO.FileStream(Server.MapPath("/content/assets/images/image.png"), System.IO.FileMode.Open, FileAccess.Read, FileShare.Read), "image/jpeg");
            }
        }
    }
}
