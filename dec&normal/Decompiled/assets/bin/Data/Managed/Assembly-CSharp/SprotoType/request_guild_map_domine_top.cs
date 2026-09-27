using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004AC RID: 1196
	public class request_guild_map_domine_top
	{
		// Token: 0x020004AD RID: 1197
		public class request : SprotoTypeBase
		{
			// Token: 0x060023F8 RID: 9208 RVA: 0x000A5474 File Offset: 0x000A3674
			public request() : base(request_guild_map_domine_top.request.max_field_count)
			{
			}

			// Token: 0x060023F9 RID: 9209 RVA: 0x000A5484 File Offset: 0x000A3684
			public request(byte[] buffer) : base(request_guild_map_domine_top.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A13 RID: 2579
			// (get) Token: 0x060023FB RID: 9211 RVA: 0x000A54A0 File Offset: 0x000A36A0
			// (set) Token: 0x060023FC RID: 9212 RVA: 0x000A54A8 File Offset: 0x000A36A8
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

			// Token: 0x17000A14 RID: 2580
			// (get) Token: 0x060023FD RID: 9213 RVA: 0x000A54C0 File Offset: 0x000A36C0
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060023FE RID: 9214 RVA: 0x000A54D0 File Offset: 0x000A36D0
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

			// Token: 0x060023FF RID: 9215 RVA: 0x000A552C File Offset: 0x000A372C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C59 RID: 7257
			private static int max_field_count = 1;

			// Token: 0x04001C5A RID: 7258
			private string _id;
		}
	}
}
