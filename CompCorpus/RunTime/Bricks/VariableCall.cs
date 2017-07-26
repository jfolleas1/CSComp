

namespace CompCorpus.RunTime.Bricks
{
    public class VariableCall : Brick
    {
        public string name { get; }
        private bool local { get; set; }
        public string typeString { get; }
        public AbstractExpression expression { get { return getExpression(); } }

        private AbstractExpression getExpression()
        {
            
            Affectation exp = null;
            if (Program.mainMontage.mapOfCalculExpressions.TryGetValue(this.name, out exp))
            {
                return exp.expression;
            }
            else
            {
                return null;
            }

        }

        public VariableCall(string text, bool local, string typeString)
        {
            this.name = text;
            this.local = local;
            this.typeString = typeString;
        }

        public override string Write()
        {
           
            string htmlText = "";
            if (local)
            {
                htmlText += " {{" + name + "()}}";
            }
            else
            {
                htmlText += " <input type=\"";
                htmlText += GetHtmlType();
                htmlText += "\" ng-model=\"" + name + "\"";
                htmlText += " data-toggle=\"tooltip\" data-placement=\"top\" title=\"" + name + "\"";
                htmlText += " onkeypress = \"this.style.width =" +
                    " Math.max( ((this.value.length) * 10 + 16 ),50) + 'px';\" > ";

            }
            return htmlText;
        }

        private string GetHtmlType()
        {
            if (typeString == ExpressionType.NOMBRE.ToString())
            {
                return "number";
            }
            else
            {
                return "text";
            }
        }
    }
}
