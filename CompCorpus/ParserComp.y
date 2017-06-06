%namespace Analyser
%output=ParserComp.cs

%{
    using System;
    namespace Analyser;
%}

%start program

%union {
        public string String;
        public long Integer;
        public double Float;
}

// Defining Tokens
%token PLUS
%token MINUS
%token MUL
%token DIV

%token <String> ID
%token <String> STRING
%token <String> INTEGER
%token <String> FLOAT


// Priority
%left PLUS MINUS
%left DIV MUL

%% // Grammar rules section

program  : /* nothing */    { Console.WriteLine("empty Prgm"); }
         | expression       { Console.WriteLine("statement"); }
         ;

expression  :       expression PLUS expression      { Console.WriteLine("PLUS"); }
            |       expression MUL expression       { Console.WriteLine("MUL"); }
            |       expression DIV expression       { Console.WriteLine("DIV"); }
            |       expression MINUS expression     { Console.WriteLine("MINUS"); }
            |       var                             { Console.WriteLine("var"); }
            |       constante                       { Console.WriteLine("constante"); }
            ;

var     :   ID      { Console.WriteLine("id" + $1 ); }
        ;

constante   :   INTEGER   { Console.WriteLine("int" + $1 ); }
            ;

%%

// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) {}

