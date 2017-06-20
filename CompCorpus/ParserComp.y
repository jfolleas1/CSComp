%using CompCorpus.RunTime;
%using CompCorpus.RunTime.declaration;
%using CompCorpus.RunTime.Bricks;

%namespace CompCorpus.Analyzer
%output=Analyzer\ParserComp.cs

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
		public Declaration declaration ;
		public List<Declaration> listDeclaration ;
		public List<Affectation> listAffectation ;
		public Brick brick ;
		public List<Brick> listBrick ;
		public VariableCall variableCall;
		public Title Title;
		public Proposition proposition;
		public List<Proposition> listProposition;
		public Choice choice;

}

// Defining Tokens

%token TITREACTEKW

%token TRUE FALSE DOLLAR

%token PLUS MINUS MUL DIV

%token PARENTOPEN PARENTCLOSE BRACEOPEN BRACECLOSE

%token AND OR NOT
%token EGALE INF INFEGALE SUP SUPEGALE

%token ASSIGN

%token SEMICOLON COMMA

%token SEPARATOR
%token CODEINDIC
%token NOUVLIGNE NOUVPARAG

%token CHOIXCKW

%token <String> ID
%token <String> STRING
%token <Integer> INTEGER
%token <Float> FLOAT
%token <String> DEADWORD
%token <String> TITLEID

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
%type<String> affectationType
%type<declaration> declaration
%type<listDeclaration> listDeclaration

%type<String> textBloc
%type<String> textBlocElement

%type<brick> brick
%type<listBrick> document
%type<listBrick> listBrick


%type<variableCall> callVar


%type<Title> title
%type<String> titleContent
%type<String> titleContentElement


%type<choice> choice
%type<listProposition> listProposition
%type<proposition> proposition


// Priority

%left AND OR 
%left NOT
%left EGALE INF INFEGALE SUP SUPEGALE


%left PLUS MINUS
%left DIV MUL
%left ID


%% // Grammar rules section

montage		:	  /*Empty*/																{ Console.WriteLine(" Empty programe "); }
			|	defActeTitle listDeclaration SEPARATOR listAffectation document 		{  montage.nameOfTheMontage=$1; montage.listOfDeclarations=$2; montage.listOfCalculExpressions=$4; montage.listOfBricks=$5; }
			;

defActeTitle	:	TITREACTEKW BRACEOPEN deadText BRACECLOSE		{ $$ = $3; }
				;

deadText		:	DEADWORD						{ $$ = $1; }
				|	ID								{ $$ = $1; }
				|	deadText DEADWORD				{ $$ = $1 + " " + $2; }
				|	deadText ID						{ $$ = $1 + " " + $2; }
				;

listDeclaration :	/*Empty*/						{ $$ = new List<Declaration>(); }
				|   listDeclaration declaration		{ $$=$1; $$.Add($2); }
				;

declaration		:	declaredVariableName  declaredVariableType SEMICOLON											{ $$ = new Declaration($1, $2); montage.IsValideTypeString($2,@2.StartLine, @2.StartColumn); montage.AddSymbole($$); }
				|	declaredVariableName declaredVariableType BRACEOPEN listDeclaration BRACECLOSE SEMICOLON		{ $$ = new DeclarationStruct($1, $2, $4); montage.IsValideTypeString($2,@2.StartLine, @2.StartColumn); montage.AddSymbole($$.GetSymboles()); }
				;

declaredVariableName	:	ID		{ $$ = $1; }
						;



							

listAffectation :	/*Empty*/						{ $$ = new List<Affectation>();  }
				|   listAffectation affectation		{ $$=$1; $$.Add($2);  }
				;

affectation	:		var affectationType ASSIGN expression SEMICOLON				{ $$ = new Affectation($1, $4); montage.CheckAffectationIsValid($4.dataType, $2, $1.name ,@1.StartLine);  montage.AddSymbole($$);  }
			;

affectationType			:	declaredVariableType		{ @$=@1; $$ = $1; }
						| /*Empty*/						{ $$=null; }
						;

declaredVariableType	:	ID		{ @$=@1; $$ = $1.ToUpper(); }
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







document	: /* Empty */					{ $$=new List<Brick>(); }
			| SEPARATOR listBrick			{ $$ = $2; }
			;

listBrick	:  /* Empty */			{ $$ = new List<Brick>();  }
			| listBrick brick		{ $$ = $1; $$.Add($2); }
			;

brick		: textBloc				{ $$ = new DeadText($1, montage.paragraphOpen); montage.paragraphOpen = true; }
			| callVar				{ $$ = $1; }
			| title					{ $$ = $1; montage.paragraphOpen = false; }
			| choice				{ $$ = new DeadText(" Choix , voir la sortie de console ", montage.paragraphOpen); montage.paragraphOpen = true;}
			;


textBloc	: textBlocElement				{ $$ = $1; }
			| textBloc textBlocElement		{ $$ = $1; $$ += (" " + $2); }
			;

textBlocElement		: DEADWORD		{ $$ = $1; }
					| ID			{ $$ = $1; }
					| DOLLAR		{ $$ = "$"; }
					| PARENTOPEN	{ $$ = "("; }
					| PARENTCLOSE	{ $$ = ")"; }
					| SEMICOLON		{ $$ = ";"; }
					| COMMA			{ $$ = ","; }
					| INTEGER		{ $$ = $1.ToString(); }
					| FLOAT			{ $$ = $1.ToString(); }
					| STRING		{ $$ = $1; }
					| NOUVLIGNE		{ $$ = "$nouvligne"; }
					| NOUVPARAG		{ $$ = "$nouvparag"; }
					;


title		: TITLEID BRACEOPEN titleContent BRACECLOSE				{  $$ = new Title($1, $3, montage.paragraphOpen); }
			;

titleContent	: titleContentElement					{ $$ = $1; }
				| titleContent titleContentElement		{ $$ = $1; $$ += (" " + $2); }
				;

titleContentElement		: DEADWORD		{ $$ = $1; }
						| ID			{ $$ = $1; }
						| DOLLAR		{ $$ = "$"; }
						| PARENTOPEN	{ $$ = "("; }
						| PARENTCLOSE	{ $$ = ")"; }
						| SEMICOLON		{ $$ = ";"; }
						| COMMA			{ $$ = ","; }
						| INTEGER		{ $$ = $1.ToString(); }
						| FLOAT			{ $$ = $1.ToString(); }
						| STRING		{ $$ = $1; }
						| callVar		{ $$ = $1.Write(); }
						;
	

callVar		: CODEINDIC BRACEOPEN ID BRACECLOSE			{ $$ = new VariableCall($3, montage.isLocalVar($3, @3.StartLine, @3.StartColumn), montage.GetVarTypeString($3)); }
			;


choice		: CHOIXCKW PARENTOPEN ID COMMA STRING PARENTCLOSE BRACEOPEN listProposition BRACECLOSE		{ $$ = new Choice($3, $5, $8); $$.Print(); }
			;

listProposition		: proposition							{ $$ = new List<Proposition>(); $$.Add($1); }
					| listProposition proposition			{ $$ = $1; $$.Add($2); }
					;

proposition			: PARENTOPEN STRING PARENTCLOSE BRACEOPEN listBrick BRACECLOSE			{ $$ = new Proposition($2, $5); }
					;

%%

// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) {}

