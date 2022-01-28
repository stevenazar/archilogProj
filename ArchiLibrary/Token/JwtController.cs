using ArchiLibrary.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchiLibrary.Token
{
    [ApiController]
    [Route("api/[Controller]")]
    public class JwtController : ControllerBase
    {
        [HttpGet]
        [Route("{login}/{password}")]
        public ActionResult<string> GetJwt(string login, string password)
        {
            if(password == "archilog")
            {
                return new ObjectResult(JwtToken.GenerateJwtToken());
            }
            else
            {
                return BadRequest("compte invalide");
            }
        }
    }
}
