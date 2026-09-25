using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000535 RID: 1333
	public class ret_guild_leave
	{
		// Token: 0x02000536 RID: 1334
		public class request : SprotoTypeBase
		{
			// Token: 0x060026DD RID: 9949 RVA: 0x000AAB9C File Offset: 0x000A8D9C
			public request() : base(ret_guild_leave.request.max_field_count)
			{
			}

			// Token: 0x060026DE RID: 9950 RVA: 0x000AABAC File Offset: 0x000A8DAC
			public request(byte[] buffer) : base(ret_guild_leave.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AE9 RID: 2793
			// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000AABC8 File Offset: 0x000A8DC8
			// (set) Token: 0x060026E1 RID: 9953 RVA: 0x000AABD0 File Offset: 0x000A8DD0
			public long guildId
			{
				get
				{
					return this._guildId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guildId = value;
				}
			}

			// Token: 0x17000AEA RID: 2794
			// (get) Token: 0x060026E2 RID: 9954 RVA: 0x000AABE8 File Offset: 0x000A8DE8
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000AEB RID: 2795
			// (get) Token: 0x060026E3 RID: 9955 RVA: 0x000AABF8 File Offset: 0x000A8DF8
			// (set) Token: 0x060026E4 RID: 9956 RVA: 0x000AAC00 File Offset: 0x000A8E00
			public long dismissTime
			{
				get
				{
					return this._dismissTime;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._dismissTime = value;
				}
			}

			// Token: 0x17000AEC RID: 2796
			// (get) Token: 0x060026E5 RID: 9957 RVA: 0x000AAC18 File Offset: 0x000A8E18
			public bool HasDismissTime
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060026E6 RID: 9958 RVA: 0x000AAC28 File Offset: 0x000A8E28
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
							this.dismissTime = this.deserialize.read_integer();
						}
					}
					else
					{
						this.guildId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060026E7 RID: 9959 RVA: 0x000AACA0 File Offset: 0x000A8EA0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.guildId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.dismissTime, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D10 RID: 7440
			private static int max_field_count = 2;

			// Token: 0x04001D11 RID: 7441
			private long _guildId;

			// Token: 0x04001D12 RID: 7442
			private long _dismissTime;
		}
	}
}
