using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OCTOBER.Shared.DTO;

public class GradeTypeDTO
{
    [Key]
    [Column("SCHOOL_ID")]
    [Precision(8)]
    public int SchoolId { get; set; }
    [Key]
    [Column("GRADE_TYPE_CODE")]
    [StringLength(2)]
    [Unicode(false)]
    public string GradeTypeCode { get; set; } = null!;
    [Column("DESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string Description { get; set; } = null!;
    [Column("CREATED_BY")]
    [StringLength(30)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;
    [Column("CREATED_DATE", TypeName = "DATE")]
    public DateTime CreatedDate { get; set; }
    [Column("MODIFIED_BY")]
    [StringLength(30)]
    [Unicode(false)]
    public string ModifiedBy { get; set; } = null!;
    [Column("MODIFIED_DATE", TypeName = "DATE")]
    public DateTime ModifiedDate { get; set; }
}