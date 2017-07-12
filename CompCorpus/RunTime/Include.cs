using CompCorpus.Analyzer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompCorpus.RunTime.Bricks;

namespace CompCorpus.RunTime
{
    public class Include
    {
        public string fileName { get; }
        public List<Brick> brickList { get; }
        public static string directoryPath { get; set; }

        public Include(string fileName)
        {
            this.fileName = fileName;
            this.brickList = MakeListBrick(fileName);
        }

        private List<Brick> MakeListBrick(string fileName)
        {
            List<Brick> localBrickList = new List<Brick>();
            // We got the include file path 
            string filePath = directoryPath + '\\' + fileName;
            try
            {
                string copiedSourceFiletoInclude = ReadFileWithPath(filePath);

                // We check if the included file is valide
                using (Stream toIncludeStream = new FileStream(filePath, FileMode.Open))
                {
                    Scanner scn = new Scanner(toIncludeStream);
                    Parser parser = new Parser(scn);
                    parser.montage.SetDeclarationSymboleAffectationFromOther(Program.mainMontage);
                    parser.Parse();
                    if (parser.montage == null || parser.montage.errorList.Any() || scn.hasErrors)
                    {
                        LogManager.AddLog("L'inclussion " + fileName + " n'a pas été effectuée car le montage contenait des erreures.");
                    }
                    else
                    {
                        localBrickList.AddRange(parser.montage.listOfBricks);
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.AddLog("In Compile Include function from parse:");
                LogManager.AddLog(e.Message);
            }

            // We return the path of the file to include, which now do not containt any includes
            return localBrickList;
        }

        private static string ReadFileWithPath(string filePath)
        {
            string fileText = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    fileText = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                LogManager.AddLog("In ReadFileWithPath function :");
                LogManager.AddLog(e.Message);
            }
            return fileText;
        }

    }
}
