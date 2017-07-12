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
		public List<KeyValuePair<long, List<Brick>>> listCel ;
		public List<List<KeyValuePair<long, List<Brick>>>> listRow ;
		public Table Table;
		public VariableCall variableCall;
		public Title Title;
		public Proposition proposition;
		public List<Proposition> listProposition;
		public Choice choice;
		public Option option;
		public Condition condition;
		public Iteration iteration;
		public IteratorStr iterator;
		public StructDecNameAndType decNameAndType;
		public Include Include;

}

// Defining Tokens

%token TITREACTEKW

%token TRUE FALSE DOLLAR

%token PLUS MINUS MUL DIV

%token PARENTOPEN PARENTCLOSE BRACEOPEN BRACECLOSE

%token AND OR NOT
%token EGALE INF INFEGALE SUP SUPEGALE NOTEGALE

%token ASSIGN

%token SEMICOLON COMMA COLON

%token SEPARATOR
%token CODEINDIC
%token NOUVLIGNE NOUVPARAG

%token CHOIXCKW OPTIONCKW CONDITIONCKW POURCHAQUECKW IMPLIQUECKW TABCKW INCLUDECKW
%token DOUBLECOTE

%token <String> ID
%token <String> STRING
%token <Integer> INTEGER
%token <Float> FLOAT
%token <String> DEADWORD
%token <String> TITLEID

//types

%type<expression> montage
%type<affectation> affectation
%type<affectation> affectationWithCond
%type<expression> expression
%type<constante> constante
%type<variable> var
%type<listAffectation> listAffectation
%type<listAffectation> listAffectationBis
%type<listAffectation> listAffectationWithCond
%type<String> defActeTitle
%type<String> deadText
%type<String> declaredVariableName
%type<String> declaredVariableType
%type<decNameAndType> decNameAndType
%type<String> affectationType
%type<declaration> declaration
%type<listDeclaration> listDeclaration
%type<declaration> inStructDeclaration
%type<listDeclaration> inStructListDeclaration

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

%type<option> option

%type<condition> condition

%type<iteration> iteration
%type<iterator> iterator

%type<listAffectation> implication

%type<Table> table
%type<listRow> listTabRow
%type<listCel> listTabCel

%type<Include> include 

// Priority

%left AND OR 
%left NOT
%left EGALE INF INFEGALE SUP SUPEGALE NOTEGALE


%left PLUS MINUS
%left DIV MUL
%left ID


%% // Grammar rules section

montage		:	/*Empty*/																{ Console.WriteLine(" Empty programe "); }
			|	defActeTitle listDeclaration SEPARATOR listAffectation document 		{  montage.nameOfTheMontage=$1; montage.listOfDeclarations.AddRange($2); montage.listOfBricks=$5; }
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

declaration		:	declaredVariableName declaredVariableType SEMICOLON													{ $$ = new Declaration($1, $2); montage.IsValideTypeString($2,@2.StartLine, @2.StartColumn); montage.AddSymbole($$); }
				|	decNameAndType BRACEOPEN inStructListDeclaration BRACECLOSE SEMICOLON								{ $$ = new DeclarationStruct($1.name, $1.type, $3); montage.IsValideTypeStructString($1.type,@1.StartLine, @1.StartColumn); montage.AddSymbole($$.GetSymboles()); 
																														  montage.AddFunctionForList($$); DeclarationStruct.PopContexte(); }
				;

inStructListDeclaration :	/*Empty*/										{ $$ = new List<Declaration>(); }
						|   inStructListDeclaration inStructDeclaration		{ $$=$1; $$.Add($2); }
						;

inStructDeclaration		:	declaredVariableName declaredVariableType SEMICOLON													{ $$ = new Declaration($1, $2); montage.IsValideTypeString($2,@2.StartLine, @2.StartColumn);  }
						|	decNameAndType BRACEOPEN inStructListDeclaration BRACECLOSE SEMICOLON								{ $$ = new DeclarationStruct($1.name, $1.type, $3); montage.IsValideTypeStructString($1.type,@1.StartLine, @1.StartColumn); 
																																	montage.AddFunctionForList($$); DeclarationStruct.PopContexte(); }
						;

decNameAndType  :	declaredVariableName declaredVariableType { $$ = new StructDecNameAndType($1,$2); @$ = @2; DeclarationStruct.PushContexte($1,$2); }
				;


declaredVariableName	:	ID		{ $$ = $1; }
						;


listAffectation :	listAffectationBis						{ $$ = $1; montage.AddListCalculExpression($1);}
				;
							

listAffectationBis	:	/*Empty*/							{ $$ = new List<Affectation>();  }
					|   listAffectationBis affectation		{ $$=$1; $$.Add($2);  }
					;

affectation	:		var affectationType ASSIGN expression SEMICOLON				{ $$ = new Affectation($1, $4); montage.CheckAffectationIsValid($4.dataType, $2, $1.name ,@1.StartLine);  montage.AddSymbole($$);  }
			;

affectationType			:	declaredVariableType		{ @$=@1; $$ = $1; }
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
			|       expression NOTEGALE expression		{ /*Console.WriteLine("EGALE");*/	$$ = new Expression(ExpressionSymbole.NOTEGALE, $1, $3); }
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
			| listBrick include		{ $$ = $1; $$.AddRange($2.brickList); }
			;

brick		: textBloc				{ $$ = new DeadText($1); }
			| callVar				{ $$ = $1; }
			| choice				{ $$ = $1; }
			| option				{ $$ = $1; } 
			| title					{ $$ = $1; }
			| condition				{ $$ = $1; }
			| iteration				{ $$ = $1; }
			| table					{ $$ = new DeadText($1.Write()); }
			;

include		: INCLUDECKW ID	SEMICOLON { $$ = new Include($2); }
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
					| COLON			{ $$ = ":"; }
					| MINUS			{ $$ = "-"; }
					| DOUBLECOTE	{ $$ = "\""; }
					| INTEGER		{ $$ = $1.ToString(); }
					| FLOAT			{ $$ = $1.ToString(); }
					| STRING		{ $$ = $1; }
					| NOUVLIGNE		{ $$ = "$nouvligne"; }
					| NOUVPARAG		{ $$ = "$nouvparag"; }
					;


title		: TITLEID BRACEOPEN titleContent BRACECLOSE				{  $$ = new Title($1, $3); }
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
						| COLON			{ $$ = ":"; }
						| MINUS			{ $$ = "-"; }
						| DOUBLECOTE	{ $$ = "\""; }
						| INTEGER		{ $$ = $1.ToString(); }
						| FLOAT			{ $$ = $1.ToString(); }
						| STRING		{ $$ = $1; }
						| callVar		{ $$ = $1.Write(); }
						;
	

callVar		: CODEINDIC BRACEOPEN ID BRACECLOSE			{ $$ = new VariableCall($3, montage.isLocalVar($3, @3.StartLine, @3.StartColumn), montage.GetVarTypeString($3)); }
			;


choice		: CHOIXCKW PARENTOPEN ID COMMA STRING PARENTCLOSE BRACEOPEN listProposition BRACECLOSE		{ $$ = new Choice($3, $5, $8); montage.AddListConditionalAffectation($8,  new VariableId($3, "LTEXTE"));}
			;



listProposition		: proposition							{ $$ = new List<Proposition>(); $$.Add($1); }
					| listProposition proposition			{ $$ = $1; $$.Add($2); }
					;

proposition			: PARENTOPEN ID COMMA STRING PARENTCLOSE BRACEOPEN listBrick BRACECLOSE							{ $$ = new Proposition($2,$4, $7); }
					| PARENTOPEN ID COMMA STRING PARENTCLOSE implication BRACEOPEN listBrick BRACECLOSE				{ $$ = new Proposition($2,$4, $8, $6); }
					;




option		: OPTIONCKW PARENTOPEN ID COMMA STRING PARENTCLOSE BRACEOPEN listBrick BRACECLOSE					{ $$ = new Option($3, $5, $8); }
			| OPTIONCKW PARENTOPEN ID COMMA STRING PARENTCLOSE implication BRACEOPEN listBrick BRACECLOSE		{ $$ = new Option($3, $5, $9); montage.AddListConditionalAffectation($7, new VariableId($3, "LBOOL")); }
			;


condition	: CONDITIONCKW PARENTOPEN expression PARENTCLOSE BRACEOPEN listBrick BRACECLOSE					{ $$ = new Condition($3, $6); montage.CheckConditionExpressionIsBoolean($3.dataType, @2.StartLine, @2.StartColumn); }
			| CONDITIONCKW PARENTOPEN expression PARENTCLOSE implication BRACEOPEN listBrick BRACECLOSE		{ $$ = new Condition($3, $7); montage.CheckConditionExpressionIsBoolean($3.dataType, @2.StartLine, @2.StartColumn); 
																												montage.AddListConditionalAffectation($5, $3); }
			;

table		: TABCKW BRACEOPEN listTabRow BRACECLOSE				{ $$ = new Table($3); }
			;

listTabRow			: BRACEOPEN listTabCel BRACECLOSE						{ $$ = new List<List<KeyValuePair<long, List<Brick>>>>(); $$.Add($2); }
					| listTabRow BRACEOPEN listTabCel BRACECLOSE			{ $$ = $1; $$.Add($3); }
					;

listTabCel			: PARENTOPEN INTEGER PARENTCLOSE BRACEOPEN listBrick BRACECLOSE						{ $$ = new List<KeyValuePair<long, List<Brick>>>(); $$.Add(new KeyValuePair<long, List<Brick>>($2,$5)); }
					| listTabCel PARENTOPEN INTEGER PARENTCLOSE BRACEOPEN listBrick BRACECLOSE				{ $$ = $1; $$.Add(new KeyValuePair<long, List<Brick>>($3,$6)); }
					;

implication			: IMPLIQUECKW BRACEOPEN listAffectationWithCond BRACECLOSE											{ $$ = $3; }
					;

					
listAffectationWithCond	:	/*Empty*/										{ $$ = new List<Affectation>();  }
						|   listAffectationWithCond affectationWithCond		{ $$=$1; $$.Add($2);  }
						;

affectationWithCond		:		var ASSIGN expression SEMICOLON				{ $$ = new Affectation($1, $3, @1.StartLine, @1.StartColumn); }
						;

iteration 	: POURCHAQUECKW PARENTOPEN iterator PARENTCLOSE BRACEOPEN listBrick BRACECLOSE		{ $$ = new Iteration($3.iteratorName, $3.listData , $6);  montage.RemoveSymboles($3.GetListVariableOfIterator(montage));
																									IteratorStr.PopContexte(); }
			;

iterator	: ID COLON ID { $$ = new IteratorStr($1,new VariableId($3, montage.GetVarTypeStringForIteration($3,  @3.StartLine, @3.StartColumn)));
							montage.AddSymbole($$.GetListVariableOfIterator(montage)); IteratorStr.PushContexte($3); }
			;

%%

// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) {}

