using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000320 RID: 800
	public class buy_big_pack
	{
		// Token: 0x02000321 RID: 801
		public class request : SprotoTypeBase
		{
			// Token: 0x06001709 RID: 5897 RVA: 0x0008AFF4 File Offset: 0x000891F4
			public request() : base(buy_big_pack.request.max_field_count)
			{
			}

			// Token: 0x0600170A RID: 5898 RVA: 0x0008B004 File Offset: 0x00089204
			public request(byte[] buffer) : base(buy_big_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004E7 RID: 1255
			// (get) Token: 0x0600170C RID: 5900 RVA: 0x0008B020 File Offset: 0x00089220
			// (set) Token: 0x0600170D RID: 5901 RVA: 0x0008B028 File Offset: 0x00089228
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

			// Token: 0x170004E8 RID: 1256
			// (get) Token: 0x0600170E RID: 5902 RVA: 0x0008B040 File Offset: 0x00089240
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600170F RID: 5903 RVA: 0x0008B050 File Offset: 0x00089250
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
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001710 RID: 5904 RVA: 0x0008B0AC File Offset: 0x000892AC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018CE RID: 6350
			private static int max_field_count = 1;

			// Token: 0x040018CF RID: 6351
			private string _ID;
		}
	}
}
