using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;
using SGP_Aplicacion.Models;

namespace SGP_Aplicacion.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        public UsuarioController(ISessionFactory factory,
            IHostingEnvironment hstv) : base(factory)
        {
            _hstv = hstv;
        }

        private readonly IHostingEnvironment _hstv;

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario _usuario = null;

            using (var sn = factory.OpenSession())
            {
                _usuario = await sn.Query<Usuario>()
                    .Where(x => x.Alias.Equals(usuario.Alias) 
                    && x.Password.Equals(usuario.Password)
                    && !x.Eliminado)
                    .FirstOrDefaultAsync();
            }

            if (_usuario == null)
            {
                return NotFound();
            }

            return Ok(_usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IList<Usuario> lista;

            try
            {
                using (var sn = factory.OpenSession())
                {
                    lista = await sn.Query<Usuario>().Where(x => !x.Eliminado).ToListAsync();
                    return Ok(lista);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
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
                        usuario.Eliminado = false;
                        sn.Save(usuario);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
            {
                return BadRequest();
            }

            using (var sn = factory.OpenSession())
            {
                using (var tx = sn.BeginTransaction())
                {
                    try
                    {
                        usuario.Eliminado = false;
                        sn.SaveOrUpdate(usuario);

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
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
                        var usuario = sn.Get<Usuario>(id);
                        usuario.Eliminado = true;

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