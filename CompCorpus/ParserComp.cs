// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  FIDF3675368
// DateTime: 07/06/2017 09:17:09
// UserName: j.folleas
// Input file <ParserComp.y - 07/06/2017 09:16:36>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace Analyser
{
public enum Tokens {error=2,EOF=3,TRUE=4,FALSE=5,DOLLAR=6,
    PLUS=7,MINUS=8,MUL=9,DIV=10,PARENTOUV=11,PARENTFERM=12,
    AND=13,OR=14,NOT=15,EGALE=16,INF=17,INFEGALE=18,
    SUP=19,SUPEGALE=20,ASSIGN=21,ID=22,STRING=23,INTEGER=24,
    FLOAT=25};

public struct ValueType
{
        public string String;
        public long Integer;
        public double Float;
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
  // Verbatim content from ParserComp.y - 07/06/2017 09:16:36
    
    
  // End verbatim content from ParserComp.y - 07/06/2017 09:16:36

#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[23];
  private static State[] states = new State[37];
  private static string[] nonTerms = new string[] {
      "program", "$accept", "expression", "var", "constante", };

  static Parser() {
    states[0] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36,3,-2},new int[]{-1,1,-3,3,-4,31,-5,33});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{7,4,9,6,10,8,8,10,13,12,14,14,16,16,17,18,18,20,19,22,20,24,3,-3});
    states[4] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,5,-4,31,-5,33});
    states[5] = new State(new int[]{7,-4,9,6,10,8,8,-4,13,-4,14,-4,16,-4,17,-4,18,-4,19,-4,20,-4,3,-4,12,-4});
    states[6] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,7,-4,31,-5,33});
    states[7] = new State(-5);
    states[8] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,9,-4,31,-5,33});
    states[9] = new State(-6);
    states[10] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,11,-4,31,-5,33});
    states[11] = new State(new int[]{7,-7,9,6,10,8,8,-7,13,-7,14,-7,16,-7,17,-7,18,-7,19,-7,20,-7,3,-7,12,-7});
    states[12] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,13,-4,31,-5,33});
    states[13] = new State(new int[]{7,4,9,6,10,8,8,10,13,-8,14,-8,16,16,17,18,18,20,19,22,20,24,3,-8,12,-8});
    states[14] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,15,-4,31,-5,33});
    states[15] = new State(new int[]{7,4,9,6,10,8,8,10,13,-9,14,-9,16,16,17,18,18,20,19,22,20,24,3,-9,12,-9});
    states[16] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,17,-4,31,-5,33});
    states[17] = new State(new int[]{7,4,9,6,10,8,8,10,13,-11,14,-11,16,-11,17,-11,18,-11,19,-11,20,-11,3,-11,12,-11});
    states[18] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,19,-4,31,-5,33});
    states[19] = new State(new int[]{7,4,9,6,10,8,8,10,13,-12,14,-12,16,-12,17,-12,18,-12,19,-12,20,-12,3,-12,12,-12});
    states[20] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,21,-4,31,-5,33});
    states[21] = new State(new int[]{7,4,9,6,10,8,8,10,13,-13,14,-13,16,-13,17,-13,18,-13,19,-13,20,-13,3,-13,12,-13});
    states[22] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,23,-4,31,-5,33});
    states[23] = new State(new int[]{7,4,9,6,10,8,8,10,13,-14,14,-14,16,-14,17,-14,18,-14,19,-14,20,-14,3,-14,12,-14});
    states[24] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,25,-4,31,-5,33});
    states[25] = new State(new int[]{7,4,9,6,10,8,8,10,13,-15,14,-15,16,-15,17,-15,18,-15,19,-15,20,-15,3,-15,12,-15});
    states[26] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,27,-4,31,-5,33});
    states[27] = new State(new int[]{7,4,9,6,10,8,8,10,13,-10,14,-10,16,16,17,18,18,20,19,22,20,24,3,-10,12,-10});
    states[28] = new State(new int[]{15,26,11,28,22,32,24,34,25,35,23,36},new int[]{-3,29,-4,31,-5,33});
    states[29] = new State(new int[]{12,30,7,4,9,6,10,8,8,10,13,12,14,14,16,16,17,18,18,20,19,22,20,24});
    states[30] = new State(-16);
    states[31] = new State(-17);
    states[32] = new State(-19);
    states[33] = new State(-18);
    states[34] = new State(-20);
    states[35] = new State(-21);
    states[36] = new State(-22);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{});
    rules[3] = new Rule(-1, new int[]{-3});
    rules[4] = new Rule(-3, new int[]{-3,7,-3});
    rules[5] = new Rule(-3, new int[]{-3,9,-3});
    rules[6] = new Rule(-3, new int[]{-3,10,-3});
    rules[7] = new Rule(-3, new int[]{-3,8,-3});
    rules[8] = new Rule(-3, new int[]{-3,13,-3});
    rules[9] = new Rule(-3, new int[]{-3,14,-3});
    rules[10] = new Rule(-3, new int[]{15,-3});
    rules[11] = new Rule(-3, new int[]{-3,16,-3});
    rules[12] = new Rule(-3, new int[]{-3,17,-3});
    rules[13] = new Rule(-3, new int[]{-3,18,-3});
    rules[14] = new Rule(-3, new int[]{-3,19,-3});
    rules[15] = new Rule(-3, new int[]{-3,20,-3});
    rules[16] = new Rule(-3, new int[]{11,-3,12});
    rules[17] = new Rule(-3, new int[]{-4});
    rules[18] = new Rule(-3, new int[]{-5});
    rules[19] = new Rule(-4, new int[]{22});
    rules[20] = new Rule(-5, new int[]{24});
    rules[21] = new Rule(-5, new int[]{25});
    rules[22] = new Rule(-5, new int[]{23});
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
      case 2: // program -> /* empty */
{ Console.WriteLine("empty Prgm"); }
        break;
      case 3: // program -> expression
{ Console.WriteLine("statement"); }
        break;
      case 4: // expression -> expression, PLUS, expression
{ Console.WriteLine("PLUS"); }
        break;
      case 5: // expression -> expression, MUL, expression
{ Console.WriteLine("MUL"); }
        break;
      case 6: // expression -> expression, DIV, expression
{ Console.WriteLine("DIV"); }
        break;
      case 7: // expression -> expression, MINUS, expression
{ Console.WriteLine("MINUS"); }
        break;
      case 8: // expression -> expression, AND, expression
{ Console.WriteLine("AND"); }
        break;
      case 9: // expression -> expression, OR, expression
{ Console.WriteLine("OR"); }
        break;
      case 10: // expression -> NOT, expression
{ Console.WriteLine("NOT");}
        break;
      case 11: // expression -> expression, EGALE, expression
{ Console.WriteLine("EGALE");}
        break;
      case 12: // expression -> expression, INF, expression
{ Console.WriteLine("INF");}
        break;
      case 13: // expression -> expression, INFEGALE, expression
{ Console.WriteLine("INFEGALE");}
        break;
      case 14: // expression -> expression, SUP, expression
{ Console.WriteLine("SUP");}
        break;
      case 15: // expression -> expression, SUPEGALE, expression
{ Console.WriteLine("SUPEGALE");}
        break;
      case 16: // expression -> PARENTOUV, expression, PARENTFERM
{ Console.WriteLine("PARENT");}
        break;
      case 17: // expression -> var
{ Console.WriteLine("var"); }
        break;
      case 18: // expression -> constante
{ Console.WriteLine("constante"); }
        break;
      case 19: // var -> ID
{ Console.WriteLine("var :" +ValueStack[ValueStack.Depth-1].String ); }
        break;
      case 20: // constante -> INTEGER
{ Console.WriteLine("int :" + ValueStack[ValueStack.Depth-1].Integer ); }
        break;
      case 21: // constante -> FLOAT
{ Console.WriteLine("float :" + ValueStack[ValueStack.Depth-1].Float );}
        break;
      case 22: // constante -> STRING
{ Console.WriteLine("string :" + ValueStack[ValueStack.Depth-1].String );}
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
