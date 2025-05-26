using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalManagentApi.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Clinic;
using HospitalManagentApi.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicRepo _clinicRepo;
        private readonly IMapper _mapper;

        public ClinicsController(IClinicRepo clinicRepo,IMapper mapper)
        {
            
            _clinicRepo = clinicRepo;
            _mapper = mapper;
        }

        // GET: api/Clinics
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GetClinicModel>>> GetClinics()
        {
            var clinics = await _clinicRepo.GetAllAsync();
                //_clinicRepo.Clinics.ToListAsync();
            var records = _mapper.Map<List<GetClinicModel>>(clinics);
            return Ok(records);
        }

        // GET: api/Clinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetClinicModel>> GetClinic(int id)
        {
            var clinic = await _clinicRepo.GetAsync(id);
            
            if (clinic == null)
            {
                return NotFound();
            }
            var record = _mapper.Map<GetClinicModel>(clinic);

            return Ok(record);
        }

        // PUT: api/Clinics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutClinic(int id, UpdateCLinicModel updateCLinic)
        {
            if (id != updateCLinic.Id)
            {
                return BadRequest();
            }

            var clinic = await _clinicRepo.GetAsync(id);
                //_clinicRepo.Clinics.FindAsync(id);
            if (!await ClinicExists(id))
                return NotFound();
            //_clinicRepo.Entry(clinic).State = EntityState.Modified;
            _mapper.Map(updateCLinic, clinic);
            try
            {
                await _clinicRepo.UpdateAsync(clinic);
                //_clinicRepo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClinicExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clinics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Clinic>> PostClinic(CreateClinicModel CreatedClinic)
        {

            var clinic = _mapper.Map<Clinic>(CreatedClinic);
            //_clinicRepo.Clinics.Add(clinic);
            //await _clinicRepo.SaveChangesAsync();

            await _clinicRepo.AddAsync(clinic);

            return CreatedAtAction("GetClinic", new { id = clinic.Id }, clinic);
        }

        // DELETE: api/Clinics/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            var clinic = await _clinicRepo.GetAsync(id);
                //_clinicRepo.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            //_clinicRepo.Clinics.Remove(clinic);
            //await _clinicRepo.SaveChangesAsync();
            _clinicRepo.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> ClinicExists(int id)
        {
            return await _clinicRepo.Exists(id);
            //_clinicRepo.Clinics.Any(e => e.Id == id);
        }
    }
}
