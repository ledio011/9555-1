using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200031A RID: 794
	public class bar_fight_notify
	{
		// Token: 0x0200031B RID: 795
		public class request : SprotoTypeBase
		{
			// Token: 0x060016D5 RID: 5845 RVA: 0x0008A950 File Offset: 0x00088B50
			public request() : base(bar_fight_notify.request.max_field_count)
			{
			}

			// Token: 0x060016D6 RID: 5846 RVA: 0x0008A960 File Offset: 0x00088B60
			public request(byte[] buffer) : base(bar_fight_notify.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x060016D8 RID: 5848 RVA: 0x0008A97C File Offset: 0x00088B7C
			// (set) Token: 0x060016D9 RID: 5849 RVA: 0x0008A984 File Offset: 0x00088B84
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x170004D4 RID: 1236
			// (get) Token: 0x060016DA RID: 5850 RVA: 0x0008A99C File Offset: 0x00088B9C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060016DB RID: 5851 RVA: 0x0008A9AC File Offset: 0x00088BAC
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
						this.id = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060016DC RID: 5852 RVA: 0x0008AA08 File Offset: 0x00088C08
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018C0 RID: 6336
			private static int max_field_count = 1;

			// Token: 0x040018C1 RID: 6337
			private string _id;
		}
	}
}
