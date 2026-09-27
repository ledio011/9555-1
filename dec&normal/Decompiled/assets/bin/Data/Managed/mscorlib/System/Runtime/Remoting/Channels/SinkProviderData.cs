using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000270 RID: 624
	[ComVisible(true)]
	public class SinkProviderData
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x00047C44 File Offset: 0x00045E44
		public SinkProviderData(string name)
		{
			this.sinkName = name;
			this.children = new ArrayList();
			this.properties = new Hashtable();
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00047C6C File Offset: 0x00045E6C
		public IList Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00047C74 File Offset: 0x00045E74
		public IDictionary Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04000A8E RID: 2702
		private string sinkName;

		// Token: 0x04000A8F RID: 2703
		private ArrayList children;

		// Token: 0x04000A90 RID: 2704
		private Hashtable properties;
	}
}
