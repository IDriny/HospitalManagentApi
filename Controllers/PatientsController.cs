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
using HospitalManagentApi.Models.Appointment;
using HospitalManagentApi.Models.Patient;
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepo _patientRepo;
        private readonly IMapper _mapper;

        public PatientsController(IPatientRepo patientRepo, IMapper mapper)
        {
            _patientRepo = patientRepo;
            _mapper = mapper;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPatientModel>>> GetPatient()
        {
            var patients = await _patientRepo.GetAllAsync();
                //_patientRepo.Patient.ToListAsync();
            var records = _mapper.Map<List<GetPatientModel>>(patients);
            return Ok(records);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientModel>> GetPatient(int id)
        {
            var patient = await _patientRepo.GetDetailsAsync(id);
                //_patientRepo.Patient.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<PatientModel>(patient);
            //var Appoint= await _patientRepo.Appointments.Where(a => a.PatientId == patient.Id).ToListAsync();
            //record.Appointment = _mapper.Map<List<GetAppointmentModel>>(Appoint);


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

            var patient = await _patientRepo.GetAsync(id);
                //_patientRepo.Patient.FindAsync(id);

            if (!await PatientExists(id))
                return NotFound();

            _mapper.Map(updatePatient, patient);
            patient.FullName = updatePatient.fName + " " + updatePatient.lName;
            
            //_patientRepo.Entry(updatePatient).State = EntityState.Modified;

            try
            {
                await _patientRepo.UpdateAsync(patient);
                //_patientRepo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PatientExists(id))
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
            patient.FullName = NewPatient.fName + " " + NewPatient.lName;
            await _patientRepo.AddAsync(patient);
            
            //_patientRepo.Patient.Add(patient);
            //await _patientRepo.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _patientRepo.GetAsync(id);
                //_patientRepo.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            //_patientRepo.Patient.Remove(patient);
            //await _patientRepo.SaveChangesAsync();

            _patientRepo.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> PatientExists(int id)
        {
            return await _patientRepo.Exists(id);
        }
    }
}
