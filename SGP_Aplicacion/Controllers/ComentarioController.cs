﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using SGP_Aplicacion.Models;

namespace SGP_Aplicacion.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : BaseController
    {
        public ComentarioController(ISessionFactory factory,
            IHostingEnvironment hstv) : base(factory)
        {
            _hstv = hstv;
        }

        private readonly IHostingEnvironment _hstv;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Comentario comentario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var sn = factory.OpenSession())
            {
                using (var tx = sn.BeginTransaction())
                {
                    try
                    {
                        comentario.FechaHora = DateTime.Now;

                        sn.Save(comentario);

                        await tx.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await tx.RollbackAsync();
                        return StatusCode(500, ex.Message);
                    }
                }
            }

            return Ok(true);
        }
    }
}