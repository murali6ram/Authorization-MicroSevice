using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models;


public class LoginViewModel : IValidatableObject
{
    public string PhoneNumber { get; set; }

    public string UserEmail { get; set; }

    [Required(ErrorMessage = "OTP is required")]
    public int Otp { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(PhoneNumber) && string.IsNullOrWhiteSpace(UserEmail))
        {
            yield return new ValidationResult(
                "Either Phonenumber  or Email is required.",
                new[] { nameof(PhoneNumber), nameof(UserEmail) }
            );
        }

        if (!string.IsNullOrWhiteSpace(UserEmail) &&
            !new EmailAddressAttribute().IsValid(UserEmail))
        {
            yield return new ValidationResult("Invalid Email format.", new[] { nameof(UserEmail) });
        }
    }
}

