using System;
using System.Collections;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200025D RID: 605
	internal class RemoteActivationAttribute : Attribute, IContextAttribute
	{
		// Token: 0x06001430 RID: 5168 RVA: 0x00046B20 File Offset: 0x00044D20
		public RemoteActivationAttribute(IList contextProperties)
		{
			this._contextProperties = contextProperties;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x00046B30 File Offset: 0x00044D30
		public bool IsContextOK(Context ctx, IConstructionCallMessage ctor)
		{
			return false;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00046B34 File Offset: 0x00044D34
		public void GetPropertiesForNewContext(IConstructionCallMessage ctor)
		{
			if (this._contextProperties != null)
			{
				foreach (object value in this._contextProperties)
				{
					ctor.ContextProperties.Add(value);
				}
			}
		}

		// Token: 0x04000A72 RID: 2674
		private IList _contextProperties;
	}
}
