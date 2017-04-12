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

            int garbage;

            // Open file that contains xml for replacement
            try {
                replacerFile.Load("Replacer.xml");
            }
            catch (XmlException exception) {
                Console.WriteLine("Not a valid xml file. ", exception);
                var trash = Console.ReadLine();
            }

            // Holding root node for xml file caontaining replacement
            replacer = replacerFile.DocumentElement;

            // Finding childnode index for matscale1x
            foreach (XmlNode n in replacer.ChildNodes) {
                if (n.Attributes["name"].Value == "MatScale1X")
                {
                    break;
                }
                replaceIndex++;
            }

            // Loop through all input xml files
            foreach (string file in args)
            {
                // Attempt to open current file
                try
                {
                    shaderFile.Load(file);
                }
                catch (XmlException exception)
                {
                    Console.WriteLine("Not a valid xml file. ", exception);
                    var trash = Console.ReadLine();
                    continue;
                }

                // Reset index count for matScales when opening new file
                matIndex = 0;

                // Find the first child node of file
                child = shaderFile.DocumentElement.FirstChild;

                // Hold values of all instance of matscale1x in all child nodes
                matScales = shaderFile.SelectNodes("//EMMParameter[@name='MatScale1X']");

                // DO NOT DELETE!!! FOR SOME REASON WILL BREAK PROGRAM!!!
                garbage = matScales.Count;

                // Go through each child node
                while (child != null)
                {
                    // Replace new matscale1x with original
                    replacer.ReplaceChild(replacer.OwnerDocument.ImportNode(matScales[matIndex], true),
                        replacer.ChildNodes[replaceIndex]);

                    // Delete all children of current node
                    while (child.HasChildNodes)
                    {
                        child.RemoveChild(child.FirstChild);
                    }

                    // Copy all values from replacement file
                    foreach (XmlNode n in replacer.ChildNodes) {
                        // "Kill me 2: Electric Boogaloo" - also this line probably
                        child.AppendChild(child.OwnerDocument.ImportNode(n, false));
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
