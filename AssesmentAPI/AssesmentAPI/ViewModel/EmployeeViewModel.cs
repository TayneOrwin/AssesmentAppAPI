using System;
using AssesmentAPI.Models.Entities;

namespace AssesmentAPI.ViewModel
{
    public class EmployeeViewModel
    { 

        public string name { get; set; }
        public string surname { get; set; }
        public DateTime dob { get; set; }
        public double salary { get; set; }
        public string ProfileImage { get; set; }
        public int AccessRoleID { get; set; }
        public int ManagerID { get; set; }





    }
}

