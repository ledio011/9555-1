using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000346 RID: 838
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		// Token: 0x06001900 RID: 6400 RVA: 0x0005BBFC File Offset: 0x00059DFC
		public ReflectionPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.flags = ReflectionPermissionFlag.AllFlags;
			}
			else
			{
				this.flags = ReflectionPermissionFlag.NoFlags;
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0005BC24 File Offset: 0x00059E24
		public ReflectionPermission(ReflectionPermissionFlag flag)
		{
			this.Flags = flag;
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0005BC34 File Offset: 0x00059E34
		// (set) Token: 0x06001903 RID: 6403 RVA: 0x0005BC3C File Offset: 0x00059E3C
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				if ((value & (ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess)) != value)
				{
					string message = string.Format(Locale.GetText("Invalid flags {0}"), value);
					throw new ArgumentException(message, "ReflectionPermissionFlag");
				}
				this.flags = value;
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0005BC7C File Offset: 0x00059E7C
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.flags = ReflectionPermissionFlag.AllFlags;
			}
			else
			{
				this.flags = ReflectionPermissionFlag.NoFlags;
				string text = esd.Attributes["Flags"] as string;
				if (text.IndexOf("MemberAccess") >= 0)
				{
					this.flags |= ReflectionPermissionFlag.MemberAccess;
				}
				if (text.IndexOf("ReflectionEmit") >= 0)
				{
					this.flags |= ReflectionPermissionFlag.ReflectionEmit;
				}
				if (text.IndexOf("TypeInformation") >= 0)
				{
					this.flags |= ReflectionPermissionFlag.TypeInformation;
				}
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0005BD28 File Offset: 0x00059F28
		public override bool IsSubsetOf(IPermission target)
		{
			ReflectionPermission reflectionPermission = this.Cast(target);
			if (reflectionPermission == null)
			{
				return this.flags == ReflectionPermissionFlag.NoFlags;
			}
			if (this.IsUnrestricted())
			{
				return reflectionPermission.IsUnrestricted();
			}
			return reflectionPermission.IsUnrestricted() || (this.flags & reflectionPermission.Flags) == this.flags;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0005BD84 File Offset: 0x00059F84
		public bool IsUnrestricted()
		{
			return this.flags == ReflectionPermissionFlag.AllFlags;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0005BD90 File Offset: 0x00059F90
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.flags == ReflectionPermissionFlag.NoFlags)
			{
				securityElement.AddAttribute("Flags", "NoFlags");
			}
			else if ((this.flags & ReflectionPermissionFlag.AllFlags) == ReflectionPermissionFlag.AllFlags)
			{
				securityElement.AddAttribute("Flags", "AllFlags");
			}
			else
			{
				string text = string.Empty;
				if ((this.flags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess)
				{
					text = "MemberAccess";
				}
				if ((this.flags & ReflectionPermissionFlag.ReflectionEmit) == ReflectionPermissionFlag.ReflectionEmit)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "ReflectionEmit";
				}
				if ((this.flags & ReflectionPermissionFlag.TypeInformation) == ReflectionPermissionFlag.TypeInformation)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "TypeInformation";
				}
				securityElement.AddAttribute("Flags", text);
			}
			return securityElement;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0005BE94 File Offset: 0x0005A094
		private ReflectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ReflectionPermission reflectionPermission = target as ReflectionPermission;
			if (reflectionPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(ReflectionPermission));
			}
			return reflectionPermission;
		}

		// Token: 0x04000D9A RID: 3482
		private const int version = 1;

		// Token: 0x04000D9B RID: 3483
		private ReflectionPermissionFlag flags;
	}
}
