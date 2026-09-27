using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000378 RID: 888
	public class drop_item_info
	{
		// Token: 0x02000379 RID: 889
		public class request : SprotoTypeBase
		{
			// Token: 0x06001ADA RID: 6874 RVA: 0x0009303C File Offset: 0x0009123C
			public request() : base(drop_item_info.request.max_field_count)
			{
			}

			// Token: 0x06001ADB RID: 6875 RVA: 0x0009304C File Offset: 0x0009124C
			public request(byte[] buffer) : base(drop_item_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700069B RID: 1691
			// (get) Token: 0x06001ADD RID: 6877 RVA: 0x00093068 File Offset: 0x00091268
			// (set) Token: 0x06001ADE RID: 6878 RVA: 0x00093070 File Offset: 0x00091270
			public long serverId
			{
				get
				{
					return this._serverId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._serverId = value;
				}
			}

			// Token: 0x1700069C RID: 1692
			// (get) Token: 0x06001ADF RID: 6879 RVA: 0x00093088 File Offset: 0x00091288
			public bool HasServerId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700069D RID: 1693
			// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00093098 File Offset: 0x00091298
			// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x000930A0 File Offset: 0x000912A0
			public long pos_x
			{
				get
				{
					return this._pos_x;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._pos_x = value;
				}
			}

			// Token: 0x1700069E RID: 1694
			// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x000930B8 File Offset: 0x000912B8
			public bool HasPos_x
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700069F RID: 1695
			// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x000930C8 File Offset: 0x000912C8
			// (set) Token: 0x06001AE4 RID: 6884 RVA: 0x000930D0 File Offset: 0x000912D0
			public long pos_z
			{
				get
				{
					return this._pos_z;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._pos_z = value;
				}
			}

			// Token: 0x170006A0 RID: 1696
			// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x000930E8 File Offset: 0x000912E8
			public bool HasPos_z
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170006A1 RID: 1697
			// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000930F8 File Offset: 0x000912F8
			// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x00093100 File Offset: 0x00091300
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._type = value;
				}
			}

			// Token: 0x170006A2 RID: 1698
			// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00093118 File Offset: 0x00091318
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x170006A3 RID: 1699
			// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00093128 File Offset: 0x00091328
			// (set) Token: 0x06001AEA RID: 6890 RVA: 0x00093130 File Offset: 0x00091330
			public item item
			{
				get
				{
					return this._item;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._item = value;
				}
			}

			// Token: 0x170006A4 RID: 1700
			// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00093148 File Offset: 0x00091348
			public bool HasItem
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x170006A5 RID: 1701
			// (get) Token: 0x06001AEC RID: 6892 RVA: 0x00093158 File Offset: 0x00091358
			// (set) Token: 0x06001AED RID: 6893 RVA: 0x00093160 File Offset: 0x00091360
			public long ownServerId
			{
				get
				{
					return this._ownServerId;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._ownServerId = value;
				}
			}

			// Token: 0x170006A6 RID: 1702
			// (get) Token: 0x06001AEE RID: 6894 RVA: 0x00093178 File Offset: 0x00091378
			public bool HasOwnServerId
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x06001AEF RID: 6895 RVA: 0x00093188 File Offset: 0x00091388
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.serverId = this.deserialize.read_integer();
						continue;
					case 1:
						this.pos_x = this.deserialize.read_integer();
						continue;
					case 2:
						this.pos_z = this.deserialize.read_integer();
						continue;
					case 3:
						this.type = this.deserialize.read_integer();
						continue;
					case 4:
						this.item = this.deserialize.read_obj<item>();
						continue;
					case 7:
						this.ownServerId = this.deserialize.read_integer();
						continue;
					}
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06001AF0 RID: 6896 RVA: 0x00093270 File Offset: 0x00091470
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.serverId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.pos_x, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.pos_z, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.type, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_obj(this.item, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.ownServerId, 7);
				}
				return this.serialize.close();
			}

			// Token: 0x040019EB RID: 6635
			private static int max_field_count = 7;

			// Token: 0x040019EC RID: 6636
			private long _serverId;

			// Token: 0x040019ED RID: 6637
			private long _pos_x;

			// Token: 0x040019EE RID: 6638
			private long _pos_z;

			// Token: 0x040019EF RID: 6639
			private long _type;

			// Token: 0x040019F0 RID: 6640
			private item _item;

			// Token: 0x040019F1 RID: 6641
			private long _ownServerId;
		}
	}
}
