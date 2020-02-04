using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CompanyBroker.Addons.CustomElements
{
    /// <summary>
    /// A custom button element class
    /// Contains same functionality as Button, but with ekstra customs
    /// </summary>
    public class Custom_button : Button
    {
        /// <summary>
        /// Constructor
        /// base() makes a new instance of the button class
        /// </summary>
        public Custom_button() : base()
        {

        }

        /// <summary>
        /// The new element of string, which is public to be binded
        /// </summary>
        public string CustomButtonNameTag
        {
            get => (string)GetValue(CustomButtonNameTag_DP);
            set => SetValue(CustomButtonNameTag_DP, value); 
        }

        /// <summary>
        /// DependencyProperty 
        /// </summary>
        private DependencyProperty CustomButtonNameTag_DP = DependencyProperty.Register("CustomButtonNameTag", typeof(string), typeof(Custom_button), new UIPropertyMetadata(null));

    }
}
