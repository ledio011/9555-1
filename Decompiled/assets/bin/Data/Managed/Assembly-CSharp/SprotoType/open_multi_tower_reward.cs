using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200043E RID: 1086
	public class open_multi_tower_reward
	{
		// Token: 0x0200043F RID: 1087
		public class request : SprotoTypeBase
		{
			// Token: 0x060021DA RID: 8666 RVA: 0x000A16D0 File Offset: 0x0009F8D0
			public request() : base(open_multi_tower_reward.request.max_field_count)
			{
			}

			// Token: 0x060021DB RID: 8667 RVA: 0x000A16E0 File Offset: 0x0009F8E0
			public request(byte[] buffer) : base(open_multi_tower_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700098B RID: 2443
			// (get) Token: 0x060021DD RID: 8669 RVA: 0x000A16FC File Offset: 0x0009F8FC
			// (set) Token: 0x060021DE RID: 8670 RVA: 0x000A1704 File Offset: 0x0009F904
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._index = value;
				}
			}

			// Token: 0x1700098C RID: 2444
			// (get) Token: 0x060021DF RID: 8671 RVA: 0x000A171C File Offset: 0x0009F91C
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700098D RID: 2445
			// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000A172C File Offset: 0x0009F92C
			// (set) Token: 0x060021E1 RID: 8673 RVA: 0x000A1734 File Offset: 0x0009F934
			public long floor
			{
				get
				{
					return this._floor;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._floor = value;
				}
			}

			// Token: 0x1700098E RID: 2446
			// (get) Token: 0x060021E2 RID: 8674 RVA: 0x000A174C File Offset: 0x0009F94C
			public bool HasFloor
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060021E3 RID: 8675 RVA: 0x000A175C File Offset: 0x0009F95C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.floor = this.deserialize.read_integer();
						}
					}
					else
					{
						this.index = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060021E4 RID: 8676 RVA: 0x000A17D4 File Offset: 0x0009F9D4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.index, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.floor, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BDC RID: 7132
			private static int max_field_count = 2;

			// Token: 0x04001BDD RID: 7133
			private long _index;

			// Token: 0x04001BDE RID: 7134
			private long _floor;
		}
	}
}
