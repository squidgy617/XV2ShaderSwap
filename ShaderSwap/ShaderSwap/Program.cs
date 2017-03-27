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
            // Loop through all input xml files
            foreach (string file in args) {
                // Attempt to open current file
                XDocument shaderFile = new XDocument();
                try {
                    shaderFile = XDocument.Load(file);
                }
                catch (XmlException exception) {
                    Console.WriteLine("Not a valid xml file. ", exception);
                }

                // Find the first child node of file
                XNode child = shaderFile.Root.FirstNode;
                Console.WriteLine("Made it to here!");
                // Go through each child node
                //while (child != null) {

                //}
            }
        }
    }
}
