using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Xml;

namespace System.Security
{
	// Token: 0x0200037E RID: 894
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityElement
	{
		// Token: 0x06001A2F RID: 6703 RVA: 0x000610B8 File Offset: 0x0005F2B8
		public SecurityElement(string tag) : this(tag, null)
		{
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000610C4 File Offset: 0x0005F2C4
		public SecurityElement(string tag, string text)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + tag);
			}
			this.tag = tag;
			this.Text = text;
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x000611A4 File Offset: 0x0005F3A4
		public Hashtable Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					return null;
				}
				Hashtable hashtable = new Hashtable(this.attributes.Count);
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					hashtable.Add(securityAttribute.Name, securityAttribute.Value);
				}
				return hashtable;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00061234 File Offset: 0x0005F434
		public ArrayList Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x0006123C File Offset: 0x0005F43C
		public string Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00061244 File Offset: 0x0005F444
		// (set) Token: 0x06001A36 RID: 6710 RVA: 0x0006124C File Offset: 0x0005F44C
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value != null && !SecurityElement.IsValidText(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + value);
				}
				this.text = SecurityElement.Unescape(value);
			}
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00061288 File Offset: 0x0005F488
		public void AddAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.GetAttribute(name) != null)
			{
				throw new ArgumentException(Locale.GetText("Duplicate attribute : " + name));
			}
			if (this.attributes == null)
			{
				this.attributes = new ArrayList();
			}
			this.attributes.Add(new SecurityElement.SecurityAttribute(name, value));
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00061304 File Offset: 0x0005F504
		public void AddChild(SecurityElement child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.children == null)
			{
				this.children = new ArrayList();
			}
			this.children.Add(child);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0006133C File Offset: 0x0005F53C
		public string Attribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			SecurityElement.SecurityAttribute attribute = this.GetAttribute(name);
			return (attribute != null) ? attribute.Value : null;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00061374 File Offset: 0x0005F574
		public static string Escape(string str)
		{
			if (str == null)
			{
				return null;
			}
			if (str.IndexOfAny(SecurityElement.invalid_chars) == -1)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				char c2 = c;
				switch (c2)
				{
				case '"':
					stringBuilder.Append("&quot;");
					break;
				default:
					switch (c2)
					{
					case '<':
						stringBuilder.Append("&lt;");
						goto IL_D9;
					case '>':
						stringBuilder.Append("&gt;");
						goto IL_D9;
					}
					stringBuilder.Append(c);
					break;
				case '&':
					stringBuilder.Append("&amp;");
					break;
				case '\'':
					stringBuilder.Append("&apos;");
					break;
				}
				IL_D9:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0006146C File Offset: 0x0005F66C
		private static string Unescape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(str);
			stringBuilder.Replace("&lt;", "<");
			stringBuilder.Replace("&gt;", ">");
			stringBuilder.Replace("&amp;", "&");
			stringBuilder.Replace("&quot;", "\"");
			stringBuilder.Replace("&apos;", "'");
			return stringBuilder.ToString();
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000614E4 File Offset: 0x0005F6E4
		public static SecurityElement FromString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			if (xml.Length == 0)
			{
				throw new XmlSyntaxException(Locale.GetText("Empty string."));
			}
			SecurityElement result;
			try
			{
				SecurityParser securityParser = new SecurityParser();
				securityParser.LoadXml(xml);
				result = securityParser.ToXml();
			}
			catch (Exception inner)
			{
				string message = Locale.GetText("Invalid XML.");
				throw new XmlSyntaxException(message, inner);
			}
			return result;
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00061568 File Offset: 0x0005F768
		public static bool IsValidAttributeName(string name)
		{
			return name != null && name.IndexOfAny(SecurityElement.invalid_attr_name_chars) == -1;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00061584 File Offset: 0x0005F784
		public static bool IsValidAttributeValue(string value)
		{
			return value != null && value.IndexOfAny(SecurityElement.invalid_attr_value_chars) == -1;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000615A0 File Offset: 0x0005F7A0
		public static bool IsValidTag(string tag)
		{
			return tag != null && tag.IndexOfAny(SecurityElement.invalid_tag_chars) == -1;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x000615BC File Offset: 0x0005F7BC
		public static bool IsValidText(string text)
		{
			return text != null && text.IndexOfAny(SecurityElement.invalid_text_chars) == -1;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000615D8 File Offset: 0x0005F7D8
		public SecurityElement SearchForChildByTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.children == null)
			{
				return null;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				SecurityElement securityElement = (SecurityElement)this.children[i];
				if (securityElement.tag == tag)
				{
					return securityElement;
				}
			}
			return null;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00061648 File Offset: 0x0005F848
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToXml(ref stringBuilder, 0);
			return stringBuilder.ToString();
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0006166C File Offset: 0x0005F86C
		private void ToXml(ref StringBuilder s, int level)
		{
			s.Append("<");
			s.Append(this.tag);
			if (this.attributes != null)
			{
				s.Append(" ");
				for (int i = 0; i < this.attributes.Count; i++)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)this.attributes[i];
					s.Append(securityAttribute.Name).Append("=\"").Append(SecurityElement.Escape(securityAttribute.Value)).Append("\"");
					if (i != this.attributes.Count - 1)
					{
						s.Append(Environment.NewLine);
					}
				}
			}
			if ((this.text == null || this.text == string.Empty) && (this.children == null || this.children.Count == 0))
			{
				s.Append("/>").Append(Environment.NewLine);
			}
			else
			{
				s.Append(">").Append(SecurityElement.Escape(this.text));
				if (this.children != null)
				{
					s.Append(Environment.NewLine);
					foreach (object obj in this.children)
					{
						SecurityElement securityElement = (SecurityElement)obj;
						securityElement.ToXml(ref s, level + 1);
					}
				}
				s.Append("</").Append(this.tag).Append(">").Append(Environment.NewLine);
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00061840 File Offset: 0x0005FA40
		internal SecurityElement.SecurityAttribute GetAttribute(string name)
		{
			if (this.attributes != null)
			{
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					if (securityAttribute.Name == name)
					{
						return securityAttribute;
					}
				}
			}
			return null;
		}

		// Token: 0x04000E6E RID: 3694
		private string text;

		// Token: 0x04000E6F RID: 3695
		private string tag;

		// Token: 0x04000E70 RID: 3696
		private ArrayList attributes;

		// Token: 0x04000E71 RID: 3697
		private ArrayList children;

		// Token: 0x04000E72 RID: 3698
		private static readonly char[] invalid_tag_chars = new char[]
		{
			' ',
			'<',
			'>'
		};

		// Token: 0x04000E73 RID: 3699
		private static readonly char[] invalid_text_chars = new char[]
		{
			'<',
			'>'
		};

		// Token: 0x04000E74 RID: 3700
		private static readonly char[] invalid_attr_name_chars = new char[]
		{
			' ',
			'<',
			'>'
		};

		// Token: 0x04000E75 RID: 3701
		private static readonly char[] invalid_attr_value_chars = new char[]
		{
			'"',
			'<',
			'>'
		};

		// Token: 0x04000E76 RID: 3702
		private static readonly char[] invalid_chars = new char[]
		{
			'<',
			'>',
			'"',
			'\'',
			'&'
		};

		// Token: 0x0200037F RID: 895
		internal class SecurityAttribute
		{
			// Token: 0x06001A45 RID: 6725 RVA: 0x000618C4 File Offset: 0x0005FAC4
			public SecurityAttribute(string name, string value)
			{
				if (!SecurityElement.IsValidAttributeName(name))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute name") + ": " + name);
				}
				if (!SecurityElement.IsValidAttributeValue(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute value") + ": " + value);
				}
				this._name = name;
				this._value = SecurityElement.Unescape(value);
			}

			// Token: 0x170004B7 RID: 1207
			// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00061938 File Offset: 0x0005FB38
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x170004B8 RID: 1208
			// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00061940 File Offset: 0x0005FB40
			public string Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x04000E77 RID: 3703
			private string _name;

			// Token: 0x04000E78 RID: 3704
			private string _value;
		}
	}
}
