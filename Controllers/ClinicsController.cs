using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Clinic;
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        private readonly IMapper _mapper;

        public ClinicsController(HospitalDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Clinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetClinicModel>>> GetClinics()
        {
            var clinics= await _context.Clinics.ToListAsync();
            var records = _mapper.Map<List<GetClinicModel>>(clinics);
            return Ok(records);
        }

        // GET: api/Clinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetClinicModel>> GetClinic(int id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            
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
        public async Task<IActionResult> PutClinic(int id, UpdateCLinicModel updateCLinic)
        {
            if (id != updateCLinic.Id)
            {
                return BadRequest();
            }
            var clinic=await _context.Clinics.FindAsync(id);
            if (!ClinicExists(id))
                return NotFound();
            //_context.Entry(clinic).State = EntityState.Modified;
            _mapper.Map(updateCLinic, clinic);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(id))
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
        public async Task<ActionResult<Clinic>> PostClinic(CreateClinicModel CreatedClinic)
        {

            var clinic = _mapper.Map<Clinic>(CreatedClinic);
            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClinic", new { id = clinic.Id }, clinic);
        }

        // DELETE: api/Clinics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClinicExists(int id)
        {
            return _context.Clinics.Any(e => e.Id == id);
        }
    }
}
