using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Security
{
	// Token: 0x02000354 RID: 852
	[Serializable]
	public class PermissionSet
	{
		// Token: 0x06001944 RID: 6468 RVA: 0x0005D414 File Offset: 0x0005B614
		public PermissionSet()
		{
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0005D41C File Offset: 0x0005B61C
		internal PermissionSet(string xml)
		{
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0005D424 File Offset: 0x0005B624
		public PermissionSet(PermissionState state)
		{
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0005D42C File Offset: 0x0005B62C
		public PermissionSet(PermissionSet permSet)
		{
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0005D434 File Offset: 0x0005B634
		public IPermission AddPermission(IPermission perm)
		{
			return perm;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0005D438 File Offset: 0x0005B638
		public virtual PermissionSet Copy()
		{
			return new PermissionSet(this);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0005D440 File Offset: 0x0005B640
		public virtual IPermission GetPermission(Type permClass)
		{
			return null;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0005D444 File Offset: 0x0005B644
		public virtual PermissionSet Intersect(PermissionSet other)
		{
			return other;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0005D448 File Offset: 0x0005B648
		public virtual void FromXml(SecurityElement et)
		{
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0005D44C File Offset: 0x0005B64C
		public virtual SecurityElement ToXml()
		{
			return null;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0005D450 File Offset: 0x0005B650
		public virtual bool IsSubsetOf(PermissionSet target)
		{
			return true;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0005D454 File Offset: 0x0005B654
		internal void SetReadOnly(bool value)
		{
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0005D458 File Offset: 0x0005B658
		public bool IsUnrestricted()
		{
			return true;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0005D45C File Offset: 0x0005B65C
		public PermissionSet Union(PermissionSet other)
		{
			return new PermissionSet();
		}

		// Token: 0x1700048D RID: 1165
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x0005D464 File Offset: 0x0005B664
		internal PolicyLevel Resolver
		{
			[CompilerGenerated]
			set
			{
				this.<Resolver>k__BackingField = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (set) Token: 0x06001953 RID: 6483 RVA: 0x0005D470 File Offset: 0x0005B670
		internal bool DeclarativeSecurity
		{
			[CompilerGenerated]
			set
			{
				this.<DeclarativeSecurity>k__BackingField = value;
			}
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0005D47C File Offset: 0x0005B67C
		internal static PermissionSet CreateFromBinaryFormat(byte[] data)
		{
			return new PermissionSet();
		}
	}
}
