﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace se2_loon_hh.Forms.FormsAPI.Validation.Validators
{
    class RegexValidator : Validation
    {
        /// <summary>
        /// The value to be evaluated.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// The regular expression to used to validate the
        /// value.
        /// </summary>
        public string Regex { get; set; }
        /// <summary>
        /// The element binded to the validator.
        /// </summary>
        public Control FormElement { get; set; }
        /// <summary>
        /// The error message for the form element.
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RegexValidator()
        {
            Value = "";
            Regex = "";
        }
        /// <summary>
        /// Constructor that accepts the value and regex
        /// for evaluation.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regex"></param>
        public RegexValidator(string value, string regex)
        {
            Value = value;
            Regex = regex;
        }
        /// <summary>
        /// Evaluates value against regular expression.
        /// </summary>
        /// <returns>Whether or not the value meets the regular expression.</returns>
        public override bool isValid()
        {
            return (System.Text.RegularExpressions.Regex.IsMatch(Value, Regex)) ? true : false;
        }
    }
}