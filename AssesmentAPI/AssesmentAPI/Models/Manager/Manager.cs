using System;
using System.ComponentModel.DataAnnotations;

namespace AssesmentAPI.Models.Entities
{
    public class Manager
    {
        [Key]
        public int ManagerID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime dob { get; set; }
        public double salary { get; set; }
        public string ProfileImage { get; set; }

    }
}

