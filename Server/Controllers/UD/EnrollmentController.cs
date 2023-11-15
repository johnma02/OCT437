using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Shared;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using AutoMapper;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : BaseController, GenericRestController<EnrollmentDTO>
    {
        public EnrollmentController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
            : base(context, httpContextAccessor)
        {
        }


        [HttpGet]
        [Route("Get/{SchoolId}/{SectionId}/{StudentId}")]
        public async Task<IActionResult> Get(int SchoolId, int SectionId, int StudentId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                EnrollmentDTO? result = await _context
                    .Enrollments
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.SectionId == SectionId)
                    .Where(x => x.StudentId == StudentId)
                    .Select(sp => new EnrollmentDTO
                        {
                            SchoolId = sp.SchoolId,
                            SectionId = sp.SectionId,
                            EnrollDate = sp.EnrollDate,
                            FinalGrade = sp.FinalGrade,
                            StudentId = sp.StudentId,
                            CreatedBy = sp.CreatedBy,
                            CreatedDate = sp.CreatedDate,
                            ModifiedBy = sp.ModifiedBy,
                            ModifiedDate = sp.ModifiedDate
                        }
                    )
                    .SingleOrDefaultAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Enrollments.Select(sp => new EnrollmentDTO
                    {
                        SchoolId = sp.SchoolId,
                        SectionId = sp.SectionId,
                        EnrollDate = sp.EnrollDate,
                        FinalGrade = sp.FinalGrade,
                        StudentId = sp.StudentId,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate
                    })
                    .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }


        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments
                    .Where(x => x.StudentId == _EnrollmentDTO.StudentId)
                    .Where(x => x.SectionId == _EnrollmentDTO.SectionId)
                    .Where(x => x.SchoolId == _EnrollmentDTO.SchoolId)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    Enrollment s = new Enrollment
                    {
                        SchoolId = _EnrollmentDTO.SchoolId,
                        SectionId = _EnrollmentDTO.SectionId,
                        EnrollDate = _EnrollmentDTO.EnrollDate,
                        FinalGrade = _EnrollmentDTO.FinalGrade,
                        StudentId = _EnrollmentDTO.StudentId,
                    };
                    _context.Enrollments.Add(s);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }


        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments
                    .Where(x => x.StudentId == _EnrollmentDTO.StudentId)
                    .Where(x => x.SectionId == _EnrollmentDTO.SectionId)
                    .Where(x => x.SchoolId == _EnrollmentDTO.SchoolId)
                    .FirstOrDefaultAsync();
                
                itm.EnrollDate = _EnrollmentDTO.EnrollDate;
                itm.FinalGrade = _EnrollmentDTO.FinalGrade;
                
                _context.Enrollments.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }


        [HttpDelete]
        [Route("Delete/{SchoolID}/{SectionId}/{StudentId}")]
        public async Task<IActionResult> Delete(int SchoolId, int SectionId, int StudentId)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.SectionId == SectionId)
                    .Where(x => x.StudentId == StudentId)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Enrollments.Remove(itm);
                }

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }
    }
}