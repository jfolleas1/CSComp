// appel des using avec le % pour les bout de code driect en C#

%using QUT.Gppg;

%namespace CompCorpus.Analyzer

%option stack, minimize, parser, verbose, persistbuffer, unicode, compressNext, embedbuffers

%{
    
    /*

    Ce code va être copier dans le ficher d'output
	*/

	public bool hasErrors = false ;
	public override void yyerror(string format, params object[] args) // remember to add override back
	{
		System.Console.Error.WriteLine("Error: line {0} - column {1} " + format, yyline, yycol);
		hasErrors = true ;
	}

%}

// Liste des regex



Identifier [A-Za-z][A-Za-z0-9_\.]*
Integer [0-9]+
Float [0-9]+,[0-9]+
CharString \"(\\.|[^\\"])*\"
TitleId ($titre)[1-6]

DeadWord	[^ ,\t\n{}\(\)\$+\-/*(&&)(||)!(==)(<=)(<)(>=)(>)(:=);(%%)]+

%%

[ \n\r\t]			{ /*ignore*/ yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); }

//
// Start of Rules
//

%{ //user-code that will be executed before getting the next token
%}

"$Titre"				{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.TITREACTEKW;}
"$true"                 {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.TRUE; }
"$false"                {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.FALSE;}
"$dollar"				{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.DOLLAR;}
"$nouvligne"            {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.NOUVLIGNE;}
"$nouvparag"			{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.NOUVPARAG;}

{TitleId}				{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); yylval.String = yytext; return (int)Tokens.TITLEID;}
"$choix"				{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.CHOIXCKW;}
"$option"				{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.OPTIONCKW;}
"$condition"			{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.CONDITIONCKW;}


"+"                     {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.PLUS;}
"-"                     {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.MINUS;}
"/"                     {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.DIV;}
"*"                     {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.MUL;}

"("						{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.PARENTOPEN;}
")"						{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.PARENTCLOSE;}
"{"						{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.BRACEOPEN;}
"}"						{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.BRACECLOSE;}

"&&"                    {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.AND;}
"||"                    {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.OR;}
"!"                     {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.NOT;}
"=="                    {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.EGALE;}
"<"                     {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.INF;}
"<="                    {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.INFEGALE;}
">"                     {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.SUP;}
">="                    {yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.SUPEGALE;}

":="					{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.ASSIGN;}

";"						{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.SEMICOLON; }
","						{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.COMMA; }

"%%"					{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.SEPARATOR; }

"$"						{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); return (int)Tokens.CODEINDIC; }

{Integer}				{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol);  Int64.TryParse (yytext, NumberStyles.Integer, CultureInfo.CurrentCulture, out yylval.Integer);  return (int)Tokens.INTEGER;}
{Float}					{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); double.TryParse (yytext, NumberStyles.Float, CultureInfo.CurrentCulture, out yylval.Float); return (int)Tokens.FLOAT;}
{CharString}			{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); yylval.String = yytext; return (int)Tokens.STRING;}
{Identifier}			{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); yylval.String = yytext; return (int)Tokens.ID;}

{DeadWord}				{yylloc = new LexLocation(tokLin,tokCol+1,tokELin,tokECol); yylval.String = yytext; return (int)Tokens.DEADWORD;}


%% //User-code Section