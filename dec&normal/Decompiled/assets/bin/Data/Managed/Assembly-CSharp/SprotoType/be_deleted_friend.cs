using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200031D RID: 797
	public class be_deleted_friend
	{
		// Token: 0x0200031E RID: 798
		public class request : SprotoTypeBase
		{
			// Token: 0x060016F5 RID: 5877 RVA: 0x0008AD7C File Offset: 0x00088F7C
			public request() : base(be_deleted_friend.request.max_field_count)
			{
			}

			// Token: 0x060016F6 RID: 5878 RVA: 0x0008AD8C File Offset: 0x00088F8C
			public request(byte[] buffer) : base(be_deleted_friend.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x060016F8 RID: 5880 RVA: 0x0008ADA8 File Offset: 0x00088FA8
			// (set) Token: 0x060016F9 RID: 5881 RVA: 0x0008ADB0 File Offset: 0x00088FB0
			public long characterId
			{
				get
				{
					return this._characterId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._characterId = value;
				}
			}

			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x060016FA RID: 5882 RVA: 0x0008ADC8 File Offset: 0x00088FC8
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060016FB RID: 5883 RVA: 0x0008ADD8 File Offset: 0x00088FD8
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
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060016FC RID: 5884 RVA: 0x0008AE34 File Offset: 0x00089034
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018C9 RID: 6345
			private static int max_field_count = 1;

			// Token: 0x040018CA RID: 6346
			private long _characterId;
		}
	}
}
