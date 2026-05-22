using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Fonts;
    using System.IO;

namespace MrLabandero
{
    public class WindowsFontResolver : IFontResolver
    {
        private static readonly string FontFolder =
            Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

        public byte[] GetFont(string faceName)
        { 
            string fileName;
            switch (faceName)
            {
                case "Helvetica#Bold":
                    fileName = "arialbd.ttf";
                    break;
                case "Helvetica#Italic":
                    fileName = "ariali.ttf";
                    break;
                case "Helvetica#BoldItalic":
                    fileName = "arialbi.ttf";
                    break;
                default:
                    fileName = "arial.ttf";
                    break;
            }

            string fullPath = Path.Combine(FontFolder, fileName);

            if (File.Exists(fullPath))
                return File.ReadAllBytes(fullPath);

            throw new FileNotFoundException($"Font not found: {fullPath}");
        }

        public FontResolverInfo ResolveTypeface(
            string familyName, bool isBold, bool isItalic)
        {
            string faceName = familyName;

            if (isBold && isItalic) faceName += "#BoldItalic";
            else if (isBold) faceName += "#Bold";
            else if (isItalic) faceName += "#Italic";

            return new FontResolverInfo(faceName);
        }
    }
}
