using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iam.Payroll.Common;
using Iam.Payroll.Helpers;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Iam.Payroll.Services
{
    public class ImagesService
    {
        readonly IImagesRepository repo;

        public ImagesService(IImagesRepository repo)
        {
            this.repo = repo;
        }

        public List<ImageEx> GetPaged(string where = "", string orderby = "", int PageNo = 1, int PageSize = 10)
        {
            return repo.GetPaged(where, orderby, PageNo, PageSize);
        }

        public int Count(string where)
        {
            return repo.Count(where);
        }

        public ImageEx Get(int Id)
        {
            return repo.Get(Id);
        }

        public ImageEx Create()
        {
            return repo.Create();
        }

        public ImageEx Add(ImageEx item)
        {
            return repo.Add(item);
        }

        public ImageEx Update(ImageEx item)
        {
            return repo.Update(item);
        }

        public void Remove(ImageEx item)
        {
            repo.Remove(item);
        }



        public List<ImageEx> GetAll(string where = "")
        {
            return repo.GetAll(where);
        }

        public List<System.Drawing.Image> Images(int Id, int Width, int Height)
        {
            string sSource = ConfigurationManager.AppSettings["ImageSourceFolder"] + "\\" + Id + ".jpg";

            List<System.Drawing.Image> oReturn = new List<System.Drawing.Image>();

            if (File.Exists(sSource))
            {
                System.Drawing.Image oImage = System.Drawing.Image.FromFile(sSource);
                oReturn.Add(Iam.Payroll.Helpers.Images.ResizeImage(oImage, new System.Drawing.Size(Width, Height)));
                oImage.Dispose();
            }
            return oReturn;
        }

        public void Remove(int id)
        {
            repo.Remove(id);
        }

        public void Remove(string where)
        {
            repo.Remove(where);
        }
    }
}
