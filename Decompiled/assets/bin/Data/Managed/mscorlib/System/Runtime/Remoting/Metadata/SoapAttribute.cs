using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020002C7 RID: 711
	[ComVisible(true)]
	public class SoapAttribute : Attribute
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0004E4F4 File Offset: 0x0004C6F4
		public virtual bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0004E4FC File Offset: 0x0004C6FC
		public virtual string XmlNamespace
		{
			get
			{
				return this.ProtXmlNamespace;
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0004E504 File Offset: 0x0004C704
		internal virtual void SetReflectionObject(object reflectionObject)
		{
			this.ReflectInfo = reflectionObject;
		}

		// Token: 0x04000B71 RID: 2929
		private bool _nested;

		// Token: 0x04000B72 RID: 2930
		private bool _useAttribute;

		// Token: 0x04000B73 RID: 2931
		protected string ProtXmlNamespace;

		// Token: 0x04000B74 RID: 2932
		protected object ReflectInfo;
	}
}
