using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tareas.DTO;
using Tareas.Models;

namespace Tareas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly ItemDBContext _itemDBContext;
        public ItemsController(ItemDBContext itemDBContext)
        {
            this._itemDBContext = itemDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _itemDBContext.Items.Where(r=>r.Estate!=0).ToListAsync();

                return Ok(items);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpGet]
        [Route("User/{id}")]
        public async Task<IActionResult> GetAllItemsByUser([FromRoute] string id)
        {
            try
            {
                var items = await _itemDBContext.Items.Where(r => r.IdUser == id && r.Estate!=0).ToListAsync();

                return Ok(items);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddItems([FromBody] ItemsDTO itemRequest)
        {


            var item = new Items()
            {
                Id = Guid.NewGuid(),
                Title = itemRequest.Title,
                Description = itemRequest.Description,
                Responsible = itemRequest.Responsible,
                IsComplete = itemRequest.IsComplete,
                Estate = 1,
                Dia = itemRequest.Dia,
                Mes = itemRequest.Mes,
                Anio = itemRequest.Anio,
                IdUser = itemRequest.IdUser
            };

            await _itemDBContext.Items.AddAsync(item);
            await _itemDBContext.SaveChangesAsync();

            return Ok(item);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id)
        {
            var item = await _itemDBContext.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateItem([FromRoute] Guid id, ItemDTOAc itemsD)
        {
            var item = await _itemDBContext.Items.FindAsync(id);

            if (item != null)
            {
                item.Title = itemsD.Title;
                item.Description = itemsD.Description;
                item.Responsible = itemsD.Responsible;
                item.IsComplete = itemsD.IsComplete;
                item.Dia = itemsD.Dia;
                item.Mes = itemsD.Mes;
                item.Anio = itemsD.Anio;

                await _itemDBContext.SaveChangesAsync();
                return Ok(item);

            }


            return NotFound();
            
        }

        [HttpPut]
        [Route("complete/{id:guid}")]
        public async Task<IActionResult> ActualizarOneItem([FromRoute] Guid id, int complete)
        {
            //var result = _dbContext.TodoItems.Where(r => r.Estate == 0).ToList();

            try
            {
                var respon = await _itemDBContext.Items.FindAsync(id);
                respon.IsComplete = complete + 1;
                await _itemDBContext.SaveChangesAsync();

                return Ok("la tarea se ha editado de forma correcta!");
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ItemAvailable(id))
                {

                    return NotFound("Problemas en la actualizacion de la base de datos");

                }
                else { throw; }

            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {

            var item = await _itemDBContext.Items.FindAsync(id);
            var recordToUpdate = _itemDBContext.Items.FirstOrDefault(r => r.Id == id);

            try
            {
                if (recordToUpdate != null)
                {
                    recordToUpdate.Estate = 0;
                    _itemDBContext.SaveChanges();
                }
                else { return NotFound(); }


                return Ok(new { code = 200, message = $"El usuario con id {id} fue eliminado" });
            }
            catch (Exception)
            {
                throw;
            }

        }

        private bool ItemAvailable(Guid id)
        {

            return (_itemDBContext.Items?.Any(x => x.Id == id)).GetValueOrDefault();

        }


    }


}
