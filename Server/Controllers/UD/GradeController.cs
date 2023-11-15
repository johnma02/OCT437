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
    public class GradeController : BaseController, GenericRestController<GradeDTO>
    {
        public GradeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
            : base(context, httpContextAccessor)
        {
        }


        [HttpGet]
        [Route("Get/{SchoolId}/{SectionId}/{StudentId}/{GradeTypeCode}/{GradeCodeOccurrence}")]
        public async Task<IActionResult> Get(int SchoolId, int SectionId, int StudentId, string GradeTypeCode,
            byte GradeCodeOccurrence)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeDTO? result = await _context
                    .Grades
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.SectionId == SectionId)
                    .Where(x => x.StudentId == StudentId)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Where(x => x.GradeCodeOccurrence == GradeCodeOccurrence)
                    .Select(sp => new GradeDTO
                        {
                            SchoolId = sp.SchoolId,
                            SectionId = sp.SectionId,
                            GradeTypeCode = sp.GradeTypeCode,
                            GradeCodeOccurrence = sp.GradeCodeOccurrence,
                            NumericGrade = sp.NumericGrade,
                            Comments = sp.Comments,
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

                var result = await _context.Grades.Select(sp => new GradeDTO
                    {
                        SchoolId = sp.SchoolId,
                        SectionId = sp.SectionId,
                        GradeTypeCode = sp.GradeTypeCode,
                        GradeCodeOccurrence = sp.GradeCodeOccurrence,
                        NumericGrade = sp.NumericGrade,
                        Comments = sp.Comments,
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
        public async Task<IActionResult> Post([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades
                    .Where(x => x.StudentId == _GradeDTO.StudentId)
                    .Where(x => x.SectionId == _GradeDTO.SectionId)
                    .Where(x => x.SchoolId == _GradeDTO.SchoolId)
                    .Where(x => x.GradeTypeCode == _GradeDTO.GradeTypeCode)
                    .Where(x => x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    Grade s = new Grade
                    {
                        SchoolId = _GradeDTO.SchoolId,
                        SectionId = _GradeDTO.SectionId,
                        GradeTypeCode = _GradeDTO.GradeTypeCode,
                        GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence,
                        NumericGrade = _GradeDTO.NumericGrade,
                        Comments = _GradeDTO.Comments,
                        StudentId = _GradeDTO.StudentId,
                    };
                    _context.Grades.Add(s);
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
        public async Task<IActionResult> Put([FromBody] GradeDTO _GradeDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades
                    .Where(x => x.StudentId == _GradeDTO.StudentId)
                    .Where(x => x.SectionId == _GradeDTO.SectionId)
                    .Where(x => x.SchoolId == _GradeDTO.SchoolId)
                    .Where(x => x.GradeTypeCode == _GradeDTO.GradeTypeCode)
                    .Where(x => x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence)
                    .FirstOrDefaultAsync();

                itm.NumericGrade = _GradeDTO.NumericGrade;
                itm.Comments = _GradeDTO.Comments;

                _context.Grades.Update(itm);
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
        [Route("Delete/{SchoolID}/{SectionId}/{StudentId}/{GradeTypeCode}/{GradeCodeOccurrence}")]
        public async Task<IActionResult> Delete(int SchoolId, int SectionId, int StudentId, string GradeTypeCode,
            byte GradeCodeOccurrence)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.SectionId == SectionId)
                    .Where(x => x.StudentId == StudentId)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Where(x => x.GradeCodeOccurrence == GradeCodeOccurrence)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Grades.Remove(itm);
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