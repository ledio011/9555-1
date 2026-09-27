using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020002CC RID: 716
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	[ComVisible(true)]
	public sealed class SoapTypeAttribute : SoapAttribute
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0004E61C File Offset: 0x0004C81C
		public override bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0004E624 File Offset: 0x0004C824
		public string XmlElementName
		{
			get
			{
				return this._xmlElementName;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0004E62C File Offset: 0x0004C82C
		public override string XmlNamespace
		{
			get
			{
				return this._xmlNamespace;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0004E634 File Offset: 0x0004C834
		public string XmlTypeName
		{
			get
			{
				return this._xmlTypeName;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0004E63C File Offset: 0x0004C83C
		public string XmlTypeNamespace
		{
			get
			{
				return this._xmlTypeNamespace;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0004E644 File Offset: 0x0004C844
		internal bool IsInteropXmlElement
		{
			get
			{
				return this._isElement;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0004E64C File Offset: 0x0004C84C
		internal bool IsInteropXmlType
		{
			get
			{
				return this._isType;
			}
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0004E654 File Offset: 0x0004C854
		internal override void SetReflectionObject(object reflectionObject)
		{
			Type type = (Type)reflectionObject;
			if (this._xmlElementName == null)
			{
				this._xmlElementName = type.Name;
			}
			if (this._xmlTypeName == null)
			{
				this._xmlTypeName = type.Name;
			}
			if (this._xmlTypeNamespace == null)
			{
				string assemblyName;
				if (type.Assembly == typeof(object).Assembly)
				{
					assemblyName = string.Empty;
				}
				else
				{
					assemblyName = type.Assembly.GetName().Name;
				}
				this._xmlTypeNamespace = SoapServices.CodeXmlNamespaceForClrTypeNamespace(type.Namespace, assemblyName);
			}
			if (this._xmlNamespace == null)
			{
				this._xmlNamespace = this._xmlTypeNamespace;
			}
		}

		// Token: 0x04000B85 RID: 2949
		private SoapOption _soapOption;

		// Token: 0x04000B86 RID: 2950
		private bool _useAttribute;

		// Token: 0x04000B87 RID: 2951
		private string _xmlElementName;

		// Token: 0x04000B88 RID: 2952
		private XmlFieldOrderOption _xmlFieldOrder;

		// Token: 0x04000B89 RID: 2953
		private string _xmlNamespace;

		// Token: 0x04000B8A RID: 2954
		private string _xmlTypeName;

		// Token: 0x04000B8B RID: 2955
		private string _xmlTypeNamespace;

		// Token: 0x04000B8C RID: 2956
		private bool _isType;

		// Token: 0x04000B8D RID: 2957
		private bool _isElement;
	}
}
