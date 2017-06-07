
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
%token TRUE FALSE DOLLAR

%token PLUS MINUS MUL DIV

%token PARENTOUV PARENTFERM

%token AND OR NOT
%token EGALE INF INFEGALE SUP SUPEGALE

%token ASSIGN


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
			|       expression AND expression		{ Console.WriteLine("AND"); }
            |       expression OR expression		{ Console.WriteLine("OR"); }
            |       NOT expression					{ Console.WriteLine("NOT");}
            |       expression EGALE expression		{ Console.WriteLine("EGALE");}
            |       expression INF expression		{ Console.WriteLine("INF");}
            |       expression INFEGALE expression	{ Console.WriteLine("INFEGALE");}
            |       expression SUP expression		{ Console.WriteLine("SUP");}
            |       expression SUPEGALE expression	{ Console.WriteLine("SUPEGALE");}
			|		PARENTOUV expression PARENTFERM	{ Console.WriteLine("PARENT");}
            |       var                             { Console.WriteLine("var"); }
            |       constante                       { Console.WriteLine("constante"); }
            ;

var     :   ID      { Console.WriteLine("var :" +$1 ); }
        ;

constante   :   INTEGER		{ Console.WriteLine("int :" + $1 ); }
			|	FLOAT		{ Console.WriteLine("float :" + $1 );}
			|	STRING		{ Console.WriteLine("string :" + $1 );}
            ;

%%

// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) {}

