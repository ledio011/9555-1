using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000462 RID: 1122
	public class receive_level_reward
	{
		// Token: 0x02000463 RID: 1123
		public class request : SprotoTypeBase
		{
			// Token: 0x060022B7 RID: 8887 RVA: 0x000A3160 File Offset: 0x000A1360
			public request() : base(receive_level_reward.request.max_field_count)
			{
			}

			// Token: 0x060022B8 RID: 8888 RVA: 0x000A3170 File Offset: 0x000A1370
			public request(byte[] buffer) : base(receive_level_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009D1 RID: 2513
			// (get) Token: 0x060022BA RID: 8890 RVA: 0x000A318C File Offset: 0x000A138C
			// (set) Token: 0x060022BB RID: 8891 RVA: 0x000A3194 File Offset: 0x000A1394
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

			// Token: 0x170009D2 RID: 2514
			// (get) Token: 0x060022BC RID: 8892 RVA: 0x000A31AC File Offset: 0x000A13AC
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170009D3 RID: 2515
			// (get) Token: 0x060022BD RID: 8893 RVA: 0x000A31BC File Offset: 0x000A13BC
			// (set) Token: 0x060022BE RID: 8894 RVA: 0x000A31C4 File Offset: 0x000A13C4
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._index = value;
				}
			}

			// Token: 0x170009D4 RID: 2516
			// (get) Token: 0x060022BF RID: 8895 RVA: 0x000A31DC File Offset: 0x000A13DC
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060022C0 RID: 8896 RVA: 0x000A31EC File Offset: 0x000A13EC
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
							this.index = this.deserialize.read_integer();
						}
					}
					else
					{
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060022C1 RID: 8897 RVA: 0x000A3264 File Offset: 0x000A1464
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.index, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C13 RID: 7187
			private static int max_field_count = 2;

			// Token: 0x04001C14 RID: 7188
			private string _ID;

			// Token: 0x04001C15 RID: 7189
			private long _index;
		}
	}
}
