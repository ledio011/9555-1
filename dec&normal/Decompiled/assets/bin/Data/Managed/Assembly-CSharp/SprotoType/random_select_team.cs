using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200044C RID: 1100
	public class random_select_team
	{
		// Token: 0x0200044D RID: 1101
		public class request : SprotoTypeBase
		{
			// Token: 0x0600225D RID: 8797 RVA: 0x000A27A8 File Offset: 0x000A09A8
			public request() : base(random_select_team.request.max_field_count)
			{
			}

			// Token: 0x0600225E RID: 8798 RVA: 0x000A27B8 File Offset: 0x000A09B8
			public request(byte[] buffer) : base(random_select_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009C1 RID: 2497
			// (get) Token: 0x06002260 RID: 8800 RVA: 0x000A27D4 File Offset: 0x000A09D4
			// (set) Token: 0x06002261 RID: 8801 RVA: 0x000A27DC File Offset: 0x000A09DC
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

			// Token: 0x170009C2 RID: 2498
			// (get) Token: 0x06002262 RID: 8802 RVA: 0x000A27F4 File Offset: 0x000A09F4
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170009C3 RID: 2499
			// (get) Token: 0x06002263 RID: 8803 RVA: 0x000A2804 File Offset: 0x000A0A04
			// (set) Token: 0x06002264 RID: 8804 RVA: 0x000A280C File Offset: 0x000A0A0C
			public long type1
			{
				get
				{
					return this._type1;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type1 = value;
				}
			}

			// Token: 0x170009C4 RID: 2500
			// (get) Token: 0x06002265 RID: 8805 RVA: 0x000A2824 File Offset: 0x000A0A24
			public bool HasType1
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002266 RID: 8806 RVA: 0x000A2834 File Offset: 0x000A0A34
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
							this.type1 = this.deserialize.read_integer();
						}
					}
					else
					{
						this.id = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002267 RID: 8807 RVA: 0x000A28AC File Offset: 0x000A0AAC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type1, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C00 RID: 7168
			private static int max_field_count = 2;

			// Token: 0x04001C01 RID: 7169
			private string _id;

			// Token: 0x04001C02 RID: 7170
			private long _type1;
		}
	}
}
