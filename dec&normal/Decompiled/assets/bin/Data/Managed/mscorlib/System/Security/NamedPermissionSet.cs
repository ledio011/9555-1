using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x02000334 RID: 820
	[ComVisible(true)]
	[Serializable]
	public sealed class NamedPermissionSet : PermissionSet
	{
		// Token: 0x060018AD RID: 6317 RVA: 0x0005A620 File Offset: 0x00058820
		internal NamedPermissionSet()
		{
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0005A628 File Offset: 0x00058828
		public NamedPermissionSet(string name, PermissionState state) : base(state)
		{
			this.Name = name;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0005A638 File Offset: 0x00058838
		public NamedPermissionSet(NamedPermissionSet permSet) : base(permSet)
		{
			this.name = permSet.name;
			this.description = permSet.description;
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0005A65C File Offset: 0x0005885C
		// (set) Token: 0x060018B1 RID: 6321 RVA: 0x0005A664 File Offset: 0x00058864
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new ArgumentException(Locale.GetText("invalid name"));
				}
				this.name = value;
			}
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0005A694 File Offset: 0x00058894
		public override PermissionSet Copy()
		{
			return new NamedPermissionSet(this);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0005A69C File Offset: 0x0005889C
		public override void FromXml(SecurityElement et)
		{
			base.FromXml(et);
			this.name = et.Attribute("Name");
			this.description = et.Attribute("Description");
			if (this.description == null)
			{
				this.description = string.Empty;
			}
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0005A6E8 File Offset: 0x000588E8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.ToXml();
			if (this.name != null)
			{
				securityElement.AddAttribute("Name", this.name);
			}
			if (this.description != null)
			{
				securityElement.AddAttribute("Description", this.description);
			}
			return securityElement;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0005A738 File Offset: 0x00058938
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			NamedPermissionSet namedPermissionSet = obj as NamedPermissionSet;
			return namedPermissionSet != null && this.name == namedPermissionSet.Name && base.Equals(obj);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0005A77C File Offset: 0x0005897C
		[ComVisible(false)]
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.name != null)
			{
				num ^= this.name.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000D4E RID: 3406
		private string name;

		// Token: 0x04000D4F RID: 3407
		private string description;
	}
}
