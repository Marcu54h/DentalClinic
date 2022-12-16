namespace DentalClinic.Windows
{

    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Base class for View-Model in MVVM pattern.
    /// </summary>
    public class BaseViewModel : ObservableObject, IDataErrorInfo
    {
        #region Fields



        #endregion // Fields

        #region Properties

        public string this[string columnName]
        {
            get { return OnValidate(columnName); }
        }

        [Obsolete]
        public string Error
        { get { throw new NotSupportedException(); } }

        #endregion // Properties

        #region Constructors



        #endregion // Constructors

        #region Methods

        protected virtual string OnValidate(string propertyName)
        {
            var context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            var results = new Collection<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                ValidationResult result = results.SingleOrDefault(p =>
                                                                  p.MemberNames.Any(memberName =>
                                                                                    memberName == propertyName));
                return result == null ? null : result.ErrorMessage;
            }
            return null;
        }

        #endregion // Methods
    }
}
