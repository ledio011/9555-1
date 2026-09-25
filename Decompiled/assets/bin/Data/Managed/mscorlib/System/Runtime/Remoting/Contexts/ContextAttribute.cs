using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000275 RID: 629
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class ContextAttribute : Attribute, IContextAttribute, IContextProperty
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x000492D0 File Offset: 0x000474D0
		public ContextAttribute(string name)
		{
			this.AttributeName = name;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x000492E0 File Offset: 0x000474E0
		public virtual string Name
		{
			get
			{
				return this.AttributeName;
			}
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x000492E8 File Offset: 0x000474E8
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			if (!(o is ContextAttribute))
			{
				return false;
			}
			ContextAttribute contextAttribute = (ContextAttribute)o;
			return !(contextAttribute.AttributeName != this.AttributeName);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0004932C File Offset: 0x0004752C
		public virtual void Freeze(Context newContext)
		{
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00049330 File Offset: 0x00047530
		public override int GetHashCode()
		{
			if (this.AttributeName == null)
			{
				return 0;
			}
			return this.AttributeName.GetHashCode();
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0004934C File Offset: 0x0004754C
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			IList contextProperties = ctorMsg.ContextProperties;
			contextProperties.Add(this);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0004937C File Offset: 0x0004757C
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			if (ctx == null)
			{
				throw new ArgumentNullException("ctx");
			}
			if (!ctorMsg.ActivationType.IsContextful)
			{
				return true;
			}
			IContextProperty property = ctx.GetProperty(this.AttributeName);
			return property != null && this == property;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000493DC File Offset: 0x000475DC
		public virtual bool IsNewContextOK(Context newCtx)
		{
			return true;
		}

		// Token: 0x04000AAB RID: 2731
		protected string AttributeName;
	}
}
