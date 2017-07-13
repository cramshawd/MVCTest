using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Microsoft.Practices.Unity;


namespace MVC_Evaluation_1.Models
{
    [Table("EmployeeTypes")]
    public class EmployeeType
    {
        public EmployeeType()
        {

        }
        public EmployeeType(int id, string name, int sortOrder)
        {
            Id = id;
            Name = name;
            SortOrder = sortOrder;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Index("IX_EMP_TYPE", IsClustered = false, IsUnique = true)]
        [Display(Name="Employee Type")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }
    }
}