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
using HospitalManagentApi.Models.ClinicDoctor;
using HospitalManagentApi.Models.Doctor;
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepo _doctorRepo;
        private readonly IMapper _mapper;

        public DoctorsController(IDoctorRepo doctorRepo,IMapper mapper)
        {
            _doctorRepo = doctorRepo;
            _mapper = mapper;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDoctorModel>>> GetDoctor()
        {
            var doctors = await _doctorRepo.GetAllAsync();
                //_doctorRepo.Doctor.ToListAsync();
            var records = _mapper.Map<List<GetDoctorModel>>(doctors);



            return Ok(records);
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorModel>> GetDoctor(int id)
        {
            var doctor = await _doctorRepo.GetDetailsAsync(id);
                //_doctorRepo.Doctor.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }
            var record = _mapper.Map<DoctorModel>(doctor);
            //var Appoint = await _doctorRepo.Appointments.Where(a => a.DoctorId == doctor.Id).ToListAsync();
            //var Clinic = await _doctorRepo.ClinicDoctors.Where(c => c.DoctorId == doctor.Id).ToListAsync();
            //record.Appointment = _mapper.Map<List<GetAppointmentModel>>(Appoint);
            //record.Clinic = _mapper.Map<List<GetClinicDoctorModel>>(Clinic);
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

            var doctor = await _doctorRepo.GetAsync(id);
                //_doctorRepo.Doctor.FindAsync(id);
            if (!await DoctorExists(id))
                return NotFound();

            _mapper.Map(updateDoctor, doctor);

            //_doctorRepo.Entry(updateDoctor).State = EntityState.Modified;

            try
            {
                await _doctorRepo.UpdateAsync(doctor);
                //_doctorRepo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DoctorExists(id))
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
            //_doctorRepo.Doctor.Add(doctor);
            //await _doctorRepo.SaveChangesAsync();
            await _doctorRepo.AddAsync(doctor);

            return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _doctorRepo.GetAsync(id);
                //_doctorRepo.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            //_doctorRepo.Doctor.Remove(doctor);
            //await _doctorRepo.SaveChangesAsync();
            _doctorRepo.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> DoctorExists(int id)
        {
            return await _doctorRepo.Exists(id);
            //_doctorRepo.Doctor.Any(e => e.Id == id);
        }
    }
}
