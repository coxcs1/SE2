﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace se2_loon_hh.Forms.FormsAPI.Validation.Validators
{
    class LengthValidator : Validation
    {
        /// <summary>
        /// The string to be evaluated.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// The length desired.
        /// </summary>
        public int Length { get; set; }
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
        public LengthValidator()
        {
            Length = 0;
            Value = "";
        }
        /// <summary>
        /// Constructor that accepts a length and the string
        /// </summary>
        /// <param name="length"></param>
        /// <param name="value"></param>
        public LengthValidator(int length, string value)
        {
            Length = length;
            Value = value;
        }
        /// <summary>
        /// Checks whether or not the length of the given value is
        /// the same length as desired.
        /// </summary>
        /// <returns>Whether or not the length is the same.</returns>
        public override bool isValid()
        {
            return (Value.Length == Length) ? true : false;
        }
    }
}