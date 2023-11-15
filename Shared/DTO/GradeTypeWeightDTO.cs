using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OCTOBER.Shared.DTO;

public class GradeTypeWeightDTO
{
    [Key]
    [Column("SCHOOL_ID")]
    [Precision(8)]
    public int SchoolId { get; set; }
    [Key]
    [Column("SECTION_ID")]
    [Precision(8)]
    public int SectionId { get; set; }
    [Key]
    [Column("GRADE_TYPE_CODE")]
    [StringLength(2)]
    [Unicode(false)]
    public string GradeTypeCode { get; set; } = null!;
    [Column("NUMBER_PER_SECTION")]
    [Precision(3)]
    public byte NumberPerSection { get; set; }
    [Column("PERCENT_OF_FINAL_GRADE")]
    [Precision(3)]
    public byte PercentOfFinalGrade { get; set; }
    [Column("DROP_LOWEST")]
    [Precision(1)]
    public bool DropLowest { get; set; }
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