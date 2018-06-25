using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegExPerf
{
    public class Benchmarks
    {
        // A series of patterns (all valid and non pathological) and inputs (which they may or may not match)
        public static IEnumerable<object[]> Match_TestData()
        {
            yield return new object[] { "[abcd-[d]]+", "dddaabbccddd", RegexOptions.None };
            yield return new object[] { @"[\d-[357]]+", "33312468955", RegexOptions.None };
            yield return new object[] { @"[\d-[357]]+", "51246897", RegexOptions.None };
            yield return new object[] { @"[\d-[357]]+", "3312468977", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[\d]]+", "0AZaz9", RegexOptions.None };
            yield return new object[] { @"[\w-[\p{Ll}]]+", "a09AZz", RegexOptions.None };
            yield return new object[] { @"[\d-[13579]]+", "1024689", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[13579]]+", "\x066102468\x0660", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[13579]]+", "\x066102468\x0660", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\p{Ll}-[ae-z]]+", "aaabbbcccdddeee", RegexOptions.None };
            yield return new object[] { @"[\p{Nd}-[2468]]+", "20135798", RegexOptions.None };
            yield return new object[] { @"[\P{Lu}-[ae-z]]+", "aaabbbcccdddeee", RegexOptions.None };
            yield return new object[] { @"[\P{Nd}-[\p{Ll}]]+", "az09AZ'[]", RegexOptions.None };
            yield return new object[] { "[abcd-[def]]+", "fedddaabbccddd", RegexOptions.None };
            yield return new object[] { @"[\d-[357a-z]]+", "az33312468955", RegexOptions.None };
            yield return new object[] { @"[\d-[de357fgA-Z]]+", "AZ51246897", RegexOptions.None };
            yield return new object[] { @"[\d-[357\p{Ll}]]+", "az3312468977", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y\s]]+", " \tbbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[\d\p{Po}]]+", "!#0AZaz9", RegexOptions.None };
            yield return new object[] { @"[\w-[\p{Ll}\s]]+", "a09AZz", RegexOptions.None };
            yield return new object[] { @"[\d-[13579a-zA-Z]]+", "AZ1024689", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[13579abcd]]+", "abcd\x066102468\x0660", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[13579\s]]+", " \t\x066102468\x0660", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y\p{Po}]]+", "!#bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y!.,]]+", "!.,bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { "[\\w-[b-y\x00-\x0F]]+", "\0bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\p{Ll}-[ae-z0-9]]+", "09aaabbbcccdddeee", RegexOptions.None };
            yield return new object[] { @"[\p{Nd}-[2468az]]+", "az20135798", RegexOptions.None };
            yield return new object[] { @"[\P{Lu}-[ae-zA-Z]]+", "AZaaabbbcccdddeee", RegexOptions.None };
            yield return new object[] { @"[\P{Nd}-[\p{Ll}0123456789]]+", "09az09AZ'[]", RegexOptions.None };
            yield return new object[] { "[abc-[defg]]+", "dddaabbccddd", RegexOptions.None };
            yield return new object[] { @"[\d-[abc]]+", "abc09abc", RegexOptions.None };
            yield return new object[] { @"[\d-[a-zA-Z]]+", "az09AZ", RegexOptions.None };
            yield return new object[] { @"[\d-[\p{Ll}]]+", "az09az", RegexOptions.None };
            yield return new object[] { @"[\w-[\x00-\x0F]]+", "bbbaaaABYZ09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[\s]]+", "0AZaz9", RegexOptions.None };
            yield return new object[] { @"[\w-[\W]]+", "0AZaz9", RegexOptions.None };
            yield return new object[] { @"[\w-[\p{Po}]]+", "#a09AZz!", RegexOptions.None };
            yield return new object[] { @"[\d-[\D]]+", "azAZ1024689", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[a-zA-Z]]+", "azAZ\x066102468\x0660", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[\p{Ll}]]+", "\x066102468\x0660", RegexOptions.None };
            yield return new object[] { @"[a-zA-Z0-9-[\s]]+", " \tazAZ09", RegexOptions.None };
            yield return new object[] { @"[a-zA-Z0-9-[\W]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[a-zA-Z0-9-[^a-zA-Z0-9]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\p{Ll}-[A-Z]]+", "AZaz09", RegexOptions.None };
            yield return new object[] { @"[\p{Nd}-[a-z]]+", "az09", RegexOptions.None };
            yield return new object[] { @"[\P{Lu}-[\p{Lu}]]+", "AZazAZ", RegexOptions.None };
            yield return new object[] { @"[\P{Lu}-[A-Z]]+", "AZazAZ", RegexOptions.None };
            yield return new object[] { @"[\P{Nd}-[\p{Nd}]]+", "azAZ09", RegexOptions.None };
            yield return new object[] { @"[\P{Nd}-[2-8]]+", "1234567890azAZ1234567890", RegexOptions.None };
            yield return new object[] { @"([ ]|[\w-[0-9]])+", "09az AZ90", RegexOptions.None };
            yield return new object[] { @"([0-9-[02468]]|[0-9-[13579]])+", "az1234567890za", RegexOptions.None };
            yield return new object[] { @"([^0-9-[a-zAE-Z]]|[\w-[a-zAF-Z]])+", "azBCDE1234567890BCDEFza", RegexOptions.None };
            yield return new object[] { @"([\p{Ll}-[aeiou]]|[^\w-[\s]])+", "aeiobcdxyz!@#aeio", RegexOptions.None };
            yield return new object[] { @"98[\d-[9]][\d-[8]][\d-[0]]", "98911 98881 98870 98871", RegexOptions.None };
            yield return new object[] { @"m[\w-[^aeiou]][\w-[^aeiou]]t", "mbbt mect meet", RegexOptions.None };
            yield return new object[] { "[abcdef-[^bce]]+", "adfbcefda", RegexOptions.None };
            yield return new object[] { "[^cde-[ag]]+", "agbfxyzga", RegexOptions.None };
            yield return new object[] { @"[\p{L}-[^\p{Lu}]]+", "09',.abcxyzABCXYZ", RegexOptions.None };
            yield return new object[] { @"[\p{IsGreek}-[\P{Lu}]]+", "\u0390\u03FE\u0386\u0388\u03EC\u03EE\u0400", RegexOptions.None };
            yield return new object[] { @"[\p{IsBasicLatin}-[G-L]]+", "GAFMZL", RegexOptions.None };
            yield return new object[] { "[a-zA-Z-[aeiouAEIOU]]+", "aeiouAEIOUbcdfghjklmnpqrstvwxyz", RegexOptions.None };
            yield return new object[] { @"^
            (?<octet>^
                (
                    (
                        (?<Octet2xx>[\d-[013-9]])
                        |
                        [\d-[2-9]]
                    )
                    (?(Octet2xx)
                        (
                            (?<Octet25x>[\d-[01-46-9]])
                            |
                            [\d-[5-9]]
                        )
                        (
                            (?(Octet25x)
                                [\d-[6-9]]
                                |
                                [\d]
                            )
                        )
                        |
                        [\d]{2}
                    )
                )
                |
                ([\d][\d])
                |
                [\d]
            )$"
            , "255", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"[abcd\-d-[bc]]+", "bbbaaa---dddccc", RegexOptions.None };
            yield return new object[] { @"[abcd\-d-[bc]]+", "bbbaaa---dddccc", RegexOptions.None };
            yield return new object[] { @"[^a-f-[\x00-\x60\u007B-\uFFFF]]+", "aaafffgggzzz{{{", RegexOptions.None };
            yield return new object[] { @"[\[\]a-f-[[]]+", "gggaaafff]]][[[", RegexOptions.None };
            yield return new object[] { @"[\[\]a-f-[]]]+", "gggaaafff[[[]]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "a]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "b]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "c]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "d]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "a]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "b]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "c]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "d]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "-]]", RegexOptions.None };
            yield return new object[] { @"[a-[c-e]]+", "bbbaaaccc", RegexOptions.None };
            yield return new object[] { @"[a-[c-e]]+", "```aaaccc", RegexOptions.None };
            yield return new object[] { @"[a-d\--[bc]]+", "cccaaa--dddbbb", RegexOptions.None };
            yield return new object[] { @"[\0- [bc]+", "!!!\0\0\t\t  [[[[bbbcccaaa", RegexOptions.None };
            yield return new object[] { "[[abcd]-[bc]]+", "a-b]", RegexOptions.None };
            yield return new object[] { "[-[e-g]+", "ddd[[[---eeefffggghhh", RegexOptions.None };
            yield return new object[] { "[-e-g]+", "ddd---eeefffggghhh", RegexOptions.None };
            yield return new object[] { "[-e-g]+", "ddd---eeefffggghhh", RegexOptions.None };
            yield return new object[] { "[a-e - m-p]+", "---a b c d e m n o p---", RegexOptions.None };
            yield return new object[] { "[^-[bc]]", "b] c] -] aaaddd]", RegexOptions.None };
            yield return new object[] { "[^-[bc]]", "b] c] -] aaa]ddd]", RegexOptions.None };
            yield return new object[] { @"[a\-[bc]+", "```bbbaaa---[[[cccddd", RegexOptions.None };
            yield return new object[] { @"[a\-[\-\-bc]+", "```bbbaaa---[[[cccddd", RegexOptions.None };
            yield return new object[] { @"[a\-\[\-\[\-bc]+", "```bbbaaa---[[[cccddd", RegexOptions.None };
            yield return new object[] { @"[abc\--[b]]+", "[[[```bbbaaa---cccddd", RegexOptions.None };
            yield return new object[] { @"[abc\-z-[b]]+", "```aaaccc---zzzbbb", RegexOptions.None };
            yield return new object[] { @"[a-d\-[b]+", "```aaabbbcccddd----[[[[]]]", RegexOptions.None };
            yield return new object[] { @"[abcd\-d\-[bc]+", "bbbaaa---[[[dddccc", RegexOptions.None };
            yield return new object[] { "[a - c - [ b ] ]+", "dddaaa   ccc [[[[ bbb ]]]", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { "[a - c - [ b ] +", "dddaaa   ccc [[[[ bbb ]]]", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(\p{Lu}\w*)\s(\p{Lu}\w*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"(\p{Lu}\p{Ll}*)\s(\p{Lu}\p{Ll}*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"(\P{Ll}\p{Ll}*)\s(\P{Ll}\p{Ll}*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"(\P{Lu}+\p{Lu})\s(\P{Lu}+\p{Lu})", "hellO worlD", RegexOptions.None };
            yield return new object[] { @"(\p{Lt}\w*)\s(\p{Lt}*\w*)", "\u01C5ello \u01C5orld", RegexOptions.None };
            yield return new object[] { @"(\P{Lt}\w*)\s(\P{Lt}*\w*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"[@-D]+", "eE?@ABCDabcdeE", RegexOptions.IgnoreCase };
            yield return new object[] { @"[>-D]+", "eE=>?@ABCDabcdeE", RegexOptions.IgnoreCase };
            yield return new object[] { @"[\u0554-\u0557]+", "\u0583\u0553\u0554\u0555\u0556\u0584\u0585\u0586\u0557\u0558", RegexOptions.IgnoreCase };
            yield return new object[] { @"[X-\]]+", "wWXYZxyz[\\]^", RegexOptions.IgnoreCase };
            yield return new object[] { @"[X-\u0533]+", "\u0551\u0554\u0560AXYZaxyz\u0531\u0532\u0533\u0561\u0562\u0563\u0564", RegexOptions.IgnoreCase };
            yield return new object[] { @"[X-a]+", "wWAXYZaxyz", RegexOptions.IgnoreCase };
            yield return new object[] { @"[X-c]+", "wWABCXYZabcxyz", RegexOptions.IgnoreCase };
            yield return new object[] { @"[X-\u00C0]+", "\u00C1\u00E1\u00C0\u00E0wWABCXYZabcxyz", RegexOptions.IgnoreCase };
            yield return new object[] { @"[\u0100\u0102\u0104]+", "\u00FF \u0100\u0102\u0104\u0101\u0103\u0105\u0106", RegexOptions.IgnoreCase };
            yield return new object[] { @"[B-D\u0130]+", "aAeE\u0129\u0131\u0068 BCDbcD\u0130\u0069\u0070", RegexOptions.IgnoreCase };
            yield return new object[] { @"[\u013B\u013D\u013F]+", "\u013A\u013B\u013D\u013F\u013C\u013E\u0140\u0141", RegexOptions.IgnoreCase };
            yield return new object[] { "(Cat)\r(Dog)", "Cat\rDog", RegexOptions.None };
            yield return new object[] { "(Cat)\t(Dog)", "Cat\tDog", RegexOptions.None };
            yield return new object[] { "(Cat)\f(Dog)", "Cat\fDog", RegexOptions.None };
            yield return new object[] { @"{5", "hello {5 world", RegexOptions.None };
            yield return new object[] { @"{5,", "hello {5, world", RegexOptions.None };
            yield return new object[] { @"{5,6", "hello {5,6 world", RegexOptions.None };
            yield return new object[] { @"(?n:(?<cat>cat)(\s+)(?<dog>dog))", "cat   dog", RegexOptions.None };
            yield return new object[] { @"(?n:(cat)(\s+)(dog))", "cat   dog", RegexOptions.None };
            yield return new object[] { @"(?n:(cat)(?<SpaceChars>\s+)(dog))", "cat   dog", RegexOptions.None };
            yield return new object[] { @"(?x:
                            (?<cat>cat) # Cat statement
                            (\s+) # Whitespace chars
                            (?<dog>dog # Dog statement
                            ))", "cat   dog", RegexOptions.None };
            yield return new object[] { @"(?+i:cat)", "CAT", RegexOptions.None };
            yield return new object[] { @"cat([\d]*)dog", "hello123cat230927dog1412d", RegexOptions.None };
            yield return new object[] { @"([\D]*)dog", "65498catdog58719", RegexOptions.None };
            yield return new object[] { @"cat([\s]*)dog", "wiocat   dog3270", RegexOptions.None };
            yield return new object[] { @"cat([\S]*)", "sfdcatdog    3270", RegexOptions.None };
            yield return new object[] { @"cat([\w]*)", "sfdcatdog    3270", RegexOptions.None };
            yield return new object[] { @"cat([\W]*)dog", "wiocat   dog3270", RegexOptions.None };
            yield return new object[] { @"([\p{Lu}]\w*)\s([\p{Lu}]\w*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"([\P{Ll}][\p{Ll}]*)\s([\P{Ll}][\p{Ll}]*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"(cat)([\x41]*)(dog)", "catAAAdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\u0041]*)(dog)", "catAAAdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\a]*)(dog)", "cat\a\a\adog", RegexOptions.None };
            yield return new object[] { @"(cat)([\b]*)(dog)", "cat\b\b\bdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\e]*)(dog)", "cat\u001B\u001B\u001Bdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\f]*)(dog)", "cat\f\f\fdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\r]*)(dog)", "cat\r\r\rdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\v]*)(dog)", "cat\v\v\vdog", RegexOptions.None };
            yield return new object[] { @"cat([\d]*)dog", "hello123cat230927dog1412d", RegexOptions.ECMAScript };
            yield return new object[] { @"([\D]*)dog", "65498catdog58719", RegexOptions.ECMAScript };
            yield return new object[] { @"cat([\s]*)dog", "wiocat   dog3270", RegexOptions.ECMAScript };
            yield return new object[] { @"cat([\S]*)", "sfdcatdog    3270", RegexOptions.ECMAScript };
            yield return new object[] { @"cat([\w]*)", "sfdcatdog    3270", RegexOptions.ECMAScript };
            yield return new object[] { @"cat([\W]*)dog", "wiocat   dog3270", RegexOptions.ECMAScript };
            yield return new object[] { @"([\p{Lu}]\w*)\s([\p{Lu}]\w*)", "Hello World", RegexOptions.ECMAScript };
            yield return new object[] { @"([\P{Ll}][\p{Ll}]*)\s([\P{Ll}][\p{Ll}]*)", "Hello World", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\d*dog", "hello123cat230927dog1412d", RegexOptions.ECMAScript };
            yield return new object[] { @"\D*(dog)", "65498catdog58719", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\s*(dog)", "wiocat   dog3270", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\S*", "sfdcatdog    3270", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\w*", "sfdcatdog    3270", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\W*(dog)", "wiocat   dog3270", RegexOptions.ECMAScript };
            yield return new object[] { @"\p{Lu}(\w*)\s\p{Lu}(\w*)", "Hello World", RegexOptions.ECMAScript };
            yield return new object[] { @"\P{Ll}\p{Ll}*\s\P{Ll}\p{Ll}*", "Hello World", RegexOptions.ECMAScript };
            yield return new object[] { @"cat(?<dog121>dog)", "catcatdogdogcat", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s*(?<cat>dog)", "catcat    dogdogcat", RegexOptions.None };
            yield return new object[] { @"(?<1>cat)\s*(?<1>dog)", "catcat    dogdogcat", RegexOptions.None };
            yield return new object[] { @"(?<2048>cat)\s*(?<2048>dog)", "catcat    dogdogcat", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\w+(?<dog-cat>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\w+(?<-cat>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\w+(?<cat-cat>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<1>cat)\w+(?<dog-1>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\w+(?<2-cat>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<1>cat)\w+(?<2-1>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){", "STARTcat{", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){fdsa", "STARTcat{fdsa", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1", "STARTcat{1", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1END", "STARTcat{1END", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1,", "STARTcat{1,", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1,END", "STARTcat{1,END", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1,2", "STARTcat{1,2", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1,2END", "STARTcat{1,2END", RegexOptions.None };
            yield return new object[] { @"(cat) #cat
                            \s+ #followed by 1 or more whitespace
                            (dog)  #followed by dog
                            ", "cat    dog", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(cat) #cat
                            \s+ #followed by 1 or more whitespace
                            (dog)  #followed by dog", "cat    dog", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(cat) (?#cat)    \s+ (?#followed by 1 or more whitespace) (dog)  (?#followed by dog)", "cat    dog", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(?<cat>cat)(?<dog>dog)\k<cat>", "asdfcatdogcatdog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\k<cat>", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\k'cat'", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\<cat>", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\'cat'", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\k<1>", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\k'1'", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\<1>", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\'1'", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\1", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\1", "asdfcat   dogcat   dog", RegexOptions.ECMAScript };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\k<dog>", "asdfcat   dogdog   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\2", "asdfcat   dogdog   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\2", "asdfcat   dogdog   dog", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\077)", "hellocat?dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\77)", "hellocat?dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\176)", "hellocat~dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\400)", "hellocat\0dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\300)", "hellocat\u00C0dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\300)", "hellocat\u00C0dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\477)", "hellocat\u003Fdogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\777)", "hellocat\u00FFdogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\7770)", "hellocat\u00FF0dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\077)", "hellocat?dogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\77)", "hellocat?dogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\7)", "hellocat\adogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\40)", "hellocat dogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\040)", "hellocat dogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\176)", "hellocatcat76dogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\377)", "hellocat\u00FFdogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\400)", "hellocat 0Fdogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\s+(?<2147483646>dog)", "asdlkcat  dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)\s+(?<2147483647>dog)", "asdlkcat  dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2a*)(dog)", "asdlkcat***dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2b*)(dog)", "asdlkcat+++dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2c*)(dog)", "asdlkcat,,,dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2d*)(dog)", "asdlkcat---dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2e*)(dog)", "asdlkcat...dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2A*)(dog)", "asdlkcat***dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2B*)(dog)", "asdlkcat+++dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2C*)(dog)", "asdlkcat,,,dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2D*)(dog)", "asdlkcat---dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2E*)(dog)", "asdlkcat...dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\c@*)(dog)", "asdlkcat\0\0dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cA*)(dog)", "asdlkcat\u0001dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\ca*)(dog)", "asdlkcat\u0001dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cC*)(dog)", "asdlkcat\u0003dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cc*)(dog)", "asdlkcat\u0003dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cD*)(dog)", "asdlkcat\u0004dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cd*)(dog)", "asdlkcat\u0004dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cX*)(dog)", "asdlkcat\u0018dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cx*)(dog)", "asdlkcat\u0018dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cZ*)(dog)", "asdlkcat\u001adogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cz*)(dog)", "asdlkcat\u001adogiwod", RegexOptions.None };
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\n   dog", RegexOptions.None };
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\n   dog", RegexOptions.Multiline };
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\n   dog", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog", RegexOptions.None };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog", RegexOptions.Multiline };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog\n", RegexOptions.None };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog\n", RegexOptions.Multiline };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog\n", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog", RegexOptions.None };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog", RegexOptions.Multiline };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog", RegexOptions.ECMAScript };
            yield return new object[] { @"\b@cat", "123START123@catEND", RegexOptions.None };
            yield return new object[] { @"\b\<cat", "123START123<catEND", RegexOptions.None };
            yield return new object[] { @"\b,cat", "satwe,,,START,catEND", RegexOptions.None };
            yield return new object[] { @"\b\[cat", "`12START123[catEND", RegexOptions.None };
            yield return new object[] { @"\B@cat", "123START123;@catEND", RegexOptions.None };
            yield return new object[] { @"\B\<cat", "123START123'<catEND", RegexOptions.None };
            yield return new object[] { @"\B,cat", "satwe,,,START',catEND", RegexOptions.None };
            yield return new object[] { @"\B\[cat", "`12START123'[catEND", RegexOptions.None };
            yield return new object[] { @"(\w+)\s+(\w+)", "cat\u02b0 dog\u02b1", RegexOptions.None };
            yield return new object[] { @"(cat\w+)\s+(dog\w+)", "STARTcat\u30FC dog\u3005END", RegexOptions.None };
            yield return new object[] { @"(cat\w+)\s+(dog\w+)", "STARTcat\uff9e dog\uff9fEND", RegexOptions.None };
            yield return new object[] { @"[^a]|d", "d", RegexOptions.None };
            yield return new object[] { @"([^a]|[d])*", "Hello Worlddf", RegexOptions.None };
            yield return new object[] { @"([^{}]|\n)+", "{{{{Hello\n World \n}END", RegexOptions.None };
            yield return new object[] { @"([a-d]|[^abcd])+", "\tonce\n upon\0 a- ()*&^%#time?", RegexOptions.None };
            yield return new object[] { @"([^a]|[a])*", "once upon a time", RegexOptions.None };
            yield return new object[] { @"([a-d]|[^abcd]|[x-z]|^wxyz])+", "\tonce\n upon\0 a- ()*&^%#time?", RegexOptions.None };
            yield return new object[] { @"([a-d]|[e-i]|[^e]|wxyz])+", "\tonce\n upon\0 a- ()*&^%#time?", RegexOptions.None };
            yield return new object[] { @"^(([^b]+ )|(.* ))$", "aaa ", RegexOptions.None };
            yield return new object[] { @"^(([^b]+ )|(.*))$", "aaa", RegexOptions.None };
            yield return new object[] { @"^(([^b]+ )|(.* ))$", "bbb ", RegexOptions.None };
            yield return new object[] { @"^(([^b]+ )|(.*))$", "bbb", RegexOptions.None };
            yield return new object[] { @"^((a*)|(.*))$", "aaa", RegexOptions.None };
            yield return new object[] { @"^((a*)|(.*))$", "aaabbb", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))*", "{hello 1234567890 world}", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))+", "{hello 1234567890 world}", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))*", "{HELLO 1234567890 world}", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))+", "{HELLO 1234567890 world}", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))*", "{1234567890 hello  world}", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))+", "{1234567890 hello world}", RegexOptions.None };
            yield return new object[] { @"^(([a-d]*)|([a-z]*))$", "aaabbbcccdddeeefff", RegexOptions.None };
            yield return new object[] { @"^(([d-f]*)|([c-e]*))$", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"^(([c-e]*)|([d-f]*))$", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"(([a-d]*)|([a-z]*))", "aaabbbcccdddeeefff", RegexOptions.None };
            yield return new object[] { @"(([d-f]*)|([c-e]*))", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"(([c-e]*)|([d-f]*))", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"(([a-d]*)|(.*))", "aaabbbcccdddeeefff", RegexOptions.None };
            yield return new object[] { @"(([d-f]*)|(.*))", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"(([c-e]*)|(.*))", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"\p{Pi}(\w*)\p{Pf}", "\u00ABCat\u00BB   \u00BBDog\u00AB'", RegexOptions.None };
            yield return new object[] { @"\p{Pi}(\w*)\p{Pf}", "\u2018Cat\u2019   \u2019Dog\u2018'", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\s+\123\s+\234", "asdfcat   dog     cat23    dog34eia", RegexOptions.ECMAScript };
            yield return new object[] { @"<div> 
            (?> 
                <div>(?<DEPTH>) |   
                </div> (?<-DEPTH>) |  
                .?
            )*?
            (?(DEPTH)(?!)) 
            </div>", "<div>this is some <div>red</div> text</div></div></div>", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(
            ((?'open'<+)[^<>]*)+
            ((?'close-open'>+)[^<>]*)+
            )+", "<01deep_01<02deep_01<03deep_01>><02deep_02><02deep_03<03deep_03>>>", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(
            (?<start><)?
            [^<>]?
            (?<end-start>>)?
            )*", "<01deep_01<02deep_01<03deep_01>><02deep_02><02deep_03<03deep_03>>>", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(
            (?<start><[^/<>]*>)?
            [^<>]?
            (?<end-start></[^/<>]*>)?
            )*", "<b><a>Cat</a></b>", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(
            (?<start><(?<TagName>[^/<>]*)>)?
            [^<>]?
            (?<end-start></\k<TagName>>)?
            )*", "<b>cat</b><a>dog</a>", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"([0-9]+?)([\w]+?)", "55488aheiaheiad", RegexOptions.ECMAScript };
            yield return new object[] { @"([0-9]+?)([a-z]+?)", "55488aheiaheiad", RegexOptions.ECMAScript };
            yield return new object[] { @"\G<%#(?<code>.*?)?%>", @"<%# DataBinder.Eval(this, ""MyNumber"") %>", RegexOptions.Singleline };
            yield return new object[] { @"^[abcd]{0,0x10}*$", "a{0,0x10}}}", RegexOptions.None };
            yield return new object[] { @"([a-z]*?)([\w])", "cat", RegexOptions.IgnoreCase };
            yield return new object[] { @"^([a-z]*?)([\w])$", "cat", RegexOptions.IgnoreCase };
            yield return new object[] { @"([a-z]*)([\w])", "cat", RegexOptions.IgnoreCase };
            yield return new object[] { @"^([a-z]*)([\w])$", "cat", RegexOptions.IgnoreCase };
            yield return new object[] { @"(cat){", "cat{", RegexOptions.None };
            yield return new object[] { @"(cat){}", "cat{}", RegexOptions.None };
            yield return new object[] { @"(cat){,", "cat{,", RegexOptions.None };
            yield return new object[] { @"(cat){,}", "cat{,}", RegexOptions.None };
            yield return new object[] { @"(cat){cat}", "cat{cat}", RegexOptions.None };
            yield return new object[] { @"(cat){cat,5}", "cat{cat,5}", RegexOptions.None };
            yield return new object[] { @"(cat){5,dog}", "cat{5,dog}", RegexOptions.None };
            yield return new object[] { @"(cat){cat,dog}", "cat{cat,dog}", RegexOptions.None };
            yield return new object[] { @"(cat){,}?", "cat{,}?", RegexOptions.None };
            yield return new object[] { @"(cat){cat}?", "cat{cat}?", RegexOptions.None };
            yield return new object[] { @"(cat){cat,5}?", "cat{cat,5}?", RegexOptions.None };
            yield return new object[] { @"(cat){5,dog}?", "cat{5,dog}?", RegexOptions.None };
            yield return new object[] { @"(cat){cat,dog}?", "cat{cat,dog}?", RegexOptions.None };
            yield return new object[] { @"()", "cat", RegexOptions.None };
            yield return new object[] { @"(?<cat>)", "cat", RegexOptions.None };
            yield return new object[] { @"(?'cat')", "cat", RegexOptions.None };
            yield return new object[] { @"(?:)", "cat", RegexOptions.None };
            yield return new object[] { @"(?imn)", "cat", RegexOptions.None };
            yield return new object[] { @"(?imn)cat", "(?imn)cat", RegexOptions.None };
            yield return new object[] { @"(?=)", "cat", RegexOptions.None };
            yield return new object[] { @"(?<=)", "cat", RegexOptions.None };
            yield return new object[] { @"(?>)", "cat", RegexOptions.None };
            yield return new object[] { @"(?()|)", "(?()|)", RegexOptions.None };
            yield return new object[] { @"(?(cat)|)", "cat", RegexOptions.None };
            yield return new object[] { @"(?(cat)|)", "dog", RegexOptions.None };
            yield return new object[] { @"(?(cat)catdog|)", "catdog", RegexOptions.None };
            yield return new object[] { @"(?(cat)catdog|)", "dog", RegexOptions.None };
            yield return new object[] { @"(?(cat)dog|)", "dog", RegexOptions.None };
            yield return new object[] { @"(?(cat)dog|)", "cat", RegexOptions.None };
            yield return new object[] { @"(?(cat)|catdog)", "cat", RegexOptions.None };
            yield return new object[] { @"(?(cat)|catdog)", "catdog", RegexOptions.None };
            yield return new object[] { @"(?(cat)|dog)", "dog", RegexOptions.None };
            yield return new object[] { "([\u0000-\uFFFF-[azAZ09]]|[\u0000-\uFFFF-[^azAZ09]])+", "azAZBCDE1234567890BCDEFAZza", RegexOptions.None };
            yield return new object[] { "[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[a]]]]]]+", "abcxyzABCXYZ123890", RegexOptions.None };
            yield return new object[] { "[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[a]]]]]]]+", "bcxyzABCXYZ123890a", RegexOptions.None };
            yield return new object[] { "[\u0000-\uFFFF-[\\p{P}\\p{S}\\p{C}]]+", "!@`';.,$+<>=\x0001\x001FazAZ09", RegexOptions.None };
            yield return new object[] { @"[\uFFFD-\uFFFF]+", "\uFFFC\uFFFD\uFFFE\uFFFF", RegexOptions.IgnoreCase };
            yield return new object[] { @"[\uFFFC-\uFFFE]+", "\uFFFB\uFFFC\uFFFD\uFFFE\uFFFF", RegexOptions.IgnoreCase };
            yield return new object[] { @"([a*]*)+?$", "ab", RegexOptions.None };
            yield return new object[] { @"(a*)+?$", "b", RegexOptions.None };

            // Testing octal sequence matches: "\\060(\\061)?\\061"
            // Octal \061 is ASCII 49 ('1')
            yield return new object[] { @"\060(\061)?\061", "011", RegexOptions.None, 0, 3, true, "011" };

            // Testing hexadecimal sequence matches: "(\\x30\\x31\\x32)"
            // Hex \x31 is ASCII 49 ('1')
            yield return new object[] { @"(\x30\x31\x32)", "012", RegexOptions.None, 0, 3, true, "012" };

            // Testing control character escapes???: "2", "(\u0032)"
            yield return new object[] { "(\u0034)", "4", RegexOptions.None, 0, 1, true, "4", };

            // Using *, +, ?, {}: Actual - "a+\\.?b*\\.?c{2}"
            yield return new object[] { @"a+\.?b*\.+c{2}", "ab.cc", RegexOptions.None, 0, 5, true, "ab.cc" };

            // Using [a-z], \s, \w: Actual - "([a-zA-Z]+)\\s(\\w+)"
            yield return new object[] { @"([a-zA-Z]+)\s(\w+)", "David Bau", RegexOptions.None, 0, 9, true, "David Bau" };

            // \\S, \\d, \\D, \\W: Actual - "(\\S+):\\W(\\d+)\\s(\\D+)"
            yield return new object[] { @"(\S+):\W(\d+)\s(\D+)", "Price: 5 dollars", RegexOptions.None, 0, 16, true, "Price: 5 dollars" };

            // \\S, \\d, \\D, \\W: Actual - "[^0-9]+(\\d+)"
            yield return new object[] { @"[^0-9]+(\d+)", "Price: 30 dollars", RegexOptions.None, 0, 17, true, "Price: 30" };

            // Zero-width negative lookahead assertion: Actual - "abc(?!XXX)\\w+"
            yield return new object[] { @"abc(?!XXX)\w+", "abcXXXdef", RegexOptions.None, 0, 9, false, string.Empty };

            // Zero-width positive lookbehind assertion: Actual - "(\\w){6}(?<=XXX)def"
            yield return new object[] { @"(\w){6}(?<=XXX)def", "abcXXXdef", RegexOptions.None, 0, 9, true, "abcXXXdef" };

            // Zero-width negative lookbehind assertion: Actual - "(\\w){6}(?<!XXX)def"
            yield return new object[] { @"(\w){6}(?<!XXX)def", "XXXabcdef", RegexOptions.None, 0, 9, true, "XXXabcdef" };

            // Nonbacktracking subexpression: Actual - "[^0-9]+(?>[0-9]+)3"
            // The last 3 causes the match to fail, since the non backtracking subexpression does not give up the last digit it matched
            // for it to be a success. For a correct match, remove the last character, '3' from the pattern
            yield return new object[] { "[^0-9]+(?>[0-9]+)3", "abc123", RegexOptions.None, 0, 6, false, string.Empty };

            // Using beginning/end of string chars \A, \Z: Actual - "\\Aaaa\\w+zzz\\Z"
            yield return new object[] { @"\Aaaa\w+zzz\Z", "aaaasdfajsdlfjzzz", RegexOptions.None, 0, 17, true, "aaaasdfajsdlfjzzz" };

            // Using beginning/end of string chars \A, \Z: Actual - "\\Aaaa\\w+zzz\\Z"
            yield return new object[] { @"\Aaaa\w+zzz\Z", "aaaasdfajsdlfjzzza", RegexOptions.None, 0, 18, false, string.Empty };

            // Using beginning/end of string chars \A, \Z: Actual - "\\Aaaa\\w+zzz\\Z"
            yield return new object[] { @"\A(line2\n)line3\Z", "line2\nline3\n", RegexOptions.Multiline, 0, 12, true, "line2\nline3" };

            // Using beginning/end of string chars ^: Actual - "^b"
            yield return new object[] { "^b", "ab", RegexOptions.None, 0, 2, false, string.Empty };

            // Actual - "(?<char>\\w)\\<char>"
            yield return new object[] { @"(?<char>\w)\<char>", "aa", RegexOptions.None, 0, 2, true, "aa" };

            // Actual - "(?<43>\\w)\\43"
            yield return new object[] { @"(?<43>\w)\43", "aa", RegexOptions.None, 0, 2, true, "aa" };

            // Actual - "abc(?(1)111|222)"
            yield return new object[] { "(abbc)(?(1)111|222)", "abbc222", RegexOptions.None, 0, 7, false, string.Empty };

            // "x" option. Removes unescaped whitespace from the pattern: Actual - " ([^/]+) ","x"
            yield return new object[] { "            ((.)+)      ", "abc", RegexOptions.IgnorePatternWhitespace, 0, 3, true, "abc" };

            // "x" option. Removes unescaped whitespace from the pattern. : Actual - "\x20([^/]+)\x20","x"
            yield return new object[] { "\x20([^/]+)\x20\x20\x20\x20\x20\x20\x20", " abc       ", RegexOptions.IgnorePatternWhitespace, 0, 10, true, " abc      " };

            // Turning on case insensitive option in mid-pattern : Actual - "aaa(?i:match this)bbb"
            if ("i".ToUpper() == "I")
            {
                yield return new object[] { "aaa(?i:match this)bbb", "aaaMaTcH ThIsbbb", RegexOptions.None, 0, 16, true, "aaaMaTcH ThIsbbb" };
            }

            // Turning off case insensitive option in mid-pattern : Actual - "aaa(?-i:match this)bbb", "i"
            yield return new object[] { "aaa(?-i:match this)bbb", "AaAmatch thisBBb", RegexOptions.IgnoreCase, 0, 16, true, "AaAmatch thisBBb" };

            // Turning on/off all the options at once : Actual - "aaa(?imnsx-imnsx:match this)bbb", "i"
            yield return new object[] { "aaa(?-i:match this)bbb", "AaAmatcH thisBBb", RegexOptions.IgnoreCase, 0, 16, false, string.Empty };

            // Actual - "aaa(?#ignore this completely)bbb"
            yield return new object[] { "aaa(?#ignore this completely)bbb", "aaabbb", RegexOptions.None, 0, 6, true, "aaabbb" };

            // Trying empty string: Actual "[a-z0-9]+", ""
            yield return new object[] { "[a-z0-9]+", "", RegexOptions.None, 0, 0, false, string.Empty };

            // Numbering pattern slots: "(?<1>\\d{3})(?<2>\\d{3})(?<3>\\d{4})"
            yield return new object[] { @"(?<1>\d{3})(?<2>\d{3})(?<3>\d{4})", "8885551111", RegexOptions.None, 0, 10, true, "8885551111" };
            yield return new object[] { @"(?<1>\d{3})(?<2>\d{3})(?<3>\d{4})", "Invalid string", RegexOptions.None, 0, 14, false, string.Empty };

            // Not naming pattern slots at all: "^(cat|chat)"
            yield return new object[] { "^(cat|chat)", "cats are bad", RegexOptions.None, 0, 12, true, "cat" };

            yield return new object[] { "abc", "abc", RegexOptions.None, 0, 3, true, "abc" };
            yield return new object[] { "abc", "aBc", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { "abc", "aBc", RegexOptions.IgnoreCase, 0, 3, true, "aBc" };

            // Using *, +, ?, {}: Actual - "a+\\.?b*\\.?c{2}"
            yield return new object[] { @"a+\.?b*\.+c{2}", "ab.cc", RegexOptions.None, 0, 5, true, "ab.cc" };

            // RightToLeft
            yield return new object[] { @"\s+\d+", "sdf 12sad", RegexOptions.RightToLeft, 0, 9, true, " 12" };
            yield return new object[] { @"\s+\d+", " asdf12 ", RegexOptions.RightToLeft, 0, 6, false, string.Empty };
            yield return new object[] { "aaa", "aaabbb", RegexOptions.None, 3, 3, false, string.Empty };

            yield return new object[] { @"foo\d+", "0123456789foo4567890foo         ", RegexOptions.RightToLeft, 10, 3, false, string.Empty };
            yield return new object[] { @"foo\d+", "0123456789foo4567890foo         ", RegexOptions.RightToLeft, 11, 21, false, string.Empty };

            // IgnoreCase
            yield return new object[] { "AAA", "aaabbb", RegexOptions.IgnoreCase, 0, 6, true, "aaa" };
            yield return new object[] { @"\p{Lu}", "1bc", RegexOptions.IgnoreCase, 0, 3, true, "b" };
            yield return new object[] { @"\p{Ll}", "1bc", RegexOptions.IgnoreCase, 0, 3, true, "b" };
            yield return new object[] { @"\p{Lt}", "1bc", RegexOptions.IgnoreCase, 0, 3, true, "b" };
            yield return new object[] { @"\p{Lo}", "1bc", RegexOptions.IgnoreCase, 0, 3, false, string.Empty };

            // "\D+"
            yield return new object[] { @"\D+", "12321", RegexOptions.None, 0, 5, false, string.Empty };

            // Groups
            yield return new object[] { "(?<first_name>\\S+)\\s(?<last_name>\\S+)", "David Bau", RegexOptions.None, 0, 9, true, "David Bau" };

            // "^b"
            yield return new object[] { "^b", "abc", RegexOptions.None, 0, 3, false, string.Empty };

            // RightToLeft
            yield return new object[] { @"foo\d+", "0123456789foo4567890foo         ", RegexOptions.RightToLeft, 0, 32, true, "foo4567890" };
            yield return new object[] { @"foo\d+", "0123456789foo4567890foo         ", RegexOptions.RightToLeft, 10, 22, true, "foo4567890" };
            yield return new object[] { @"foo\d+", "0123456789foo4567890foo         ", RegexOptions.RightToLeft, 10, 4, true, "foo4" };

            // Trim leading and trailing whitespace
            yield return new object[] { @"\s*(.*?)\s*$", " Hello World ", RegexOptions.None, 0, 13, true, " Hello World " };

            // < in group
            yield return new object[] { @"(?<cat>cat)\w+(?<dog-0>dog)", "cat_Hello_World_dog", RegexOptions.None, 0, 19, false, string.Empty };

            // Atomic Zero-Width Assertions \A \Z \z \G \b \B
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\ncat     dog", RegexOptions.None, 0, 20, false, string.Empty };
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\ncat     dog", RegexOptions.Multiline, 0, 20, false, string.Empty };
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\ncat     dog", RegexOptions.ECMAScript, 0, 20, false, string.Empty };

            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   dog\n\n\ncat", RegexOptions.None, 0, 15, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   dog\n\n\ncat     ", RegexOptions.Multiline, 0, 20, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   dog\n\n\ncat     ", RegexOptions.ECMAScript, 0, 20, false, string.Empty };

            yield return new object[] { @"(cat)\s+(dog)\z", "cat   dog\n\n\ncat", RegexOptions.None, 0, 15, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   dog\n\n\ncat     ", RegexOptions.Multiline, 0, 20, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   dog\n\n\ncat     ", RegexOptions.ECMAScript, 0, 20, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog\n", RegexOptions.None, 0, 16, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog\n", RegexOptions.Multiline, 0, 16, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog\n", RegexOptions.ECMAScript, 0, 16, false, string.Empty };

            yield return new object[] { @"\b@cat", "123START123;@catEND", RegexOptions.None, 0, 19, false, string.Empty };
            yield return new object[] { @"\b<cat", "123START123'<catEND", RegexOptions.None, 0, 19, false, string.Empty };
            yield return new object[] { @"\b,cat", "satwe,,,START',catEND", RegexOptions.None, 0, 21, false, string.Empty };
            yield return new object[] { @"\b\[cat", "`12START123'[catEND", RegexOptions.None, 0, 19, false, string.Empty };

            yield return new object[] { @"\B@cat", "123START123@catEND", RegexOptions.None, 0, 18, false, string.Empty };
            yield return new object[] { @"\B<cat", "123START123<catEND", RegexOptions.None, 0, 18, false, string.Empty };
            yield return new object[] { @"\B,cat", "satwe,,,START,catEND", RegexOptions.None, 0, 20, false, string.Empty };
            yield return new object[] { @"\B\[cat", "`12START123[catEND", RegexOptions.None, 0, 18, false, string.Empty };

            // Lazy operator Backtracking
            yield return new object[] { @"http://([a-zA-z0-9\-]*\.?)*?(:[0-9]*)??/", "http://www.msn.com", RegexOptions.IgnoreCase, 0, 18, false, string.Empty };

            // Grouping Constructs Invalid Regular Expressions
            yield return new object[] { "(?!)", "(?!)cat", RegexOptions.None, 0, 7, false, string.Empty };
            yield return new object[] { "(?<!)", "(?<!)cat", RegexOptions.None, 0, 8, false, string.Empty };

            // Alternation construct
            yield return new object[] { "(?(cat)|dog)", "cat", RegexOptions.None, 0, 3, true, string.Empty };
            yield return new object[] { "(?(cat)|dog)", "catdog", RegexOptions.None, 0, 6, true, string.Empty };
            yield return new object[] { "(?(cat)dog1|dog2)", "catdog1", RegexOptions.None, 0, 7, false, string.Empty };
            yield return new object[] { "(?(cat)dog1|dog2)", "catdog2", RegexOptions.None, 0, 7, true, "dog2" };
            yield return new object[] { "(?(cat)dog1|dog2)", "catdog1dog2", RegexOptions.None, 0, 11, true, "dog2" };
            yield return new object[] { "(?(dog2))", "dog2", RegexOptions.None, 0, 4, true, string.Empty };
            yield return new object[] { "(?(cat)|dog)", "oof", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { "(?(a:b))", "a", RegexOptions.None, 0, 1, true, string.Empty };
            yield return new object[] { "(?(a:))", "a", RegexOptions.None, 0, 1, true, string.Empty };

            // No Negation
            yield return new object[] { "[abcd-[abcd]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { "[1234-[1234]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // All Negation
            yield return new object[] { "[^abcd-[^abcd]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { "[^1234-[^1234]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // No Negation        
            yield return new object[] { "[a-z-[a-z]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { "[0-9-[0-9]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // All Negation
            yield return new object[] { "[^a-z-[^a-z]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { "[^0-9-[^0-9]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // No Negation
            yield return new object[] { @"[\w-[\w]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\W-[\W]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\s-[\s]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\S-[\S]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\d-[\d]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\D-[\D]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // All Negation
            yield return new object[] { @"[^\w-[^\w]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\W-[^\W]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\s-[^\s]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\S-[^\S]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\d-[^\d]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\D-[^\D]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // MixedNegation
            yield return new object[] { @"[^\w-[\W]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\w-[^\W]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\s-[\S]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\s-[^\S]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\d-[\D]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\d-[^\D]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // No Negation
            yield return new object[] { @"[\p{Ll}-[\p{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\P{Ll}-[\P{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\p{Lu}-[\p{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\P{Lu}-[\P{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\p{Nd}-[\p{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\P{Nd}-[\P{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // All Negation
            yield return new object[] { @"[^\p{Ll}-[^\p{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\P{Ll}-[^\P{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\p{Lu}-[^\p{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\P{Lu}-[^\P{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\p{Nd}-[^\p{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\P{Nd}-[^\P{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // MixedNegation
            yield return new object[] { @"[^\p{Ll}-[\P{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\p{Ll}-[^\P{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\p{Lu}-[\P{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\p{Lu}-[^\P{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\p{Nd}-[\P{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\p{Nd}-[^\P{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // Character Class Substraction
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "[]]", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "-]]", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "`]]", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "e]]", RegexOptions.None, 0, 3, false, string.Empty };

            yield return new object[] { @"[ab\-\[cd-[[]]]]", "']]", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "e]]", RegexOptions.None, 0, 3, false, string.Empty };

            yield return new object[] { @"[a-[a-f]]", "abcdefghijklmnopqrstuvwxyz", RegexOptions.None, 0, 26, false, string.Empty };

            yield return new object[] { "[abcd-[d]]+", "dddaabbccddd", RegexOptions.None };
            yield return new object[] { @"[\d-[357]]+", "33312468955", RegexOptions.None };
            yield return new object[] { @"[\d-[357]]+", "51246897", RegexOptions.None };
            yield return new object[] { @"[\d-[357]]+", "3312468977", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[\d]]+", "0AZaz9", RegexOptions.None };
            yield return new object[] { @"[\w-[\p{Ll}]]+", "a09AZz", RegexOptions.None };
            yield return new object[] { @"[\d-[13579]]+", "1024689", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[13579]]+", "\x066102468\x0660", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[13579]]+", "\x066102468\x0660", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\p{Ll}-[ae-z]]+", "aaabbbcccdddeee", RegexOptions.None };
            yield return new object[] { @"[\p{Nd}-[2468]]+", "20135798", RegexOptions.None };
            yield return new object[] { @"[\P{Lu}-[ae-z]]+", "aaabbbcccdddeee", RegexOptions.None };
            yield return new object[] { @"[\P{Nd}-[\p{Ll}]]+", "az09AZ'[]", RegexOptions.None };
            yield return new object[] { "[abcd-[def]]+", "fedddaabbccddd", RegexOptions.None };
            yield return new object[] { @"[\d-[357a-z]]+", "az33312468955", RegexOptions.None };
            yield return new object[] { @"[\d-[de357fgA-Z]]+", "AZ51246897", RegexOptions.None };
            yield return new object[] { @"[\d-[357\p{Ll}]]+", "az3312468977", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y\s]]+", " \tbbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[\d\p{Po}]]+", "!#0AZaz9", RegexOptions.None };
            yield return new object[] { @"[\w-[\p{Ll}\s]]+", "a09AZz", RegexOptions.None };
            yield return new object[] { @"[\d-[13579a-zA-Z]]+", "AZ1024689", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[13579abcd]]+", "abcd\x066102468\x0660", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[13579\s]]+", " \t\x066102468\x0660", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y\p{Po}]]+", "!#bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[b-y!.,]]+", "!.,bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { "[\\w-[b-y\x00-\x0F]]+", "\0bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\p{Ll}-[ae-z0-9]]+", "09aaabbbcccdddeee", RegexOptions.None };
            yield return new object[] { @"[\p{Nd}-[2468az]]+", "az20135798", RegexOptions.None };
            yield return new object[] { @"[\P{Lu}-[ae-zA-Z]]+", "AZaaabbbcccdddeee", RegexOptions.None };
            yield return new object[] { @"[\P{Nd}-[\p{Ll}0123456789]]+", "09az09AZ'[]", RegexOptions.None };
            yield return new object[] { "[abc-[defg]]+", "dddaabbccddd", RegexOptions.None };
            yield return new object[] { @"[\d-[abc]]+", "abc09abc", RegexOptions.None };
            yield return new object[] { @"[\d-[a-zA-Z]]+", "az09AZ", RegexOptions.None };
            yield return new object[] { @"[\d-[\p{Ll}]]+", "az09az", RegexOptions.None };
            yield return new object[] { @"[\w-[\x00-\x0F]]+", "bbbaaaABYZ09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\w-[\s]]+", "0AZaz9", RegexOptions.None };
            yield return new object[] { @"[\w-[\W]]+", "0AZaz9", RegexOptions.None };
            yield return new object[] { @"[\w-[\p{Po}]]+", "#a09AZz!", RegexOptions.None };
            yield return new object[] { @"[\d-[\D]]+", "azAZ1024689", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[a-zA-Z]]+", "azAZ\x066102468\x0660", RegexOptions.ECMAScript };
            yield return new object[] { @"[\d-[\p{Ll}]]+", "\x066102468\x0660", RegexOptions.None };
            yield return new object[] { @"[a-zA-Z0-9-[\s]]+", " \tazAZ09", RegexOptions.None };
            yield return new object[] { @"[a-zA-Z0-9-[\W]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[a-zA-Z0-9-[^a-zA-Z0-9]]+", "bbbaaaABCD09zzzyyy", RegexOptions.None };
            yield return new object[] { @"[\p{Ll}-[A-Z]]+", "AZaz09", RegexOptions.None };
            yield return new object[] { @"[\p{Nd}-[a-z]]+", "az09", RegexOptions.None };
            yield return new object[] { @"[\P{Lu}-[\p{Lu}]]+", "AZazAZ", RegexOptions.None };
            yield return new object[] { @"[\P{Lu}-[A-Z]]+", "AZazAZ", RegexOptions.None };
            yield return new object[] { @"[\P{Nd}-[\p{Nd}]]+", "azAZ09", RegexOptions.None };
            yield return new object[] { @"[\P{Nd}-[2-8]]+", "1234567890azAZ1234567890", RegexOptions.None };
            yield return new object[] { @"([ ]|[\w-[0-9]])+", "09az AZ90", RegexOptions.None };
            yield return new object[] { @"([0-9-[02468]]|[0-9-[13579]])+", "az1234567890za", RegexOptions.None };
            yield return new object[] { @"([^0-9-[a-zAE-Z]]|[\w-[a-zAF-Z]])+", "azBCDE1234567890BCDEFza", RegexOptions.None };
            yield return new object[] { @"([\p{Ll}-[aeiou]]|[^\w-[\s]])+", "aeiobcdxyz!@#aeio", RegexOptions.None };
            yield return new object[] { @"98[\d-[9]][\d-[8]][\d-[0]]", "98911 98881 98870 98871", RegexOptions.None };
            yield return new object[] { @"m[\w-[^aeiou]][\w-[^aeiou]]t", "mbbt mect meet", RegexOptions.None };
            yield return new object[] { "[abcdef-[^bce]]+", "adfbcefda", RegexOptions.None };
            yield return new object[] { "[^cde-[ag]]+", "agbfxyzga", RegexOptions.None };
            yield return new object[] { @"[\p{L}-[^\p{Lu}]]+", "09',.abcxyzABCXYZ", RegexOptions.None };
            yield return new object[] { @"[\p{IsGreek}-[\P{Lu}]]+", "\u0390\u03FE\u0386\u0388\u03EC\u03EE\u0400", RegexOptions.None };
            yield return new object[] { @"[\p{IsBasicLatin}-[G-L]]+", "GAFMZL", RegexOptions.None };
            yield return new object[] { "[a-zA-Z-[aeiouAEIOU]]+", "aeiouAEIOUbcdfghjklmnpqrstvwxyz", RegexOptions.None };
            yield return new object[] { @"^
            (?<octet>^
                (
                    (
                        (?<Octet2xx>[\d-[013-9]])
                        |
                        [\d-[2-9]]
                    )
                    (?(Octet2xx)
                        (
                            (?<Octet25x>[\d-[01-46-9]])
                            |
                            [\d-[5-9]]
                        )
                        (
                            (?(Octet25x)
                                [\d-[6-9]]
                                |
                                [\d]
                            )
                        )
                        |
                        [\d]{2}
                    )
                )
                |
                ([\d][\d])
                |
                [\d]
            )$"
            , "255", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"[abcd\-d-[bc]]+", "bbbaaa---dddccc", RegexOptions.None };
            yield return new object[] { @"[abcd\-d-[bc]]+", "bbbaaa---dddccc", RegexOptions.None };
            yield return new object[] { @"[^a-f-[\x00-\x60\u007B-\uFFFF]]+", "aaafffgggzzz{{{", RegexOptions.None };
            yield return new object[] { @"[\[\]a-f-[[]]+", "gggaaafff]]][[[", RegexOptions.None };
            yield return new object[] { @"[\[\]a-f-[]]]+", "gggaaafff[[[]]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "a]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "b]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "c]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "d]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "a]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "b]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "c]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "d]]", RegexOptions.None };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "-]]", RegexOptions.None };
            yield return new object[] { @"[a-[c-e]]+", "bbbaaaccc", RegexOptions.None };
            yield return new object[] { @"[a-[c-e]]+", "```aaaccc", RegexOptions.None };
            yield return new object[] { @"[a-d\--[bc]]+", "cccaaa--dddbbb", RegexOptions.None };
            yield return new object[] { @"[\0- [bc]+", "!!!\0\0\t\t  [[[[bbbcccaaa", RegexOptions.None };
            yield return new object[] { "[[abcd]-[bc]]+", "a-b]", RegexOptions.None };
            yield return new object[] { "[-[e-g]+", "ddd[[[---eeefffggghhh", RegexOptions.None };
            yield return new object[] { "[-e-g]+", "ddd---eeefffggghhh", RegexOptions.None };
            yield return new object[] { "[-e-g]+", "ddd---eeefffggghhh", RegexOptions.None };
            yield return new object[] { "[a-e - m-p]+", "---a b c d e m n o p---", RegexOptions.None };
            yield return new object[] { "[^-[bc]]", "b] c] -] aaaddd]", RegexOptions.None };
            yield return new object[] { "[^-[bc]]", "b] c] -] aaa]ddd]", RegexOptions.None };
            yield return new object[] { @"[a\-[bc]+", "```bbbaaa---[[[cccddd", RegexOptions.None };
            yield return new object[] { @"[a\-[\-\-bc]+", "```bbbaaa---[[[cccddd", RegexOptions.None };
            yield return new object[] { @"[a\-\[\-\[\-bc]+", "```bbbaaa---[[[cccddd", RegexOptions.None };
            yield return new object[] { @"[abc\--[b]]+", "[[[```bbbaaa---cccddd", RegexOptions.None };
            yield return new object[] { @"[abc\-z-[b]]+", "```aaaccc---zzzbbb", RegexOptions.None };
            yield return new object[] { @"[a-d\-[b]+", "```aaabbbcccddd----[[[[]]]", RegexOptions.None };
            yield return new object[] { @"[abcd\-d\-[bc]+", "bbbaaa---[[[dddccc", RegexOptions.None };
            yield return new object[] { "[a - c - [ b ] ]+", "dddaaa   ccc [[[[ bbb ]]]", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { "[a - c - [ b ] +", "dddaaa   ccc [[[[ bbb ]]]", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(\p{Lu}\w*)\s(\p{Lu}\w*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"(\p{Lu}\p{Ll}*)\s(\p{Lu}\p{Ll}*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"(\P{Ll}\p{Ll}*)\s(\P{Ll}\p{Ll}*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"(\P{Lu}+\p{Lu})\s(\P{Lu}+\p{Lu})", "hellO worlD", RegexOptions.None };
            yield return new object[] { @"(\p{Lt}\w*)\s(\p{Lt}*\w*)", "\u01C5ello \u01C5orld", RegexOptions.None };
            yield return new object[] { @"(\P{Lt}\w*)\s(\P{Lt}*\w*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"[@-D]+", "eE?@ABCDabcdeE", RegexOptions.IgnoreCase };
            yield return new object[] { @"[>-D]+", "eE=>?@ABCDabcdeE", RegexOptions.IgnoreCase };
            yield return new object[] { @"[\u0554-\u0557]+", "\u0583\u0553\u0554\u0555\u0556\u0584\u0585\u0586\u0557\u0558", RegexOptions.IgnoreCase };
            yield return new object[] { @"[X-\]]+", "wWXYZxyz[\\]^", RegexOptions.IgnoreCase };
            yield return new object[] { @"[X-\u0533]+", "\u0551\u0554\u0560AXYZaxyz\u0531\u0532\u0533\u0561\u0562\u0563\u0564", RegexOptions.IgnoreCase };
            yield return new object[] { @"[X-a]+", "wWAXYZaxyz", RegexOptions.IgnoreCase };
            yield return new object[] { @"[X-c]+", "wWABCXYZabcxyz", RegexOptions.IgnoreCase };
            yield return new object[] { @"[X-\u00C0]+", "\u00C1\u00E1\u00C0\u00E0wWABCXYZabcxyz", RegexOptions.IgnoreCase };
            yield return new object[] { @"[\u0100\u0102\u0104]+", "\u00FF \u0100\u0102\u0104\u0101\u0103\u0105\u0106", RegexOptions.IgnoreCase };
            yield return new object[] { @"[B-D\u0130]+", "aAeE\u0129\u0131\u0068 BCDbcD\u0130\u0069\u0070", RegexOptions.IgnoreCase };
            yield return new object[] { @"[\u013B\u013D\u013F]+", "\u013A\u013B\u013D\u013F\u013C\u013E\u0140\u0141", RegexOptions.IgnoreCase };
            yield return new object[] { "(Cat)\r(Dog)", "Cat\rDog", RegexOptions.None };
            yield return new object[] { "(Cat)\t(Dog)", "Cat\tDog", RegexOptions.None };
            yield return new object[] { "(Cat)\f(Dog)", "Cat\fDog", RegexOptions.None };
            yield return new object[] { @"{5", "hello {5 world", RegexOptions.None };
            yield return new object[] { @"{5,", "hello {5, world", RegexOptions.None };
            yield return new object[] { @"{5,6", "hello {5,6 world", RegexOptions.None };
            yield return new object[] { @"(?n:(?<cat>cat)(\s+)(?<dog>dog))", "cat   dog", RegexOptions.None };
            yield return new object[] { @"(?n:(cat)(\s+)(dog))", "cat   dog", RegexOptions.None };
            yield return new object[] { @"(?n:(cat)(?<SpaceChars>\s+)(dog))", "cat   dog", RegexOptions.None };
            yield return new object[] { @"(?x:
                            (?<cat>cat) # Cat statement
                            (\s+) # Whitespace chars
                            (?<dog>dog # Dog statement
                            ))", "cat   dog", RegexOptions.None };
            yield return new object[] { @"(?+i:cat)", "CAT", RegexOptions.None };
            yield return new object[] { @"cat([\d]*)dog", "hello123cat230927dog1412d", RegexOptions.None };
            yield return new object[] { @"([\D]*)dog", "65498catdog58719", RegexOptions.None };
            yield return new object[] { @"cat([\s]*)dog", "wiocat   dog3270", RegexOptions.None };
            yield return new object[] { @"cat([\S]*)", "sfdcatdog    3270", RegexOptions.None };
            yield return new object[] { @"cat([\w]*)", "sfdcatdog    3270", RegexOptions.None };
            yield return new object[] { @"cat([\W]*)dog", "wiocat   dog3270", RegexOptions.None };
            yield return new object[] { @"([\p{Lu}]\w*)\s([\p{Lu}]\w*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"([\P{Ll}][\p{Ll}]*)\s([\P{Ll}][\p{Ll}]*)", "Hello World", RegexOptions.None };
            yield return new object[] { @"(cat)([\x41]*)(dog)", "catAAAdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\u0041]*)(dog)", "catAAAdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\a]*)(dog)", "cat\a\a\adog", RegexOptions.None };
            yield return new object[] { @"(cat)([\b]*)(dog)", "cat\b\b\bdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\e]*)(dog)", "cat\u001B\u001B\u001Bdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\f]*)(dog)", "cat\f\f\fdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\r]*)(dog)", "cat\r\r\rdog", RegexOptions.None };
            yield return new object[] { @"(cat)([\v]*)(dog)", "cat\v\v\vdog", RegexOptions.None };
            yield return new object[] { @"cat([\d]*)dog", "hello123cat230927dog1412d", RegexOptions.ECMAScript };
            yield return new object[] { @"([\D]*)dog", "65498catdog58719", RegexOptions.ECMAScript };
            yield return new object[] { @"cat([\s]*)dog", "wiocat   dog3270", RegexOptions.ECMAScript };
            yield return new object[] { @"cat([\S]*)", "sfdcatdog    3270", RegexOptions.ECMAScript };
            yield return new object[] { @"cat([\w]*)", "sfdcatdog    3270", RegexOptions.ECMAScript };
            yield return new object[] { @"cat([\W]*)dog", "wiocat   dog3270", RegexOptions.ECMAScript };
            yield return new object[] { @"([\p{Lu}]\w*)\s([\p{Lu}]\w*)", "Hello World", RegexOptions.ECMAScript };
            yield return new object[] { @"([\P{Ll}][\p{Ll}]*)\s([\P{Ll}][\p{Ll}]*)", "Hello World", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\d*dog", "hello123cat230927dog1412d", RegexOptions.ECMAScript };
            yield return new object[] { @"\D*(dog)", "65498catdog58719", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\s*(dog)", "wiocat   dog3270", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\S*", "sfdcatdog    3270", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\w*", "sfdcatdog    3270", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\W*(dog)", "wiocat   dog3270", RegexOptions.ECMAScript };
            yield return new object[] { @"\p{Lu}(\w*)\s\p{Lu}(\w*)", "Hello World", RegexOptions.ECMAScript };
            yield return new object[] { @"\P{Ll}\p{Ll}*\s\P{Ll}\p{Ll}*", "Hello World", RegexOptions.ECMAScript };
            yield return new object[] { @"cat(?<dog121>dog)", "catcatdogdogcat", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s*(?<cat>dog)", "catcat    dogdogcat", RegexOptions.None };
            yield return new object[] { @"(?<1>cat)\s*(?<1>dog)", "catcat    dogdogcat", RegexOptions.None };
            yield return new object[] { @"(?<2048>cat)\s*(?<2048>dog)", "catcat    dogdogcat", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\w+(?<dog-cat>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\w+(?<-cat>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\w+(?<cat-cat>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<1>cat)\w+(?<dog-1>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\w+(?<2-cat>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<1>cat)\w+(?<2-1>dog)", "cat_Hello_World_dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){", "STARTcat{", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){fdsa", "STARTcat{fdsa", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1", "STARTcat{1", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1END", "STARTcat{1END", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1,", "STARTcat{1,", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1,END", "STARTcat{1,END", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1,2", "STARTcat{1,2", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat){1,2END", "STARTcat{1,2END", RegexOptions.None };
            yield return new object[] { @"(cat) #cat
                            \s+ #followed by 1 or more whitespace
                            (dog)  #followed by dog
                            ", "cat    dog", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(cat) #cat
                            \s+ #followed by 1 or more whitespace
                            (dog)  #followed by dog", "cat    dog", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(cat) (?#cat)    \s+ (?#followed by 1 or more whitespace) (dog)  (?#followed by dog)", "cat    dog", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(?<cat>cat)(?<dog>dog)\k<cat>", "asdfcatdogcatdog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\k<cat>", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\k'cat'", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\<cat>", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\'cat'", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\k<1>", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\k'1'", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\<1>", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\'1'", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\1", "asdfcat   dogcat   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\1", "asdfcat   dogcat   dog", RegexOptions.ECMAScript };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\k<dog>", "asdfcat   dogdog   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\2", "asdfcat   dogdog   dog", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\2", "asdfcat   dogdog   dog", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\077)", "hellocat?dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\77)", "hellocat?dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\176)", "hellocat~dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\400)", "hellocat\0dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\300)", "hellocat\u00C0dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\300)", "hellocat\u00C0dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\477)", "hellocat\u003Fdogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\777)", "hellocat\u00FFdogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\7770)", "hellocat\u00FF0dogworld", RegexOptions.None };
            yield return new object[] { @"(cat)(\077)", "hellocat?dogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\77)", "hellocat?dogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\7)", "hellocat\adogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\40)", "hellocat dogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\040)", "hellocat dogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\176)", "hellocatcat76dogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\377)", "hellocat\u00FFdogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)(\400)", "hellocat 0Fdogworld", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\s+(?<2147483646>dog)", "asdlkcat  dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)\s+(?<2147483647>dog)", "asdlkcat  dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2a*)(dog)", "asdlkcat***dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2b*)(dog)", "asdlkcat+++dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2c*)(dog)", "asdlkcat,,,dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2d*)(dog)", "asdlkcat---dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2e*)(dog)", "asdlkcat...dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2A*)(dog)", "asdlkcat***dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2B*)(dog)", "asdlkcat+++dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2C*)(dog)", "asdlkcat,,,dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2D*)(dog)", "asdlkcat---dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\x2E*)(dog)", "asdlkcat...dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\c@*)(dog)", "asdlkcat\0\0dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cA*)(dog)", "asdlkcat\u0001dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\ca*)(dog)", "asdlkcat\u0001dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cC*)(dog)", "asdlkcat\u0003dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cc*)(dog)", "asdlkcat\u0003dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cD*)(dog)", "asdlkcat\u0004dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cd*)(dog)", "asdlkcat\u0004dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cX*)(dog)", "asdlkcat\u0018dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cx*)(dog)", "asdlkcat\u0018dogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cZ*)(dog)", "asdlkcat\u001adogiwod", RegexOptions.None };
            yield return new object[] { @"(cat)(\cz*)(dog)", "asdlkcat\u001adogiwod", RegexOptions.None };
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\n   dog", RegexOptions.None };
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\n   dog", RegexOptions.Multiline };
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\n   dog", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog", RegexOptions.None };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog", RegexOptions.Multiline };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog\n", RegexOptions.None };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog\n", RegexOptions.Multiline };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   \n\n\n   dog\n", RegexOptions.ECMAScript };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog", RegexOptions.None };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog", RegexOptions.Multiline };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog", RegexOptions.ECMAScript };
            yield return new object[] { @"\b@cat", "123START123@catEND", RegexOptions.None };
            yield return new object[] { @"\b\<cat", "123START123<catEND", RegexOptions.None };
            yield return new object[] { @"\b,cat", "satwe,,,START,catEND", RegexOptions.None };
            yield return new object[] { @"\b\[cat", "`12START123[catEND", RegexOptions.None };
            yield return new object[] { @"\B@cat", "123START123;@catEND", RegexOptions.None };
            yield return new object[] { @"\B\<cat", "123START123'<catEND", RegexOptions.None };
            yield return new object[] { @"\B,cat", "satwe,,,START',catEND", RegexOptions.None };
            yield return new object[] { @"\B\[cat", "`12START123'[catEND", RegexOptions.None };
            yield return new object[] { @"(\w+)\s+(\w+)", "cat\u02b0 dog\u02b1", RegexOptions.None };
            yield return new object[] { @"(cat\w+)\s+(dog\w+)", "STARTcat\u30FC dog\u3005END", RegexOptions.None };
            yield return new object[] { @"(cat\w+)\s+(dog\w+)", "STARTcat\uff9e dog\uff9fEND", RegexOptions.None };
            yield return new object[] { @"[^a]|d", "d", RegexOptions.None };
            yield return new object[] { @"([^a]|[d])*", "Hello Worlddf", RegexOptions.None };
            yield return new object[] { @"([^{}]|\n)+", "{{{{Hello\n World \n}END", RegexOptions.None };
            yield return new object[] { @"([a-d]|[^abcd])+", "\tonce\n upon\0 a- ()*&^%#time?", RegexOptions.None };
            yield return new object[] { @"([^a]|[a])*", "once upon a time", RegexOptions.None };
            yield return new object[] { @"([a-d]|[^abcd]|[x-z]|^wxyz])+", "\tonce\n upon\0 a- ()*&^%#time?", RegexOptions.None };
            yield return new object[] { @"([a-d]|[e-i]|[^e]|wxyz])+", "\tonce\n upon\0 a- ()*&^%#time?", RegexOptions.None };
            yield return new object[] { @"^(([^b]+ )|(.* ))$", "aaa ", RegexOptions.None };
            yield return new object[] { @"^(([^b]+ )|(.*))$", "aaa", RegexOptions.None };
            yield return new object[] { @"^(([^b]+ )|(.* ))$", "bbb ", RegexOptions.None };
            yield return new object[] { @"^(([^b]+ )|(.*))$", "bbb", RegexOptions.None };
            yield return new object[] { @"^((a*)|(.*))$", "aaa", RegexOptions.None };
            yield return new object[] { @"^((a*)|(.*))$", "aaabbb", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))*", "{hello 1234567890 world}", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))+", "{hello 1234567890 world}", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))*", "{HELLO 1234567890 world}", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))+", "{HELLO 1234567890 world}", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))*", "{1234567890 hello  world}", RegexOptions.None };
            yield return new object[] { @"(([0-9])|([a-z])|([A-Z]))+", "{1234567890 hello world}", RegexOptions.None };
            yield return new object[] { @"^(([a-d]*)|([a-z]*))$", "aaabbbcccdddeeefff", RegexOptions.None };
            yield return new object[] { @"^(([d-f]*)|([c-e]*))$", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"^(([c-e]*)|([d-f]*))$", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"(([a-d]*)|([a-z]*))", "aaabbbcccdddeeefff", RegexOptions.None };
            yield return new object[] { @"(([d-f]*)|([c-e]*))", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"(([c-e]*)|([d-f]*))", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"(([a-d]*)|(.*))", "aaabbbcccdddeeefff", RegexOptions.None };
            yield return new object[] { @"(([d-f]*)|(.*))", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"(([c-e]*)|(.*))", "dddeeeccceee", RegexOptions.None };
            yield return new object[] { @"\p{Pi}(\w*)\p{Pf}", "\u00ABCat\u00BB   \u00BBDog\u00AB'", RegexOptions.None };
            yield return new object[] { @"\p{Pi}(\w*)\p{Pf}", "\u2018Cat\u2019   \u2019Dog\u2018'", RegexOptions.None };
            yield return new object[] { @"(?<cat>cat)\s+(?<dog>dog)\s+\123\s+\234", "asdfcat   dog     cat23    dog34eia", RegexOptions.ECMAScript };
            yield return new object[] { @"<div> 
            (?> 
                <div>(?<DEPTH>) |   
                </div> (?<-DEPTH>) |  
                .?
            )*?
            (?(DEPTH)(?!)) 
            </div>", "<div>this is some <div>red</div> text</div></div></div>", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(
            ((?'open'<+)[^<>]*)+
            ((?'close-open'>+)[^<>]*)+
            )+", "<01deep_01<02deep_01<03deep_01>><02deep_02><02deep_03<03deep_03>>>", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(
            (?<start><)?
            [^<>]?
            (?<end-start>>)?
            )*", "<01deep_01<02deep_01<03deep_01>><02deep_02><02deep_03<03deep_03>>>", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(
            (?<start><[^/<>]*>)?
            [^<>]?
            (?<end-start></[^/<>]*>)?
            )*", "<b><a>Cat</a></b>", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"(
            (?<start><(?<TagName>[^/<>]*)>)?
            [^<>]?
            (?<end-start></\k<TagName>>)?
            )*", "<b>cat</b><a>dog</a>", RegexOptions.IgnorePatternWhitespace };
            yield return new object[] { @"([0-9]+?)([\w]+?)", "55488aheiaheiad", RegexOptions.ECMAScript };
            yield return new object[] { @"([0-9]+?)([a-z]+?)", "55488aheiaheiad", RegexOptions.ECMAScript };
            yield return new object[] { @"\G<%#(?<code>.*?)?%>", @"<%# DataBinder.Eval(this, ""MyNumber"") %>", RegexOptions.Singleline };
            yield return new object[] { @"^[abcd]{0,0x10}*$", "a{0,0x10}}}", RegexOptions.None };
            yield return new object[] { @"([a-z]*?)([\w])", "cat", RegexOptions.IgnoreCase };
            yield return new object[] { @"^([a-z]*?)([\w])$", "cat", RegexOptions.IgnoreCase };
            yield return new object[] { @"([a-z]*)([\w])", "cat", RegexOptions.IgnoreCase };
            yield return new object[] { @"^([a-z]*)([\w])$", "cat", RegexOptions.IgnoreCase };
            yield return new object[] { @"(cat){", "cat{", RegexOptions.None };
            yield return new object[] { @"(cat){}", "cat{}", RegexOptions.None };
            yield return new object[] { @"(cat){,", "cat{,", RegexOptions.None };
            yield return new object[] { @"(cat){,}", "cat{,}", RegexOptions.None };
            yield return new object[] { @"(cat){cat}", "cat{cat}", RegexOptions.None };
            yield return new object[] { @"(cat){cat,5}", "cat{cat,5}", RegexOptions.None };
            yield return new object[] { @"(cat){5,dog}", "cat{5,dog}", RegexOptions.None };
            yield return new object[] { @"(cat){cat,dog}", "cat{cat,dog}", RegexOptions.None };
            yield return new object[] { @"(cat){,}?", "cat{,}?", RegexOptions.None };
            yield return new object[] { @"(cat){cat}?", "cat{cat}?", RegexOptions.None };
            yield return new object[] { @"(cat){cat,5}?", "cat{cat,5}?", RegexOptions.None };
            yield return new object[] { @"(cat){5,dog}?", "cat{5,dog}?", RegexOptions.None };
            yield return new object[] { @"(cat){cat,dog}?", "cat{cat,dog}?", RegexOptions.None };
            yield return new object[] { @"()", "cat", RegexOptions.None };
            yield return new object[] { @"(?<cat>)", "cat", RegexOptions.None };
            yield return new object[] { @"(?'cat')", "cat", RegexOptions.None };
            yield return new object[] { @"(?:)", "cat", RegexOptions.None };
            yield return new object[] { @"(?imn)", "cat", RegexOptions.None };
            yield return new object[] { @"(?imn)cat", "(?imn)cat", RegexOptions.None };
            yield return new object[] { @"(?=)", "cat", RegexOptions.None };
            yield return new object[] { @"(?<=)", "cat", RegexOptions.None };
            yield return new object[] { @"(?>)", "cat", RegexOptions.None };
            yield return new object[] { @"(?()|)", "(?()|)", RegexOptions.None };
            yield return new object[] { @"(?(cat)|)", "cat", RegexOptions.None };
            yield return new object[] { @"(?(cat)|)", "dog", RegexOptions.None };
            yield return new object[] { @"(?(cat)catdog|)", "catdog", RegexOptions.None };
            yield return new object[] { @"(?(cat)catdog|)", "dog", RegexOptions.None };
            yield return new object[] { @"(?(cat)dog|)", "dog", RegexOptions.None };
            yield return new object[] { @"(?(cat)dog|)", "cat", RegexOptions.None };
            yield return new object[] { @"(?(cat)|catdog)", "cat", RegexOptions.None };
            yield return new object[] { @"(?(cat)|catdog)", "catdog", RegexOptions.None };
            yield return new object[] { @"(?(cat)|dog)", "dog", RegexOptions.None };
            yield return new object[] { "([\u0000-\uFFFF-[azAZ09]]|[\u0000-\uFFFF-[^azAZ09]])+", "azAZBCDE1234567890BCDEFAZza", RegexOptions.None };
            yield return new object[] { "[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[a]]]]]]+", "abcxyzABCXYZ123890", RegexOptions.None };
            yield return new object[] { "[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[\u0000-\uFFFF-[a]]]]]]]+", "bcxyzABCXYZ123890a", RegexOptions.None };
            yield return new object[] { "[\u0000-\uFFFF-[\\p{P}\\p{S}\\p{C}]]+", "!@`';.,$+<>=\x0001\x001FazAZ09", RegexOptions.None };
            yield return new object[] { @"[\uFFFD-\uFFFF]+", "\uFFFC\uFFFD\uFFFE\uFFFF", RegexOptions.IgnoreCase };
            yield return new object[] { @"[\uFFFC-\uFFFE]+", "\uFFFB\uFFFC\uFFFD\uFFFE\uFFFF", RegexOptions.IgnoreCase };
            yield return new object[] { @"([a*]*)+?$", "ab", RegexOptions.None };
            yield return new object[] { @"(a*)+?$", "b", RegexOptions.None };

            // Testing octal sequence matches: "\\060(\\061)?\\061"
            // Octal \061 is ASCII 49 ('1')
            yield return new object[] { @"\060(\061)?\061", "011", RegexOptions.None, 0, 3, true, "011" };

            // Testing hexadecimal sequence matches: "(\\x30\\x31\\x32)"
            // Hex \x31 is ASCII 49 ('1')
            yield return new object[] { @"(\x30\x31\x32)", "012", RegexOptions.None, 0, 3, true, "012" };

            // Testing control character escapes???: "2", "(\u0032)"
            yield return new object[] { "(\u0034)", "4", RegexOptions.None, 0, 1, true, "4", };

            // Using *, +, ?, {}: Actual - "a+\\.?b*\\.?c{2}"
            yield return new object[] { @"a+\.?b*\.+c{2}", "ab.cc", RegexOptions.None, 0, 5, true, "ab.cc" };

            // Using [a-z], \s, \w: Actual - "([a-zA-Z]+)\\s(\\w+)"
            yield return new object[] { @"([a-zA-Z]+)\s(\w+)", "David Bau", RegexOptions.None, 0, 9, true, "David Bau" };

            // \\S, \\d, \\D, \\W: Actual - "(\\S+):\\W(\\d+)\\s(\\D+)"
            yield return new object[] { @"(\S+):\W(\d+)\s(\D+)", "Price: 5 dollars", RegexOptions.None, 0, 16, true, "Price: 5 dollars" };

            // \\S, \\d, \\D, \\W: Actual - "[^0-9]+(\\d+)"
            yield return new object[] { @"[^0-9]+(\d+)", "Price: 30 dollars", RegexOptions.None, 0, 17, true, "Price: 30" };

            // Zero-width negative lookahead assertion: Actual - "abc(?!XXX)\\w+"
            yield return new object[] { @"abc(?!XXX)\w+", "abcXXXdef", RegexOptions.None, 0, 9, false, string.Empty };

            // Zero-width positive lookbehind assertion: Actual - "(\\w){6}(?<=XXX)def"
            yield return new object[] { @"(\w){6}(?<=XXX)def", "abcXXXdef", RegexOptions.None, 0, 9, true, "abcXXXdef" };

            // Zero-width negative lookbehind assertion: Actual - "(\\w){6}(?<!XXX)def"
            yield return new object[] { @"(\w){6}(?<!XXX)def", "XXXabcdef", RegexOptions.None, 0, 9, true, "XXXabcdef" };

            // Nonbacktracking subexpression: Actual - "[^0-9]+(?>[0-9]+)3"
            // The last 3 causes the match to fail, since the non backtracking subexpression does not give up the last digit it matched
            // for it to be a success. For a correct match, remove the last character, '3' from the pattern
            yield return new object[] { "[^0-9]+(?>[0-9]+)3", "abc123", RegexOptions.None, 0, 6, false, string.Empty };

            // Using beginning/end of string chars \A, \Z: Actual - "\\Aaaa\\w+zzz\\Z"
            yield return new object[] { @"\Aaaa\w+zzz\Z", "aaaasdfajsdlfjzzz", RegexOptions.None, 0, 17, true, "aaaasdfajsdlfjzzz" };

            // Using beginning/end of string chars \A, \Z: Actual - "\\Aaaa\\w+zzz\\Z"
            yield return new object[] { @"\Aaaa\w+zzz\Z", "aaaasdfajsdlfjzzza", RegexOptions.None, 0, 18, false, string.Empty };

            // Using beginning/end of string chars \A, \Z: Actual - "\\Aaaa\\w+zzz\\Z"
            yield return new object[] { @"\A(line2\n)line3\Z", "line2\nline3\n", RegexOptions.Multiline, 0, 12, true, "line2\nline3" };

            // Using beginning/end of string chars ^: Actual - "^b"
            yield return new object[] { "^b", "ab", RegexOptions.None, 0, 2, false, string.Empty };

            // Actual - "(?<char>\\w)\\<char>"
            yield return new object[] { @"(?<char>\w)\<char>", "aa", RegexOptions.None, 0, 2, true, "aa" };

            // Actual - "(?<43>\\w)\\43"
            yield return new object[] { @"(?<43>\w)\43", "aa", RegexOptions.None, 0, 2, true, "aa" };

            // Actual - "abc(?(1)111|222)"
            yield return new object[] { "(abbc)(?(1)111|222)", "abbc222", RegexOptions.None, 0, 7, false, string.Empty };

            // "x" option. Removes unescaped whitespace from the pattern: Actual - " ([^/]+) ","x"
            yield return new object[] { "            ((.)+)      ", "abc", RegexOptions.IgnorePatternWhitespace, 0, 3, true, "abc" };

            // "x" option. Removes unescaped whitespace from the pattern. : Actual - "\x20([^/]+)\x20","x"
            yield return new object[] { "\x20([^/]+)\x20\x20\x20\x20\x20\x20\x20", " abc       ", RegexOptions.IgnorePatternWhitespace, 0, 10, true, " abc      " };

            // Turning on case insensitive option in mid-pattern : Actual - "aaa(?i:match this)bbb"
            if ("i".ToUpper() == "I")
            {
                yield return new object[] { "aaa(?i:match this)bbb", "aaaMaTcH ThIsbbb", RegexOptions.None, 0, 16, true, "aaaMaTcH ThIsbbb" };
            }

            // Turning off case insensitive option in mid-pattern : Actual - "aaa(?-i:match this)bbb", "i"
            yield return new object[] { "aaa(?-i:match this)bbb", "AaAmatch thisBBb", RegexOptions.IgnoreCase, 0, 16, true, "AaAmatch thisBBb" };

            // Turning on/off all the options at once : Actual - "aaa(?imnsx-imnsx:match this)bbb", "i"
            yield return new object[] { "aaa(?-i:match this)bbb", "AaAmatcH thisBBb", RegexOptions.IgnoreCase, 0, 16, false, string.Empty };

            // Actual - "aaa(?#ignore this completely)bbb"
            yield return new object[] { "aaa(?#ignore this completely)bbb", "aaabbb", RegexOptions.None, 0, 6, true, "aaabbb" };

            // Trying empty string: Actual "[a-z0-9]+", ""
            yield return new object[] { "[a-z0-9]+", "", RegexOptions.None, 0, 0, false, string.Empty };

            // Numbering pattern slots: "(?<1>\\d{3})(?<2>\\d{3})(?<3>\\d{4})"
            yield return new object[] { @"(?<1>\d{3})(?<2>\d{3})(?<3>\d{4})", "8885551111", RegexOptions.None, 0, 10, true, "8885551111" };
            yield return new object[] { @"(?<1>\d{3})(?<2>\d{3})(?<3>\d{4})", "Invalid string", RegexOptions.None, 0, 14, false, string.Empty };

            // Not naming pattern slots at all: "^(cat|chat)"
            yield return new object[] { "^(cat|chat)", "cats are bad", RegexOptions.None, 0, 12, true, "cat" };

            yield return new object[] { "abc", "abc", RegexOptions.None, 0, 3, true, "abc" };
            yield return new object[] { "abc", "aBc", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { "abc", "aBc", RegexOptions.IgnoreCase, 0, 3, true, "aBc" };

            // Using *, +, ?, {}: Actual - "a+\\.?b*\\.?c{2}"
            yield return new object[] { @"a+\.?b*\.+c{2}", "ab.cc", RegexOptions.None, 0, 5, true, "ab.cc" };

            // RightToLeft
            yield return new object[] { @"\s+\d+", "sdf 12sad", RegexOptions.RightToLeft, 0, 9, true, " 12" };
            yield return new object[] { @"\s+\d+", " asdf12 ", RegexOptions.RightToLeft, 0, 6, false, string.Empty };
            yield return new object[] { "aaa", "aaabbb", RegexOptions.None, 3, 3, false, string.Empty };

            yield return new object[] { @"foo\d+", "0123456789foo4567890foo         ", RegexOptions.RightToLeft, 10, 3, false, string.Empty };
            yield return new object[] { @"foo\d+", "0123456789foo4567890foo         ", RegexOptions.RightToLeft, 11, 21, false, string.Empty };

            // IgnoreCase
            yield return new object[] { "AAA", "aaabbb", RegexOptions.IgnoreCase, 0, 6, true, "aaa" };
            yield return new object[] { @"\p{Lu}", "1bc", RegexOptions.IgnoreCase, 0, 3, true, "b" };
            yield return new object[] { @"\p{Ll}", "1bc", RegexOptions.IgnoreCase, 0, 3, true, "b" };
            yield return new object[] { @"\p{Lt}", "1bc", RegexOptions.IgnoreCase, 0, 3, true, "b" };
            yield return new object[] { @"\p{Lo}", "1bc", RegexOptions.IgnoreCase, 0, 3, false, string.Empty };

            // "\D+"
            yield return new object[] { @"\D+", "12321", RegexOptions.None, 0, 5, false, string.Empty };

            // Groups
            yield return new object[] { "(?<first_name>\\S+)\\s(?<last_name>\\S+)", "David Bau", RegexOptions.None, 0, 9, true, "David Bau" };

            // "^b"
            yield return new object[] { "^b", "abc", RegexOptions.None, 0, 3, false, string.Empty };

            // RightToLeft
            yield return new object[] { @"foo\d+", "0123456789foo4567890foo         ", RegexOptions.RightToLeft, 0, 32, true, "foo4567890" };
            yield return new object[] { @"foo\d+", "0123456789foo4567890foo         ", RegexOptions.RightToLeft, 10, 22, true, "foo4567890" };
            yield return new object[] { @"foo\d+", "0123456789foo4567890foo         ", RegexOptions.RightToLeft, 10, 4, true, "foo4" };

            // Trim leading and trailing whitespace
            yield return new object[] { @"\s*(.*?)\s*$", " Hello World ", RegexOptions.None, 0, 13, true, " Hello World " };

            // < in group
            yield return new object[] { @"(?<cat>cat)\w+(?<dog-0>dog)", "cat_Hello_World_dog", RegexOptions.None, 0, 19, false, string.Empty };

            // Atomic Zero-Width Assertions \A \Z \z \G \b \B
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\ncat     dog", RegexOptions.None, 0, 20, false, string.Empty };
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\ncat     dog", RegexOptions.Multiline, 0, 20, false, string.Empty };
            yield return new object[] { @"\A(cat)\s+(dog)", "cat   \n\n\ncat     dog", RegexOptions.ECMAScript, 0, 20, false, string.Empty };

            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   dog\n\n\ncat", RegexOptions.None, 0, 15, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   dog\n\n\ncat     ", RegexOptions.Multiline, 0, 20, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\Z", "cat   dog\n\n\ncat     ", RegexOptions.ECMAScript, 0, 20, false, string.Empty };

            yield return new object[] { @"(cat)\s+(dog)\z", "cat   dog\n\n\ncat", RegexOptions.None, 0, 15, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   dog\n\n\ncat     ", RegexOptions.Multiline, 0, 20, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   dog\n\n\ncat     ", RegexOptions.ECMAScript, 0, 20, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog\n", RegexOptions.None, 0, 16, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog\n", RegexOptions.Multiline, 0, 16, false, string.Empty };
            yield return new object[] { @"(cat)\s+(dog)\z", "cat   \n\n\n   dog\n", RegexOptions.ECMAScript, 0, 16, false, string.Empty };

            yield return new object[] { @"\b@cat", "123START123;@catEND", RegexOptions.None, 0, 19, false, string.Empty };
            yield return new object[] { @"\b<cat", "123START123'<catEND", RegexOptions.None, 0, 19, false, string.Empty };
            yield return new object[] { @"\b,cat", "satwe,,,START',catEND", RegexOptions.None, 0, 21, false, string.Empty };
            yield return new object[] { @"\b\[cat", "`12START123'[catEND", RegexOptions.None, 0, 19, false, string.Empty };

            yield return new object[] { @"\B@cat", "123START123@catEND", RegexOptions.None, 0, 18, false, string.Empty };
            yield return new object[] { @"\B<cat", "123START123<catEND", RegexOptions.None, 0, 18, false, string.Empty };
            yield return new object[] { @"\B,cat", "satwe,,,START,catEND", RegexOptions.None, 0, 20, false, string.Empty };
            yield return new object[] { @"\B\[cat", "`12START123[catEND", RegexOptions.None, 0, 18, false, string.Empty };

            // Lazy operator Backtracking
            yield return new object[] { @"http://([a-zA-z0-9\-]*\.?)*?(:[0-9]*)??/", "http://www.msn.com", RegexOptions.IgnoreCase, 0, 18, false, string.Empty };

            // Grouping Constructs Invalid Regular Expressions
            yield return new object[] { "(?!)", "(?!)cat", RegexOptions.None, 0, 7, false, string.Empty };
            yield return new object[] { "(?<!)", "(?<!)cat", RegexOptions.None, 0, 8, false, string.Empty };

            // Alternation construct
            yield return new object[] { "(?(cat)|dog)", "cat", RegexOptions.None, 0, 3, true, string.Empty };
            yield return new object[] { "(?(cat)|dog)", "catdog", RegexOptions.None, 0, 6, true, string.Empty };
            yield return new object[] { "(?(cat)dog1|dog2)", "catdog1", RegexOptions.None, 0, 7, false, string.Empty };
            yield return new object[] { "(?(cat)dog1|dog2)", "catdog2", RegexOptions.None, 0, 7, true, "dog2" };
            yield return new object[] { "(?(cat)dog1|dog2)", "catdog1dog2", RegexOptions.None, 0, 11, true, "dog2" };
            yield return new object[] { "(?(dog2))", "dog2", RegexOptions.None, 0, 4, true, string.Empty };
            yield return new object[] { "(?(cat)|dog)", "oof", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { "(?(a:b))", "a", RegexOptions.None, 0, 1, true, string.Empty };
            yield return new object[] { "(?(a:))", "a", RegexOptions.None, 0, 1, true, string.Empty };

            // No Negation
            yield return new object[] { "[abcd-[abcd]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { "[1234-[1234]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // All Negation
            yield return new object[] { "[^abcd-[^abcd]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { "[^1234-[^1234]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // No Negation        
            yield return new object[] { "[a-z-[a-z]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { "[0-9-[0-9]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // All Negation
            yield return new object[] { "[^a-z-[^a-z]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { "[^0-9-[^0-9]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // No Negation
            yield return new object[] { @"[\w-[\w]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\W-[\W]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\s-[\s]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\S-[\S]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\d-[\d]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\D-[\D]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // All Negation
            yield return new object[] { @"[^\w-[^\w]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\W-[^\W]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\s-[^\s]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\S-[^\S]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\d-[^\d]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\D-[^\D]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // MixedNegation
            yield return new object[] { @"[^\w-[\W]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\w-[^\W]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\s-[\S]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\s-[^\S]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\d-[\D]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\d-[^\D]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // No Negation
            yield return new object[] { @"[\p{Ll}-[\p{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\P{Ll}-[\P{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\p{Lu}-[\p{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\P{Lu}-[\P{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\p{Nd}-[\p{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\P{Nd}-[\P{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // All Negation
            yield return new object[] { @"[^\p{Ll}-[^\p{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\P{Ll}-[^\P{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\p{Lu}-[^\p{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\P{Lu}-[^\P{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\p{Nd}-[^\p{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\P{Nd}-[^\P{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // MixedNegation
            yield return new object[] { @"[^\p{Ll}-[\P{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\p{Ll}-[^\P{Ll}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\p{Lu}-[\P{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\p{Lu}-[^\P{Lu}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[^\p{Nd}-[\P{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };
            yield return new object[] { @"[\p{Nd}-[^\P{Nd}]]+", "abcxyzABCXYZ`!@#$%^&*()_-+= \t\n", RegexOptions.None, 0, 30, false, string.Empty };

            // Character Class Substraction
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "[]]", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "-]]", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "`]]", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { @"[ab\-\[cd-[-[]]]]", "e]]", RegexOptions.None, 0, 3, false, string.Empty };

            yield return new object[] { @"[ab\-\[cd-[[]]]]", "']]", RegexOptions.None, 0, 3, false, string.Empty };
            yield return new object[] { @"[ab\-\[cd-[[]]]]", "e]]", RegexOptions.None, 0, 3, false, string.Empty };

            yield return new object[] { @"[a-[a-f]]", "abcdefghijklmnopqrstuvwxyz", RegexOptions.None, 0, 26, false, string.Empty };
        }

        [Benchmark]
        public void RegexCtor()
        {
            for (int i = 0; i < 30; i++)
                foreach (object[] test in Match_TestData())
                    new Regex((string)test[0], (RegexOptions)test[2]);
        }
    }
}
