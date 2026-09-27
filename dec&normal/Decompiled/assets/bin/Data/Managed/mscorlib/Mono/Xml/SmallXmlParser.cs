using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x0200004A RID: 74
	internal class SmallXmlParser
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000B98C File Offset: 0x00009B8C
		private Exception Error(string msg)
		{
			return new SmallXmlParserException(msg, this.line, this.column);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		private Exception UnexpectedEndError()
		{
			string[] array = new string[this.elementNames.Count];
			this.elementNames.CopyTo(array, 0);
			return this.Error(string.Format("Unexpected end of stream. Element stack content is {0}", string.Join(",", array)));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		private bool IsNameChar(char c, bool start)
		{
			if (c == '-' || c == '.')
			{
				return !start;
			}
			if (c == ':' || c == '_')
			{
				return true;
			}
			if (c > 'Ā')
			{
				if (c == 'ۥ' || c == 'ۦ' || c == 'ՙ')
				{
					return true;
				}
				if ('ʻ' <= c && c <= 'ˁ')
				{
					return true;
				}
			}
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				return true;
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.DecimalDigitNumber:
				return !start;
			default:
				return false;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		private bool IsWhitespace(int c)
		{
			switch (c)
			{
			case 9:
			case 10:
			case 13:
				break;
			default:
				if (c != 32)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		public void SkipWhitespaces()
		{
			this.SkipWhitespaces(false);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000BAFC File Offset: 0x00009CFC
		private void HandleWhitespaces()
		{
			while (this.IsWhitespace(this.Peek()))
			{
				this.buffer.Append((char)this.Read());
			}
			if (this.Peek() != 60 && this.Peek() >= 0)
			{
				this.isWhitespace = false;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000BB54 File Offset: 0x00009D54
		public void SkipWhitespaces(bool expected)
		{
			for (;;)
			{
				int num = this.Peek();
				switch (num)
				{
				case 9:
				case 10:
				case 13:
					break;
				default:
					if (num != 32)
					{
						goto Block_0;
					}
					break;
				}
				this.Read();
				if (expected)
				{
					expected = false;
				}
			}
			Block_0:
			if (expected)
			{
				throw this.Error("Whitespace is expected.");
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		private int Peek()
		{
			return this.reader.Peek();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		private int Read()
		{
			int num = this.reader.Read();
			if (num == 10)
			{
				this.resetColumn = true;
			}
			if (this.resetColumn)
			{
				this.line++;
				this.resetColumn = false;
				this.column = 1;
			}
			else
			{
				this.column++;
			}
			return num;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000BC34 File Offset: 0x00009E34
		public void Expect(int c)
		{
			int num = this.Read();
			if (num < 0)
			{
				throw this.UnexpectedEndError();
			}
			if (num != c)
			{
				throw this.Error(string.Format("Expected '{0}' but got {1}", (char)c, (char)num));
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000BC7C File Offset: 0x00009E7C
		private string ReadUntil(char until, bool handleReferences)
		{
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == until)
				{
					string result = this.buffer.ToString();
					this.buffer.Length = 0;
					return result;
				}
				if (handleReferences && c == '&')
				{
					this.ReadReference();
				}
				else
				{
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		public string ReadName()
		{
			int num = 0;
			if (this.Peek() < 0 || !this.IsNameChar((char)this.Peek(), true))
			{
				throw this.Error("XML name start character is expected.");
			}
			for (int i = this.Peek(); i >= 0; i = this.Peek())
			{
				char c = (char)i;
				if (!this.IsNameChar(c, false))
				{
					break;
				}
				if (num == this.nameBuffer.Length)
				{
					char[] destinationArray = new char[num * 2];
					Array.Copy(this.nameBuffer, destinationArray, num);
					this.nameBuffer = destinationArray;
				}
				this.nameBuffer[num++] = c;
				this.Read();
			}
			if (num == 0)
			{
				throw this.Error("Valid XML name is expected.");
			}
			return new string(this.nameBuffer, 0, num);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000BDBC File Offset: 0x00009FBC
		public void Parse(TextReader input, SmallXmlParser.IContentHandler handler)
		{
			this.reader = input;
			this.handler = handler;
			handler.OnStartParsing(this);
			while (this.Peek() >= 0)
			{
				this.ReadContent();
			}
			this.HandleBufferedContent();
			if (this.elementNames.Count > 0)
			{
				throw this.Error(string.Format("Insufficient close tag: {0}", this.elementNames.Peek()));
			}
			handler.OnEndParsing(this);
			this.Cleanup();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000BE38 File Offset: 0x0000A038
		private void Cleanup()
		{
			this.line = 1;
			this.column = 0;
			this.handler = null;
			this.reader = null;
			this.elementNames.Clear();
			this.xmlSpaces.Clear();
			this.attributes.Clear();
			this.buffer.Length = 0;
			this.xmlSpace = null;
			this.isWhitespace = false;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000BE9C File Offset: 0x0000A09C
		public void ReadContent()
		{
			if (this.IsWhitespace(this.Peek()))
			{
				if (this.buffer.Length == 0)
				{
					this.isWhitespace = true;
				}
				this.HandleWhitespaces();
			}
			if (this.Peek() != 60)
			{
				this.ReadCharacters();
				return;
			}
			this.Read();
			int num = this.Peek();
			if (num != 33)
			{
				if (num != 47)
				{
					string text;
					if (num != 63)
					{
						this.HandleBufferedContent();
						text = this.ReadName();
						while (this.Peek() != 62 && this.Peek() != 47)
						{
							this.ReadAttribute(this.attributes);
						}
						this.handler.OnStartElement(text, this.attributes);
						this.attributes.Clear();
						this.SkipWhitespaces();
						if (this.Peek() == 47)
						{
							this.Read();
							this.handler.OnEndElement(text);
						}
						else
						{
							this.elementNames.Push(text);
							this.xmlSpaces.Push(this.xmlSpace);
						}
						this.Expect(62);
						return;
					}
					this.HandleBufferedContent();
					this.Read();
					text = this.ReadName();
					this.SkipWhitespaces();
					string text2 = string.Empty;
					if (this.Peek() != 63)
					{
						for (;;)
						{
							text2 += this.ReadUntil('?', false);
							if (this.Peek() == 62)
							{
								break;
							}
							text2 += "?";
						}
					}
					this.handler.OnProcessingInstruction(text, text2);
					this.Expect(62);
					return;
				}
				else
				{
					this.HandleBufferedContent();
					if (this.elementNames.Count == 0)
					{
						throw this.UnexpectedEndError();
					}
					this.Read();
					string text = this.ReadName();
					this.SkipWhitespaces();
					string text3 = (string)this.elementNames.Pop();
					this.xmlSpaces.Pop();
					if (this.xmlSpaces.Count > 0)
					{
						this.xmlSpace = (string)this.xmlSpaces.Peek();
					}
					else
					{
						this.xmlSpace = null;
					}
					if (text != text3)
					{
						throw this.Error(string.Format("End tag mismatch: expected {0} but found {1}", text3, text));
					}
					this.handler.OnEndElement(text);
					this.Expect(62);
					return;
				}
			}
			else
			{
				this.Read();
				if (this.Peek() == 91)
				{
					this.Read();
					if (this.ReadName() != "CDATA")
					{
						throw this.Error("Invalid declaration markup");
					}
					this.Expect(91);
					this.ReadCDATASection();
					return;
				}
				else
				{
					if (this.Peek() == 45)
					{
						this.ReadComment();
						return;
					}
					if (this.ReadName() != "DOCTYPE")
					{
						throw this.Error("Invalid declaration markup.");
					}
					throw this.Error("This parser does not support document type.");
				}
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000C174 File Offset: 0x0000A374
		private void HandleBufferedContent()
		{
			if (this.buffer.Length == 0)
			{
				return;
			}
			if (this.isWhitespace)
			{
				this.handler.OnIgnorableWhitespace(this.buffer.ToString());
			}
			else
			{
				this.handler.OnChars(this.buffer.ToString());
			}
			this.buffer.Length = 0;
			this.isWhitespace = false;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		private void ReadCharacters()
		{
			this.isWhitespace = false;
			for (;;)
			{
				int num = this.Peek();
				int num2 = num;
				if (num2 == -1)
				{
					break;
				}
				if (num2 != 38)
				{
					if (num2 == 60)
					{
						return;
					}
					this.buffer.Append((char)this.Read());
				}
				else
				{
					this.Read();
					this.ReadReference();
				}
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000C250 File Offset: 0x0000A450
		private void ReadReference()
		{
			if (this.Peek() != 35)
			{
				string text = this.ReadName();
				this.Expect(59);
				string text2 = text;
				switch (text2)
				{
				case "amp":
					this.buffer.Append('&');
					return;
				case "quot":
					this.buffer.Append('"');
					return;
				case "apos":
					this.buffer.Append('\'');
					return;
				case "lt":
					this.buffer.Append('<');
					return;
				case "gt":
					this.buffer.Append('>');
					return;
				}
				throw this.Error("General non-predefined entity reference is not supported in this parser.");
			}
			this.Read();
			this.ReadCharacterReference();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000C384 File Offset: 0x0000A584
		private int ReadCharacterReference()
		{
			int num = 0;
			if (this.Peek() == 120)
			{
				this.Read();
				for (int i = this.Peek(); i >= 0; i = this.Peek())
				{
					if (48 <= i && i <= 57)
					{
						num <<= 4 + i - 48;
					}
					else if (65 <= i && i <= 70)
					{
						num <<= 4 + i - 65 + 10;
					}
					else
					{
						if (97 > i || i > 102)
						{
							break;
						}
						num <<= 4 + i - 97 + 10;
					}
					this.Read();
				}
			}
			else
			{
				for (int j = this.Peek(); j >= 0; j = this.Peek())
				{
					if (48 > j || j > 57)
					{
						break;
					}
					num <<= 4 + j - 48;
					this.Read();
				}
			}
			return num;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000C484 File Offset: 0x0000A684
		private void ReadAttribute(SmallXmlParser.AttrListImpl a)
		{
			this.SkipWhitespaces(true);
			if (this.Peek() == 47 || this.Peek() == 62)
			{
				return;
			}
			string text = this.ReadName();
			this.SkipWhitespaces();
			this.Expect(61);
			this.SkipWhitespaces();
			int num = this.Read();
			string value;
			if (num != 34)
			{
				if (num != 39)
				{
					throw this.Error("Invalid attribute value markup.");
				}
				value = this.ReadUntil('\'', true);
			}
			else
			{
				value = this.ReadUntil('"', true);
			}
			if (text == "xml:space")
			{
				this.xmlSpace = value;
			}
			a.Add(text, value);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000C534 File Offset: 0x0000A734
		private void ReadCDATASection()
		{
			int num = 0;
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == ']')
				{
					num++;
				}
				else
				{
					if (c == '>' && num > 1)
					{
						for (int i = num; i > 2; i--)
						{
							this.buffer.Append(']');
						}
						return;
					}
					for (int j = 0; j < num; j++)
					{
						this.buffer.Append(']');
					}
					num = 0;
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		private void ReadComment()
		{
			this.Expect(45);
			this.Expect(45);
			for (;;)
			{
				if (this.Read() == 45)
				{
					if (this.Read() == 45)
					{
						break;
					}
				}
			}
			if (this.Read() != 62)
			{
				throw this.Error("'--' is not allowed inside comment markup.");
			}
		}

		// Token: 0x04000133 RID: 307
		private SmallXmlParser.IContentHandler handler;

		// Token: 0x04000134 RID: 308
		private TextReader reader;

		// Token: 0x04000135 RID: 309
		private Stack elementNames = new Stack();

		// Token: 0x04000136 RID: 310
		private Stack xmlSpaces = new Stack();

		// Token: 0x04000137 RID: 311
		private string xmlSpace;

		// Token: 0x04000138 RID: 312
		private StringBuilder buffer = new StringBuilder(200);

		// Token: 0x04000139 RID: 313
		private char[] nameBuffer = new char[30];

		// Token: 0x0400013A RID: 314
		private bool isWhitespace;

		// Token: 0x0400013B RID: 315
		private SmallXmlParser.AttrListImpl attributes = new SmallXmlParser.AttrListImpl();

		// Token: 0x0400013C RID: 316
		private int line = 1;

		// Token: 0x0400013D RID: 317
		private int column;

		// Token: 0x0400013E RID: 318
		private bool resetColumn;

		// Token: 0x0200004B RID: 75
		private class AttrListImpl : SmallXmlParser.IAttrList
		{
			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000156 RID: 342 RVA: 0x0000C65C File Offset: 0x0000A85C
			public int Length
			{
				get
				{
					return this.attrNames.Count;
				}
			}

			// Token: 0x06000157 RID: 343 RVA: 0x0000C66C File Offset: 0x0000A86C
			public string GetName(int i)
			{
				return (string)this.attrNames[i];
			}

			// Token: 0x06000158 RID: 344 RVA: 0x0000C680 File Offset: 0x0000A880
			public string GetValue(int i)
			{
				return (string)this.attrValues[i];
			}

			// Token: 0x06000159 RID: 345 RVA: 0x0000C694 File Offset: 0x0000A894
			public string GetValue(string name)
			{
				for (int i = 0; i < this.attrNames.Count; i++)
				{
					if ((string)this.attrNames[i] == name)
					{
						return (string)this.attrValues[i];
					}
				}
				return null;
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600015A RID: 346 RVA: 0x0000C6EC File Offset: 0x0000A8EC
			public string[] Names
			{
				get
				{
					return (string[])this.attrNames.ToArray(typeof(string));
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600015B RID: 347 RVA: 0x0000C708 File Offset: 0x0000A908
			public string[] Values
			{
				get
				{
					return (string[])this.attrValues.ToArray(typeof(string));
				}
			}

			// Token: 0x0600015C RID: 348 RVA: 0x0000C724 File Offset: 0x0000A924
			internal void Clear()
			{
				this.attrNames.Clear();
				this.attrValues.Clear();
			}

			// Token: 0x0600015D RID: 349 RVA: 0x0000C73C File Offset: 0x0000A93C
			internal void Add(string name, string value)
			{
				this.attrNames.Add(name);
				this.attrValues.Add(value);
			}

			// Token: 0x04000140 RID: 320
			private ArrayList attrNames = new ArrayList();

			// Token: 0x04000141 RID: 321
			private ArrayList attrValues = new ArrayList();
		}

		// Token: 0x0200004C RID: 76
		public interface IAttrList
		{
			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600015E RID: 350
			int Length { get; }

			// Token: 0x0600015F RID: 351
			string GetName(int i);

			// Token: 0x06000160 RID: 352
			string GetValue(int i);

			// Token: 0x06000161 RID: 353
			string GetValue(string name);

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000162 RID: 354
			string[] Names { get; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000163 RID: 355
			string[] Values { get; }
		}

		// Token: 0x0200004D RID: 77
		public interface IContentHandler
		{
			// Token: 0x06000164 RID: 356
			void OnStartParsing(SmallXmlParser parser);

			// Token: 0x06000165 RID: 357
			void OnEndParsing(SmallXmlParser parser);

			// Token: 0x06000166 RID: 358
			void OnStartElement(string name, SmallXmlParser.IAttrList attrs);

			// Token: 0x06000167 RID: 359
			void OnEndElement(string name);

			// Token: 0x06000168 RID: 360
			void OnProcessingInstruction(string name, string text);

			// Token: 0x06000169 RID: 361
			void OnChars(string text);

			// Token: 0x0600016A RID: 362
			void OnIgnorableWhitespace(string text);
		}
	}
}
