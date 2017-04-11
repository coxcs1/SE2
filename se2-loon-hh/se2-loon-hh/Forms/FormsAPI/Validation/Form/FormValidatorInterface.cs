using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace se2_loon_hh.Forms.FormsAPI.Validation.Form
{
    interface FormValidatorInterface
    {
        void setupRules();
        bool isValid();
    }
}