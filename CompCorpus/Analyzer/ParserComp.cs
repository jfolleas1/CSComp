// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  FIDF3675368
// DateTime: 19/06/2017 14:55:13
// UserName: j.folleas
// Input file <ParserComp.y - 19/06/2017 14:55:11>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using CompCorpus.RunTime;
using CompCorpus.RunTime.declaration;
using CompCorpus.RunTime.Bricks;

namespace CompCorpus.Analyzer
{
public enum Tokens {error=2,EOF=3,TITREACTEKW=4,TRUE=5,FALSE=6,
    DOLLAR=7,PLUS=8,MINUS=9,MUL=10,DIV=11,PARENTOPEN=12,
    PARENTCLOSE=13,BRACEOPEN=14,BRACECLOSE=15,AND=16,OR=17,NOT=18,
    EGALE=19,INF=20,INFEGALE=21,SUP=22,SUPEGALE=23,ASSIGN=24,
    SEMICOLON=25,SEPARATOR=26,CODEINDIC=27,NOUVLIGNE=28,NOUVPARAG=29,ID=30,
    STRING=31,INTEGER=32,FLOAT=33,DEADWORD=34,TITLEID=35};

public struct ValueType
{
        public string String;
        public long Integer;
        public double Float;
		public Affectation affectation;
		public AbstractExpression expression;
		public Variable constante;
		public VariableId variable ;
		public Declaration declaration ;
		public List<Declaration> listDeclaration ;
		public List<Affectation> listAffectation ;
		public Brick brick ;
		public List<Brick> listBrick ;
		public VariableCall variableCall;
		public Title Title;
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
  // Verbatim content from ParserComp.y - 19/06/2017 14:55:11
    
    public Montage montage = new Montage();

  // End verbatim content from ParserComp.y - 19/06/2017 14:55:11

#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[61];
  private static State[] states = new State[93];
  private static string[] nonTerms = new string[] {
      "montage", "affectation", "expression", "constante", "var", "listAffectation", 
      "defActeTitle", "deadText", "declaredVariableName", "declaredVariableType", 
      "affectationType", "declaration", "listDeclaration", "textBloc", "textBlocElement", 
      "brick", "document", "listBrick", "callVar", "title", "$accept", };

  static Parser() {
    states[0] = new State(new int[]{4,85,3,-2},new int[]{-1,1,-7,3});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(-9,new int[]{-13,4});
    states[4] = new State(new int[]{26,5,30,84},new int[]{-12,76,-9,77});
    states[5] = new State(-14,new int[]{-6,6});
    states[6] = new State(new int[]{26,9,30,69,3,-39},new int[]{-17,7,-2,8,-5,36});
    states[7] = new State(-3);
    states[8] = new State(-15);
    states[9] = new State(-41,new int[]{-18,10});
    states[10] = new State(new int[]{34,14,30,15,7,16,12,17,13,18,25,19,32,20,33,21,31,22,28,23,29,24,27,27,35,32,3,-40},new int[]{-16,11,-14,12,-15,25,-19,26,-20,31});
    states[11] = new State(-42);
    states[12] = new State(new int[]{34,14,30,15,7,16,12,17,13,18,25,19,32,20,33,21,31,22,28,23,29,24,27,-43,35,-43,3,-43},new int[]{-15,13});
    states[13] = new State(-47);
    states[14] = new State(-48);
    states[15] = new State(-49);
    states[16] = new State(-50);
    states[17] = new State(-51);
    states[18] = new State(-52);
    states[19] = new State(-53);
    states[20] = new State(-54);
    states[21] = new State(-55);
    states[22] = new State(-56);
    states[23] = new State(-57);
    states[24] = new State(-58);
    states[25] = new State(-46);
    states[26] = new State(-44);
    states[27] = new State(new int[]{14,28});
    states[28] = new State(new int[]{30,29});
    states[29] = new State(new int[]{15,30});
    states[30] = new State(-59);
    states[31] = new State(-45);
    states[32] = new State(new int[]{14,33});
    states[33] = new State(new int[]{34,14,30,15,7,16,12,17,13,18,25,19,32,20,33,21,31,22,28,23,29,24},new int[]{-14,34,-15,25});
    states[34] = new State(new int[]{15,35,34,14,30,15,7,16,12,17,13,18,25,19,32,20,33,21,31,22,28,23,29,24},new int[]{-15,13});
    states[35] = new State(-60);
    states[36] = new State(new int[]{30,75,24,-18},new int[]{-11,37,-10,74});
    states[37] = new State(new int[]{24,38});
    states[38] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,39,-5,68,-4,70});
    states[39] = new State(new int[]{25,40,8,41,10,43,11,45,9,47,16,49,17,51,19,53,20,55,21,57,22,59,23,61});
    states[40] = new State(-16);
    states[41] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,42,-5,68,-4,70});
    states[42] = new State(new int[]{8,-20,10,43,11,45,9,-20,16,-20,17,-20,19,-20,20,-20,21,-20,22,-20,23,-20,25,-20,13,-20});
    states[43] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,44,-5,68,-4,70});
    states[44] = new State(-21);
    states[45] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,46,-5,68,-4,70});
    states[46] = new State(-22);
    states[47] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,48,-5,68,-4,70});
    states[48] = new State(new int[]{8,-23,10,43,11,45,9,-23,16,-23,17,-23,19,-23,20,-23,21,-23,22,-23,23,-23,25,-23,13,-23});
    states[49] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,50,-5,68,-4,70});
    states[50] = new State(new int[]{8,41,10,43,11,45,9,47,16,-24,17,-24,19,53,20,55,21,57,22,59,23,61,25,-24,13,-24});
    states[51] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,52,-5,68,-4,70});
    states[52] = new State(new int[]{8,41,10,43,11,45,9,47,16,-25,17,-25,19,53,20,55,21,57,22,59,23,61,25,-25,13,-25});
    states[53] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,54,-5,68,-4,70});
    states[54] = new State(new int[]{8,41,10,43,11,45,9,47,16,-27,17,-27,19,-27,20,-27,21,-27,22,-27,23,-27,25,-27,13,-27});
    states[55] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,56,-5,68,-4,70});
    states[56] = new State(new int[]{8,41,10,43,11,45,9,47,16,-28,17,-28,19,-28,20,-28,21,-28,22,-28,23,-28,25,-28,13,-28});
    states[57] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,58,-5,68,-4,70});
    states[58] = new State(new int[]{8,41,10,43,11,45,9,47,16,-29,17,-29,19,-29,20,-29,21,-29,22,-29,23,-29,25,-29,13,-29});
    states[59] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,60,-5,68,-4,70});
    states[60] = new State(new int[]{8,41,10,43,11,45,9,47,16,-30,17,-30,19,-30,20,-30,21,-30,22,-30,23,-30,25,-30,13,-30});
    states[61] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,62,-5,68,-4,70});
    states[62] = new State(new int[]{8,41,10,43,11,45,9,47,16,-31,17,-31,19,-31,20,-31,21,-31,22,-31,23,-31,25,-31,13,-31});
    states[63] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,64,-5,68,-4,70});
    states[64] = new State(new int[]{8,41,10,43,11,45,9,47,16,-26,17,-26,19,53,20,55,21,57,22,59,23,61,25,-26,13,-26});
    states[65] = new State(new int[]{18,63,12,65,30,69,32,71,33,72,31,73},new int[]{-3,66,-5,68,-4,70});
    states[66] = new State(new int[]{13,67,8,41,10,43,11,45,9,47,16,49,17,51,19,53,20,55,21,57,22,59,23,61});
    states[67] = new State(-32);
    states[68] = new State(-33);
    states[69] = new State(-35);
    states[70] = new State(-34);
    states[71] = new State(-36);
    states[72] = new State(-37);
    states[73] = new State(-38);
    states[74] = new State(-17);
    states[75] = new State(-19);
    states[76] = new State(-10);
    states[77] = new State(new int[]{30,75},new int[]{-10,78});
    states[78] = new State(new int[]{25,79,14,80});
    states[79] = new State(-11);
    states[80] = new State(-9,new int[]{-13,81});
    states[81] = new State(new int[]{15,82,30,84},new int[]{-12,76,-9,77});
    states[82] = new State(new int[]{25,83});
    states[83] = new State(-12);
    states[84] = new State(-13);
    states[85] = new State(new int[]{14,86});
    states[86] = new State(new int[]{34,91,30,92},new int[]{-8,87});
    states[87] = new State(new int[]{15,88,34,89,30,90});
    states[88] = new State(-4);
    states[89] = new State(-7);
    states[90] = new State(-8);
    states[91] = new State(-5);
    states[92] = new State(-6);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-21, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{});
    rules[3] = new Rule(-1, new int[]{-7,-13,26,-6,-17});
    rules[4] = new Rule(-7, new int[]{4,14,-8,15});
    rules[5] = new Rule(-8, new int[]{34});
    rules[6] = new Rule(-8, new int[]{30});
    rules[7] = new Rule(-8, new int[]{-8,34});
    rules[8] = new Rule(-8, new int[]{-8,30});
    rules[9] = new Rule(-13, new int[]{});
    rules[10] = new Rule(-13, new int[]{-13,-12});
    rules[11] = new Rule(-12, new int[]{-9,-10,25});
    rules[12] = new Rule(-12, new int[]{-9,-10,14,-13,15,25});
    rules[13] = new Rule(-9, new int[]{30});
    rules[14] = new Rule(-6, new int[]{});
    rules[15] = new Rule(-6, new int[]{-6,-2});
    rules[16] = new Rule(-2, new int[]{-5,-11,24,-3,25});
    rules[17] = new Rule(-11, new int[]{-10});
    rules[18] = new Rule(-11, new int[]{});
    rules[19] = new Rule(-10, new int[]{30});
    rules[20] = new Rule(-3, new int[]{-3,8,-3});
    rules[21] = new Rule(-3, new int[]{-3,10,-3});
    rules[22] = new Rule(-3, new int[]{-3,11,-3});
    rules[23] = new Rule(-3, new int[]{-3,9,-3});
    rules[24] = new Rule(-3, new int[]{-3,16,-3});
    rules[25] = new Rule(-3, new int[]{-3,17,-3});
    rules[26] = new Rule(-3, new int[]{18,-3});
    rules[27] = new Rule(-3, new int[]{-3,19,-3});
    rules[28] = new Rule(-3, new int[]{-3,20,-3});
    rules[29] = new Rule(-3, new int[]{-3,21,-3});
    rules[30] = new Rule(-3, new int[]{-3,22,-3});
    rules[31] = new Rule(-3, new int[]{-3,23,-3});
    rules[32] = new Rule(-3, new int[]{12,-3,13});
    rules[33] = new Rule(-3, new int[]{-5});
    rules[34] = new Rule(-3, new int[]{-4});
    rules[35] = new Rule(-5, new int[]{30});
    rules[36] = new Rule(-4, new int[]{32});
    rules[37] = new Rule(-4, new int[]{33});
    rules[38] = new Rule(-4, new int[]{31});
    rules[39] = new Rule(-17, new int[]{});
    rules[40] = new Rule(-17, new int[]{26,-18});
    rules[41] = new Rule(-18, new int[]{});
    rules[42] = new Rule(-18, new int[]{-18,-16});
    rules[43] = new Rule(-16, new int[]{-14});
    rules[44] = new Rule(-16, new int[]{-19});
    rules[45] = new Rule(-16, new int[]{-20});
    rules[46] = new Rule(-14, new int[]{-15});
    rules[47] = new Rule(-14, new int[]{-14,-15});
    rules[48] = new Rule(-15, new int[]{34});
    rules[49] = new Rule(-15, new int[]{30});
    rules[50] = new Rule(-15, new int[]{7});
    rules[51] = new Rule(-15, new int[]{12});
    rules[52] = new Rule(-15, new int[]{13});
    rules[53] = new Rule(-15, new int[]{25});
    rules[54] = new Rule(-15, new int[]{32});
    rules[55] = new Rule(-15, new int[]{33});
    rules[56] = new Rule(-15, new int[]{31});
    rules[57] = new Rule(-15, new int[]{28});
    rules[58] = new Rule(-15, new int[]{29});
    rules[59] = new Rule(-19, new int[]{27,14,30,15});
    rules[60] = new Rule(-20, new int[]{35,14,-14,15});
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
      case 3: // montage -> defActeTitle, listDeclaration, SEPARATOR, listAffectation, document
{  montage.nameOfTheMontage=ValueStack[ValueStack.Depth-5].String; montage.listOfDeclarations=ValueStack[ValueStack.Depth-4].listDeclaration; montage.listOfCalculExpressions=ValueStack[ValueStack.Depth-2].listAffectation; montage.listOfBricks=ValueStack[ValueStack.Depth-1].listBrick; }
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
{ CurrentSemanticValue.listDeclaration = new List<Declaration>(); }
        break;
      case 10: // listDeclaration -> listDeclaration, declaration
{ CurrentSemanticValue.listDeclaration=ValueStack[ValueStack.Depth-2].listDeclaration; CurrentSemanticValue.listDeclaration.Add(ValueStack[ValueStack.Depth-1].declaration); }
        break;
      case 11: // declaration -> declaredVariableName, declaredVariableType, SEMICOLON
{ CurrentSemanticValue.declaration = new Declaration(ValueStack[ValueStack.Depth-3].String, ValueStack[ValueStack.Depth-2].String); montage.IsValideTypeString(ValueStack[ValueStack.Depth-2].String,LocationStack[LocationStack.Depth-2].StartLine, LocationStack[LocationStack.Depth-2].StartColumn); montage.AddSymbole(CurrentSemanticValue.declaration); }
        break;
      case 12: // declaration -> declaredVariableName, declaredVariableType, BRACEOPEN, 
               //                listDeclaration, BRACECLOSE, SEMICOLON
{ CurrentSemanticValue.declaration = new DeclarationStruct(ValueStack[ValueStack.Depth-6].String, ValueStack[ValueStack.Depth-5].String, ValueStack[ValueStack.Depth-3].listDeclaration); montage.IsValideTypeString(ValueStack[ValueStack.Depth-5].String,LocationStack[LocationStack.Depth-5].StartLine, LocationStack[LocationStack.Depth-5].StartColumn); montage.AddSymbole(CurrentSemanticValue.declaration.GetSymboles()); }
        break;
      case 13: // declaredVariableName -> ID
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String; }
        break;
      case 14: // listAffectation -> /* empty */
{ CurrentSemanticValue.listAffectation = new List<Affectation>();  }
        break;
      case 15: // listAffectation -> listAffectation, affectation
{ CurrentSemanticValue.listAffectation=ValueStack[ValueStack.Depth-2].listAffectation; CurrentSemanticValue.listAffectation.Add(ValueStack[ValueStack.Depth-1].affectation);  }
        break;
      case 16: // affectation -> var, affectationType, ASSIGN, expression, SEMICOLON
{ CurrentSemanticValue.affectation = new Affectation(ValueStack[ValueStack.Depth-5].variable, ValueStack[ValueStack.Depth-2].expression); montage.CheckAffectationIsValid(ValueStack[ValueStack.Depth-2].expression.dataType, ValueStack[ValueStack.Depth-4].String, ValueStack[ValueStack.Depth-5].variable.name ,LocationStack[LocationStack.Depth-5].StartLine);  montage.AddSymbole(CurrentSemanticValue.affectation);  }
        break;
      case 17: // affectationType -> declaredVariableType
{ CurrentLocationSpan=LocationStack[LocationStack.Depth-1]; CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String; }
        break;
      case 18: // affectationType -> /* empty */
{ CurrentSemanticValue.String=null; }
        break;
      case 19: // declaredVariableType -> ID
{ CurrentLocationSpan=LocationStack[LocationStack.Depth-1]; CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String.ToUpper(); }
        break;
      case 20: // expression -> expression, PLUS, expression
{ /*Console.WriteLine("PLUS");*/	CurrentSemanticValue.expression = new Expression(ExpressionSymbole.PLUS, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 21: // expression -> expression, MUL, expression
{ /*Console.WriteLine("MUL");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.MUL, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression);}
        break;
      case 22: // expression -> expression, DIV, expression
{ /*Console.WriteLine("DIV");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.DIV, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 23: // expression -> expression, MINUS, expression
{ /*Console.WriteLine("MINUS");*/	CurrentSemanticValue.expression = new Expression(ExpressionSymbole.MINUS, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 24: // expression -> expression, AND, expression
{ /*Console.WriteLine("AND");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.AND, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 25: // expression -> expression, OR, expression
{ /*Console.WriteLine("OR");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.OR, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 26: // expression -> NOT, expression
{ /*Console.WriteLine("NOT");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.NOT, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 27: // expression -> expression, EGALE, expression
{ /*Console.WriteLine("EGALE");*/	CurrentSemanticValue.expression = new Expression(ExpressionSymbole.EGALE, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 28: // expression -> expression, INF, expression
{ /*Console.WriteLine("INF");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.INF, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 29: // expression -> expression, INFEGALE, expression
{ /*Console.WriteLine("INFEGALE");*/ CurrentSemanticValue.expression = new Expression(ExpressionSymbole.INFEGALE, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 30: // expression -> expression, SUP, expression
{ /*Console.WriteLine("SUP");*/		CurrentSemanticValue.expression = new Expression(ExpressionSymbole.SUP, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 31: // expression -> expression, SUPEGALE, expression
{ /*Console.WriteLine("SUPEGALE");*/ CurrentSemanticValue.expression = new Expression(ExpressionSymbole.SUPEGALE, ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].expression); }
        break;
      case 32: // expression -> PARENTOPEN, expression, PARENTCLOSE
{ /*Console.WriteLine("PARENT");*/	CurrentSemanticValue.expression = new Expression(ExpressionSymbole.PARENT, ValueStack[ValueStack.Depth-2].expression); }
        break;
      case 33: // expression -> var
{ /*Console.WriteLine("var :" +$1 );*/ montage.IsInSymboleTable(ValueStack[ValueStack.Depth-1].variable.name, LocationStack[LocationStack.Depth-1].StartLine, LocationStack[LocationStack.Depth-1].StartColumn ); CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].variable; }
        break;
      case 34: // expression -> constante
{ /*Console.WriteLine("constante");*/  CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].constante; }
        break;
      case 35: // var -> ID
{ CurrentLocationSpan = LocationStack[LocationStack.Depth-1]; CurrentSemanticValue.variable = new VariableId(ValueStack[ValueStack.Depth-1].String, montage.GetVarTypeString(ValueStack[ValueStack.Depth-1].String)); }
        break;
      case 36: // constante -> INTEGER
{ CurrentLocationSpan = LocationStack[LocationStack.Depth-1]; /*Console.WriteLine("int :" + $1 );*/		CurrentSemanticValue.constante = new VariableInteger(ValueStack[ValueStack.Depth-1].Integer);}
        break;
      case 37: // constante -> FLOAT
{ CurrentLocationSpan = LocationStack[LocationStack.Depth-1]; /*Console.WriteLine("float :" + $1 );*/		CurrentSemanticValue.constante = new VariableFloat(ValueStack[ValueStack.Depth-1].Float);}
        break;
      case 38: // constante -> STRING
{ CurrentLocationSpan = LocationStack[LocationStack.Depth-1]; /*Console.WriteLine("string :" + $1 );*/		CurrentSemanticValue.constante = new VariableString(ValueStack[ValueStack.Depth-1].String);}
        break;
      case 39: // document -> /* empty */
{ CurrentSemanticValue.listBrick=null; }
        break;
      case 40: // document -> SEPARATOR, listBrick
{ CurrentSemanticValue.listBrick = ValueStack[ValueStack.Depth-1].listBrick; }
        break;
      case 41: // listBrick -> /* empty */
{ CurrentSemanticValue.listBrick = new List<Brick>();  }
        break;
      case 42: // listBrick -> listBrick, brick
{ CurrentSemanticValue.listBrick = ValueStack[ValueStack.Depth-2].listBrick; CurrentSemanticValue.listBrick.Add(ValueStack[ValueStack.Depth-1].brick); }
        break;
      case 43: // brick -> textBloc
{ CurrentSemanticValue.brick = new DeadText(ValueStack[ValueStack.Depth-1].String, montage.paragraphOpen); montage.paragraphOpen = true;  Console.WriteLine("textBloc op " + montage.paragraphOpen ); }
        break;
      case 44: // brick -> callVar
{ CurrentSemanticValue.brick = ValueStack[ValueStack.Depth-1].variableCall; }
        break;
      case 45: // brick -> title
{ CurrentSemanticValue.brick = ValueStack[ValueStack.Depth-1].Title; montage.paragraphOpen = false; }
        break;
      case 46: // textBloc -> textBlocElement
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String; }
        break;
      case 47: // textBloc -> textBloc, textBlocElement
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-2].String; CurrentSemanticValue.String += (" " + ValueStack[ValueStack.Depth-1].String); }
        break;
      case 48: // textBlocElement -> DEADWORD
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String; }
        break;
      case 49: // textBlocElement -> ID
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String; }
        break;
      case 50: // textBlocElement -> DOLLAR
{ CurrentSemanticValue.String = "$"; }
        break;
      case 51: // textBlocElement -> PARENTOPEN
{ CurrentSemanticValue.String = "("; }
        break;
      case 52: // textBlocElement -> PARENTCLOSE
{ CurrentSemanticValue.String = ")"; }
        break;
      case 53: // textBlocElement -> SEMICOLON
{ CurrentSemanticValue.String = ";"; }
        break;
      case 54: // textBlocElement -> INTEGER
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].Integer.ToString(); }
        break;
      case 55: // textBlocElement -> FLOAT
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].Float.ToString(); }
        break;
      case 56: // textBlocElement -> STRING
{ CurrentSemanticValue.String = ValueStack[ValueStack.Depth-1].String; }
        break;
      case 57: // textBlocElement -> NOUVLIGNE
{ CurrentSemanticValue.String = "$nouvligne"; }
        break;
      case 58: // textBlocElement -> NOUVPARAG
{ CurrentSemanticValue.String = "$nouvparag"; }
        break;
      case 59: // callVar -> CODEINDIC, BRACEOPEN, ID, BRACECLOSE
{ CurrentSemanticValue.variableCall = new VariableCall(ValueStack[ValueStack.Depth-2].String, montage.isLocalVar(ValueStack[ValueStack.Depth-2].String, LocationStack[LocationStack.Depth-2].StartLine, LocationStack[LocationStack.Depth-2].StartColumn), montage.GetVarTypeString(ValueStack[ValueStack.Depth-2].String)); }
        break;
      case 60: // title -> TITLEID, BRACEOPEN, textBloc, BRACECLOSE
{  CurrentSemanticValue.Title = new Title(ValueStack[ValueStack.Depth-4].String, ValueStack[ValueStack.Depth-2].String, montage.paragraphOpen); }
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
