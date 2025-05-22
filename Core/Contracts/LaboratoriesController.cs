using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Laboratory;
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Core.Contracts
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoriesController : ControllerBase
    {
        private readonly ILabRepo _labRepo;
        private readonly IMapper _mapper;


        public LaboratoriesController(ILabRepo labRepo,IMapper mapper)
        {
            _labRepo = labRepo;
            _mapper = mapper;
            
        }

        // GET: api/Laboratories
        [HttpGet]
        public async Task<ActionResult<List<GetLabModel>>> GetLaboratory()
        {
            var labs = await _labRepo.GetAllAsync();
            var records = _mapper.Map<List<GetLabModel>>(labs);
            return records;
        }

        // GET: api/Laboratories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LabModel>> GetLaboratory(int id)
        {
            var laboratory = await _labRepo.GetLabDetailsAsync(id);
            if (laboratory == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<LabModel>(laboratory);
            record.PatientName = laboratory.Patient.FullName;

            return record;
        }

        // PUT: api/Laboratories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaboratory(int id, UpdateLabModel updateLab)
        {
            
            if (id != updateLab.Id)
            {
                return BadRequest();
            }
            var lab = await _labRepo.GetAsync(updateLab.Id);
            if (!await LaboratoryExists(id))
                return NotFound();

            _mapper.Map(updateLab, lab);
            //_context.Entry(laboratory).State = EntityState.Modified;

            try
            {
                await _labRepo.UpdateAsync(lab);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LaboratoryExists(id))
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

        // POST: api/Laboratories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Laboratory>> PostLaboratory(CreateLabModel newLab)
        {
            var lab = _mapper.Map<Laboratory>(newLab);
            await _labRepo.AddAsync(lab);
            //_context.Laboratory.Add(laboratory);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetLaboratory", new { id = lab.Id }, lab);
        }

        // DELETE: api/Laboratories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaboratory(int id)
        {
            var laboratory = await _labRepo.GetAsync(id);
            /*_context.Laboratory.FindAsync(id);*/
            if (laboratory == null)
            {
                return NotFound();
            }

            //_context.Laboratory.Remove(laboratory);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> LaboratoryExists(int id)
        {
            return await _labRepo.Exists(id);
            //_context.Laboratory.Any(e => e.Id == id);
        }
    }
}
