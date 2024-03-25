using System.ComponentModel.DataAnnotations;

namespace snglrtycrvtureofspce.User.Handlers.UsersController.RegisterUser;

public class RegisterUserRequest
{
    [Required] 
    [Display(Name = "UserName")] 
    public string UserName { get; set; }
    
    [Required] 
    [Display(Name = "Email")] 
    public string Email { get; set; }
    
    [Required]
    [Display(Name = "FirstName")]
    public string FirstName { get; set; }
    
    [Required]
    [Display(Name = "LastName")]
    public string LastName { get; set; }
    
    [Display(Name = "MiddleName")]
    public string? MiddleName { get; set; }
    
    [Display(Name = "DateOfBirth")]
    public DateTime? DateOfBirth { get; set; }
    
    [Display(Name = "Country")]
    public string? Country { get; set; }
    
    [Display(Name = "City")]
    public string? City { get; set; }
    
    [Required]
    [Display(Name = "Agreement")]
    public bool Agreement { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "The passwords don't match")]
    [DataType(DataType.Password)]
    [Display(Name = "PasswordConfirm")]
    public string PasswordConfirm { get; set; }
}