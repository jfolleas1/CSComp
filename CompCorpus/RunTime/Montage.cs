﻿using System;
using System.Collections.Generic;
using System.IO;
using CompCorpus.RunTime.error;
using CompCorpus.RunTime.declaration;

namespace CompCorpus.RunTime
{
    public class Montage
    {
        public Dictionary<string, string> symboleTabe { get; set; }
        public string nameOfTheMontage { get; set; }
        public List<Affectation> listOfCalculExpressions { get; set; }
        public List<Declaration> listOfDeclarations { get; set; }
        public List<Error> errorList { get; }


        public Montage()
        {
            listOfCalculExpressions = new List<Affectation>();
            listOfDeclarations = new List<Declaration>();
            nameOfTheMontage = "";
            symboleTabe = new Dictionary<string, string>();
            errorList = new List<Error>();

        }


        public void AddSymboleFromFile(string filename)
        {
            Console.WriteLine("AddSymboleFromFile");
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filename))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        String[] substrings = line.Split(' ');
                        if (substrings.Length == 2)
                        {
                            symboleTabe.Add(substrings[0], substrings[1]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void AddSymbole(string varName, string typeString)
        {
            if (symboleTabe.ContainsKey(varName))
            {
                symboleTabe.Remove(varName);
            }
            symboleTabe.Add(varName, typeString);
        }

        public void AddSymbole(Affectation aff)
        {
           AddSymbole(aff.variableName.name, ("L"+aff.expression.dataType.ToString()));
        }

        public void AddSymbole(Declaration dec )
        {
            AddSymbole(dec.name, dec.type.ToString());
        }

        public void AddSymbole(List< Tuple <string,string>> ls)
        {
            foreach (Tuple<string, string> tp in ls)
            {
                AddSymbole(tp.Item1,tp.Item2);
            }
        }

        public bool IsValideTypeString(string typeString, int line, int column)
        {
            ExpressionType type;
            if (!Enum.TryParse(typeString, out type) || (type == ExpressionType.INVALIDE))
            {
                errorList.Add(new Error(ErrorType.INVALIDE_TYPE, typeString, line, column));
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsInSymboleTable(string symbole, int line, int column)
        {
            if (!symboleTabe.ContainsKey(symbole))
            {
                errorList.Add(new Error(ErrorType.UNKNOW_VARIABLE, symbole, line, column));
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CheckAffectationIsValid(ExpressionType type, string symbole, int line)
        {
            if (type == ExpressionType.INVALIDE)
            {
                errorList.Add(new Error(ErrorType.INVALIDE_OPERATION, symbole, line, 0));
            }
        }

        public void PrintErrors()
        {
            foreach (Error err in errorList)
            {
                Console.WriteLine(err.GetMessage());
            }
        }

        public string GetVarTypeString(string varName)
        {
            string value = "";
            if (!symboleTabe.TryGetValue(varName, out value))
                value = "NULL";
            return value;
        }

        

        public static string GetLocalFileName(string fileName)
        {
            String[] substrings = fileName.Split('\\');
            return substrings[substrings.Length-1];
        }

        public void Print()
        {
            Console.WriteLine(@"// Liste des définition des expressions");
            foreach (Affectation aff in listOfCalculExpressions)
            {
                aff.Print();
            }
        }

        public void PrintFutureFiles()
        {
            Console.WriteLine("-----CODE IN THE HTML DOCUMENT------");
            string beginOfTheHtmlDoc = "<!DOCTYPE html> \n<html lang=\"fr - FR\"> \n" +
                "< script src = \"https://ajax.googleapis.com/ajax/libs/angularjs/1.4.8/angular.min.js\" ></ script >\n" +
                "< script src = \"http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js\" ></ script >\n" +
                "< script src = \"https://code.jquery.com/ui/1.12.1/jquery-ui.js\" ></ script >\n" +
                "< head >\n" +
                "\t< meta charset = \"utf-8\" />\n" +
                "\t< title > " + nameOfTheMontage + "</title>\n" +
                "\t< link rel = \"stylesheet\" type = \"text/css\" href = \"styleActe.css\" >\n" +
                "\t< link rel = \"stylesheet\" href = \"//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css\" >\n" +
                "</head>\n\n<body>\n" +
                "<div ng-app=\"myActe\" ng-controller=\"myCtrl\">\n";
            Console.Write(beginOfTheHtmlDoc);

            // what will be written in the html document

            string endOfTheDoc = "\n</div>\n\n<script src=\"targetFileNameController.js\"></script>\n</body>\n</html>\n\n";
            Console.Write(endOfTheDoc);

            Console.WriteLine("-----CODE IN THE JS DOCUMENT------");
            string beginOfTheJSDoc = "var app = angular.module('myActe', []);\n"+
                                     "app.controller('myCtrl', function($scope) { \n\n";
            Console.WriteLine(beginOfTheJSDoc);


            // Variables declarations list


            Console.WriteLine(@"// Liste des définition des expressions");

            foreach (Affectation aff in listOfCalculExpressions)
            {
                Console.Write(aff.Write());
            }


            string endOfTheJSDoc = "});\n\n" +
               "var listeInput = document.getElementsByTagName(\"input\");" +
               "for (var iter = 0; iter < listeInput.length; iter++) {" +
               "\tif (listeInput[iter].type == \"text\") {" +
               "\t\tlisteInput[iter].style.width = ((listeInput[iter].value.length + 1) * 6) + 'px';" +
               "\t}\n}\n\n";
            Console.WriteLine(endOfTheJSDoc);



        }


        public string DataFromFile(string filename)
        {
            Console.WriteLine("AddDataFromFile");
            string text = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filename))
                {
                    // Read the stream to a string, and write the string to the console
                    text = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return text;
        }


        public void WriteInFiles( string targetFilehtmlName, string targetFileJSName)
        {

            try
            {
                // Instanciation du StreamWriter avec passage du nom du fichier
                if (File.Exists(targetFilehtmlName))
                {
                    File.WriteAllText(targetFilehtmlName, String.Empty);
                }

                StreamWriter monStreamWriter = new StreamWriter(targetFilehtmlName, true);

                //Ecriture du texte dans votre fichier 
                string beginOfTheHtmlDoc = "<!DOCTYPE html> \n<html lang=\"fr - FR\"> \n" +
                    "<script src=\"https://ajax.googleapis.com/ajax/libs/angularjs/1.4.8/angular.min.js\"></script>\n" +
                    "<script src=\"http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js\"></script>\n" +
                    "<script src=\"https://code.jquery.com/ui/1.12.1/jquery-ui.js\"></script>\n" +
                    "<head>\n" +
                    "\t<meta charset=\"utf-8\"/>\n" +
                    "\t<title>" + nameOfTheMontage + "</title>\n" +
                    "\t<link rel=\"stylesheet\" type=\"text/css\" href=\"styleActe.css\">\n" +
                    "\t<link rel=\"stylesheet\" href=\"//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css\">\n" +
                    "</head>\n\n<body>\n" +
                    "<div ng-app=\"myActe\" ng-controller=\"myCtrl\">\n";
                monStreamWriter.Write(beginOfTheHtmlDoc);

                // what will be written in the html document

                string endOfTheDoc = "\n</div>\n\n<script src=\""+ GetLocalFileName(targetFileJSName) +"\"></script>\n</body>\n</html>\n\n";
                monStreamWriter.Write(endOfTheDoc);

                // close the StreamWriter (realy important) 
                monStreamWriter.Close();
            }
            catch (Exception ex)
            {
                // Code exécuté en cas d'exception 
                Console.Write(ex.Message);
            }

            try
            {
                // Instanciation du StreamWriter avec passage du nom du fichier
                if (File.Exists(targetFileJSName))
                {
                    File.WriteAllText(targetFileJSName, String.Empty);
                }

                StreamWriter myStreamWriter = new StreamWriter(targetFileJSName, true);

                string beginOfTheJSDoc = "var app = angular.module('myActe', []);\n" +
                                    "app.controller('myCtrl', function($scope) { \n\n";
                myStreamWriter.Write(beginOfTheJSDoc);

                //write the declaration of variables in data base
                string varInDataBaseDec = DataFromFile(@"C:\Users\j.folleas\Desktop\settings\DataBase.txt");
                myStreamWriter.Write(varInDataBaseDec+"\n\n\n");


                //add the differents declarations
                foreach (Declaration dec in listOfDeclarations)
                {
                    myStreamWriter.Write(dec.Write());
                    myStreamWriter.Write(" \n\n");
                }

                //add the differents expressions in functions
                foreach (Affectation aff in listOfCalculExpressions)
                {
                    myStreamWriter.Write(aff.Write());
                    myStreamWriter.Write(" \n\n");
                }


                string endOfTheJSDoc = "});\n\n" +
                   "var listeInput = document.getElementsByTagName(\"input\");\n" +
                   "for (var iter = 0; iter < listeInput.length; iter++) {\n" +
                   "\tif (listeInput[iter].type == \"text\") {\n" +
                   "\t\tlisteInput[iter].style.width = ((listeInput[iter].value.length + 1) * 6) + 'px';\n" +
                   "\t}\n}\n\n";
                myStreamWriter.Write(endOfTheJSDoc);

                // close the StreamWriter (realy important) 
                myStreamWriter.Close();
            }
            catch (Exception ex)
            {
                // Code exécuté en cas d'exception 
                Console.Write(ex.Message);
            }

           
        }
    }
}
