using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserManagementApp.Helpers
{
    public static class ValidationHelper
    {
        public static List<string> Validate(object model)
        {
            var errors = new List<string>();

            // Built-in attribute validation
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            errors.AddRange(results.Select(r => r.ErrorMessage ?? "Unknown error"));

            // Custom password validation if model has a Password property
            var passwordProp = model.GetType().GetProperty("Password");
            if (passwordProp != null)
            {
                var password = passwordProp.GetValue(model) as string ?? "";                

                if (!Regex.IsMatch(password, @"[A-Z]"))
                    errors.Add("Password must contain at least one uppercase letter");

                if (!Regex.IsMatch(password, @"[0-9]"))
                    errors.Add("Password must contain at least one number");

                if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]"))
                    errors.Add("Password must contain at least one special character");
            }

            var emailProp =model.GetType().GetProperty("Email");
            if (emailProp != null)
            {
                var email = emailProp.GetValue(model) as string ?? "";
                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") || !IsValidEmail(email))
                    errors.Add("Please provide valid EmailAddress");                
            }

            return errors;

        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }
    }
}
