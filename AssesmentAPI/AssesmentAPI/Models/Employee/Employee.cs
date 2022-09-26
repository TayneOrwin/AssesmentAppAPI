using System;
using System.ComponentModel.DataAnnotations;

namespace AssesmentAPI.Models.Entities
{
    public class Employee
    {
        [Key]
        public int employeeNumber { get; set; }

        public string name { get; set; }
        public string surname { get; set; }
        public DateTime dob { get; set; }
        public double salary { get; set; }
        public string ProfileImage { get; set; }
        public AccessRole accessRole { get; set; }
        public int AccessRoleID { get; set; }

        public Manager Manager { get; set; }
            public int ManagerID { get; set; }


    }
}

