using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicDoctorsController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public ClinicDoctorsController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: api/ClinicDoctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicDoctor>>> GetClinicDoctors()
        {
            return await _context.ClinicDoctors.ToListAsync();
        }

        // GET: api/ClinicDoctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicDoctor>> GetClinicDoctor(int id)
        {
            var clinicDoctor = await _context.ClinicDoctors.FindAsync(id);

            if (clinicDoctor == null)
            {
                return NotFound();
            }

            return clinicDoctor;
        }

        // PUT: api/ClinicDoctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinicDoctor(int id, ClinicDoctor clinicDoctor)
        {
            if (id != clinicDoctor.Id)
            {
                return BadRequest();
            }

            _context.Entry(clinicDoctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicDoctorExists(id))
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

        // POST: api/ClinicDoctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClinicDoctor>> PostClinicDoctor(ClinicDoctor clinicDoctor)
        {
            _context.ClinicDoctors.Add(clinicDoctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClinicDoctor", new { id = clinicDoctor.Id }, clinicDoctor);
        }

        // DELETE: api/ClinicDoctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinicDoctor(int id)
        {
            var clinicDoctor = await _context.ClinicDoctors.FindAsync(id);
            if (clinicDoctor == null)
            {
                return NotFound();
            }

            _context.ClinicDoctors.Remove(clinicDoctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClinicDoctorExists(int id)
        {
            return _context.ClinicDoctors.Any(e => e.Id == id);
        }
    }
}
