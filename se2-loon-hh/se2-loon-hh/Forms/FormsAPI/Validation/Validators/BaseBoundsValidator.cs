using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace se2_loon_hh.Forms.FormsAPI.Validation.Validators
{
    class BaseBoundsValidator : Validation
    {
        /// <summary>
        /// The bottom number.
        /// </summary>
        public int Base { get; set; }
        /// <summary>
        /// The top number.
        /// </summary>
        public int Bounds { get; set; }
        /// <summary>
        /// The value to evaluate.
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Whether or not evaulate as inclusive or exclusive.
        /// </summary>
        public bool Inclusive { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseBoundsValidator()
        {
            Base = 0;
            Bounds = 0;
            Value = 0;
            Inclusive = false;
        }
        /// <summary>
        /// Constructor that accepts the value, base, bounds, and
        /// inclusive as false by default.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bottom"></param>
        /// <param name="top"></param>
        /// <param name="inclusive"></param>
        public BaseBoundsValidator(int value, int bottom, int top, bool inclusive = false)
        {
            Value = value;
            Base = bottom;
            Bounds = top;
            Inclusive = inclusive;
        }
        /// <summary>
        /// Evaluates the value and checks whether
        /// it's within the range. Exclusive or Inclusive.
        /// </summary>
        /// <returns>Whether or not the value is within the given range.</returns>
        public override bool isValid()
        {
            bool valid = false;//valid bool
            if (Inclusive)//if inclusive then include base & bounds in evaulation
            {
                valid = (Value >= Base && Value <= Bounds) ? true : false;
            }
            else//do not include the base and bound in evaluation
            {
                valid = (Value > Base && Value < Bounds) ? true : false;

            }
            return valid;
        }
    }
}
