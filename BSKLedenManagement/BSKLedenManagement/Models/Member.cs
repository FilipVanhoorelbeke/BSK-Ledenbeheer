using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BSKLedenManagement.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Kring { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }

        public string Adres { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        // Set to inactive automatically after 1 year and manage this somehow?

        public bool IsActive { get; set; }

        public bool Verzekering { get; set; }

    }
}