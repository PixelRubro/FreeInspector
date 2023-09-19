using System;

namespace PixelRouge.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class IconAttribute : BaseAttribute
    {
        /// <summary>
        /// Full path of the .png or .tiff file starting from the "Assets" folder.
        /// </summary>
        public string IconPath = null;

        /// <summary>
        /// Show a personalized icon in the field's label.
        /// </summary>
        /// <param name="path">Full path of the .png or .tiff file starting from the "Assets" folder.</param>
        public IconAttribute(string path)
        {
            IconPath = path;
        }
    }
}
