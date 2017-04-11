using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace se2_loon_hh.Forms.FormsAPI.Validation
{
    /// <summary>
    /// This interface requires that a form element, error message, and validation logic
    /// be implemented for each validator created.
    /// </summary>
    abstract class Validation
    {
        public Control FormElement { get; set; }
        public string ErrorMessage { get; set; }
        abstract public bool isValid();
    }
}