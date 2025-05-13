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
using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Models.Prescription;
using Microsoft.OpenApi.Validations;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionRepo _prescriptionRepo;
        private readonly IMapper _mapper;

        public PrescriptionsController(IPrescriptionRepo prescriptionRepo,IMapper mapper)
        {
            _prescriptionRepo = prescriptionRepo;
            _mapper = mapper;
        }

        // GET: api/Prescriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPrescriptionModel>>> GetPrescriptions()
        {
            var prescription = await _prescriptionRepo.GetAllAsync();
                //_context.Prescriptions.ToListAsync();
            var record = _mapper.Map<List<GetPrescriptionModel>>(prescription);
            return Ok(record);
        }

        // GET: api/Prescriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionModel>> GetPrescription(int id)
        {
            var prescription = await _prescriptionRepo.GetPrescriptionDetalils(id);
                //_context.Prescriptions.Include(p=>p.Patient).FirstOrDefaultAsync(p=>p.Id==id);

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

            var Prescription = await _prescriptionRepo.GetAsync(id);
           //_context.Prescriptions.FindAsync(id);
            if(!await PrescriptionExists(id))
                return NotFound();

            _mapper.Map(updatePrescription, Prescription);
            //_context.Entry(prescription).State = EntityState.Modified;

            try
            {
                await _prescriptionRepo.UpdateAsync(Prescription);
                //_context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PrescriptionExists(id))
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

            await _prescriptionRepo.AddAsync(prescription);
            //_context.Prescriptions.Add(prescription);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrescription", new { id = prescription.Id }, prescription);
        }

        // DELETE: api/Prescriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var prescription = await _prescriptionRepo.GetAsync(id);
            //_context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            await _prescriptionRepo.DeleteAsync(id);
            //_context.Prescriptions.Remove(prescription);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PrescriptionExists(int id)
        {
            return await _prescriptionRepo.Exists(id);
        }
    }
}
