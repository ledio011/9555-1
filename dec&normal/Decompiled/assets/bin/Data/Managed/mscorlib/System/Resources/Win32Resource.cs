using System;

namespace System.Resources
{
	// Token: 0x020001FC RID: 508
	internal abstract class Win32Resource
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x000451D8 File Offset: 0x000433D8
		public Win32ResourceType ResourceType
		{
			get
			{
				if (this.type.IsName)
				{
					return (Win32ResourceType)(-1);
				}
				return (Win32ResourceType)this.type.Id;
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000451F8 File Offset: 0x000433F8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Win32Resource (Kind=",
				this.ResourceType,
				", Name=",
				this.name,
				")"
			});
		}

		// Token: 0x04000991 RID: 2449
		private NameOrId type;

		// Token: 0x04000992 RID: 2450
		private NameOrId name;

		// Token: 0x04000993 RID: 2451
		private int language;
	}
}
