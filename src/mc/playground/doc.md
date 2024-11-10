
# Lexer

Source encoding
Each source file is interpreted as a sequence of Unicode characters encoded in UTF-8. It is an error if the file is not valid UTF-8.

## Keywords

FUNCTION: func
LET: let
RET: ret

## Delimiters

OPEN_CURLYBRACES: {
CLOSE_CURLYBRACES: }
OPEN_PARENTHESES: (
CLOSE_PARENTHESES: )

# Punctuation

PLUS: +
COLON: :
EQUAL: =
SEMICOLON: ;

## Identifiers

IDENTIFIER: [a-zA-Z_]

## Literal

STRING: "[^"]*" Any unicode char but "

## Numbers

DECIMAL: [0-9]+

## Whitespace

U+0009 (horizontal tab, '\t')
U+000A (line feed, '\n')
U+000B (vertical tab)
U+000C (form feed)
U+000D (carriage return, '\r')
U+0020 (space, ' ')
U+0085 (next line)
U+200E (left-to-right mark)
U+200F (right-to-left mark)
U+2028 (line separator)
U+2029 (paragraph separator)
