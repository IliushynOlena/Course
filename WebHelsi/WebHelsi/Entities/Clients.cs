﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebHelsi.Entities
{
    [Table("tblClients")]
    public class Clients
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 250)]
        public string Name { get; set; }

        public string Surname { get; set; }
        public string Spetialization { get; set; }

        public DateTime DateBirthday { get; set; }

        [ForeignKey("Cities")]
        public int CitiesId { get; set; }
        public virtual Cities City { get; set; }

        [ForeignKey("Clinic")]
        public int ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }

        [ForeignKey("ListDoctors")]
        public int ListDoctorsId { get; set; }
        public virtual ListDoctors ListDoctors { get; set; }

        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}
