using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.ClinicDoctor;
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicDoctorsController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        private readonly IMapper _mapper;

        public ClinicDoctorsController(HospitalDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ClinicDoctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetClinicDoctorModel>>> GetClinicDoctors()
        {
            var Clinics= await _context.ClinicDoctors.ToListAsync();
            var records = _mapper.Map<List<GetClinicDoctorModel>>(Clinics);
            return Ok(records);
        }

        // GET: api/ClinicDoctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicDoctorModel>> GetClinicDoctor(int id)
        {
            var clinicDoctor = await _context.ClinicDoctors.FindAsync(id);

            if (clinicDoctor == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<ClinicDoctorModel>(clinicDoctor);
            record.DrName = clinicDoctor.Doctor.FullName;
            record.ClinicName = clinicDoctor.Clinic.Name;
            return Ok(record);
        }

        // PUT: api/ClinicDoctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinicDoctor(int id, UpdateClinicDoctorModel updateClinicDoctor)
        {
            if (id != updateClinicDoctor.Id)
            {
                return BadRequest();
            }

            var clinicDoctor = await _context.ClinicDoctors.FindAsync(id);

            if (!ClinicDoctorExists(id))
                return NotFound();

            _mapper.Map(updateClinicDoctor, clinicDoctor);

            //_context.Entry(updateClinicDoctor).State = EntityState.Modified;

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
        public async Task<ActionResult<ClinicDoctor>> PostClinicDoctor(CreateClinicDoctorModel CreatedClinicDoctor)
        {
            var clinicDoctor = _mapper.Map<ClinicDoctor>(CreatedClinicDoctor);
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
