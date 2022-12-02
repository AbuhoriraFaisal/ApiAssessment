using ApiAssessment.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork<Customer> _coustomer;

        public CustomerController(IUnitOfWork<Customer> coustomer)
        {
            _coustomer = coustomer;
        }
        [HttpGet("/GetAllCustomers")]
        public async Task<IActionResult> Get()
        {
            var result = await _coustomer.Entity.GetAll();
            return Ok(result);
        }
        [HttpPost("/GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var result = await _coustomer.Entity.GetTById(Id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPost("/CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerModel model)
        {
            Customer customer = new Customer
            {
                CustomerId = model.Id,
                CustomerName = model.Name,
                PhoneNumber = model.PhoneNumber,
                Invoices = null
            };
            await _coustomer.Entity.Insert(customer);
            bool result = await _coustomer.CompeleteAsync();
            if (result)
                return Ok(result);
            return BadRequest();
        }
        [HttpPost("/UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(CustomerModel model)
        {
            var customerForEdit = _coustomer.Entity.GetTById(model.Id);
            //await _coustomer.CompeleteAsync();
            if (customerForEdit == null)
                return NotFound();
            Customer customer = new Customer
            {
                CustomerId = model.Id,
                CustomerName = model.Name,
                PhoneNumber = model.PhoneNumber,
            };
            await _coustomer.Entity.Update(customer);
            
            bool result = await _coustomer.CompeleteAsync();
            if (result)
                return Ok(result);
            return BadRequest();
        }
        [HttpPost("/DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int Id)
        {
            bool isDeleted = await _coustomer.Entity.Delete(Id);
            if (!isDeleted)
                return NotFound();
            bool result = await _coustomer.CompeleteAsync();
            if (result)
                return Ok(result);
            return BadRequest();
        }
    }
}
