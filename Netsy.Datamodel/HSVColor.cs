//-----------------------------------------------------------------------
// <copyright file="HsvColor.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//-----------------------------------------------------------------------
namespace Netsy.DataModel
{
    /// <summary>
    /// A color value as HSV (hue, saturation and value)
    /// </summary>
    public class HsvColor
    {
        /// <summary>
        /// Initializes a new instance of the HsvColor class from numeric values
        /// </summary>
        /// <param name="hue">the hue of the color</param>
        /// <param name="saturation">the saturation of the color</param>
        /// <param name="value">the value of the color></param>
        public HsvColor(int hue, int saturation, int value)
        {
            this.Hue = hue;
            this.Saturation = saturation;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the HsvColor class from astring value
        /// </summary>
        /// <param name="value">contains the full color value</param>
        public HsvColor(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                // no color
                this.Hue = 0;
                this.Saturation = 0;
                this.Value = 0;
            }
            else
            {
                // HSV color values seperated by semicolons
                string[] values = value.Split(';');

                this.Hue = int.Parse(values[0]);
                this.Saturation = int.Parse(values[1]);
                this.Value = int.Parse(values[2]);
            }
        }

        /// <summary>
        /// Gets the hue of the colour
        /// </summary>
        public int Hue { get;  private set; }
        
        /// <summary>
        /// Gets the Saturation  of the colour
        /// </summary>
        public int Saturation { get; private set; }

        /// <summary>
        /// Gets the Value of the colour
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Display for debug or for sending to server
        /// </summary>
        /// <returns>the HSV color as a string</returns>
        public override string ToString()
        {
            return this.Hue + ";" + this.Saturation + ";" + this.Value;
        }
    }
}
