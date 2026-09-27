using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000653 RID: 1619
	public class update_team_setting
	{
		// Token: 0x02000654 RID: 1620
		public class request : SprotoTypeBase
		{
			// Token: 0x06002ED8 RID: 11992 RVA: 0x000BAB20 File Offset: 0x000B8D20
			public request() : base(update_team_setting.request.max_field_count)
			{
			}

			// Token: 0x06002ED9 RID: 11993 RVA: 0x000BAB30 File Offset: 0x000B8D30
			public request(byte[] buffer) : base(update_team_setting.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DCB RID: 3531
			// (get) Token: 0x06002EDB RID: 11995 RVA: 0x000BAB4C File Offset: 0x000B8D4C
			// (set) Token: 0x06002EDC RID: 11996 RVA: 0x000BAB54 File Offset: 0x000B8D54
			public long isVerfiy
			{
				get
				{
					return this._isVerfiy;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._isVerfiy = value;
				}
			}

			// Token: 0x17000DCC RID: 3532
			// (get) Token: 0x06002EDD RID: 11997 RVA: 0x000BAB6C File Offset: 0x000B8D6C
			public bool HasIsVerfiy
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002EDE RID: 11998 RVA: 0x000BAB7C File Offset: 0x000B8D7C
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
						this.isVerfiy = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002EDF RID: 11999 RVA: 0x000BABD8 File Offset: 0x000B8DD8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.isVerfiy, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F43 RID: 8003
			private static int max_field_count = 1;

			// Token: 0x04001F44 RID: 8004
			private long _isVerfiy;
		}
	}
}
