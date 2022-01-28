using System;
using System.Collections.Generic;
using System.Text;
using ArchiLibrary.Data;
using ArchiLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ArchiLibrary.Filter;
using Microsoft.AspNetCore.Authorization;
using ArchiLibrary.Extensions;
using ArchiLibrary.Token;
using System.Linq.Expressions;

namespace ArchiLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.1")]
    // BaseController qui prend deux paramètre de généricité à savoir le context et le model
    // lequel hérité de ControllerBase ou TContext hérite de BaseDbContext (lui même hérite de DbContext) ou TModel 
    // hérite de BaseModel 
    public class BaseController<TContext, TModel> : ControllerBase where TContext : BaseDbContext where TModel : BaseModel
    {

        protected readonly TContext _context;
        public BaseController(TContext context)
        {
            _context = context;
        }

        // GET: api/[controller]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TModel>>> GetAll([FromQuery] Params param, [FromQuery] PaginationFilter filter)
        {
         
            var query = _context.Set<TModel>().Where(x => x.Active == true);

            //WHERE



            //QueryExtensions.Sort(result2, param);
            query = query.Sort(param);

            var totalRecords = await query.CountAsync();

            //this.Response.Headers.Add("Accepted Range", "12");

            //return await resultOrd.ToListAsync();
            if (filter.PageSize != null && filter.PageNumber != null)
            {
                if (filter.PageSize < 3)
                {
                    var start = (filter.PageNumber - 1) * filter.PageSize;

                    query = query.Skip(Convert.ToInt32(start)).Take(Convert.ToInt32(filter.PageSize));

                    //this.Response.Headers.Add()
                }
                else
                {
                    return BadRequest();
                }
            }

            //return new PagedResponse<List<TModel>>(pagedData, validFilter.PageNumber, validFilter.PageSize)
            return await query.ToListAsync();


            //return await _context.Set<TModel>().Where(x => x.Active == true).ToListAsync();
        }
       
        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TModel>> GetById(int id)
        {
            var item = await _context.Set<TModel>().SingleOrDefaultAsync(x => x.ID == id && x.Active);

            if (item == null)
            { 
                return NotFound();
            }

            return item;
        }

        // PUT: api/[controller]/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, TModel model)
        {

            if (id != model.ID)
            {
                return BadRequest();
            }

            if (!ModelExists(id))
            {
                return NotFound();
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        

        // POST: api/[controller]
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TModel>> PostItem(TModel model)
        {
            _context.Set<TModel>().Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.ID }, model);
        }
       

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TModel>> DeleteItem(int id)
        {
            var item = await _context.Set<TModel>().FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            //_context.Set<TModel>().Remove(item);
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return item;
        }

        // fonction de recherche
        [HttpGet("recherche")]
        public async Task<ActionResult<IEnumerable<TModel>>> SearchingName ()
        {
            
            // on récupère la clé de notre paramètre
            var Propertykey = Request.Query.First().Key;
            // on récupère la valeur de notre proprité
            var valueProperty = Request.Query.First().Value.ToString();

            // creation de notre expression Lambda 
            var parameter = Expression.Parameter(typeof(TModel), "x");
            var property = Expression.Property(parameter, Propertykey);
            var constanteProperty = Expression.Constant(valueProperty, typeof(string));
            BinaryExpression binaryExpression = Expression.Equal(property, constanteProperty);
            var Lambda = Expression.Lambda<Func<TModel, bool>>(binaryExpression, parameter);

            // resultat retouné 
            var result = await _context.Set<TModel>().Where(Lambda).ToListAsync();
            return result; 
            
        }

        // fonction qui permet de filtrer les résultats 
        
        // fonction booléenne permettant de renvoyer si oui ou non le existe
        private bool ModelExists(int id)
        {
            return _context.Set<TModel>().Any(e => e.ID == id && e.Active);
        }

        /*[HttpGet]
        [Route("search/{item}")]
        public HttpResponseMessage search (string item)
        {
            try
            {
                Customer customer = new Customer();
                var response = new HttpResponseMessage();
                response.Content = new StringContent (JsonConvert.SerializeObject.)
            }
        }*/

        
    }
}
