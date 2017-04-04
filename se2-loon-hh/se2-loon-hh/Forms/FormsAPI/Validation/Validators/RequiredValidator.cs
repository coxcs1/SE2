using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace se2_loon_hh.Forms.FormsAPI.Validation.Validators
{
    class RequiredValidator : Validation
    {
        /// <summary>
        /// The value to be evaulated.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RequiredValidator()
        {
            Value = "";
        }
        /// <summary>
        /// Constructor that accepts the value to be evaluated.
        /// </summary>
        /// <param name="value"></param>
        public RequiredValidator(string value, Control formElement)
        {
            Value = value;
            FormElement = formElement;
        }
        /// <summary>
        /// Evaluates whether or not the value is empty or null.
        /// </summary>
        /// <returns>Whether or not the value is empty or null.</returns>
        public override bool isValid()
        {
            return (!string.IsNullOrEmpty(Value)) ? true : false;
        }
    }
}
