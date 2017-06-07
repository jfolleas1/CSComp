%using RunTime;

%namespace Analyser
%output=ParserComp.cs

%{
    
    public AbstractExpression program = null;
%}

%start program

%union {
        public string String;
        public long Integer;
        public double Float;
		public AbstractExpression expression;
		public Variable constante;
		public Variable variable ;	
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

//types

%type<expression> program
%type<expression> expression
%type<constante> constante
%type<variable> var

// Priority

%left AND OR 
%left NOT
%left EGALE INF INFEGALE SUP SUPEGALE


%left PLUS MINUS
%left DIV MUL


%% // Grammar rules section

program  : /* nothing */    { Console.WriteLine("empty Prgm"); }
         | expression       { program = $1; }
         ;

expression  :       expression PLUS expression      { Console.WriteLine("PLUS"); $$ = new Expression(ExpressionSymbole.PLUS, $1, $3); }
            |       expression MUL expression       { Console.WriteLine("MUL"); $$ = new Expression(ExpressionSymbole.MUL, $1, $3);}
            |       expression DIV expression       { Console.WriteLine("DIV"); $$ = new Expression(ExpressionSymbole.DIV, $1, $3); }
            |       expression MINUS expression     { Console.WriteLine("MINUS"); $$ = new Expression(ExpressionSymbole.MINUS, $1, $3); }
			|       expression AND expression		{ Console.WriteLine("AND"); $$ = new Expression(ExpressionSymbole.AND, $1, $3); }
            |       expression OR expression		{ Console.WriteLine("OR"); $$ = new Expression(ExpressionSymbole.OR, $1, $3); }
            |       NOT expression					{ Console.WriteLine("NOT");$$ = new Expression(ExpressionSymbole.NOT, $2); }
            |       expression EGALE expression		{ Console.WriteLine("EGALE");$$ = new Expression(ExpressionSymbole.EGALE, $1, $3); }
            |       expression INF expression		{ Console.WriteLine("INF");$$ = new Expression(ExpressionSymbole.INF, $1, $3); }
            |       expression INFEGALE expression	{ Console.WriteLine("INFEGALE");$$ = new Expression(ExpressionSymbole.INFEGALE, $1, $3); }
            |       expression SUP expression		{ Console.WriteLine("SUP");$$ = new Expression(ExpressionSymbole.SUP, $1, $3); }
            |       expression SUPEGALE expression	{ Console.WriteLine("SUPEGALE");$$ = new Expression(ExpressionSymbole.SUPEGALE, $1, $3); }
			|		PARENTOUV expression PARENTFERM	{ Console.WriteLine("PARENT"); $$ = new Expression(ExpressionSymbole.PARENT, $2); }
            |       var                             { Console.WriteLine("var"); $$ = $1; }
            |       constante                       { Console.WriteLine("constante");  $$ = $1; }
            ;

var     :   ID      { Console.WriteLine("var :" +$1 ); $$ = new VariableId($1); }
        ;

constante   :   INTEGER		{ Console.WriteLine("int :" + $1 ); $$ = new VariableInteger($1);}
			|	FLOAT		{ Console.WriteLine("float :" + $1 ); $$ = new VariableFloat($1);}
			|	STRING		{ Console.WriteLine("string :" + $1 ); $$ = new VariableString($1);}
            ;

%%

// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) {}

