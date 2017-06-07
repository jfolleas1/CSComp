// appel des using avec le % pour les bout de code driect en C#


%namespace Analyser

%option stack, minimize, parser, verbose, persistbuffer, unicode, compressNext, embedbuffers

%{
    
    /*

    Ce code va être copier dans le ficher d'output
	*/
	public static int currentLine = 1; 

	public override void yyerror(string format, params object[] args) // remember to add override back
	{
		System.Console.Error.WriteLine("Error: line {0} - column {1} " + format, yyline, yycol);
	}

%}

// Liste des regex



Identifier [A-Za-z][A-Za-z0-9_]*
Integer [0-9]+
Float [0-9]+,[0-9]+
CharString \"(\\.|[^\\"])*\"

%%

[ \r\t]			{ /*ignore*/ }
"\n"			{ currentLine++; }
//
// Start of Rules
//

%{ //user-code that will be executed before getting the next token
%}

"$true"                 {return (int)Tokens.TRUE; }
"$false"                {return (int)Tokens.FALSE;}
"$dollar"				{return (int)Tokens.DOLLAR;}

"+"                     {return (int)Tokens.PLUS;}
"-"                     {return (int)Tokens.MINUS;}
"/"                     {return (int)Tokens.DIV;}
"*"                     {return (int)Tokens.MUL;}

"("						{return (int)Tokens.PARENTOPEN;}
")"						{return (int)Tokens.PARENTCLOSE;}
"{"						{return (int)Tokens.BRACEOPEN;}
"}"						{return (int)Tokens.BRACECLOSE;}

"&&"                    {return (int)Tokens.AND;}
"||"                    {return (int)Tokens.OR;}
"!"                     {return (int)Tokens.NOT;}
"=="                    {return (int)Tokens.EGALE;}
"<"                     {return (int)Tokens.INF;}
"<="                    {return (int)Tokens.INFEGALE;}
">"                     {return (int)Tokens.SUP;}
">="                    {return (int)Tokens.SUPEGALE;}

":="					{return (int)Tokens.ASSIGN;}


{Integer}              { Int64.TryParse (yytext, NumberStyles.Integer, CultureInfo.CurrentCulture, out yylval.Integer);  return (int)Tokens.INTEGER;}
{Float}                {double.TryParse (yytext, NumberStyles.Float, CultureInfo.CurrentCulture, out yylval.Float); Console.WriteLine("LEX: " + yytext); return (int)Tokens.FLOAT;}
{CharString}           {yylval.String = yytext; return (int)Tokens.STRING;}
{Identifier}           {yylval.String = yytext; return (int)Tokens.ID;}

%% //User-code Section