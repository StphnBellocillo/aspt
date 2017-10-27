using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Iam.Payroll.Data;
using Iam.Payroll.Common;
using Iam.Payroll.Services;
using Iam.Payroll.Data.Models;
using Ninject.Web.Common;

namespace Iam.Payroll.Site.Models
{
    public class SiteNinjectModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            //db binding 
            Bind<IamDatabase>().ToSelf().InRequestScope();

            //repository binding
            Bind<IAttendanceRepository>().To<AttendanceRepository>();
            Bind<IDepartmentRepository>().To<DepartmentRepository>();
            Bind<IHolidayRepository>().To<HolidayRepository>();
            Bind<IImagesRepository>().To<ImagesRepository>();
            Bind<IPersonRepository>().To<PersonRepository>();
            Bind<ITypeRepository>().To<TypeRepository>();

            //service binding 
            Bind<AttendanceService>().ToSelf();
            Bind<DepartmentService>().ToSelf();
            Bind<HolidayService>().ToSelf();
            Bind<ImagesService>().ToSelf();
            Bind<PersonService>().ToSelf();
            Bind<TypeService>().ToSelf();
        }
    }
}