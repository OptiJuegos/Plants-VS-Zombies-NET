using System;
using System.Collections.Generic;
using System.Xml;

namespace Sexy
{

    internal class XMLParser : EncodingParser
    {

        protected void Fail(string theErrorText)
        {
            this.mHasFailed = true;
            this.mErrorText = theErrorText;
        }


        protected void Init()
        {
            this.mSection = "";
            this.mLineNum = 1;
            this.mHasFailed = false;
            this.mErrorText = "";
        }


        protected bool AddAttribute(XMLElement theElement, string theAttributeKey, string theAttributeValue)
        {
            bool result = true;
            if (theElement.mAttributes.ContainsKey(theAttributeKey))
            {
                result = false;
            }
            theElement.mAttributes[theAttributeKey] = theAttributeValue;
            Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
            if (enumerator.MoveNext())
            {
                KeyValuePair<string, string> keyValuePair = enumerator.Current;
                if (keyValuePair.Key == theAttributeKey && theAttributeKey != "/")
                {
                    theElement.mAttributeIteratorList.Add(enumerator);
                }
            }
            return result;
        }


        protected bool AddAttributeEncoded(XMLElement theElement, string theAttributeKey, string theAttributeValue)
        {
            bool result = true;
            if (theElement.mAttributesEncoded.ContainsKey(theAttributeKey))
            {
                result = false;
            }
            theElement.mAttributesEncoded[theAttributeKey] = theAttributeValue;
            Dictionary<string, string>.Enumerator enumerator = theElement.mAttributesEncoded.GetEnumerator();
            if (enumerator.MoveNext())
            {
                KeyValuePair<string, string> keyValuePair = enumerator.Current;
                if (keyValuePair.Key == theAttributeKey && theAttributeKey != "/")
                {
                    theElement.mAttributeEncodedIteratorList.Add(enumerator);
                }
            }
            return result;
        }


        public XMLParser()
        {
            this.mLineNum = 0;
            this.mAllowComments = false;
            this.mFileName = string.Empty;
        }


        public override void Dispose()
        {
            base.Dispose();
        }


        public override bool OpenFile(string theFileName)
        {
            if (!base.OpenFile(theFileName))
            {
                this.mLineNum = 0;
                this.Fail("Unable to open file " + theFileName);
                return false;
            }
            this.mFileName = theFileName;
            this.Init();
            return true;
        }


        public virtual bool NextElement(ref XMLElement theElement)
        {
            string text7;
            for (; ; )
            {
                theElement.mType = XMLElementType.TYPE_NONE;
                theElement.mSection = this.mSection;
                theElement.mValue = "";
                theElement.mValueEncoded = "";
                theElement.mAttributes.Clear();
                theElement.mAttributesEncoded.Clear();
                theElement.mInstruction = "";
                theElement.mAttributeIteratorList.Clear();
                theElement.mAttributeEncodedIteratorList.Clear();
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                bool flag5 = false;
                string text = "";
                string text2 = "";
                string text3 = "";
                string text4 = "";
                char c;
                int length;
                int length2;
                string text5;
                for (; ; )
                {
                    c = '\0';
                    int num;
                    switch (this.GetChar(ref c))
                    {
                        case EncodingParser.GetCharReturnType.SUCCESSFUL:
                            num = 1;
                            break;
                        case EncodingParser.GetCharReturnType.INVALID_CHARACTER:
                            this.Fail("Illegal Character");
                            return false;
                        case EncodingParser.GetCharReturnType.END_OF_FILE:
                            num = 0;
                            break;
                        case EncodingParser.GetCharReturnType.FAILURE:
                            this.Fail("Internal Error");
                            return false;
                        default:
                            num = 0;
                            break;
                    }
                    if (num != 1)
                    {
                        if (theElement.mType != XMLElementType.TYPE_NONE)
                        {
                            this.Fail("Unexpected End of File");
                        }
                        return false;
                    }
                    bool flag6 = false;
                    if (c == '\n')
                    {
                        this.mLineNum++;
                    }
                    if (theElement.mType == XMLElementType.TYPE_COMMENT)
                    {
                        XMLElement xmlelement = theElement;
                        xmlelement.mInstruction += c;
                        length = theElement.mInstruction.Length;
                        if (c == '>' && length >= 3 && theElement.mInstruction[length - 2] == '-' && theElement.mInstruction[length - 3] == '-')
                        {
                            theElement.mInstruction = theElement.mInstruction.Substring(0, length - 3);
                            break;
                        }
                        continue;
                    }
                    else if (theElement.mType == XMLElementType.TYPE_INSTRUCTION)
                    {
                        if (theElement.mInstruction.Length != 0 || char.IsWhiteSpace(c))
                        {
                            XMLElement xmlelement2 = theElement;
                            xmlelement2.mInstruction += c;
                            length2 = theElement.mInstruction.Length;
                            text5 = theElement.mInstruction;
                        }
                        else
                        {
                            XMLElement xmlelement3 = theElement;
                            xmlelement3.mValue += c;
                            length2 = theElement.mValue.Length;
                            text5 = theElement.mValue;
                        }
                        if (c == '>' && length2 >= 2 && text5[length2 - 2] == '?')
                        {
                            text5 = text5.Substring(0, length2 - 2);
                            break;
                        }
                        continue;
                    }
                    else
                    {
                        if (c == '"')
                        {
                            flag2 = !flag2;
                            if (theElement.mType == XMLElementType.TYPE_NONE || theElement.mType == XMLElementType.TYPE_ELEMENT)
                            {
                                flag6 = true;
                            }
                            if (!flag2)
                            {
                                flag3 = true;
                            }
                        }
                        else if (!flag2)
                        {
                            if (c == '<')
                            {
                                if (theElement.mType == XMLElementType.TYPE_ELEMENT)
                                {
                                    this.PutChar(c);
                                    break;
                                }
                                if (theElement.mType != XMLElementType.TYPE_NONE)
                                {
                                    this.Fail("Unexpected '<'");
                                    return false;
                                }
                                theElement.mType = XMLElementType.TYPE_START;
                            }
                            else
                            {
                                if (c == '>')
                                {
                                    if (theElement.mType == XMLElementType.TYPE_START)
                                    {
                                        bool flag7 = false;
                                        if (text == "/")
                                        {
                                            flag7 = true;
                                        }
                                        else
                                        {
                                            if (text.Length > 0)
                                            {
                                                text3 = this.XMLDecodeString(text);
                                                text4 = text;
                                                this.AddAttribute(theElement, this.XMLDecodeString(text), this.XMLDecodeString(text2));
                                                this.AddAttributeEncoded(theElement, text, text2);
                                                text = "";
                                                text2 = "";
                                            }
                                            if (text3.Length > 0)
                                            {
                                                string text6 = theElement.mAttributes[text3];
                                                int length3 = text6.Length;
                                                if (length3 > 0 && text6[length3 - 1] == '/')
                                                {
                                                    this.AddAttribute(theElement, text3, this.XMLDecodeString(text6.Substring(0, length3 - 1)));
                                                    flag7 = true;
                                                }
                                                text6 = theElement.mAttributesEncoded[text4];
                                                length3 = text6.Length;
                                                if (length3 > 0 && text6[length3 - 1] == '/')
                                                {
                                                    this.AddAttributeEncoded(theElement, text4, text6.Substring(0, length3 - 1));
                                                    flag7 = true;
                                                }
                                            }
                                            else
                                            {
                                                int length4 = theElement.mValue.Length;
                                                if (length4 > 0 && theElement.mValue[length4 - 1] == '/')
                                                {
                                                    theElement.mValue = theElement.mValue.Substring(0, length4 - 1);
                                                    flag7 = true;
                                                }
                                            }
                                        }
                                        if (flag7)
                                        {
                                            string theString = "</" + theElement.mValue + ">";
                                            this.PutString(theString);
                                            text = "";
                                        }
                                        if (this.mSection.Length != 0)
                                        {
                                            this.mSection += "/";
                                        }
                                        this.mSection += theElement.mValue;
                                        break;
                                    }
                                    if (theElement.mType != XMLElementType.TYPE_END)
                                    {
                                        this.Fail("Unexpected '>'");
                                        return false;
                                    }
                                    int b2num2 = this.mSection.LastIndexOf('/');
                                    if (b2num2 == -1 && this.mSection.Length == 0)
                                    {
                                        this.Fail("Unexpected End");
                                        return false;
                                    }
                                    text7 = this.mSection.Substring(b2num2 + 1);
                                    if (text7 != theElement.mValue)
                                    {
                                        this.Fail(string.Concat(new string[]
                                        {
                        "End '",
                        theElement.mValue,
                        "' Doesn't Match Start '",
                        text7,
                        "'"
                                        }));
                                        return false;
                                    }
                                    if (b2num2 == -1)
                                    {
                                        this.mSection = this.mSection.Remove(0, this.mSection.Length);
                                        break;
                                    }
                                    this.mSection = this.mSection.Remove(b2num2, this.mSection.Length - b2num2);
                                    break;
                                }
                                if (c == '/' && theElement.mType == XMLElementType.TYPE_START && theElement.mValue == "")
                                {
                                    theElement.mType = XMLElementType.TYPE_END;
                                }
                                else if (c == '?' && theElement.mType == XMLElementType.TYPE_START && theElement.mValue == "")
                                {
                                    theElement.mType = XMLElementType.TYPE_INSTRUCTION;
                                }
                                else if (char.IsWhiteSpace(c))
                                {
                                    if (theElement.mValue != "")
                                    {
                                        flag = true;
                                    }
                                }
                                else
                                {
                                    if (c <= ' ')
                                    {
                                        this.Fail("Illegal Character");
                                        return false;
                                    }
                                    flag6 = true;
                                }
                            }
                        }
                        else
                        {
                            flag6 = true;
                        }
                        if (!flag6)
                        {
                            continue;
                        }
                        if (theElement.mType == XMLElementType.TYPE_NONE)
                        {
                            theElement.mType = XMLElementType.TYPE_ELEMENT;
                        }
                        if (theElement.mType != XMLElementType.TYPE_START)
                        {
                            if (flag)
                            {
                                XMLElement xmlelement4 = theElement;
                                xmlelement4.mValue += " ";
                                flag = false;
                            }
                            XMLElement xmlelement5 = theElement;
                            xmlelement5.mValue += c;
                            continue;
                        }
                        if (flag)
                        {
                            if (!flag4)
                            {
                                flag4 = true;
                                flag5 = false;
                            }
                            else if (!flag2)
                            {
                                if ((!flag5 && c != '=') || (flag5 && (!string.IsNullOrEmpty(text2) || flag3)))
                                {
                                    this.AddAttribute(theElement, this.XMLDecodeString(text), this.XMLDecodeString(text2));
                                    this.AddAttributeEncoded(theElement, text, text2);
                                    text = "";
                                    text2 = "";
                                    text3 = "";
                                    text4 = "";
                                }
                                else
                                {
                                    flag4 = true;
                                }
                                flag5 = false;
                            }
                            flag = false;
                        }
                        if (!flag4)
                        {
                            XMLElement xmlelement6 = theElement;
                            xmlelement6.mValue += c;
                            if (theElement.mValue == "!--")
                            {
                                theElement.mType = XMLElementType.TYPE_COMMENT;
                                continue;
                            }
                            continue;
                        }
                        else
                        {
                            if (!flag2 && c == '=')
                            {
                                flag5 = true;
                                flag3 = false;
                                continue;
                            }
                            if (!flag5)
                            {
                                text += c;
                                continue;
                            }
                            text2 += c;
                            continue;
                        }
                    }
                }
                if (text.Length > 0)
                {
                    this.AddAttribute(theElement, this.XMLDecodeString(text), this.XMLDecodeString(text2));
                    this.AddAttribute(theElement, text, text2);
                }
                theElement.mValueEncoded = theElement.mValue;
                theElement.mValue = this.XMLDecodeString(theElement.mValue);
                if (theElement.mType != XMLElementType.TYPE_COMMENT || this.mAllowComments)
                {
                    return true;
                }
                continue;
            }
        }


        private string XMLDecodeString(string s)
        {
            return XmlConvert.DecodeName(s);
        }


        public string GetErrorText()
        {
            return this.mErrorText;
        }


        public int GetCurrentLineNum()
        {
            return this.mLineNum;
        }


        public string GetFileName()
        {
            return this.mFileName;
        }


        public override void SetStringSource(string theString)
        {
            this.Init();
            base.SetStringSource(theString);
        }


        public void AllowComments(bool doAllow)
        {
            this.mAllowComments = doAllow;
        }


        public bool HasFailed()
        {
            return this.mHasFailed;
        }


        protected string mFileName;


        protected string mErrorText;


        protected int mLineNum;


        protected bool mHasFailed;


        protected bool mAllowComments;


        protected string mSection;
    }
}
