using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020002C8 RID: 712
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	public sealed class SoapFieldAttribute : SoapAttribute
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0004E518 File Offset: 0x0004C718
		public string XmlElementName
		{
			get
			{
				return this._elementName;
			}
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0004E520 File Offset: 0x0004C720
		public bool IsInteropXmlElement()
		{
			return this._isElement;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0004E528 File Offset: 0x0004C728
		internal override void SetReflectionObject(object reflectionObject)
		{
			FieldInfo fieldInfo = (FieldInfo)reflectionObject;
			if (this._elementName == null)
			{
				this._elementName = fieldInfo.Name;
			}
		}

		// Token: 0x04000B75 RID: 2933
		private int _order;

		// Token: 0x04000B76 RID: 2934
		private string _elementName;

		// Token: 0x04000B77 RID: 2935
		private bool _isElement;
	}
}
