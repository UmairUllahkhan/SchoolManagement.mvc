using Microsoft.AspNetCore.Mvc;
using SchoolManagement.mvc.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.mvc.DataDB;

public class StudentMeta
{
    [Required]
    [Display(Name ="Student Name")]
    public string? StudentName { get; set; }

    [Display(Name = "Student Email Address")]
    [EmailAddress]
    public string? Email { get; set; }

    [Display(Name = "Date Of Birth")]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Cell Phone ")]
    public string MobileNo { get; set; } = null!;

}

[ModelMetadataType(typeof(StudentMeta))]
public partial class StudentTable { }
