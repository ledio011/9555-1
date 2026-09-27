using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003A3 RID: 931
	public class equip_inhert
	{
		// Token: 0x020003A4 RID: 932
		public class request : SprotoTypeBase
		{
			// Token: 0x06001BD5 RID: 7125 RVA: 0x00094EA8 File Offset: 0x000930A8
			public request() : base(equip_inhert.request.max_field_count)
			{
			}

			// Token: 0x06001BD6 RID: 7126 RVA: 0x00094EB8 File Offset: 0x000930B8
			public request(byte[] buffer) : base(equip_inhert.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006EB RID: 1771
			// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00094ED4 File Offset: 0x000930D4
			// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x00094EDC File Offset: 0x000930DC
			public long indexId1
			{
				get
				{
					return this._indexId1;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._indexId1 = value;
				}
			}

			// Token: 0x170006EC RID: 1772
			// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00094EF4 File Offset: 0x000930F4
			public bool HasIndexId1
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006ED RID: 1773
			// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00094F04 File Offset: 0x00093104
			// (set) Token: 0x06001BDC RID: 7132 RVA: 0x00094F0C File Offset: 0x0009310C
			public long containertype1
			{
				get
				{
					return this._containertype1;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._containertype1 = value;
				}
			}

			// Token: 0x170006EE RID: 1774
			// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00094F24 File Offset: 0x00093124
			public bool HasContainertype1
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170006EF RID: 1775
			// (get) Token: 0x06001BDE RID: 7134 RVA: 0x00094F34 File Offset: 0x00093134
			// (set) Token: 0x06001BDF RID: 7135 RVA: 0x00094F3C File Offset: 0x0009313C
			public long indexId2
			{
				get
				{
					return this._indexId2;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._indexId2 = value;
				}
			}

			// Token: 0x170006F0 RID: 1776
			// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00094F54 File Offset: 0x00093154
			public bool HasIndexId2
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170006F1 RID: 1777
			// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x00094F64 File Offset: 0x00093164
			// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x00094F6C File Offset: 0x0009316C
			public long containertype2
			{
				get
				{
					return this._containertype2;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._containertype2 = value;
				}
			}

			// Token: 0x170006F2 RID: 1778
			// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x00094F84 File Offset: 0x00093184
			public bool HasContainertype2
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06001BE4 RID: 7140 RVA: 0x00094F94 File Offset: 0x00093194
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.indexId1 = this.deserialize.read_integer();
						break;
					case 1:
						this.containertype1 = this.deserialize.read_integer();
						break;
					case 2:
						this.indexId2 = this.deserialize.read_integer();
						break;
					case 3:
						this.containertype2 = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001BE5 RID: 7141 RVA: 0x00095040 File Offset: 0x00093240
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId1, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.containertype1, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.indexId2, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.containertype2, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A29 RID: 6697
			private static int max_field_count = 4;

			// Token: 0x04001A2A RID: 6698
			private long _indexId1;

			// Token: 0x04001A2B RID: 6699
			private long _containertype1;

			// Token: 0x04001A2C RID: 6700
			private long _indexId2;

			// Token: 0x04001A2D RID: 6701
			private long _containertype2;
		}

		// Token: 0x020003A5 RID: 933
		public class response : SprotoTypeBase
		{
			// Token: 0x06001BE6 RID: 7142 RVA: 0x000950F0 File Offset: 0x000932F0
			public response() : base(equip_inhert.response.max_field_count)
			{
			}

			// Token: 0x06001BE7 RID: 7143 RVA: 0x00095100 File Offset: 0x00093300
			public response(byte[] buffer) : base(equip_inhert.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006F3 RID: 1779
			// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0009511C File Offset: 0x0009331C
			// (set) Token: 0x06001BEA RID: 7146 RVA: 0x00095124 File Offset: 0x00093324
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x170006F4 RID: 1780
			// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0009513C File Offset: 0x0009333C
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006F5 RID: 1781
			// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0009514C File Offset: 0x0009334C
			// (set) Token: 0x06001BED RID: 7149 RVA: 0x00095154 File Offset: 0x00093354
			public long containertype1
			{
				get
				{
					return this._containertype1;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._containertype1 = value;
				}
			}

			// Token: 0x170006F6 RID: 1782
			// (get) Token: 0x06001BEE RID: 7150 RVA: 0x0009516C File Offset: 0x0009336C
			public bool HasContainertype1
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170006F7 RID: 1783
			// (get) Token: 0x06001BEF RID: 7151 RVA: 0x0009517C File Offset: 0x0009337C
			// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x00095184 File Offset: 0x00093384
			public long containertype2
			{
				get
				{
					return this._containertype2;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._containertype2 = value;
				}
			}

			// Token: 0x170006F8 RID: 1784
			// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x0009519C File Offset: 0x0009339C
			public bool HasContainertype2
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170006F9 RID: 1785
			// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x000951AC File Offset: 0x000933AC
			// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x000951B4 File Offset: 0x000933B4
			public gameitem item1
			{
				get
				{
					return this._item1;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._item1 = value;
				}
			}

			// Token: 0x170006FA RID: 1786
			// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x000951CC File Offset: 0x000933CC
			public bool HasItem1
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x170006FB RID: 1787
			// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x000951DC File Offset: 0x000933DC
			// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x000951E4 File Offset: 0x000933E4
			public gameitem item2
			{
				get
				{
					return this._item2;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._item2 = value;
				}
			}

			// Token: 0x170006FC RID: 1788
			// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x000951FC File Offset: 0x000933FC
			public bool HasItem2
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x06001BF8 RID: 7160 RVA: 0x0009520C File Offset: 0x0009340C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.state = this.deserialize.read_integer();
						break;
					case 1:
						this.containertype1 = this.deserialize.read_integer();
						break;
					case 2:
						this.containertype2 = this.deserialize.read_integer();
						break;
					case 3:
						this.item1 = this.deserialize.read_obj<gameitem>();
						break;
					case 4:
						this.item2 = this.deserialize.read_obj<gameitem>();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001BF9 RID: 7161 RVA: 0x000952D4 File Offset: 0x000934D4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.containertype1, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.containertype2, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_obj(this.item1, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_obj(this.item2, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A2E RID: 6702
			private static int max_field_count = 5;

			// Token: 0x04001A2F RID: 6703
			private long _state;

			// Token: 0x04001A30 RID: 6704
			private long _containertype1;

			// Token: 0x04001A31 RID: 6705
			private long _containertype2;

			// Token: 0x04001A32 RID: 6706
			private gameitem _item1;

			// Token: 0x04001A33 RID: 6707
			private gameitem _item2;
		}
	}
}
