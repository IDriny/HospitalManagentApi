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
using HospitalManagentApi.Models.Diagnosis;
using HospitalManagentApi.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosesController : ControllerBase
    {
        private readonly IDiagnosisRepo _diagnosisRepo;
        private readonly IMapper _mapper;

        public DiagnosesController(IDiagnosisRepo _diagnosisRepo,IMapper mapper)
        {
            this._diagnosisRepo = _diagnosisRepo;
            _mapper = mapper;
        }

        // GET: api/Diagnoses
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult<IEnumerable<GetDiagnosisModel>>> GetDiagnoses()
        {
            var diagnoses = await _diagnosisRepo.GetAllAsync();
            var records = _mapper.Map<List<GetDiagnosisModel>>(diagnoses);
            return Ok(records);
        }

        // GET: api/Diagnoses/5
        [HttpGet("{id}")]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult<DiagnosisModel>> GetDiagnosis(int id)
        {
            var diagnosis = await _diagnosisRepo.GetDiagnosisDetailsAsync(id);
                //_context.Diagnoses.Include(d=>d.Patient).FirstOrDefaultAsync(p=>p.Id==id);

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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutDiagnosis(int id, UpdateDiagnosisModel updateDiagnosis)
        {
            if (id != updateDiagnosis.Id)
            {
                return BadRequest();
            }

            var diagnosis = await _diagnosisRepo.GetAsync(id);
                //_context.Diagnoses.FindAsync(id);
            
            if (!await DiagnosisExists(id))
                return NotFound();

            //_context.Entry(diagnosis).State = EntityState.Modified;
            _mapper.Map(updateDiagnosis, diagnosis);



            try
            {
                await _diagnosisRepo.UpdateAsync(diagnosis);
                //_context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DiagnosisExists(id))
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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Diagnosis>> PostDiagnosis(CreateDiagnosisModel createDiagnosis)
        {
            var diagnosis = _mapper.Map<Diagnosis>(createDiagnosis);

            await _diagnosisRepo.AddAsync(diagnosis);
            
            //_context.Diagnoses.Add(diagnosis);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiagnosis", new { id = diagnosis.Id }, diagnosis);
        }

        // DELETE: api/Diagnoses/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteDiagnosis(int id)
        {
            var diagnosis = await _diagnosisRepo.GetAsync(id);
                //_context.Diagnoses.FindAsync(id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            await _diagnosisRepo.DeleteAsync(diagnosis.Id);
            //_context.Diagnoses.Remove(diagnosis);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool>DiagnosisExists(int id)
        {
            return await _diagnosisRepo.Exists(id);
            //_context.Diagnoses.Any(e => e.Id == id);
        }
    }
}
