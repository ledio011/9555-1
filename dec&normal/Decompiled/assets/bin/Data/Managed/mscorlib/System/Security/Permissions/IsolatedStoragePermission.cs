using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200033F RID: 831
	[ComVisible(true)]
	[Serializable]
	public abstract class IsolatedStoragePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x060018E9 RID: 6377 RVA: 0x0005B890 File Offset: 0x00059A90
		protected IsolatedStoragePermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.UsageAllowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (set) Token: 0x060018EA RID: 6378 RVA: 0x0005B8B0 File Offset: 0x00059AB0
		public long UserQuota
		{
			set
			{
				this.m_userQuota = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x0005B8BC File Offset: 0x00059ABC
		public IsolatedStorageContainment UsageAllowed
		{
			set
			{
				if (!Enum.IsDefined(typeof(IsolatedStorageContainment), value))
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "IsolatedStorageContainment");
				}
				this.m_allowed = value;
				if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
				{
					this.m_userQuota = long.MaxValue;
					this.m_machineQuota = long.MaxValue;
					this.m_expirationDays = long.MaxValue;
					this.m_permanentData = true;
				}
			}
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0005B950 File Offset: 0x00059B50
		public bool IsUnrestricted()
		{
			return IsolatedStorageContainment.UnrestrictedIsolatedStorage == this.m_allowed;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0005B960 File Offset: 0x00059B60
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Allowed", this.m_allowed.ToString());
				if (this.m_userQuota > 0L)
				{
					securityElement.AddAttribute("UserQuota", this.m_userQuota.ToString());
				}
			}
			return securityElement;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0005B9DC File Offset: 0x00059BDC
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			this.m_userQuota = 0L;
			this.m_machineQuota = 0L;
			this.m_expirationDays = 0L;
			this.m_permanentData = false;
			this.m_allowed = IsolatedStorageContainment.None;
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.UsageAllowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
			}
			else
			{
				string text = esd.Attribute("Allowed");
				if (text != null)
				{
					this.UsageAllowed = (IsolatedStorageContainment)((int)Enum.Parse(typeof(IsolatedStorageContainment), text));
				}
				text = esd.Attribute("UserQuota");
				if (text != null)
				{
					Exception ex;
					long.Parse(text, true, out this.m_userQuota, out ex);
				}
			}
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0005BA88 File Offset: 0x00059C88
		internal bool IsEmpty()
		{
			return this.m_userQuota == 0L && this.m_allowed == IsolatedStorageContainment.None;
		}

		// Token: 0x04000D80 RID: 3456
		private const int version = 1;

		// Token: 0x04000D81 RID: 3457
		internal long m_userQuota;

		// Token: 0x04000D82 RID: 3458
		internal long m_machineQuota;

		// Token: 0x04000D83 RID: 3459
		internal long m_expirationDays;

		// Token: 0x04000D84 RID: 3460
		internal bool m_permanentData;

		// Token: 0x04000D85 RID: 3461
		internal IsolatedStorageContainment m_allowed;
	}
}
