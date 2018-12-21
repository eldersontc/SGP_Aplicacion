using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGP_Aplicacion.Controllers
{
    public class BaseController: ControllerBase
    {
        public ISessionFactory factory;

        public BaseController(ISessionFactory factory)
        {
            this.factory = factory;
        }
    }
}
