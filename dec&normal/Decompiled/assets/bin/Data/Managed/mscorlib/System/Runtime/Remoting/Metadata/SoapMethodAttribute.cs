using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020002C9 RID: 713
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class SoapMethodAttribute : SoapAttribute
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0004E55C File Offset: 0x0004C75C
		public override bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0004E564 File Offset: 0x0004C764
		public override string XmlNamespace
		{
			get
			{
				return this._namespace;
			}
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0004E56C File Offset: 0x0004C76C
		internal override void SetReflectionObject(object reflectionObject)
		{
			MethodBase methodBase = (MethodBase)reflectionObject;
			if (this._responseElement == null)
			{
				this._responseElement = methodBase.Name + "Response";
			}
			if (this._responseNamespace == null)
			{
				this._responseNamespace = SoapServices.GetXmlNamespaceForMethodResponse(methodBase);
			}
			if (this._returnElement == null)
			{
				this._returnElement = "return";
			}
			if (this._soapAction == null)
			{
				this._soapAction = SoapServices.GetXmlNamespaceForMethodCall(methodBase) + "#" + methodBase.Name;
			}
			if (this._namespace == null)
			{
				this._namespace = SoapServices.GetXmlNamespaceForMethodCall(methodBase);
			}
		}

		// Token: 0x04000B78 RID: 2936
		private string _responseElement;

		// Token: 0x04000B79 RID: 2937
		private string _responseNamespace;

		// Token: 0x04000B7A RID: 2938
		private string _returnElement;

		// Token: 0x04000B7B RID: 2939
		private string _soapAction;

		// Token: 0x04000B7C RID: 2940
		private bool _useAttribute;

		// Token: 0x04000B7D RID: 2941
		private string _namespace;
	}
}
