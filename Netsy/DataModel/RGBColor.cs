//-----------------------------------------------------------------------
// <copyright file="RgbColor.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//-----------------------------------------------------------------------

namespace Netsy.DataModel
{
    using System.Globalization;

    /// <summary>
    /// A color value as RGB (red, green and blue)
    /// </summary>
    public class RgbColor : EtsyColor
    {
        /// <summary>
        /// Initializes a new instance of the RgbColor class from numeric values
        /// </summary>
        /// <param name="red">the red value of the color</param>
        /// <param name="green">the green value of the color</param>
        /// <param name="blue">the blue value of the color></param>
        public RgbColor(int red, int green, int blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        /// <summary>
        /// Initializes a new instance of the RgbColor class from a string value
        /// </summary>
        /// <param name="value">contains the full color value</param>
        public RgbColor(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                // no color
                this.Red = 0;
                this.Green = 0;
                this.Blue = 0;
            }
            else
            {
                // RGB color value is six hex digits
                string redString = value.Substring(0, 2);
                string greenString = value.Substring(2, 2);
                string blueString = value.Substring(4, 2);

                this.Red = int.Parse(redString, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                this.Green = int.Parse(greenString, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                this.Blue = int.Parse(blueString, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the red value of the colour
        /// </summary>
        public int Red { get; private set; }

        /// <summary>
        /// Gets the Green value of the colour
        /// </summary>
        public int Green { get; private set; }

        /// <summary>
        /// Gets the blue value of the colour
        /// </summary>
        public int Blue { get; private set; }

        /// <summary>
        /// Display for debug or for sending to server
        /// </summary>
        /// <returns>the RGB color as a string</returns>
        public override string ToString()
        {
            return 
                this.Red.ToString("X", CultureInfo.InvariantCulture) +
                this.Green.ToString("X", CultureInfo.InvariantCulture) +
                this.Blue.ToString("X", CultureInfo.InvariantCulture);
        }
    }
}
