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
using HospitalManagentApi.Models.Patient;
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        private readonly IMapper _mapper;

        public PatientsController(HospitalDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPatientModel>>> GetPatient()
        {
            var patients= await _context.Patient.ToListAsync();
            var records = _mapper.Map<List<GetPatientModel>>(patients);
            return Ok(records);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientModel>> GetPatient(int id)
        {
            var patient = await _context.Patient.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<PatientModel>(patient);
            var Appoint= await _context.Appointments.Where(a => a.PatientId == patient.Id).ToListAsync();
            record.Appointment = _mapper.Map<List<GetAppointmentModel>>(Appoint);
            return Ok(record);
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, UpdatePatientModel updatePatient)
        {
            if (id != updatePatient.Id)
            {
                return BadRequest();
            }
            var patient=await _context.Patient.FindAsync(id);

            if (!PatientExists(id))
                return NotFound();

            _mapper.Map(updatePatient, patient);
            
            //_context.Entry(updatePatient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(CreatePatientModel NewPatient)
        {
            var patient = _mapper.Map<Patient>(NewPatient);
            _context.Patient.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
