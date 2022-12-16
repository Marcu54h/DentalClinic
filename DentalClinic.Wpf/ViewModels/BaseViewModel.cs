// -----------------------------------------------------------------------------
//  <copyright file="BaseViewModel.cs" company="Adam Marzec">
//      Copyright (c) Adam Marzec, All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------
namespace DentalClinic.Wpf
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract class BaseViewModel : ObservableObject, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get { return OnValidate(columnName); }
        }
     
        [Obsolete]
        public string Error => throw new NotSupportedException();

        protected virtual string OnValidate(string propertyName)
        {
            ValidationContext validationContext = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            Collection<ValidationResult> validationResults = new Collection<ValidationResult>();

            bool isValid = Validator.TryValidateObject(this, validationContext, validationResults, true);

            if (!isValid)
            {
                ValidationResult validationResult = validationResults.SingleOrDefault(p =>
                                                                                      p.MemberNames.Any(memberName =>
                                                                                                        memberName == propertyName));
                return validationResult == null ? null : validationResult.ErrorMessage;
            }

            return null;
        }
    }
}
