// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturnResult.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework
{
    #region

    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Commons.Framework.Extensions;

    #endregion

    /// <summary>
    /// The error item.
    /// </summary>
    public class ErrorItem
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// The return result.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ReturnResult<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{T}" /> class.
        /// </summary>
        public ReturnResult()
        {
            this.Value = default(T);
            this.Errors = new List<ErrorItem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnResult{T}"/> class.
        /// </summary>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        public ReturnResult(T defaultValue)
        {
            this.Value = defaultValue;
            this.Errors = new List<ErrorItem>();
        }

        /// <summary>
        ///     Gets or sets the model state.
        /// </summary>
        public List<ErrorItem> Errors { get; set; }

        /// <summary>
        /// The is valid.
        /// </summary>
        public bool IsValid => !this.Errors.Any();

        /// <summary>
        ///     Gets or sets the return value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// The add error item.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void AddErrorItem(string name, string value)
        {
            this.Errors.Add(new ErrorItem { Name = name, Value = value });
        }
    }

    /// <summary>
    /// The return result.
    /// </summary>
    public class ReturnResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnResult"/> class. 
        ///     Initializes a new instance of the <see cref="ReturnResult{T}"/> class.
        /// </summary>
        public ReturnResult()
        {
            this.Errors = new List<ErrorItem>();
        }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public List<ErrorItem> Errors { get; set; }

        /// <summary>
        /// The is valid.
        /// </summary>
        public bool IsValid => !this.Errors.Any();

        /// <summary>
        /// The add error item.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void AddErrorItem(string name, string value)
        {
            this.Errors.Add(new ErrorItem { Name = name, Value = value });
        }
    }

    /// <summary>
    /// The model state dictionary extentsions.
    /// </summary>
    public static class ModelStateDictionaryExtentsions
    {

        public static List<string> ToStringList(this List<ErrorItem> errors)
        {
            return errors?.Select(e => e.Value).ToList() ?? new List<string>();
        }

        public static string ToCommaSeparatedString(this List<ErrorItem> errors)
        {
            return errors.ToStringList().ToCommaSeparatedString();
        }

        /// <summary>
        /// The get errors.
        /// </summary>
        /// <param name="modelState">
        /// The model state.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var errors = new List<string>();

            foreach (var state in modelState)
            {
                errors.AddRange(state.Value.Errors.Select(error => error.ErrorMessage));
            }

            return errors;
        }

        /// <summary>
        /// The get errors.
        /// </summary>
        /// <param name="modelState">
        /// The model state.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<string> GetErrors(this System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            var errors = new List<string>();

            foreach (var state in modelState)
            {
                errors.AddRange(state.Value.Errors.Select(error => error.ErrorMessage));
            }

            return errors;
        }

        /// <summary>
        /// The get errors.
        /// </summary>
        /// <param name="modelState">
        /// The model state.
        /// </param>
        /// <param name="separator">
        /// The separator.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetErrors(this ModelStateDictionary modelState, string separator)
        {
            return string.Join(separator, modelState.GetErrors());
        }

        /// <summary>
        /// The get errors.
        /// </summary>
        /// <param name="modelState">
        /// The model state.
        /// </param>
        /// <param name="separator">
        /// The separator.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetErrors(
            this System.Web.Http.ModelBinding.ModelStateDictionary modelState,
            string separator)
        {
            return string.Join(separator, modelState.GetErrors());
        }

        /// <summary>
        /// The merge.
        /// </summary>
        /// <param name="modelState">
        /// The model state.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        public static void Merge(this ModelStateDictionary modelState, ReturnResult result)
        {
            if (result.IsValid)
            {
                return;
            }

            if (result.Errors != null && result.Errors.Any())
            {
                foreach (var item in result.Errors)
                {
                    modelState.AddModelError(item.Name, item.Value);
                }
            }
        }

        /// <summary>
        /// The merge.
        /// </summary>
        /// <param name="modelState">
        /// The model state.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void Merge<T>(this ModelStateDictionary modelState, ReturnResult<T> result)
        {
            if (result.IsValid)
            {
                return;
            }

            if (result.Errors != null && result.Errors.Any())
            {
                foreach (var item in result.Errors)
                {
                    modelState.AddModelError(item.Name, item.Value);
                }
            }
        }

        /// <summary>
        /// The merge.
        /// </summary>
        /// <param name="modelState">
        /// The model state.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void Merge<T>(
            this System.Web.Http.ModelBinding.ModelStateDictionary modelState,
            ReturnResult<T> result)
        {
            if (result.IsValid)
            {
                return;
            }

            if (result.Errors != null && result.Errors.Any())
            {
                foreach (var item in result.Errors)
                {
                    modelState.AddModelError(item.Name, item.Value);
                }
            }
        }

        /// <summary>
        /// The merge.
        /// </summary>
        /// <param name="modelState">
        /// The model state.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        public static void Merge(this System.Web.Http.ModelBinding.ModelStateDictionary modelState, ReturnResult result)
        {
            if (result.IsValid)
            {
                return;
            }

            if (result.Errors != null && result.Errors.Any())
            {
                foreach (var item in result.Errors)
                {
                    modelState.AddModelError(item.Name, item.Value);
                }
            }
        }

        public static void Merge<T,T2>(this ReturnResult<T> result1, ReturnResult<T2> result2)
        {
            if (result2.IsValid)
            {
                return;
            }

            if (result2.Errors != null && result2.Errors.Any())
            {
                foreach (var item in result2.Errors)
                {
                    result1.AddErrorItem(item.Name, item.Value);
                }
            }
        }

        public static void Merge<T>(this ReturnResult<T> result1, ReturnResult result2)
        {
            if (result2.IsValid)
            {
                return;
            }

            if (result2.Errors != null && result2.Errors.Any())
            {
                foreach (var item in result2.Errors)
                {
                    result1.AddErrorItem(item.Name, item.Value);
                }
            }
        }


        public static void Merge<T>(this ReturnResult result1, ReturnResult<T> result2)
        {
            if (result2.IsValid)
            {
                return;
            }

            if (result2.Errors != null && result2.Errors.Any())
            {
                foreach (var item in result2.Errors)
                {
                    result1.AddErrorItem(item.Name, item.Value);
                }
            }
        }

        public static void Merge(this ReturnResult result1, ReturnResult result2)
        {
            if (result2.IsValid)
            {
                return;
            }

            if (result2.Errors != null && result2.Errors.Any())
            {
                foreach (var item in result2.Errors)
                {
                    result1.AddErrorItem(item.Name, item.Value);
                }
            }
        }
    }
}