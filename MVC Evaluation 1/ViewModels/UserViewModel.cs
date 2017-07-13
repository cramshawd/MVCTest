using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Reflection;
using Microsoft.Practices.Unity;
using MVC_Evaluation_1.Models;



namespace MVC_Evaluation_1.ViewModels
{
    public class UserViewModel
    {
        private UserService svcUser = new UserService();
        public UserViewModel()
        {
            EmpTypes = svcUser.getEmployeeTypeList();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(125)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
            ErrorMessage = "The Email Address field must be a valid format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(12)]
        [RegularExpression(@"\d{3}-\d{3}-\d{4}",
            ErrorMessage = "The Phone Number field must be a valid format")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        [ValidateCompanyName(ErrorMessage = "You must supply a company name if employee type is contractor")]
        [Display(Name = "Company Name")]
        public String CompanyName { get; set; }

        [Display(Name = "User Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Last Login Date/Time")]
        public DateTime? LastLogin { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }

        public IEnumerable<SelectListItem> EmpTypes { get; set; }
        [Required(ErrorMessage = "You must select an Employee Type")]
        public string EmpTypeSelected { get; set; }

        [Display(Name = "Employee Type")]
        public string EmpTypeName { get; set; }

        public UserViewModel(User user)
        {
            EmpTypes = svcUser.getEmployeeTypeList();
            if (user.EmpType != null)
            {
                this.EmpTypeSelected = user.EmpType.Id.ToString();
                this.EmpTypeName = user.EmpType.Name;
            }
            this.Id = user.Id;
            this.Email = user.Email;
            this.LastName = user.LastName;
            this.FirstName = user.FirstName;
            this.IsActive = user.IsActive;
            this.PhoneNumber = user.PhoneNumber;
            this.CompanyName = user.CompanyName;
            this.LastLogin = user.LastLogin;
            this.CreatedBy = user.CreatedBy;
            this.CreatedDate = user.CreatedDate;
        }
    }

    public class ValidateCompanyName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                var field = validationContext.ObjectType.GetProperty("EmpTypeSelected").GetValue(validationContext.ObjectInstance, null);
                if (field.Equals("3") && string.IsNullOrEmpty((string)value))
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
                return ValidationResult.Success;
            }
        }
    }

}