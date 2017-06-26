using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.error
{
    public enum ErrorType
    {
        INVALIDE_OPERATION,
        INVALIDE_TYPE,
        UNKNOW_VARIABLE,
        INCOMPATIBLE_AFFECTATION,
        DOUBLE_DECLARATION,
        INVALID_CONDITION_EXPR
    }

    public class Error
    {
        public int line { get; }
        public int column { get; }
        public string data { get; set; }
        public ErrorType type { get; }

        public Error(ErrorType type, string data, int line, int column = 0)
        {
            this.type = type;
            this.data = data;
            this.line = line;
            this.column = column;
        }

        public bool Equals(Error err)
        {
            bool test = true;
            test &= (this.type == err.type);
            test &= (this.data == err.data);
            test &= (this.line == err.line);
            test &= (this.column == err.column);
            return test;
        }

        public string GetMessage()
        {
            string message = "ERREUR: lig: "+line+", col: "+column;

            switch (this.type)
            {
                case ErrorType.INVALIDE_OPERATION:
                    message += " l'une des opérations pour créer l'expression "+data+" est invalide avec les types utilisés.";
                    break;
                case ErrorType.INVALIDE_TYPE:
                    message += " le type "+data+" n'existe pas.";
                    break;
                case ErrorType.UNKNOW_VARIABLE:
                    message += " la variable "+data+" est utilisée mais est inconnue.";
                    break;
                case ErrorType.INCOMPATIBLE_AFFECTATION:
                    message += " l'affectation est impossible pour la variable " + data ;
                    break;
                case ErrorType.DOUBLE_DECLARATION:
                    message = "ERREUR: le nom de variable " + data + " à été déclaré plusieur fois.";
                    break;
                case ErrorType.INVALID_CONDITION_EXPR:
                    message += " l'expression utilisé pour une condition ne retourne pas une valeur boolean";
                    break;
                default:
                    break;
            }

            return message;
        }
    }
}
