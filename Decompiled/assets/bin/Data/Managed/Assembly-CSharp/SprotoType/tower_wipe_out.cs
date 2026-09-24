using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000627 RID: 1575
	public class tower_wipe_out
	{
		// Token: 0x02000628 RID: 1576
		public class request : SprotoTypeBase
		{
			// Token: 0x06002DEA RID: 11754 RVA: 0x000B8EC8 File Offset: 0x000B70C8
			public request() : base(tower_wipe_out.request.max_field_count)
			{
			}

			// Token: 0x06002DEB RID: 11755 RVA: 0x000B8ED8 File Offset: 0x000B70D8
			public request(byte[] buffer) : base(tower_wipe_out.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D87 RID: 3463
			// (get) Token: 0x06002DED RID: 11757 RVA: 0x000B8EF4 File Offset: 0x000B70F4
			// (set) Token: 0x06002DEE RID: 11758 RVA: 0x000B8EFC File Offset: 0x000B70FC
			public long wipeType
			{
				get
				{
					return this._wipeType;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._wipeType = value;
				}
			}

			// Token: 0x17000D88 RID: 3464
			// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000B8F14 File Offset: 0x000B7114
			public bool HasWipeType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002DF0 RID: 11760 RVA: 0x000B8F24 File Offset: 0x000B7124
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
						this.wipeType = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002DF1 RID: 11761 RVA: 0x000B8F80 File Offset: 0x000B7180
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.wipeType, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F0A RID: 7946
			private static int max_field_count = 1;

			// Token: 0x04001F0B RID: 7947
			private long _wipeType;
		}
	}
}
