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
            XmlDocument replacerFile = new XmlDocument();
            XmlNode replacer;
            int replaceIndex = 0;
            int matIndex = 0;

            // Open file that contains xml for replacement
            try {
                replacerFile.Load("Replacer.xml");
            }
            catch (XmlException exception) {
                Console.WriteLine("Not a valid xml file. ", exception);
                var trash = Console.ReadLine();
            }

            // Holding root node for xml file caontaining replacement
            replacer = replacerFile.FirstChild;

            // Finding childnode index for matscale1x
            foreach (XmlNode n in replacer.ChildNodes) {
                if (n.Attributes["name"].Value == "MatScale1X") {
                    break;
                }
                replaceIndex++;
            }

            // Loop through all input xml files
            foreach (string file in args) {
                // Attempt to open current file
                try {
                    shaderFile.Load(file);
                }
                catch (XmlException exception) {
                    Console.WriteLine("Not a valid xml file. ", exception);
                    var trash = Console.ReadLine();
                    continue;
                }

                // Reset index count for matScales when opening new file
                matIndex = 0;

                // Find the first child node of file
                child = shaderFile.FirstChild.FirstChild;

                // Hold values of all instance of matscale1x in all child nodes
                matScales = shaderFile.SelectNodes("//EMMParameter[@name='MatScale1X']");

                // Go through each child node
                while (child != null) {
                    // Delete all children of current node
                    while (child.HasChildNodes) {
                        child.RemoveChild(child.FirstChild);
                    }

                    // Replace new matscale1x with original
                    replacer.ReplaceChild(matScales[matIndex], replacer.ChildNodes[replaceIndex]);

                    // Copy all values from replacement file
                    foreach (XmlNode n in replacer.ChildNodes) {
                        child.AppendChild(n);
                    }

                    // Proceeding to next node
                    child = child.NextSibling;
                    matIndex++;
                }
                shaderFile.Save(file);
            }
        }
    }
}
