using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace ShipCrudApp.Helpers
{
    public class ViewModelBase : NotifyPropertyChanged, INotifyDataErrorInfo
	{
        private Dictionary<string, List<string>> errorsList;
        private Dictionary<String, List<ValidationRule>> validationRules;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public Dictionary<string, List<ValidationRule>> ValidationRules 
        { 
            get => validationRules; 
            set => validationRules = value; 
        }

        public ViewModelBase()
        {
            errorsList = new Dictionary<string, List<string>>();
            validationRules = new Dictionary<string, List<ValidationRule>>();
        }

        public bool HasErrors => errorsList.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            return errorsList.ContainsKey(propertyName) ? errorsList[propertyName] : null;
        }

        public virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }

        public void AddError(string propertyName, string error)
        {
            if (!errorsList.ContainsKey(propertyName))
            {
                errorsList[propertyName] = new List<string>();
            }

            if (!errorsList[propertyName].Contains(error))
            {
                errorsList[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        public void ClearErrors(string propertyName)
        {
            if (errorsList.ContainsKey(propertyName))
            {
                errorsList.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        public void ClearAllErrors()
        {
            var allErrorKeys = errorsList.Keys.ToList();
            foreach (var key in allErrorKeys)
            {
                this.ClearErrors(key);
            }
        }

        public bool ValidateProperty<TValue>(TValue propertyValue, [CallerMemberName] string propertyName = null)
        {
            // Clear previous errors of the current property to be validated 
            this.errorsList.Remove(propertyName);
            OnErrorsChanged(propertyName);

            if (this.ValidationRules.TryGetValue(propertyName, out List<ValidationRule> propertyValidationRules))
            {
                // Apply all the rules that are associated with the current property 
                // and validate the property's value
                propertyValidationRules
                  .Select(validationRule => validationRule.Validate(propertyValue, CultureInfo.CurrentCulture))
                  .Where(result => !result.IsValid)
                  .ToList()
                  .ForEach(invalidResult => AddError(propertyName, invalidResult.ErrorContent as string));

                return !propertyHasErrors(propertyName);
            }

            return true;
        }

        private bool propertyHasErrors(string propertyName) => 
            this.errorsList.TryGetValue(propertyName, out List<string> propertyErrors) && propertyErrors.Any();
    }
}