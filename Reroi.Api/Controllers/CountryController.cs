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
    [Route("api/[controller]")]
    public class CountryController : Controller
    {
        private ICountryRepository countryRepository;        
        int page = 1;
        int pageSize = 20;

        public CountryController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
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
            var totalCountries = countryRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalCountries / pageSize);

            IEnumerable<Country> countries = countryRepository
                .GetAll()
                .OrderBy(s => s.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalCountries, totalPages);

            IEnumerable<CountryViewModel> countriesVM = Mapper.Map<IEnumerable<Country>, IEnumerable<CountryViewModel>>(countries);

            return new OkObjectResult(countriesVM);
        }

        [HttpGet("{id}", Name = "GetCountry")]
        public IActionResult Get(int id)
        {
            Country country = countryRepository.GetSingle(s => s.Id == id);

            if (country != null)
            {
                CountryViewModel countryVM = Mapper.Map<Country, CountryViewModel>(country);
                return new OkObjectResult(countryVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]CountryViewModel country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Country newCountry = Mapper.Map<CountryViewModel, Country>(country);

            Country newCountry = new Country { Name = country.Name, EpiIndex = country.EpiIndex };
            countryRepository.Add(newCountry);
            countryRepository.Commit();

            country = Mapper.Map<Country, CountryViewModel>(newCountry);

            CreatedAtRouteResult result = CreatedAtRoute("GetCountry", new { controller = "Country", id = country.Id }, country);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CountryViewModel country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Country updateCountry = countryRepository.GetSingle(id);

            if (updateCountry == null)
            {
                return NotFound();
            }
            else
            {
                updateCountry.Name = country.Name;
                updateCountry.EpiIndex = country.EpiIndex;
                countryRepository.Commit();
            }

            country = Mapper.Map<Country, CountryViewModel>(updateCountry);

            return new NoContentResult();
        }

        [HttpDelete("{id}", Name = "DeleteCountry")]
        public IActionResult Delete(int id)
        {
            Country country = countryRepository.GetSingle(id);

            if (country == null)
            {
                return new NotFoundResult();
            }
            else
            {                
                countryRepository.Delete(country);
                countryRepository.Commit();
                return new NoContentResult();
            }
        }        
    }
}
