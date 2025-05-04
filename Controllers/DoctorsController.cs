using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Appointment;
using HospitalManagentApi.Models.ClinicDoctor;
using HospitalManagentApi.Models.Doctor;
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        private readonly IMapper _mapper;

        public DoctorsController(HospitalDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDoctorModel>>> GetDoctor()
        {
            var doctors= await _context.Doctor.ToListAsync();
            var records = _mapper.Map<List<GetDoctorModel>>(doctors);



            return Ok(records);
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorModel>> GetDoctor(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }
            var record = _mapper.Map<DoctorModel>(doctor);
            var Appoint = await _context.Appointments.Where(a => a.DoctorId == doctor.Id).ToListAsync();
            var Clinic = await _context.ClinicDoctors.Where(c => c.DoctorId == doctor.Id).ToListAsync();
            record.Appointment = _mapper.Map<List<GetAppointmentModel>>(Appoint);
            record.Clinic = _mapper.Map<List<GetClinicDoctorModel>>(Clinic);
            return Ok(record);
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, UpdateDoctorModel updateDoctor)
        {
            if (id != updateDoctor.Id)
            {
                return BadRequest();
            }

            var doctor = await _context.Doctor.FindAsync(id);
            if (!DoctorExists(id))
                return NotFound();

            _mapper.Map(updateDoctor, doctor);

            //_context.Entry(updateDoctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(CreateDoctorModel NewDoctor)
        {
            var doctor = _mapper.Map<Doctor>(NewDoctor);
            _context.Doctor.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctor.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }
    }
}
