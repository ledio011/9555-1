using System;
using System.Collections;
using System.IO;
using System.Security;

namespace Mono.Xml
{
	// Token: 0x02000049 RID: 73
	internal class SecurityParser : SmallXmlParser, SmallXmlParser.IContentHandler
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000B824 File Offset: 0x00009A24
		public SecurityParser()
		{
			this.stack = new Stack();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000B838 File Offset: 0x00009A38
		public void LoadXml(string xml)
		{
			this.root = null;
			this.stack.Clear();
			base.Parse(new StringReader(xml), this);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000B85C File Offset: 0x00009A5C
		public SecurityElement ToXml()
		{
			return this.root;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000B864 File Offset: 0x00009A64
		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000B868 File Offset: 0x00009A68
		public void OnProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000B86C File Offset: 0x00009A6C
		public void OnIgnorableWhitespace(string s)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000B870 File Offset: 0x00009A70
		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
		{
			SecurityElement securityElement = new SecurityElement(name);
			if (this.root == null)
			{
				this.root = securityElement;
				this.current = securityElement;
			}
			else
			{
				SecurityElement securityElement2 = (SecurityElement)this.stack.Peek();
				securityElement2.AddChild(securityElement);
			}
			this.stack.Push(securityElement);
			this.current = securityElement;
			int length = attrs.Length;
			for (int i = 0; i < length; i++)
			{
				this.current.AddAttribute(attrs.GetName(i), SecurityElement.Escape(attrs.GetValue(i)));
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000B904 File Offset: 0x00009B04
		public void OnEndElement(string name)
		{
			this.current = (SecurityElement)this.stack.Pop();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000B91C File Offset: 0x00009B1C
		public void OnChars(string ch)
		{
			this.current.Text = SecurityElement.Escape(ch);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000B930 File Offset: 0x00009B30
		public void OnEndParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x04000130 RID: 304
		private SecurityElement root;

		// Token: 0x04000131 RID: 305
		private SecurityElement current;

		// Token: 0x04000132 RID: 306
		private Stack stack;
	}
}
