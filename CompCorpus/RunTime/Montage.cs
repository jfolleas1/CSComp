﻿using System;
using System.Collections.Generic;
using System.IO;
using CompCorpus.RunTime.error;
using CompCorpus.RunTime.declaration;
using CompCorpus.RunTime.Bricks;
using System.Text.RegularExpressions;
using System.Text;

namespace CompCorpus.RunTime
{
    public class Montage
    {
        public Dictionary<string, string> symboleTabe { get; set; } //
        public string nameOfTheMontage { get; set; }
        public Dictionary<string,Affectation> mapOfCalculExpressions { get; set; } //
        public List<Declaration> listOfDeclarations { get; set; } //
        public List<Brick> listOfBricks { get; set; }
        public List<Error> errorList { get; set; }
        public List<String> functionForListList { get; set; }


        public void PrintListDec()
        {
            foreach (Declaration dec in listOfDeclarations)
            {
               Console.Write(dec.Write());
                Console.Write(" \n\n");
            }
        }

        public Montage()
        {
            mapOfCalculExpressions = new Dictionary<string, Affectation>();
            listOfDeclarations = new List<Declaration>();
            nameOfTheMontage = "";
            symboleTabe = new Dictionary<string, string>();
            errorList = new List<Error>();
            functionForListList = new List<string>();
        }

        public void AddListCalculExpression(List<Affectation> list)
        {
            foreach (Affectation item in list)
            {
                // When we make an include we allready have reorded all the affectation in the file so 
                // we don't need to make it twice
                if(!mapOfCalculExpressions.ContainsKey(item.variableName.name))
                {
                    mapOfCalculExpressions.Add(item.variableName.name, item);
                }
            }
        }

        public void SetDeclarationSymboleAffectationFromOther(Montage other)
        {
            this.mapOfCalculExpressions = other.mapOfCalculExpressions;
            this.listOfDeclarations = other.listOfDeclarations;
            this.symboleTabe = other.symboleTabe;
            this.functionForListList = other.functionForListList;
        }

        public void SetCoreFromOther(Montage other)
        {
            this.listOfBricks = other.listOfBricks;
            this.errorList = other.errorList;
            
        }


        public void AddSymboleFromPreCompile(string file)
        {
            //Add of the choice variables 
            Console.WriteLine("AddSymboleFromPreCompile : Choix");
            string choicePattern = @"(\$choix\() *\w+ *,";
            MatchCollection choiceMatches;
            Regex choiceRegex = new Regex(choicePattern);
            choiceMatches = choiceRegex.Matches(file);
            // Iterate matches
            for (int ctr = 0; ctr < choiceMatches.Count; ctr++)
            {
                string choiceVarName = choiceMatches[ctr].Value.Substring("$choix(".Length,
                    (choiceMatches[ctr].Value.Length - "$choix(,".Length));
                Affectation aff = new Affectation(new VariableId(choiceVarName.Trim(), "L"+ExpressionType.TEXTE.ToString()),
                    new VariableId(choiceVarName.Trim() + "Model", ExpressionType.TEXTE.ToString()));
                this.AddSymbole(aff);
                if (!mapOfCalculExpressions.ContainsKey(aff.variableName.name))
                {
                    this.mapOfCalculExpressions.Add(aff.variableName.name, aff);
                }
            }

            //Add of the propositions variables 
            Console.WriteLine("AddSymboleFromPreCompile : Proposition");
            string propositionPattern = @"[^xni]\( *\w+ *, *"+"\"[^\"]*\""+@" *\)";
            MatchCollection propositionMatches;
            Regex propositionRegex = new Regex(propositionPattern);
            propositionMatches = propositionRegex.Matches(file);
            // Iterate matches
            for (int ctr = 0; ctr < propositionMatches.Count; ctr++)
            {
               
                string propoVarName = propositionMatches[ctr].Value.Substring("x(".Length,
                    (Regex.Match(propositionMatches[ctr].Value,@",").Index  - "x(".Length));

                int commaIndex = ("x(" + propoVarName + ",").Length;
                string propoString = propositionMatches[ctr].Value.Substring(commaIndex,
                    (propositionMatches[ctr].Value.Length - (commaIndex+1))); // We don't need the last char

                Affectation aff = new Affectation(new VariableId(propoVarName.Trim(), "LTEXTE"), new VariableString(propoString));
                this.AddSymbole(aff);
                if (!mapOfCalculExpressions.ContainsKey(aff.variableName.name))
                {
                    this.mapOfCalculExpressions.Add(aff.variableName.name, aff);
                }
            }

            //Add of the option variables 
            Console.WriteLine("AddSymboleFromPreCompile : Option");
            string optionPattern = @"(\$option\() *\w+ *,";
            MatchCollection optionMatches;
            Regex optionRegex = new Regex(optionPattern);
            optionMatches = optionRegex.Matches(file);
            // Iterate matches
            for (int ctr = 0; ctr < optionMatches.Count; ctr++)
            {
                string optionVarName = optionMatches[ctr].Value.Substring("$option(".Length,
                    (optionMatches[ctr].Value.Length - "$option(,".Length));
                Affectation aff = new Affectation(new VariableId(optionVarName.Trim(), "LBOOL"), new VariableId(optionVarName.Trim()+"Model", "BOOL"));
                this.AddSymbole(aff);
                if (!mapOfCalculExpressions.ContainsKey(aff.variableName.name))
                {
                    this.mapOfCalculExpressions.Add(aff.variableName.name, aff);
                }
            }

        }


        public void AddSymbole(string varName, string typeString)
        {
            if (symboleTabe.ContainsKey(varName))
            {
                symboleTabe.Remove(varName);
                errorList.Add(new Error(ErrorType.DOUBLE_DECLARATION, varName, 0, 0));
            }
            symboleTabe.Add(varName, typeString);
        }

        public void AddSymbole(Affectation aff)
        {
            if(aff.expression.dataType != ExpressionType.UNKNOWVAR )
            {
                if (symboleTabe.ContainsKey(aff.variableName.name))
                {
                    symboleTabe.Remove(aff.variableName.name);
                    errorList.Add(new Error(ErrorType.DOUBLE_DECLARATION, aff.variableName.name, 0, 0));
                }
                AddSymbole(aff.variableName.name, ("L" + aff.expression.dataType.ToString()));
            }
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

        public void RemoveSymboles(List<Tuple<string, string>> ls)
        {
            foreach (Tuple<string, string> tp in ls)
            {
                this.symboleTabe.Remove(tp.Item1);
            }
        }

        public void AddFunctionForList(Declaration dec)
        {
            if (dec.type == ExpressionType.LISTSTRUCT)
            {
                this.functionForListList.Add(dec.GetAddNDelFunction());
            }
        }

        public bool isLocalVar(string varName, int line, int column)
        {
            if (IsInSymboleTable(varName, line, column))
            {
                string type;
                symboleTabe.TryGetValue(varName, out type);
                if (type == ("L" + ExpressionType.BOOL.ToString()) ||
                    type == ("L" + ExpressionType.TEXTE.ToString()) ||
                    type == ("L" + ExpressionType.NOMBRE.ToString()) )
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsValideTypeString(string typeString, int line, int column)
        {
            ExpressionType type;
            if (!Enum.TryParse(typeString, out type) || (type == ExpressionType.INVALIDE))
            {
                errorList.Add(new Error(ErrorType.INVALIDE_TYPE, typeString, line, column));
                return false;
            }
            else if (type == ExpressionType.LISTSTRUCT || type == ExpressionType.STRUCT)
            {
                    string text = " : il doit être suivie par une déclaration de la forme { ... }";
                    errorList.Add(new Error(ErrorType.INVALIDE_TYPE_STR, typeString + text, line, column));
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsValideTypeStructString(string typeString, int line, int column)
        {
            ExpressionType type;
            if (!Enum.TryParse(typeString, out type) || (type == ExpressionType.INVALIDE))
            {
                errorList.Add(new Error(ErrorType.INVALIDE_TYPE, typeString, line, column));
                return false;

            }
            else if (type != ExpressionType.LISTSTRUCT && type != ExpressionType.STRUCT)
            {
                string text = " : une déclaration de la forme { ... } doit être de type struct ou listStruct";
                errorList.Add(new Error(ErrorType.INVALIDE_TYPE_STR, typeString + text, line, column));
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

        public void CheckAffectationIsValid(ExpressionType type, string expectedTypeString, string symbole, int line)
        {
            if( !(type == ExpressionType.UNKNOWVAR) )
            {   // If the expression is an unknow var then the error is already know 
                if (type == ExpressionType.INVALIDE)
                {
                    errorList.Add(new Error(ErrorType.INVALIDE_OPERATION, symbole, line, 0));
                }
                if (expectedTypeString != null)
                {
                    CheckCorespondanceExpectedAndExpressionType(type, expectedTypeString, symbole, line);
                }
            }
        }

        public void CheckConditionExpressionIsBoolean(ExpressionType expressionType, int line , int column)
        {
            if (!(expressionType == ExpressionType.UNKNOWVAR))
            {   // If the expression is an unknow var then the error is already know 
                if (expressionType != ExpressionType.BOOL)
                {
                    errorList.Add(new Error(ErrorType.INVALID_CONDITION_EXPR , "", line, column));
                }
            }
        }

        private void CheckCorespondanceExpectedAndExpressionType(ExpressionType expressionType, string expectedTypeString, string symbole, int line)
        {
            ExpressionType expectedType;
            if (!Enum.TryParse(expectedTypeString, out expectedType) && expectedTypeString != "VAR" )
            {
                errorList.Add(new Error(ErrorType.INVALIDE_TYPE, expectedTypeString, line));
            }
            else if (expectedType != expressionType && expectedTypeString != "VAR")
            {
                string data = symbole + " (attendue : " + expectedTypeString + ", retourné : ";
                data += expressionType.ToString() + ")";
                errorList.Add(new Error(ErrorType.INCOMPATIBLE_AFFECTATION, data, line));
            }

        }

        public void AddListConditionalAffectation(List<Affectation> listAff, AbstractExpression exp)
        {
            foreach (Affectation aff in listAff)
            {
                string varName = aff.variableName.name;
                if (mapOfCalculExpressions.TryGetValue(varName, out _))
                {
                    if (mapOfCalculExpressions[varName].expression.dataType == aff.expression.dataType)
                    {
                        mapOfCalculExpressions[varName].listOfConditionAndExpression.Add(
                          new Tuple<AbstractExpression, AbstractExpression>(exp, aff.expression));
                    }
                    else
                    {
                        string data = varName + " (attendue : " + mapOfCalculExpressions[varName].expression.dataType.ToString() +
                            ", retourné : " + aff.expression.dataType.ToString() + ")";
                        errorList.Add(new Error(ErrorType.INCOMPATIBLE_AFFECTATION, data, aff.line, aff.col));
                    }
                }
                else
                {
                    //Rise Error
                    errorList.Add(new Error(ErrorType.UNKNOW_VARIABLE, varName, aff.line, aff.col));
                }

            }
        }


        public void AddListConditionalAffectation(List<Proposition> listProp, AbstractExpression choiceExp)
        {
            foreach (Proposition prop in listProp)
            {
                Expression exp = new Expression(ExpressionSymbole.EGALE, choiceExp, new VariableId(prop.varName, "L"+ExpressionType.TEXTE.ToString()));
                foreach (Affectation aff in prop.listConditionnalAffectation)
                {
                    string varName = aff.variableName.name;
                    if (mapOfCalculExpressions.TryGetValue(varName, out _))
                    {
                        if (mapOfCalculExpressions[varName].expression.dataType == aff.expression.dataType)
                        {
                            mapOfCalculExpressions[varName].listOfConditionAndExpression.Add(
                              new Tuple<AbstractExpression, AbstractExpression>(exp, aff.expression));
                        }
                        else
                        {
                            string data = varName + " (attendue : " + mapOfCalculExpressions[varName].expression.dataType.ToString() +
                                ", retourné : " + aff.expression.dataType.ToString() + ")";
                            errorList.Add(new Error(ErrorType.INCOMPATIBLE_AFFECTATION, data, aff.line, aff.col));
                        }
                    }
                    else
                    {
                        //Rise Error
                        errorList.Add(new Error(ErrorType.UNKNOW_VARIABLE, varName, aff.line, aff.col));
                    }
                }

            }
        }

        public void PrintErrors()
        {
            foreach (Error err in errorList)
            {
                Console.WriteLine(err.GetMessage());
            }
        }

        public string WriteErrors()
        {
            string errorLogs = "";
            foreach (Error err in errorList)
            {
                errorLogs += (err.GetMessage() + "\n");
            }
            return errorLogs;
        }

        public string GetVarTypeString(string varName)
        {
            string value = "";
            if (!symboleTabe.TryGetValue(varName, out value))
                value = "UNKNOWVAR";
            return value;
        }

        public string GetVarTypeStringForIteration(string varName, int line, int col)
        {
            string value = GetVarTypeString(varName);
            
            if (value == "UNKNOWVAR")
            {
                //Rise Error
                errorList.Add(new Error(ErrorType.UNKNOW_VARIABLE, varName, line, col));

            }
            else if (value != "LISTSTRUCT")
            {
                //Rise Error
                errorList.Add(new Error(ErrorType.NOTLIST_USE_FOR_ITERATION,varName, line, col));
            }
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
            foreach (Affectation aff in mapOfCalculExpressions.Values)
            {
                aff.Print();
            }
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

                StreamWriter monStreamWriter = new StreamWriter(targetFilehtmlName, true, Encoding.UTF8);
                
                //Ecriture du texte dans votre fichier 
                string beginOfTheHtmlDoc = "<!DOCTYPE html> \n<html lang=\"fr - FR\"> \n" +
                    "<script src=\"https://ajax.googleapis.com/ajax/libs/angularjs/1.4.8/angular.min.js\"></script>\n" +
                    "<script src=\"http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js\"></script>\n" +
                    "<script src=\"https://code.jquery.com/ui/1.12.1/jquery-ui.js\"></script>\n" +
                    "<script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js\"></script> \n "+
                    "<head>\n" +
                    "\t<meta charset=\"utf-8\"/>\n" +
                    "\t<title>" + nameOfTheMontage + "</title>\n" +
                    "\t<link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css\">\n" +
                    "\t<link rel=\"stylesheet\" href=\"//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css\">\n" +
                    "\t<link rel=\"stylesheet\" href=\"styleActe.css\">\n" +
                    "</head>\n\n<body>\n" +
                    "<div class=\"container\" ng-app=\"myActe\" ng-controller=\"myCtrl\">\n"+
                    "<h1>" + nameOfTheMontage + "</h1>";
                monStreamWriter.Write(beginOfTheHtmlDoc);
               
                // what will be written in the html document
                foreach (Brick bk in listOfBricks)
                {
                    
                    monStreamWriter.Write(bk.Write());
                    monStreamWriter.Write("\n");
                }
                
                
                string endOfTheDoc = "\n</div>\n\n<script src=\""+ GetLocalFileName(targetFileJSName) +"\"></script>\n";
                endOfTheDoc += "<script> \n$(document).ready(function(){\n\t$('[data-toggle=\"tooltip\"]').tooltip();\n" +
                "});\n </script>\n</body>\n</html>\n\n";
                monStreamWriter.Write(endOfTheDoc);

                // close the StreamWriter (realy important) 
                monStreamWriter.Close();
                
            }
            catch (Exception ex)
            {
                // Code exécuté en cas d'exception 
                LogManager.AddLog("Lors de l'ecriture des fichier de sortie: \n");
                LogManager.AddLog(ex.Message);
            }

            try
            {
                // Instanciation du StreamWriter avec passage du nom du fichier
                if (File.Exists(targetFileJSName))
                {
                    File.WriteAllText(targetFileJSName, String.Empty);
                }

                StreamWriter myStreamWriter = new StreamWriter(targetFileJSName, true, Encoding.UTF8);

                string beginOfTheJSDoc = "var app = angular.module('myActe', []);\n" +
                                    "app.controller('myCtrl', function($scope) { \n\n";
                myStreamWriter.Write(beginOfTheJSDoc);

                myStreamWriter.Write("\n\n\n");


                //add the differents declarations
                foreach (Declaration dec in listOfDeclarations)
                {
                    myStreamWriter.Write(dec.Write());
                    myStreamWriter.Write(" \n\n");
                }

                //add the differents expressions in functions
                foreach (Affectation aff in mapOfCalculExpressions.Values)
                {
                    myStreamWriter.Write(aff.Write());
                    myStreamWriter.Write(" \n\n");
                }

                //add the functions for list structur
                foreach (String fun in functionForListList)
                {
                    myStreamWriter.Write(fun);
                    myStreamWriter.Write(" \n\n");
                }
                

                string endOfTheJSDoc = "\n\n});\n\n" +
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
                LogManager.AddLog("Lors de l'écriture des fichier de sortie");
                LogManager.AddLog(ex.Message);
            }

           
        }
    }
}
