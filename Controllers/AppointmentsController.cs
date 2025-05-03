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
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentsController(HospitalDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAppointmentModel>>> GetAppointments()
        {
            var appointments= await _context.Appointments.ToListAsync();
            var records = _mapper.Map<List<Appointment>>(appointments);
            return Ok(records);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAppointmentInfoModel>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.ID == id);

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
        public async Task<IActionResult> PutAppointment(int id, UpdateAppointmentModel Updatedappointment)
        {
            if (id != Updatedappointment.Id)
            {
                return BadRequest();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (!AppointmentExists(id))
                return NotFound();
            //_context.Entry(appointment).State = EntityState.Modified;
            _mapper.Map(Updatedappointment, appointment);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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
        public async Task<ActionResult<Appointment>> PostAppointment(CreateAppointmentModel CreatedAppointment)
        {
            var appointment = _mapper.Map<Appointment>(CreatedAppointment);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.ID }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.ID == id);
        }
    }
}
