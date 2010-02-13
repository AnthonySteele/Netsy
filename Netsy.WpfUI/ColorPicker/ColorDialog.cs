//-----------------------------------------------------------------------
// <copyright file="ColorDialog.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.WpfUI.ColorPicker
{
    using DataModel;

    /// <summary>
    /// Wrapper for the windows froms color dialog.
    /// </summary>
    public static class ColorDialog
    {
        /// <summary>
        /// Open the widows forms color dialog
        /// </summary>
        /// <returns>the color in a string</returns>
        public static string ShowColorDialog()
        {
            using (System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog())
            {
                System.Windows.Forms.DialogResult dialogResult = colorDialog.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    return ConvertColor(colorDialog.Color);
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Convert a color to a string
        /// </summary>
        /// <param name="color">the windows forms color</param>
        /// <returns>the color in a string</returns>
        private static string ConvertColor(System.Drawing.Color color)
        {
            int i = color.ToArgb();
            byte r = (byte)((i >> 16) & 255);
            byte g = (byte)((i >> 8) & 255);
            byte b = (byte)(i & 255);

            RgbColor rgbColor = new RgbColor(r, g, b);
            return rgbColor.ToString();
        }
    }
}
