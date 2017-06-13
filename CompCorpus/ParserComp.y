%using RunTime;

%namespace Analyser
%output=ParserComp.cs

%{
    
    public Montage montage = new Montage();

%}

%start montage

%union {
        public string String;
        public long Integer;
        public double Float;
		public Affectation affectation;
		public AbstractExpression expression;
		public Variable constante;
		public VariableId variable ;
		public List<Affectation> listAffectation ;
}

// Defining Tokens

%token TITREACTEKW

%token TRUE FALSE DOLLAR

%token PLUS MINUS MUL DIV

%token PARENTOPEN PARENTCLOSE BRACEOPEN BRACECLOSE

%token AND OR NOT
%token EGALE INF INFEGALE SUP SUPEGALE

%token ASSIGN

%token SEMICOLON

%token SEPARATOR

%token <String> ID
%token <String> STRING
%token <Integer> INTEGER
%token <Float> FLOAT
%token <String> DEADWORD

//types

%type<expression> montage
%type<affectation> affectation
%type<expression> expression
%type<constante> constante
%type<variable> var
%type<listAffectation> listAffectation
%type<String> defActeTitle
%type<String> deadText
%type<String> declaredVariableName
%type<String> declaredVariableType

// Priority

%left AND OR 
%left NOT
%left EGALE INF INFEGALE SUP SUPEGALE


%left PLUS MINUS
%left DIV MUL
%left ID


%% // Grammar rules section

montage		:	  /*Empty*/													{ Console.WriteLine(" Empty programe "); }
			|	defActeTitle listDeclaration SEPARATOR listAffectation		{  montage.nameOfTheMontage=$1; montage.listOfCalculExpressions=$4; }
			;

defActeTitle	:	TITREACTEKW BRACEOPEN deadText BRACECLOSE		{ $$ = $3; }
				;

deadText		:	DEADWORD						{ $$ = $1; }
				|	ID								{ $$ = $1; }
				|	deadText DEADWORD				{ $$ = $1 + " " + $2; }
				|	deadText ID						{ $$ = $1 + " " + $2; }
				;

listDeclaration :	/*Empty*/						{ Console.WriteLine("Empty Dec");  }
				|   listDeclaration declaration		{ Console.WriteLine("declaration");  }
				;

declaration		:	declaredVariableName  declaredVariableType SEMICOLON				{Console.WriteLine(" DEC :" + $1 + " " + $2 + " ;"); }
				;

declaredVariableName	:	ID		{ $$ = $1; }
						;

declaredVariableType	:	ID		{ $$ = $1; }
						;

							

listAffectation :	/*Empty*/						{ $$ = new List<Affectation>();  }
				|   listAffectation affectation		{ $$=$1; $$.Add($2);  }
				;

affectation	:		var ASSIGN expression SEMICOLON				{ $3.CheckValidity(@1.StartLine); montage.AddSymbole($1.name, ("L" + $3.dataType.ToString())); $$ = new Affectation($1, $3); }
					;

expression  :       expression PLUS expression			{ /*Console.WriteLine("PLUS");*/	$$ = new Expression(ExpressionSymbole.PLUS, $1, $3); }
            |       expression MUL expression			{ /*Console.WriteLine("MUL");*/		$$ = new Expression(ExpressionSymbole.MUL, $1, $3);}
            |       expression DIV expression			{ /*Console.WriteLine("DIV");*/		$$ = new Expression(ExpressionSymbole.DIV, $1, $3); }
            |       expression MINUS expression			{ /*Console.WriteLine("MINUS");*/	$$ = new Expression(ExpressionSymbole.MINUS, $1, $3); }
			|       expression AND expression			{ /*Console.WriteLine("AND");*/		$$ = new Expression(ExpressionSymbole.AND, $1, $3); }
            |       expression OR expression			{ /*Console.WriteLine("OR");*/		$$ = new Expression(ExpressionSymbole.OR, $1, $3); }
            |       NOT expression						{ /*Console.WriteLine("NOT");*/		$$ = new Expression(ExpressionSymbole.NOT, $2); }
            |       expression EGALE expression			{ /*Console.WriteLine("EGALE");*/	$$ = new Expression(ExpressionSymbole.EGALE, $1, $3); }
            |       expression INF expression			{ /*Console.WriteLine("INF");*/		$$ = new Expression(ExpressionSymbole.INF, $1, $3); }
            |       expression INFEGALE expression		{ /*Console.WriteLine("INFEGALE");*/ $$ = new Expression(ExpressionSymbole.INFEGALE, $1, $3); }
            |       expression SUP expression			{ /*Console.WriteLine("SUP");*/		$$ = new Expression(ExpressionSymbole.SUP, $1, $3); }
            |       expression SUPEGALE expression		{ /*Console.WriteLine("SUPEGALE");*/ $$ = new Expression(ExpressionSymbole.SUPEGALE, $1, $3); }
			|		PARENTOPEN expression PARENTCLOSE	{ /*Console.WriteLine("PARENT");*/	$$ = new Expression(ExpressionSymbole.PARENT, $2); }
            |       var									{ /*Console.WriteLine("var :" +$1 );*/ montage.IsInSymboleTable($1.name, @1.StartLine, @1.StartColumn ); $$ = $1; }
            |       constante							{ /*Console.WriteLine("constante");*/  $$ = $1; }
            ;

var     :   ID      { @$ = @1; $$ = new VariableId($1, montage.GetVarTypeString($1)); }
        ;

constante   :   INTEGER		{ @$ = @1; /*Console.WriteLine("int :" + $1 );*/		$$ = new VariableInteger($1);}
			|	FLOAT		{ @$ = @1; /*Console.WriteLine("float :" + $1 );*/		$$ = new VariableFloat($1);}
			|	STRING		{ @$ = @1; /*Console.WriteLine("string :" + $1 );*/		$$ = new VariableString($1);}
            ;

%%

// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) {}

