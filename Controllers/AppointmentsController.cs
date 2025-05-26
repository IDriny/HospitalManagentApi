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
using HospitalManagentApi.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepo _AppointmentRepo;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentRepo appointmentRepo,IMapper mapper)
        {
            _AppointmentRepo = appointmentRepo;
            _mapper = mapper;
        }

        // GET: api/Appointments
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<GetAppointmentModel>>> GetAppointments()
        {
            var appointments = await _AppointmentRepo.GetAllAsync();
            var records = _mapper.Map<List<Appointment>>(appointments);
            return Ok(records);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult<GetAppointmentInfoModel>> GetAppointment(int id)
        {
            var appointment = await _AppointmentRepo.GetDetailsAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<GetAppointmentInfoModel>(appointment);
            record.DoctorName = appointment.Doctor.FullName;
            record.PatientName = appointment.Patient.FullName;

            return Ok(record);
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "User,Administrator")]
        public async Task<IActionResult> PutAppointment(int id, UpdateAppointmentModel Updatedappointment)
        {
            if (id != Updatedappointment.Id)
            {
                return BadRequest();
            }

            var appointment = await _AppointmentRepo.GetAsync(id);
            if (!await AppointmentExists(id))
                return NotFound();
            //_AppointmentRepo.Entry(appointment).State = EntityState.Modified;
            _mapper.Map(Updatedappointment, appointment);

            try
            {
                await _AppointmentRepo.UpdateAsync(appointment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AppointmentExists(id))
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

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult<Appointment>> PostAppointment(CreateAppointmentModel CreatedAppointment)
        {
            var appointment = _mapper.Map<Appointment>(CreatedAppointment);
            //_AppointmentRepo.Appointments.Add(appointment);
            //await _AppointmentRepo.SaveChangesAsync();
            await _AppointmentRepo.AddAsync(appointment);

            return CreatedAtAction("GetAppointment", new { id = appointment.ID }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Administrator")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _AppointmentRepo.GetAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            //_AppointmentRepo.Appointments.Remove(appointment);
            //await _AppointmentRepo.SaveChangesAsync();
            await _AppointmentRepo.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> AppointmentExists(int id)
        {
            return await _AppointmentRepo.Exists(id);
        }
    }
}
