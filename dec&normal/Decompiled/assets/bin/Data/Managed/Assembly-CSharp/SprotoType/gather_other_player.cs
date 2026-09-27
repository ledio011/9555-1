using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003B9 RID: 953
	public class gather_other_player
	{
		// Token: 0x020003BA RID: 954
		public class request : SprotoTypeBase
		{
			// Token: 0x06001D16 RID: 7446 RVA: 0x000979AC File Offset: 0x00095BAC
			public request() : base(gather_other_player.request.max_field_count)
			{
			}

			// Token: 0x06001D17 RID: 7447 RVA: 0x000979BC File Offset: 0x00095BBC
			public request(byte[] buffer) : base(gather_other_player.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000789 RID: 1929
			// (get) Token: 0x06001D19 RID: 7449 RVA: 0x000979D8 File Offset: 0x00095BD8
			// (set) Token: 0x06001D1A RID: 7450 RVA: 0x000979E0 File Offset: 0x00095BE0
			public long characterid
			{
				get
				{
					return this._characterid;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._characterid = value;
				}
			}

			// Token: 0x1700078A RID: 1930
			// (get) Token: 0x06001D1B RID: 7451 RVA: 0x000979F8 File Offset: 0x00095BF8
			public bool HasCharacterid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001D1C RID: 7452 RVA: 0x00097A08 File Offset: 0x00095C08
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
						this.characterid = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001D1D RID: 7453 RVA: 0x00097A64 File Offset: 0x00095C64
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterid, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A89 RID: 6793
			private static int max_field_count = 1;

			// Token: 0x04001A8A RID: 6794
			private long _characterid;
		}
	}
}
