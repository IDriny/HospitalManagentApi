using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Diagnosis;
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosesController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        private readonly IMapper _mapper;

        public DiagnosesController(HospitalDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Diagnoses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDiagnosisModel>>> GetDiagnoses()
        {
            var diagnoses= await _context.Diagnoses.ToListAsync();
            var records = _mapper.Map<List<GetDiagnosisModel>>(diagnoses);
            return Ok(records);
        }

        // GET: api/Diagnoses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosisModel>> GetDiagnosis(int id)
        {
            var diagnosis = await _context.Diagnoses.Include(d=>d.Patient).FirstOrDefaultAsync(p=>p.Id==id);

            if (diagnosis == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<DiagnosisModel>(diagnosis);
            record.PatientName = diagnosis.Patient.FullName;

            return Ok(record);
        }

        // PUT: api/Diagnoses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnosis(int id, UpdateDiagnosisModel updateDiagnosis)
        {
            if (id != updateDiagnosis.Id)
            {
                return BadRequest();
            }

            var diagnosis = await _context.Diagnoses.FindAsync(id);
            
            if (!DiagnosisExists(id))
                return NotFound();

            //_context.Entry(diagnosis).State = EntityState.Modified;
            _mapper.Map(updateDiagnosis, diagnosis);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiagnosisExists(id))
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

        // POST: api/Diagnoses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diagnosis>> PostDiagnosis(CreateDiagnosisModel createDiagnosis)
        {
            var diagnosis = _mapper.Map<Diagnosis>(createDiagnosis);
            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiagnosis", new { id = diagnosis.Id }, diagnosis);
        }

        // DELETE: api/Diagnoses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosis(int id)
        {
            var diagnosis = await _context.Diagnoses.FindAsync(id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            _context.Diagnoses.Remove(diagnosis);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiagnosisExists(int id)
        {
            return _context.Diagnoses.Any(e => e.Id == id);
        }
    }
}
