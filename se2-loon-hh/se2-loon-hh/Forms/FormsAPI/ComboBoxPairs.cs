using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace se2_loon_hh.Forms.FormsAPI
{
    /// <summary>
    /// This class stores a key/value for a combobox item.
    /// @author Jarred Light <lightdj@etsu.edu>
    /// </summary>
    class ComboBoxPairs
    {
        public string _Key { get; set; }
        public string _Value { get; set; }

        public ComboBoxPairs(string _key, string _value)
        {
            _Key = _key;
            _Value = _value;
        }
    }
}
