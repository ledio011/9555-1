using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200033E RID: 830
	[ComVisible(true)]
	[Serializable]
	public sealed class IsolatedStorageFilePermission : IsolatedStoragePermission, IBuiltInPermission
	{
		// Token: 0x060018E5 RID: 6373 RVA: 0x0005B7B8 File Offset: 0x000599B8
		public IsolatedStorageFilePermission(PermissionState state) : base(state)
		{
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0005B7C4 File Offset: 0x000599C4
		public override bool IsSubsetOf(IPermission target)
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission = this.Cast(target);
			if (isolatedStorageFilePermission == null)
			{
				return base.IsEmpty();
			}
			return isolatedStorageFilePermission.IsUnrestricted() || (this.m_userQuota <= isolatedStorageFilePermission.m_userQuota && this.m_machineQuota <= isolatedStorageFilePermission.m_machineQuota && this.m_expirationDays <= isolatedStorageFilePermission.m_expirationDays && this.m_permanentData == isolatedStorageFilePermission.m_permanentData && this.m_allowed <= isolatedStorageFilePermission.m_allowed);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0005B854 File Offset: 0x00059A54
		[ComVisible(false)]
		[MonoTODO("(2.0) new override - something must have been added ???")]
		public override SecurityElement ToXml()
		{
			return base.ToXml();
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0005B85C File Offset: 0x00059A5C
		private IsolatedStorageFilePermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			IsolatedStorageFilePermission isolatedStorageFilePermission = target as IsolatedStorageFilePermission;
			if (isolatedStorageFilePermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(IsolatedStorageFilePermission));
			}
			return isolatedStorageFilePermission;
		}

		// Token: 0x04000D7F RID: 3455
		private const int version = 1;
	}
}
