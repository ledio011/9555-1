using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200025F RID: 607
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlAttribute : ContextAttribute
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x00046C84 File Offset: 0x00044E84
		public string UrlValue
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00046C8C File Offset: 0x00044E8C
		public override bool Equals(object o)
		{
			return o is UrlAttribute && ((UrlAttribute)o).UrlValue == this.url;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00046CB4 File Offset: 0x00044EB4
		public override int GetHashCode()
		{
			return this.url.GetHashCode();
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00046CC4 File Offset: 0x00044EC4
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00046CC8 File Offset: 0x00044EC8
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return true;
		}

		// Token: 0x04000A73 RID: 2675
		private string url;
	}
}
