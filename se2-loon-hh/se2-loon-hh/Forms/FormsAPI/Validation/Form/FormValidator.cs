using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace se2_loon_hh.Forms.FormsAPI.Validation.Form
{
    class FormValidator
    {
        public Dictionary<string, Validation> Validators { get; set; }
        public Dictionary<string, string> ErrorMessages { get; set; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public FormValidator()
        {
            Validators = new Dictionary<string, Validation>();
            ErrorMessages = new Dictionary<string, string>();
            ErrorMessages.Add("ServiceName", "Test Error Message");
        }

        /// <summary>
        /// Pass in a list of validation interfaces
        /// and add them to the list of validators.
        /// </summary>
        /// <param name="rules"></param>
        public FormValidator(List<Validation> rules)
        {
            Validators = new Dictionary<string, Validation>();
            ErrorMessages = new Dictionary<string, string>();
            foreach (var rule in rules)
            {
                Validators.Add(rule.FormElement.Name, rule);
            }
        }
        /// <summary>
        /// Handle validation logic for the form.
        /// </summary>
        /// <returns></returns>
        public bool isValid()
        {
            foreach (var rule in Validators)
            {
                if (!rule.Value.isValid())
                {
                    ErrorMessages.Add(rule.Value.FormElement.Name, rule.Value.ErrorMessage);
                }
            }
            return ErrorMessages.Count == 0;
        }
    }
}
