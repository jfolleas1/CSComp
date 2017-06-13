// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  FIDF3675368
// DateTime: 13/06/2017 09:28:05
// UserName: j.folleas
// Input file <ParserComp.y - 13/06/2017 09:24:08>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using RunTime;

namespace Analyser
{
public enum Tokens {error=2,EOF=3,TITREACTEKW=4,TRUE=5,FALSE=6,
    DOLLAR=7,PLUS=8,MINUS=9,MUL=10,DIV=11,PARENTOPEN=12,
    PARENTCLOSE=13,BRACEOPEN=14,BRACECLOSE=15,AND=16,OR=17,NOT=18,
    EGALE=19,INF=20,INFEGALE=21,SUP=22,SUPEGALE=23,ASSIGN=24,
    SEMICOLON=25,SEPARATOR=26,ID=27,STRING=28,INTEGER=29,FLOAT=30,
    DEADWORD=31};

public struct ValueType
{
        public string String;
        public long Integer;
        public double Float;
		public Affectation affectation;
		public AbstractExpression expression;
		public Variable constante;
		public VariableId variable ;
		public List<Affectation> listAffectation ;
}
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class Parser: ShiftReduceParser<ValueType, LexLocation>
{
  // Verbatim content from ParserComp.y - 13/06/2017 09:24:08
    
    public Montage montage = new Montage();

  // End verbatim content from ParserComp.y - 13/06/2017 09:24:08

#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[36];
  private static State[] states = new State[59];
  private static string[] nonTerms = new string[] {
      "montage", "affectation", "expression", "constante", "var", "listAffectation", 
      "defActeTitle", "deadText", "declaredVariableName", "declaredVariableType", 
      "$accept", "listDeclaration", "declaration", };

  static Parser() {
    states[0] = new State(new int[]{4,51,3,-2},new int[]{-1,1,-7,3});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(-9,new int[]{-12,4});
    states[4] = new State(new int[]{26,5,27,50},new int[]{-13,45,-9,46});
    states[5] = new State(-14,new int[]{-6,6});
    states[6] = new State(new int[]{27,40,3,-3},new int[]{-2,7,-5,8});
    states[7] = new State(-15);
    states[8] = new State(new int[]{24,9});
    states[9] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,10,-5,39,-4,41});
    states[10] = new State(new int[]{25,11,8,12,10,14,11,16,9,18,16,20,17,22,19,24,20,26,21,28,22,30,23,32});
    states[11] = new State(-16);
    states[12] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,13,-5,39,-4,41});
    states[13] = new State(new int[]{8,-17,10,14,11,16,9,-17,16,-17,17,-17,19,-17,20,-17,21,-17,22,-17,23,-17,25,-17,13,-17});
    states[14] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,15,-5,39,-4,41});
    states[15] = new State(-18);
    states[16] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,17,-5,39,-4,41});
    states[17] = new State(-19);
    states[18] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,19,-5,39,-4,41});
    states[19] = new State(new int[]{8,-20,10,14,11,16,9,-20,16,-20,17,-20,19,-20,20,-20,21,-20,22,-20,23,-20,25,-20,13,-20});
    states[20] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,21,-5,39,-4,41});
    states[21] = new State(new int[]{8,12,10,14,11,16,9,18,16,-21,17,-21,19,24,20,26,21,28,22,30,23,32,25,-21,13,-21});
    states[22] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,23,-5,39,-4,41});
    states[23] = new State(new int[]{8,12,10,14,11,16,9,18,16,-22,17,-22,19,24,20,26,21,28,22,30,23,32,25,-22,13,-22});
    states[24] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,25,-5,39,-4,41});
    states[25] = new State(new int[]{8,12,10,14,11,16,9,18,16,-24,17,-24,19,-24,20,-24,21,-24,22,-24,23,-24,25,-24,13,-24});
    states[26] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,27,-5,39,-4,41});
    states[27] = new State(new int[]{8,12,10,14,11,16,9,18,16,-25,17,-25,19,-25,20,-25,21,-25,22,-25,23,-25,25,-25,13,-25});
    states[28] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,29,-5,39,-4,41});
    states[29] = new State(new int[]{8,12,10,14,11,16,9,18,16,-26,17,-26,19,-26,20,-26,21,-26,22,-26,23,-26,25,-26,13,-26});
    states[30] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,31,-5,39,-4,41});
    states[31] = new State(new int[]{8,12,10,14,11,16,9,18,16,-27,17,-27,19,-27,20,-27,21,-27,22,-27,23,-27,25,-27,13,-27});
    states[32] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,33,-5,39,-4,41});
    states[33] = new State(new int[]{8,12,10,14,11,16,9,18,16,-28,17,-28,19,-28,20,-28,21,-28,22,-28,23,-28,25,-28,13,-28});
    states[34] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,35,-5,39,-4,41});
    states[35] = new State(new int[]{8,12,10,14,11,16,9,18,16,-23,17,-23,19,24,20,26,21,28,22,30,23,32,25,-23,13,-23});
    states[36] = new State(new int[]{18,34,12,36,27,40,29,42,30,43,28,44},new int[]{-3,37,-5,39,-4,41});
    states[37] = new State(new int[]{13,38,8,12,10,14,11,16,9,18,16,20,17,22,19,24,20,26,21,28,22,30,23,32});
    states[38] = new State(-29);
    states[39] = new State(-30);
    states[40] = new State(-32);
    states[41] = new State(-31);
    states[42] = new State(-33);
    states[43] = new State(-34);
    states[44] = new State(-35);
    states[45] = new State(-10);
    states[46] = new State(new int[]{27,49},new int[]{-10,47});
    states[47] = new State(new int[]{25,48});
    states[48] = new State(-11);
    states[49] = new State(-13);
    states[50] = new State(-12);
    states[51] = new State(new int[]{14,52});
    states[52] = new State(new int[]{31,57,27,58},new int[]{-8,53});
    states[53] = new State(new int[]{15,54,31,55,27,56});
    states[54] = new State(-4);
    states[55] = new State(-7);
    states[56] = new State(-8);
    states[57] = new State(-5);
    states[58] = new State(-6);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-11, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{});
    rules[3] = new Rule(-1, new int[]{-7,-12,26,-6});
    rules[4] = new Rule(-7, new int[]{4,14,-8,15});
    rules[5] = new Rule(-8, new int[]{31});
    rules[6] = new Rule(-8, new int[]{27});
    rules[7] = new Rule(-8, new int[]{-8,31});
    rules[8] = new Rule(-8, new int[]{-8,27});
    rules[9] = new Rule(-12, new int[]{});
    rules[10] = new Rule(-12, new int[]{-12,-13});
    rules[11] = new Rule(-13, new int[]{-9,-10,25});
    rules[12] = new Rule(-9, new int[]{27});
    rules[13] = new Rule(-10, new int[]{27});
    rules[14] = new Rule(-6, new int[]{});
    rules[15] = new Rule(-6, new int[]{-6,-2});
    rules[16] = new Rule(-2, new int[]{-5,24,-3,25});
    rules[17] = new Rule(-3, new int[]{-3,8,-3});
    rules[18] = new Rule(-3, new int[]{-3,10,-3});
    rules[19] = new Rule(-3, new int[]{-3,11,-3});
    rules[20] = new Rule(-3, new int[]{-3,9,-3});
    rules[21] = new Rule(-3, new int[]{-3,16,-3});
    rules[22] = new Rule(-3, new int[]{-3,17,-3});
    rules[23] = new Rule(-3, new int[]{18,-3});
    rules[24] = new Rule(-3, new int[]{-3,19,-3});
    rules[25] = new Rule(-3, new int[]{-3,20,-3});
    rules[26] = new Rule(-3, new int[]{-3,21,-3});
    rules[27] = new Rule(-3, new int[]{-3,22,-3});
    rules[28] = new Rule(-3, new int[]{-3,23,-3});
    rules[29] = new Rule(-3, new int[]{12,-3,13});
    rules[30] = new Rule(-3, new int[]{-5});
    rules[31] = new Rule(-3, new int[]{-4});
    rules[32] = new Rule(-5, new int[]{27});
    rules[33] = new Rule(-4, new int[]{29});
    rules[34] = new Rule(-4, new int[]{30});
    rules[35] = new Rule(-4, new int[]{28});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // montage -> /* empty */
{ Console.WriteLine(" Empty programe "); }
        break;
      case 3: // montage -> defActeTitle, listDeclaration, SEPARATOR, listAffectation
{  montage.nameOfTheMontage=ValueStack[ValueStack.Depth-4].String; montage.listOfCalculExpressions=ValueStack[ValueStack.Depth-1].listAffectation; }
        break;
      case 4: // defActeTitle -> TITREACTEKW, BRACEOPEN, deadText, BRACECLOSE
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-2].String; }
        break;
      case 5: // deadText -> DEADWORD
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String; }
        break;
      case 6: // deadText -> ID
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String; }
        break;
      case 7: // deadText -> deadText, DEADWORD
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-2].String + " " + ValueStack[ValueStack.Depth-1].String; }
        break;
      case 8: // deadText -> deadText, ID
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-2].String + " " + ValueStack[ValueStack.Depth-1].String; }
        break;
      case 9: // listDeclaration -> /* empty */
{ Console.WriteLine("Empty Dec");  }
        break;
      case 10: // listDeclaration -> listDeclaration, declaration
{ Console.WriteLine("declaration");  }
        break;
      case 11: // declaration -> declaredVariableName, declaredVariableType, SEMICOLON
{Console.WriteLine(" DEC :" + ValueStack[ValueStack.Depth-3].String + " " + ValueStack[ValueStack.Depth-2].String + " ;"); }
        break;
      case 12: // declaredVariableName -> ID
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String; }
        break;
      case 13: // declaredVariableType -> ID
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String; }
        break;
      case 14: // listAffectation -> /* empty */
{ CurrentSemanticValue.listAffectation = new List<Affectation>();  }
        break;
      case 15: // listAffectation -> listAffectation, affectation
{ CurrentSemanticValue.listAffectation=ValueStack[ValueStack.Depth-2].listAffectation; CurrentSemanticValue.listAffectation.Add(ValueStack[ValueStack.Depth-1].affectation);  }
        break;
      case 16: // affectation -> var, ASSIGN, expression, SEMICOLON
{ ValueStack[ValueStack.Depth-2].expression.CheckValidity(LocationStack[LocationStack.Depth-4].StartLine); montage.AddSymbole(ValueStack[ValueStack.Depth-4].variable.name, ("L" + ValueStack[ValueStack.Depth-2].expression.dataType.ToString())); CurrentSemanticValue.affectation = new Affectation(ValueStack[ValueStack.Depth-4].variable, ValueStack[ValueStack.Depth-2].expression); }
        break;
      case 17: // expression -> expression, PLUS, expression
{ /*Console.WriteLine("PLUS");*/	CurrentSemanticValue.expression = new Expression(ExpressionSymbole.PLUS, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 18: // expression -> expression, MUL, expression
{ /*Console.WriteLine("MUL");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.MUL, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression);}
        break;
      case 19: // expression -> expression, DIV, expression
{ /*Console.WriteLine("DIV");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.DIV, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 20: // expression -> expression, MINUS, expression
{ /*Console.WriteLine("MINUS");*/	CurrentSemanticValue.expression = new Expression(ExpressionSymbole.MINUS, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 21: // expression -> expression, AND, expression
{ /*Console.WriteLine("AND");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.AND, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 22: // expression -> expression, OR, expression
{ /*Console.WriteLine("OR");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.OR, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 23: // expression -> NOT, expression
{ /*Console.WriteLine("NOT");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.NOT, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 24: // expression -> expression, EGALE, expression
{ /*Console.WriteLine("EGALE");*/	CurrentSemanticValue.expression = new Expression(ExpressionSymbole.EGALE, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 25: // expression -> expression, INF, expression
{ /*Console.WriteLine("INF");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.INF, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 26: // expression -> expression, INFEGALE, expression
{ /*Console.WriteLine("INFEGALE");*/ CurrentSemanticValue.expression = new Expression(ExpressionSymbole.INFEGALE, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 27: // expression -> expression, SUP, expression
{ /*Console.WriteLine("SUP");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.SUP, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 28: // expression -> expression, SUPEGALE, expression
{ /*Console.WriteLine("SUPEGALE");*/ CurrentSemanticValue.expression = new Expression(ExpressionSymbole.SUPEGALE, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 29: // expression -> PARENTOPEN, expression, PARENTCLOSE
{ /*Console.WriteLine("PARENT");*/	CurrentSemanticValue.expression = new Expression(ExpressionSymbole.PARENT, ValueStack[ValueStack.Depth-2].expression); }
        break;
      case 30: // expression -> var
{ /*Console.WriteLine("var :" +$1 );*/ montage.IsInSymboleTable(ValueStack[ValueStack.Depth-1].variable.name, LocationStack[LocationStack.Depth-1].StartLine, LocationStack[LocationStack.Depth-1].StartColumn ); CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].variable; }
        break;
      case 31: // expression -> constante
{ /*Console.WriteLine("constante");*/  CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].constante; }
        break;
      case 32: // var -> ID
{ CurrentLocationSpan = LocationStack[LocationStack.Depth-1]; CurrentSemanticValue.variable = new VariableId(ValueStack[ValueStack.Depth-1].String, montage.GetVarTypeString(ValueStack[ValueStack.Depth-1].String)); }
        break;
      case 33: // constante -> INTEGER
{ CurrentLocationSpan = LocationStack[LocationStack.Depth-1]; /*Console.WriteLine("int :" + $1 );*/		CurrentSemanticValue.constante = new VariableInteger(ValueStack[ValueStack.Depth-1].Integer);}
        break;
      case 34: // constante -> FLOAT
{ CurrentLocationSpan = LocationStack[LocationStack.Depth-1]; /*Console.WriteLine("float :" + $1 );*/		CurrentSemanticValue.constante = new VariableFloat(ValueStack[ValueStack.Depth-1].Float);}
        break;
      case 35: // constante -> STRING
{ CurrentLocationSpan = LocationStack[LocationStack.Depth-1]; /*Console.WriteLine("string :" + $1 );*/		CurrentSemanticValue.constante = new VariableString(ValueStack[ValueStack.Depth-1].String);}
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }


// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) {}

}
}