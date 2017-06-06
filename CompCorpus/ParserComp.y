
%namespace Analyser
%output=ParserComp.cs

%{
    
    
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
%token TRUE FALSE
%token AND OR NOT
%token EGALE INF INFEGALE SUP SUPEGALE
%token ASSIGN
%token DOLLAR

%token <String> ID
%token <String> STRING
%token <Integer> INTEGER
%token <Float> FLOAT


// Priority
%left AND OR 
%left NOT
%left EGALE INF INFEGALE SUP SUPEGALE
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
			|       expression AND expression		{}
            |       expression OR expression		{}
            |       NOT expression					{}
            |       expression EGALE expression		{}
            |       expression INF expression		{}
            |       expression INFEGALE expression	{}
            |       expression SUP expression		{}
            |       expression SUPEGALE expression	{}
            |       var                             { Console.WriteLine("var"); }
            |       constante                       { Console.WriteLine("constante"); }
            ;

var     :   ID      { Console.WriteLine("id :" +$1 ); }
        ;

constante   :   INTEGER   { Console.WriteLine("int :" + $1 ); }
            ;

%%

// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) {}

