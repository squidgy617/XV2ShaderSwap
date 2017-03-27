using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ShaderSwap
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variables
            XmlDocument shaderFile = new XmlDocument();
            XmlNode child;
            XmlNodeList matScales;
            int count = 0;

            // Loop through all input xml files
            foreach (string file in args) {
                // Attempt to open current file
                try {
                    shaderFile.Load(file);
                }
                catch (XmlException exception) {
                    Console.WriteLine("Not a valid xml file. ", exception);
                    var trash = Console.ReadLine();
                }

                // Reset index count for matScales when opening new file
                count = 0;

                // Find the first child node of file
                child = shaderFile.FirstChild.FirstChild;

                // Hold values of all instance of matscale1x in all child nodes
                matScales = shaderFile.SelectNodes("//EMMParameter[@name='MatScale1X']");

                // Go through each child node
                while (child != null) {
                    // Delete all children of current node

                    // Copy all values from replacement file

                    // Replace new matscale1x with original

                    // Proceeding to next node
                    child = child.NextSibling;
                }
            }
        }
    }
}
