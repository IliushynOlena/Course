﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebHelsi.Entities
{
    [Table("tblDoctors")]
    public class Doctor
    {
        [Key, ForeignKey("User")]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 250)]
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateBirthday { get; set; }
        public string ImageDoctor { get; set; }
        public virtual DbUser User { get; set; }

        [ForeignKey("Clinic")]
        public int ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }

        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }

        //  public virtual ICollection<Client> Clients { get; set; }
    }
}
