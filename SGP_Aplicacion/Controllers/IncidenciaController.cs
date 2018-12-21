using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Transform;
using SGP_Aplicacion.Models;

namespace SGP_Aplicacion.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class IncidenciaController : BaseController
    {
        public IncidenciaController(ISessionFactory factory,
            IHostingEnvironment hstv) : base(factory) {
            _hstv = hstv;
        }

        private readonly IHostingEnvironment _hstv;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IList<Incidencia> lista;

            try
            {
                using (var sn = factory.OpenSession())
                {
                    lista = await sn.CreateSQLQuery("Usp_GetIncidencias")
                        .SetResultTransformer(Transformers.AliasToBean<Incidencia>())
                        .ListAsync<Incidencia>();
                    return Ok(lista);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Foto/{id}")]
        public async Task<IActionResult> GetFoto([FromRoute] int id)
        {
            try
            {
                var memory = new MemoryStream();

                using (var stream = new FileStream(this._hstv.ContentRootPath + "\\fotos\\" + id + ".jpeg", FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                byte[] bytes = memory.ToArray();

                string base64 = Convert.ToBase64String(bytes);

                return Ok(base64);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Incidencia incidencia)
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
                        incidencia.FechaHora = DateTime.Now;

                        sn.Save(incidencia);

                        if (!string.IsNullOrEmpty(incidencia.Base64))
                        {
                            byte[] bytes = Convert.FromBase64String(incidencia.Base64);

                            using (var imageFile = new FileStream(this._hstv.ContentRootPath + "\\fotos\\" + incidencia.Id + ".jpeg", FileMode.Create))
                            {
                                imageFile.Write(bytes, 0, bytes.Length);
                                imageFile.Flush();
                            }
                        }

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