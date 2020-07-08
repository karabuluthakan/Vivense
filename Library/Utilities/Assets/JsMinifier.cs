using System;
using System.IO;
using System.Text;

namespace Library.Utilities.Assets
{
    /// <summary>
    /// 
    /// </summary>
    public class JsMinifier
    {
        const int EOF = -1;
        StringReader sr;
        StringWriter sw;
        int theA;
        int theB;
        int theLookahead = EOF;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string CompressJS(string body)
        {
            return new JsMinifier().Minify(body);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private string Minify(string src)
        {
            var sb = new StringBuilder();
            using (sr = new StringReader(src))
            {
                using (sw = new StringWriter(sb))
                {
                    JsMin();
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Copy the input to the output, deleting the characters which are
        ///     insignificant to JavaScript.Comments will be removed.Tabs will be
        ///     replaced with spaces.Carriage returns will be replaced with linefeeds.
        ///     Most spaces and linefeeds will be removed.
        /// </summary>
        private void JsMin()
        {
            theA = '\n';
            Action(3);
            while (theA != EOF)
            {
                switch (theA)
                {
                    case ' ':
                    {
                        Action(IsAlphanum(theB) ? 1 : 2);

                        break;
                    }
                    case '\n':
                    {
                        switch (theB)
                        {
                            case '{':
                            case '[':
                            case '(':
                            case '+':
                            case '-':
                            {
                                Action(1);
                                break;
                            }
                            case ' ':
                            {
                                Action(3);
                                break;
                            }
                            default:
                            {
                                Action(IsAlphanum(theB) ? 1 : 2);

                                break;
                            }
                        }

                        break;
                    }
                    default:
                    {
                        switch (theB)
                        {
                            case ' ':
                            {
                                if (IsAlphanum(theA))
                                {
                                    Action(1);
                                    break;
                                }

                                Action(3);
                                break;
                            }
                            case '\n':
                            {
                                switch (theA)
                                {
                                    case '}':
                                    case ']':
                                    case ')':
                                    case '+':
                                    case '-':
                                    case '"':
                                    case '\'':
                                    {
                                        Action(1);
                                        break;
                                    }
                                    default:
                                    {
                                        Action(IsAlphanum(theA) ? 1 : 3);

                                        break;
                                    }
                                }

                                break;
                            }
                            default:
                            {
                                Action(1);
                                break;
                            }
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        ///     action -- do something! What you do is determined by the argument:
        ///        1   Output A.Copy B to A.Get the next B.
        ///        2   Copy B to A. Get the next B. (Delete A).
        ///        3   Get the next B. (Delete B).
        ///    action treats a string as a single character.Wow!
        ///    action recognizes a regular expression if it is preceded by (or, or =.
        /// </summary>
        /// <param name="d"></param>
        private void Action(int d)
        {
            if (d <= 1)
            {
                PutNext(theA);
            }

            if (d <= 2)
            {
                theA = theB;
                if (theA == '\'' || theA == '"')
                {
                    for (;;)
                    {
                        PutNext(theA);
                        theA = GetNext();
                        if (theA == theB)
                        {
                            break;
                        }

                        if (theA <= '\n')
                        {
                            throw new Exception($"Error: JSMIN unterminated string literal: {theA}\n");
                        }

                        if (theA == '\\')
                        {
                            PutNext(theA);
                            theA = GetNext();
                        }
                    }
                }
            }

            if (d <= 3)
            {
                theB = Next();
                if (theB == '/'
                    && (theA == '('
                        || theA == ','
                        || theA == '='
                        || theA == '['
                        || theA == '!'
                        || theA == ':'
                        || theA == '&'
                        || theA == '|'
                        || theA == '?'
                        || theA == '{'
                        || theA == '}'
                        || theA == ';'
                        || theA == '\n'))
                {
                    PutNext(theA);
                    PutNext(theB);
                    while (true)
                    {
                        theA = GetNext();
                        if (theA == '/')
                        {
                            break;
                        } else if (theA == '\\')
                        {
                            PutNext(theA);
                            theA = GetNext();
                        } else if (theA <= '\n')
                        {
                            throw new Exception($"Error: JSMIN unterminated Regular Expression literal : {theA}.\n");
                        }

                        PutNext(theA);
                    }

                    theB = Next();
                }
            }
        }

        /// <summary>
        ///     next -- get the next character, excluding comments. peek() is used to see
        ///             if a '/' is followed by a '/' or '*'.
        /// </summary>
        /// <returns></returns>
        private int Next()
        {
            var c = GetNext();
            if (c == '/')
            {
                switch (Peek())
                {
                    case '/':
                    {
                        while (true)
                        {
                            c = GetNext();
                            if (c <= '\n')
                            {
                                return c;
                            }
                        }
                    }
                    case '*':
                    {
                        GetNext();
                        while (true)
                        {
                            switch (GetNext())
                            {
                                case '*':
                                {
                                    if (Peek() == '/')
                                    {
                                        GetNext();
                                        return ' ';
                                    }

                                    break;
                                }
                                case EOF:
                                {
                                    throw new Exception("Error: JSMIN Unterminated comment.\n");
                                }
                            }
                        }
                    }
                    default:
                    {
                        return c;
                    }
                }
            }

            return c;
        }

        /// <summary>
        ///     peek -- get the next character without getting it.
        /// </summary>
        /// <returns></returns>
        private int Peek()
        {
            theLookahead = GetNext();
            return theLookahead;
        }

        /// <summary>
        ///     get --  return the next character from stdin. Watch out for lookahead. If
        ///             the character is a control character, translate it to a space or
        ///             linefeed.
        /// </summary>
        /// <returns></returns>
        private int GetNext()
        {
            var c = theLookahead;
            theLookahead = EOF;
            if (c == EOF)
            {
                c = sr.Read();
            }

            if (c >= ' ' || c == '\n' || c == EOF)
            {
                return c;
            }

            return c == '\r' ? '\n' : ' ';
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        private void PutNext(int c)
        {
            sw.Write((char) c);
        }

        /* 
        */
        /// <summary>
        ///     isAlphanum --   return true if the character is a letter, digit, underscore,
        ///                     dollar sign, or non-ASCII character.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        bool IsAlphanum(int c)
        {
            return ((c >= 'a' && c <= 'z')
                    || (c >= '0' && c <= '9')
                    || (c >= 'A' && c <= 'Z')
                    || c == '_'
                    || c == '$'
                    || c == '\\'
                    || c == '+'
                    || c > 126);
        }
    }
}