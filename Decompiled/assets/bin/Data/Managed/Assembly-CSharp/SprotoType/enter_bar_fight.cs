using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200037B RID: 891
	public class enter_bar_fight
	{
		// Token: 0x0200037C RID: 892
		public class request : SprotoTypeBase
		{
			// Token: 0x06001AFD RID: 6909 RVA: 0x000934E0 File Offset: 0x000916E0
			public request() : base(enter_bar_fight.request.max_field_count)
			{
			}

			// Token: 0x06001AFE RID: 6910 RVA: 0x000934F0 File Offset: 0x000916F0
			public request(byte[] buffer) : base(enter_bar_fight.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006AB RID: 1707
			// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0009350C File Offset: 0x0009170C
			// (set) Token: 0x06001B01 RID: 6913 RVA: 0x00093514 File Offset: 0x00091714
			public string ID
			{
				get
				{
					return this._ID;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._ID = value;
				}
			}

			// Token: 0x170006AC RID: 1708
			// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0009352C File Offset: 0x0009172C
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001B03 RID: 6915 RVA: 0x0009353C File Offset: 0x0009173C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001B04 RID: 6916 RVA: 0x00093598 File Offset: 0x00091798
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040019F5 RID: 6645
			private static int max_field_count = 1;

			// Token: 0x040019F6 RID: 6646
			private string _ID;
		}
	}
}
