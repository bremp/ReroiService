using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reroi.Api.Core.Extensions;
using Reroi.Api.ViewModels;
using Reroi.Data.Abstract;
using Reroi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reroi.Api.Controllers
{
    /// <summary>
    /// Property Controller class.
    /// </summary>
    [Route("api/[controller]")]
    public class PropertyController : Controller
    {
        private IPropertyRepository propertyRepository;        
        int page = 1;
        int pageSize = 20;

        public PropertyController(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public IActionResult Get()
        {
            var pagination = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out page);
                int.TryParse(vals[1], out pageSize);
            }

            int currentPage = page;
            int currentPageSize = pageSize;
            var totalCountries = propertyRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalCountries / pageSize);

            IEnumerable<Property> countries = propertyRepository
                .GetAll()
                .OrderBy(s => s.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalCountries, totalPages);

            IEnumerable<PropertyViewModel> countriesVM = Mapper.Map<IEnumerable<Property>, IEnumerable<PropertyViewModel>>(countries);

            return new OkObjectResult(countriesVM);
        }

        [HttpGet("{id}", Name = "GetProperty")]
        public IActionResult Get(int id)
        {
            Property property = propertyRepository.GetSingle(s => s.Id == id);

            if (property != null)
            {
                PropertyViewModel propertyVM = Mapper.Map<Property, PropertyViewModel>(property);
                return new OkObjectResult(propertyVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]PropertyViewModel property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Property newProperty = Mapper.Map<PropertyViewModel, Property>(property);

            Property newProperty = new Property {
                Mls = property.Mls,
                NetOperatingIncome = property.NetOperatingIncome,
                PurchasePrice = property.PurchasePrice };

            propertyRepository.Add(newProperty);
            propertyRepository.Commit();

            property = Mapper.Map<Property, PropertyViewModel>(newProperty);

            CreatedAtRouteResult result = CreatedAtRoute("GetProperty", new { controller = "Property", id = property.Id }, property);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PropertyViewModel property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Property updateProperty = propertyRepository.GetSingle(id);

            if (updateProperty == null)
            {
                return NotFound();
            }
            else
            {
                updateProperty.Mls = property.Mls;
                updateProperty.NetOperatingIncome = property.NetOperatingIncome;
                updateProperty.PurchasePrice = property.PurchasePrice;                
                propertyRepository.Commit();
            }

            property = Mapper.Map<Property, PropertyViewModel>(updateProperty);

            return new NoContentResult();
        }

        [HttpDelete("{id}", Name = "DeleteProperty")]
        public IActionResult Delete(int id)
        {
            Property property = propertyRepository.GetSingle(id);

            if (property == null)
            {
                return new NotFoundResult();
            }
            else
            {                
                propertyRepository.Delete(property);
                propertyRepository.Commit();
                return new NoContentResult();
            }
        }        
    }
}
