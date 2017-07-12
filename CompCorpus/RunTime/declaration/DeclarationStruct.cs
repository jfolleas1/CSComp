using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.declaration
{
    public class DeclarationStruct : Declaration
    {
        static List<KeyValuePair<string, string>> contextConcatenation { get; set; } = new List<KeyValuePair<string, string>>();
        List<Declaration> declarationList { get; }
        string itemGetterPath { get; }
        string listOfParamForAddAndDel { get; }


        public DeclarationStruct(string name, string typeString, List<Declaration> declarationList ) : base(name, GetTypeFromString(typeString))
        {
            this.declarationList = declarationList;
            itemGetterPath = GetCurrentContexte();
            listOfParamForAddAndDel = GetListofIndexParametters();

        }

        static public void PushContexte(string nameStruct, string typeStruct)
        {
            if (typeStruct == ExpressionType.LISTSTRUCT.ToString())
            {
                contextConcatenation.Add(new KeyValuePair<string, string>(nameStruct, nameStruct + "Index"));
            }
           else
            {
                contextConcatenation.Add(new KeyValuePair<string, string>(nameStruct, "null"));
            }
        }

        static public void PopContexte()
        {
            contextConcatenation.RemoveAt(contextConcatenation.Count - 1);
        }

        static private string GetCurrentContexte()
        {
            string path = "";
            int i = 0;
            int indexOfLast = contextConcatenation.Count - 1; // we dont need the last 
                                                //context beacause its the curent 
                                                //declaration 
            foreach (KeyValuePair<string, string> item in contextConcatenation)
            {
                if (i != indexOfLast)
                {
                    if (item.Value == "null")
                    {
                        path += item.Key;
                    }
                    else
                    {
                        path += item.Key + "[" + item.Value + "]";
                    }
                    path += ".";
                   i++;
                }
            }
            return path;
        }

        static private string GetListofIndexParametters()
        {
            string indexparameter = "";
            int i = 0;
            int indexOfLast = contextConcatenation.Count - 1; // we dont need the last 
                                                              //context beacause its the curent 
                                                              //declaration 
            foreach (KeyValuePair<string, string> item in contextConcatenation)
            {
                if (i != indexOfLast)
                {
                    if (item.Value != "null")
                    {
                        
                        indexparameter += item.Value + ",";

                    }
                    i++;
                }
            }
            return indexparameter;
        }

        static private ExpressionType GetTypeFromString(string typeString)
        {
            ExpressionType myType = ExpressionType.INVALIDE;
            if (typeString == "STRUCT")
            {
                myType = ExpressionType.STRUCT;
            }
            if (typeString == "LISTSTRUCT")
            {
                myType = ExpressionType.LISTSTRUCT;
            }
            return myType;
        }

        public override string Write(bool notInStruct = true, int nbTab = 0)
        {
            string text = "";
            if (notInStruct)
            {
                text += "$scope.";
            }
            for (int i = 0; i < nbTab; i++)
            {
                text += "\t";
            }
            text += name;
            if (notInStruct)
            {
                text += " = ";
            }
            else
            {
                text += " : ";
            }
            if (base.type == ExpressionType.LISTSTRUCT)
            {
                text += "[";
            }
            text += "{ \n";
            foreach (Declaration dec in declarationList)
            {
                text += (dec.Write(false, nbTab+1) + ",\n");
                
            }
            for (int i = 0; i < nbTab; i++)
            {
                text += "\t";
            }
            text += "}";
            if (base.type == ExpressionType.LISTSTRUCT)
            {
                text += "]";
            }
            
            if (notInStruct)
            {
                text += ";";
            }
            return text;
        }

        public override List<Tuple<string, string>> GetSymboles(string baseSymbole = "")
        {
            bool firstLevel = (baseSymbole == "");
            if(!firstLevel)
            {
                baseSymbole += ".";
            }
            baseSymbole += name;
            List<Tuple<string, string>> ls = new List<Tuple<string, string>>();
            ls.Add(new Tuple<string, string>(baseSymbole, type.ToString()));
            if (type == ExpressionType.LISTSTRUCT)
            {
                ls.Add(new Tuple<string, string>(baseSymbole + "nombre_elements", "L"+ExpressionType.NOMBRE.ToString()));
            }
            foreach (Declaration dec in declarationList)
            {
                ls.AddRange(dec.GetSymboles(baseSymbole));
            }
            return ls;
        }

        public override string GetAddNDelFunction()
        {
            string functionAdd = "$scope.add" + this.name + " = function(";
            if (this.listOfParamForAddAndDel != "")
            {
                //We delete the last comma
                functionAdd += this.listOfParamForAddAndDel.Substring(0, (this.listOfParamForAddAndDel.Count() - 1));
            }
            functionAdd += ") {\n";
            functionAdd += "$scope." + this.itemGetterPath + this.name + ".push({";
            foreach (Declaration dec in declarationList)
            {
                functionAdd += (dec.Write(false, 1) + ",\n");
            }
            functionAdd += "\n});\n}";
            string functionDel = "$scope.del" + this.name + " = function(" + this.listOfParamForAddAndDel;
            functionDel += this.name + "Index) {\n";
            functionDel += "$scope." + this.itemGetterPath + this.name + ".splice(" + this.name + "Index, 1); \n}\n";
            string functionCount = "$scope." + this.name + "nombre_elements" + " = function() {\n";
            functionCount += " return $scope." + this.name + ".length; \n}\n";

            return functionAdd + "\n"+ functionDel +"\n" + functionCount;
        }
    }
}
