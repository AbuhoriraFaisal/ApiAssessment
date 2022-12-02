using ApiAssessment.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork<Invoice> _invoice;
        public InvoiceController(IUnitOfWork<Invoice> invoice)
        {
            _invoice = invoice;
        }
        [HttpGet("/GetAllInvoices")]
        public async Task<IActionResult> Get()
        {
            var result = await _invoice.Entity.GetAll();
            return Ok(result);
        }
        [HttpPost("/GetInvoiceById")]
        public async Task<IActionResult> GetInvoiceById(int Id)
        {
            var result = await _invoice.Entity.GetTById(Id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPost("/CreateInvoice")]
        public async Task<IActionResult> CreateInvoice(Invoice model)
        {
            await _invoice.Entity.Insert(model);
            bool result = await _invoice.CompeleteAsync();
            if (result)
                return Ok(result);
            return BadRequest();
        }
        [HttpPost("/UpdateInvoice")]
        public async Task<IActionResult> UpdateInvoice(Invoice model)
        {
            var invoiceForEdit = _invoice.Entity.GetTById(model.InvoiceId);
            //await _invoice.CompeleteAsync();
            if (invoiceForEdit == null)
                return NotFound();
            await _invoice.Entity.Update(model);

            bool result = await _invoice.CompeleteAsync();
            if (result)
                return Ok(result);
            return BadRequest();
        }
        [HttpPost("/DeleteInvoice")]
        public async Task<IActionResult> DeleteInvoice(int Id)
        {
            bool isDeleted = await _invoice.Entity.Delete(Id);
            if (!isDeleted)
                return NotFound();
            bool result = await _invoice.CompeleteAsync();
            if (result)
                return Ok(result);
            return BadRequest();
        }
    }
}
