using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MVC_Evaluation_1.ViewModels;

namespace MVC_Evaluation_1.Models
{
    [Table("Users")]
    public class User
    {
        public User()
        {

        }
        public User(int id)
        {
            Id = id;
        }
        public User(string email, string lastName, string firstName, string phoneNumber, EmployeeType empType, string companyName, bool isActive, int createdBy, DateTime createdDate)
        {
            Email = email;
            LastName = lastName;
            FirstName = firstName;
            PhoneNumber = phoneNumber;
            EmpType = empType;
            CompanyName = companyName;
            IsActive = isActive;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
        }
        private UserService svcUser = new UserService();

        [Key]
        public int Id { get; private set; }

        [StringLength(125)]
        [Required]
        [Index("IX_USER_EMAIL", IsClustered = false, IsUnique = true)]
        [Display(Name = "Email Address")]
        public string Email { get; private set; }

        [StringLength(25)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; private set; }

        [StringLength(25)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; private set; }

        [StringLength(12)]
        [Phone]
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; private set; }


        [Required]
        [Display(Name = "Employee Type")]
        public virtual EmployeeType EmpType { get; private set; }

        [StringLength(50)]
        [Display(Name = "Company Name")]
        public String CompanyName { get; private set; }

        [Display(Name = "User is Active")]
        public bool IsActive { get; private set; }

        [Display(Name = "Last Login")]
        public DateTime? LastLogin { get; private set; }

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; private set; }
        [Required]
        [Display(Name = "Created By")]
        public int CreatedBy { get; private set; }

        public void updateUser(UserViewModel vm)
        {
            LastName = vm.LastName;
            FirstName = vm.FirstName;
            Email = vm.Email;
            PhoneNumber = vm.PhoneNumber;
            EmpType.Id = Convert.ToInt32(vm.EmpTypeSelected);
            CompanyName = vm.CompanyName;
            IsActive = vm.IsActive;
        }

        public String getDisplayName()
        {
            String DisplayName = FirstName + " " + LastName;
            if (DisplayName.Length > 30) DisplayName = DisplayName.Substring(0, 27) + "...";
            return DisplayName;
        }
        public String getDisplayNameLastFirst()
        {
            String DisplayName = LastName + ", " + FirstName;
            if (DisplayName.Length > 30) DisplayName = DisplayName.Substring(0, 27) + "...";
            return DisplayName;
        }
    }
}