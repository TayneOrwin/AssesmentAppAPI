using System;
using System.ComponentModel.DataAnnotations;

namespace AssesmentAPI.Models.Entities
{
    public class AccessRole
    {
        [Key]
        public int AccessRoleID { get; set; }
        public string RoleDescription { get; set; }


    }
}

