using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Persistence;
using AutoMapper;
using HospitalManagentApi.Models.Prescription;
using Microsoft.OpenApi.Validations;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        private readonly IMapper _mapper;

        public PrescriptionsController(HospitalDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Prescriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPrescriptionModel>>> GetPrescriptions()
        {
            var prescription= await _context.Prescriptions.ToListAsync();
            var record = _mapper.Map<List<GetPrescriptionModel>>(prescription);
            return Ok(record);
        }

        // GET: api/Prescriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionModel>> GetPrescription(int id)
        {
            var prescription = await _context.Prescriptions.Include(p=>p.Patient).FirstOrDefaultAsync(p=>p.Id==id);

            if (prescription == null)
            {
                return NotFound();
            }
            var record=_mapper.Map<PrescriptionModel>(prescription);
            record.PatientName = prescription.Patient.FullName;
            return Ok(record);
        }

        // PUT: api/Prescriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrescription(int id, UpdatePrescriptionModel updatePrescription)
        {
            if (id != updatePrescription.Id)
            {
                return BadRequest();
            }

           var Prescription=await _context.Prescriptions.FindAsync(id);
            if(!PrescriptionExists(id))
                return NotFound();

            _mapper.Map(updatePrescription, Prescription);
            //_context.Entry(prescription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionExists(id))
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

        // POST: api/Prescriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prescription>> PostPrescription(CreatePrescriptionModel createPrescription)
        {
            var prescription =_mapper.Map<Prescription>(createPrescription);
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrescription", new { id = prescription.Id }, prescription);
        }

        // DELETE: api/Prescriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(e => e.Id == id);
        }
    }
}
