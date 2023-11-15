using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCTOBER.Shared.DTO
{
    public class StudentDTO
    {
        [Key]
        [Column("STUDENT_ID")]
        [Precision(8)]
        public int StudentId { get; set; }
        [Column("SALUTATION")]
        [StringLength(5)]
        [Unicode(false)]
        public string? Salutation { get; set; }
        [Column("FIRST_NAME")]
        [StringLength(25)]
        [Unicode(false)]
        public string? FirstName { get; set; }
        [Column("LAST_NAME")]
        [StringLength(25)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        [Column("STREET_ADDRESS")]
        [StringLength(50)]
        [Unicode(false)]
        public string? StreetAddress { get; set; }
        [Column("ZIP")]
        [StringLength(5)]
        [Unicode(false)]
        public string Zip { get; set; } = null!;
        [Column("PHONE")]
        [StringLength(15)]
        [Unicode(false)]
        public string? Phone { get; set; }
        [Column("EMPLOYER")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Employer { get; set; }
        [Column("REGISTRATION_DATE", TypeName = "DATE")]
        public DateTime RegistrationDate { get; set; }
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
        [Key]
        [Column("SCHOOL_ID")]
        [Precision(8)]
        public int SchoolId { get; set; }

    }
}
