using System;
using System.Collections.Generic;
using System.IO;

namespace RunTime
{
    public class Montage
    {
        public string nameOfTheMontage { get;  }
        public List<Affectation> listOfCalculExpressions { get; }


        public Montage(string name, List<Affectation> myListOfCalculExpressions)
        {
            listOfCalculExpressions = myListOfCalculExpressions;
            nameOfTheMontage = name;
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

                //add the differents expressions in functions
                foreach (Affectation aff in listOfCalculExpressions)
                {
                    myStreamWriter.Write(aff.Write());
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
