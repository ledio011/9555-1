using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005C6 RID: 1478
	public class send_mail
	{
		// Token: 0x020005C7 RID: 1479
		public class request : SprotoTypeBase
		{
			// Token: 0x06002AA3 RID: 10915 RVA: 0x000B2380 File Offset: 0x000B0580
			public request() : base(send_mail.request.max_field_count)
			{
			}

			// Token: 0x06002AA4 RID: 10916 RVA: 0x000B2390 File Offset: 0x000B0590
			public request(byte[] buffer) : base(send_mail.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C33 RID: 3123
			// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x000B23AC File Offset: 0x000B05AC
			// (set) Token: 0x06002AA7 RID: 10919 RVA: 0x000B23B4 File Offset: 0x000B05B4
			public long receiveId
			{
				get
				{
					return this._receiveId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._receiveId = value;
				}
			}

			// Token: 0x17000C34 RID: 3124
			// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000B23CC File Offset: 0x000B05CC
			public bool HasReceiveId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C35 RID: 3125
			// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x000B23DC File Offset: 0x000B05DC
			// (set) Token: 0x06002AAA RID: 10922 RVA: 0x000B23E4 File Offset: 0x000B05E4
			public string context
			{
				get
				{
					return this._context;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._context = value;
				}
			}

			// Token: 0x17000C36 RID: 3126
			// (get) Token: 0x06002AAB RID: 10923 RVA: 0x000B23FC File Offset: 0x000B05FC
			public bool HasContext
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002AAC RID: 10924 RVA: 0x000B240C File Offset: 0x000B060C
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
							this.context = this.deserialize.read_string();
						}
					}
					else
					{
						this.receiveId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002AAD RID: 10925 RVA: 0x000B2484 File Offset: 0x000B0684
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.receiveId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.context, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E1D RID: 7709
			private static int max_field_count = 2;

			// Token: 0x04001E1E RID: 7710
			private long _receiveId;

			// Token: 0x04001E1F RID: 7711
			private string _context;
		}
	}
}
