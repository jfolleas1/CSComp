using System;
using System.Collections.Generic;

namespace RunTime
{
    public class Montage
    {
        public string nameOfTheMontage { get; }
        public List<Affectation> listOfCalculExpressions { get; set; }


        public Montage(string name)
        {
            listOfCalculExpressions = new List<Affectation>();
            nameOfTheMontage = name;
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
                "\t< title > " + nameOfTheMontage + "</title>" +
                "\t< link rel = \"stylesheet\" type = \"text/css\" href = \"styleActe.css\" >\n" +
                "\t< link rel = \"stylesheet\" href = \"//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css\" >\n" +
                "</head>\n\n<body>" +
                "<div ng-app=\"myActe\" ng-controller=\"myCtrl\">";
            Console.Write(beginOfTheHtmlDoc);

            // what will be written in the html document

            string endOfTheDoc = "<script src=\"targetFileNameController.js\"></script>\n</body>\n</html>";
            Console.Write(endOfTheDoc);

            Console.WriteLine("-----CODE IN THE JS DOCUMENT------");
            string beginOfTheJSDoc = "var app = angular.module('monActe', []);\n"+
                                     "app.controller('monCtrl', function($scope) { \n\n";
           
            
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

        public void WriteInFile( string targetFileName)
        {


            Console.WriteLine(@"// Liste des définition des expressions");
            foreach (Affectation aff in listOfCalculExpressions)
            {
                Console.Write(aff.Write());
            }
        }
    }
}
