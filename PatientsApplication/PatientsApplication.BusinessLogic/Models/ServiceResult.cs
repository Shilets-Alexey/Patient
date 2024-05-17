using System;
using System.ComponentModel.DataAnnotations;

namespace PatientsApplication.BusinessLogic.Models
{
    /// <summary>
    /// Service result
    /// </summary>
    public class ServiceResult<TKey>
    {
        /// <summary>
        /// Result
        /// </summary>
        public TKey Result { get; private set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Validation Result
        /// </summary>
        public ValidationResult ValidationResult { get; private set; }

        private ServiceResult() { }

        public static ServiceResult<TKey> Create() => new ServiceResult<TKey>() { Result = Activator.CreateInstance<TKey>(), ValidationResult = new ValidationResult(), Success = true };

        public static ServiceResult<TKey> Create(TKey data) => new ServiceResult<TKey>() { Result = data, ValidationResult = new ValidationResult(), Success = true };

        public static ServiceResult<TKey> Create(ValidationResult validationResult) => new ServiceResult<TKey>() { ValidationResult = validationResult, Result = Activator.CreateInstance<TKey>(), Success = true };

        public static ServiceResult<TKey> Create(TKey data, ValidationResult validationResult) => new ServiceResult<TKey>() { Result = data, ValidationResult = validationResult, Success = true };

        public static ServiceResult<TKey> Error(Exception ex) => new ServiceResult<TKey>() { Result = Activator.CreateInstance<TKey>(), ValidationResult = new ValidationResult(), Success = false, ErrorMessage = ex.Message };

        public static ServiceResult<TKey> Error(string errorMessage) => new ServiceResult<TKey>() { Result = Activator.CreateInstance<TKey>(), ValidationResult = new ValidationResult(), Success = false, ErrorMessage = errorMessage };

        public static ServiceResult<TKey> Error(TKey data, Exception ex) => new ServiceResult<TKey>() { Result = data, ValidationResult = new ValidationResult(), Success = false, ErrorMessage = ex.Message };

        public static ServiceResult<TKey> Error(TKey data, string errorMessage) => new ServiceResult<TKey>() { Result = data, ValidationResult = new ValidationResult(), Success = false, ErrorMessage = errorMessage };

        public static ServiceResult<TKey> Error(ValidationResult validationResult, Exception ex) => new ServiceResult<TKey>() { ValidationResult = validationResult, Success = false, ErrorMessage = ex.Message };

        public static ServiceResult<TKey> Error(ValidationResult validationResult, string errorMessage) => new ServiceResult<TKey>() { ValidationResult = validationResult, Success = false, ErrorMessage = errorMessage };

        public static ServiceResult<TKey> Error(TKey data, ValidationResult validationResult, Exception ex) => new ServiceResult<TKey>() { Result = data, ValidationResult = validationResult, Success = false, ErrorMessage = ex.Message };

        public static ServiceResult<TKey> Error(TKey data, ValidationResult validationResult, string errorMessage) => new ServiceResult<TKey>() { Result = data, ValidationResult = validationResult, Success = false, ErrorMessage = errorMessage };
    }
}
