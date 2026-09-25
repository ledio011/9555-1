using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004C5 RID: 1221
	public class request_retrieve
	{
		// Token: 0x020004C6 RID: 1222
		public class request : SprotoTypeBase
		{
			// Token: 0x06002454 RID: 9300 RVA: 0x000A5DD4 File Offset: 0x000A3FD4
			public request() : base(request_retrieve.request.max_field_count)
			{
			}

			// Token: 0x06002455 RID: 9301 RVA: 0x000A5DE4 File Offset: 0x000A3FE4
			public request(byte[] buffer) : base(request_retrieve.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A1D RID: 2589
			// (get) Token: 0x06002457 RID: 9303 RVA: 0x000A5E00 File Offset: 0x000A4000
			// (set) Token: 0x06002458 RID: 9304 RVA: 0x000A5E08 File Offset: 0x000A4008
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

			// Token: 0x17000A1E RID: 2590
			// (get) Token: 0x06002459 RID: 9305 RVA: 0x000A5E20 File Offset: 0x000A4020
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A1F RID: 2591
			// (get) Token: 0x0600245A RID: 9306 RVA: 0x000A5E30 File Offset: 0x000A4030
			// (set) Token: 0x0600245B RID: 9307 RVA: 0x000A5E38 File Offset: 0x000A4038
			public long Type
			{
				get
				{
					return this._Type;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._Type = value;
				}
			}

			// Token: 0x17000A20 RID: 2592
			// (get) Token: 0x0600245C RID: 9308 RVA: 0x000A5E50 File Offset: 0x000A4050
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600245D RID: 9309 RVA: 0x000A5E60 File Offset: 0x000A4060
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
							this.Type = this.deserialize.read_integer();
						}
					}
					else
					{
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x0600245E RID: 9310 RVA: 0x000A5ED8 File Offset: 0x000A40D8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.Type, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C6B RID: 7275
			private static int max_field_count = 2;

			// Token: 0x04001C6C RID: 7276
			private string _ID;

			// Token: 0x04001C6D RID: 7277
			private long _Type;
		}
	}
}
