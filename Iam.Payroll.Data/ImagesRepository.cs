using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using Iam.Payroll.Common;
using Iam.Payroll.Data.Models;
using Iam.Payroll.Data;

namespace Iam.Payroll.Data
{
    public class ImagesRepository : IImagesRepository
    {
        readonly IamDatabase db;
        readonly RepositoryEF<Image> repo;

        public ImagesRepository(IamDatabase db)
        {
            this.db = db;
            this.repo = new RepositoryEF<Image>(db);
        }


        public ImageEx Get(int Id)
        {
            return Transform(repo.FindById(Id));
        }

        public List<ImageEx> GetAll(string where = "")
        {
            return Transform(repo.FindAll(where));
        }

        public List<ImageEx> GetPaged(string where = "", string orderBy = "Name", int PageNo = 1, int PageSize = 10)
        {
            return Transform(repo.FindAll(where).OrderBy(orderBy).Skip((PageNo - 1) * PageSize).Take(PageSize));
        }

        public ImageEx Create()
        {
            return new ImageEx();
        }

        public ImageEx Add(ImageEx entity)
        {
            Image Image = Transform(entity);
            var oReturn = repo.Add(Image);
            db.SaveChanges();
            return Transform(oReturn);
        }

        public ImageEx Update(ImageEx entity)
        {
            Image Image = Transform(entity);
            var r = repo.Update(Image);
            db.SaveChanges();
            return Transform(r);
        }

        public void Remove(ImageEx entity)
        {
            var g = Transform(entity);
            repo.Remove(g);
            db.SaveChanges();
        }

        public void Remove(int Id)
        {
            repo.Remove(Id);
            db.SaveChanges();
        }

        public void Remove(string Id)
        {
            repo.Remove(int.Parse(Id));
            db.SaveChanges();
        }

        public int Count(string where)
        {
            return repo.FindAll(where).Count();
        }

        public ImageEx Transform(Image n)
        {
            ImageEx r = new ImageEx();
            r.Id = n.Id;
            r.Name = n.Name;
            r.Description = n.Description;
            r.FileName = n.FileName;
            r.OwnerId = n.OwnerId;
            r.OwnerType = n.OwnerType;
            r.IsDefault = n.IsDefault;
            return r;
        }
        public Image Transform(ImageEx n)
        {
            Image r;
            if (n.Id > 0)
            {
                r = repo.FindById(n.Id);
            }
            else
            {
                r = repo.Create();
            }
            r.Name = n.Name;
            r.Description = n.Description;
            r.FileName = n.FileName;
            r.OwnerId = n.OwnerId;
            r.OwnerType = n.OwnerType;
            r.IsDefault = n.IsDefault;
            return r;
        }
        public List<ImageEx> Transform(IQueryable<Image> aInput)
        {
            List<ImageEx> r = new List<ImageEx>();
            foreach (Image n in aInput)
            {
                r.Add(Transform(n));
            }
            return r;
        }

    }
}
