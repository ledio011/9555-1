using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200044A RID: 1098
	public class random_select_ok
	{
		// Token: 0x0200044B RID: 1099
		public class request : SprotoTypeBase
		{
			// Token: 0x06002251 RID: 8785 RVA: 0x000A2630 File Offset: 0x000A0830
			public request() : base(random_select_ok.request.max_field_count)
			{
			}

			// Token: 0x06002252 RID: 8786 RVA: 0x000A2640 File Offset: 0x000A0840
			public request(byte[] buffer) : base(random_select_ok.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009BD RID: 2493
			// (get) Token: 0x06002254 RID: 8788 RVA: 0x000A265C File Offset: 0x000A085C
			// (set) Token: 0x06002255 RID: 8789 RVA: 0x000A2664 File Offset: 0x000A0864
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

			// Token: 0x170009BE RID: 2494
			// (get) Token: 0x06002256 RID: 8790 RVA: 0x000A267C File Offset: 0x000A087C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170009BF RID: 2495
			// (get) Token: 0x06002257 RID: 8791 RVA: 0x000A268C File Offset: 0x000A088C
			// (set) Token: 0x06002258 RID: 8792 RVA: 0x000A2694 File Offset: 0x000A0894
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

			// Token: 0x170009C0 RID: 2496
			// (get) Token: 0x06002259 RID: 8793 RVA: 0x000A26AC File Offset: 0x000A08AC
			public bool HasType1
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600225A RID: 8794 RVA: 0x000A26BC File Offset: 0x000A08BC
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

			// Token: 0x0600225B RID: 8795 RVA: 0x000A2734 File Offset: 0x000A0934
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

			// Token: 0x04001BFD RID: 7165
			private static int max_field_count = 2;

			// Token: 0x04001BFE RID: 7166
			private string _id;

			// Token: 0x04001BFF RID: 7167
			private long _type1;
		}
	}
}
