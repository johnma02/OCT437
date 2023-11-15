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
    public class StudentController : BaseController, GenericRestController<StudentDTO>
    {
        public StudentController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
            : base(context, httpContextAccessor)
        {
        }


        [HttpGet]
        [Route("Get/{SchoolId}/{StudentId}")]
        public async Task<IActionResult> Get(int SchoolId, int StudentId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                StudentDTO? result = await _context
                    .Students
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.StudentId == StudentId)
                    .Select(sp => new StudentDTO
                        {
                            StudentId = sp.StudentId,
                            Salutation = sp.Salutation,
                            FirstName = sp.FirstName,
                            LastName = sp.LastName,
                            StreetAddress = sp.StreetAddress,
                            Zip = sp.Zip,
                            Phone = sp.Phone,
                            RegistrationDate = sp.RegistrationDate,
                            Employer = sp.Employer,
                            CreatedBy = sp.CreatedBy,
                            CreatedDate = sp.CreatedDate,
                            ModifiedBy = sp.ModifiedBy,
                            ModifiedDate = sp.ModifiedDate,
                            SchoolId = sp.SchoolId,
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

                var result = await _context.Students.Select(sp => new StudentDTO
                    {
                        StudentId = sp.StudentId,
                        Salutation = sp.Salutation,
                        FirstName = sp.FirstName,
                        LastName = sp.LastName,
                        StreetAddress = sp.StreetAddress,
                        Zip = sp.Zip,
                        Phone = sp.Phone,
                        RegistrationDate = sp.RegistrationDate,
                        Employer = sp.Employer,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate,
                        SchoolId = sp.SchoolId,
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
        public async Task<IActionResult> Post([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students
                    .Where(x => x.SchoolId == _StudentDTO.SchoolId)
                    .Where(x => x.StudentId == _StudentDTO.StudentId)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    Student s = new Student
                    {
                        StudentId = _StudentDTO.StudentId,
                        Salutation = _StudentDTO.Salutation,
                        FirstName = _StudentDTO.FirstName,
                        LastName = _StudentDTO.LastName,
                        StreetAddress = _StudentDTO.StreetAddress,
                        Zip = _StudentDTO.Zip,
                        Phone = _StudentDTO.Phone,
                        RegistrationDate = _StudentDTO.RegistrationDate,
                        Employer = _StudentDTO.Employer,
                        SchoolId = _StudentDTO.SchoolId,
                    };
                    _context.Students.Add(s);
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
        public async Task<IActionResult> Put([FromBody] StudentDTO _StudentDTO)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students
                    .Where(x => x.SchoolId == _StudentDTO.SchoolId)
                    .Where(x => x.StudentId == _StudentDTO.StudentId)
                    .FirstOrDefaultAsync();

                itm.Salutation = _StudentDTO.Salutation;
                itm.FirstName = _StudentDTO.FirstName;
                itm.LastName = _StudentDTO.LastName;
                itm.StreetAddress = _StudentDTO.StreetAddress;
                itm.Zip = _StudentDTO.Zip;
                itm.Phone = _StudentDTO.Phone;
                itm.RegistrationDate = _StudentDTO.RegistrationDate;
                itm.Employer = _StudentDTO.Employer;

                _context.Students.Update(itm);
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
        [Route("Delete/{SchoolID}/{StudentId}")]
        public async Task<IActionResult> Delete(int SchoolId, int StudentId)
        {
            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.StudentId == StudentId)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Students.Remove(itm);
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