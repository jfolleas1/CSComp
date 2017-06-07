// appel des using avec le % pour les bout de code driect en C#


%namespace Analyser

%option stack, minimize, parser, verbose, persistbuffer, unicode, compressNext, embedbuffers

%{
    
    /*

    Ce code va être copier dans le ficher d'output
	

public void yyerror(string format, params object[] args) // remember to add override back
{
	System.Console.Error.WriteLine("Error: line {0} - " + format, yyline);
}*/
%}

// Liste des regex



Identifier [A-Za-z][A-Za-z0-9_]*
Integer [0-9]+
Float [0-9]+\.[0-9]+
CharString \"(\\.|[^\\"])*\"

%%

[ \r\n\t]			{ /*ignore*/ }

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

"("						{return (int)Tokens.PARENTOUV;}
")"						{return (int)Tokens.PARENTFERM;}

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
{Float}                {double.TryParse (yytext, NumberStyles.Float, CultureInfo.CurrentCulture, out yylval.Float); return (int)Tokens.FLOAT;}
{CharString}           {yylval.String = yytext; return (int)Tokens.STRING;}
{Identifier}           {yylval.String = yytext; return (int)Tokens.ID;}

%% //User-code Section