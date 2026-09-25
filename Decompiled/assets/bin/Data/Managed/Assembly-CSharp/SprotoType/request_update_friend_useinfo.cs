using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004DD RID: 1245
	public class request_update_friend_useinfo
	{
		// Token: 0x020004DE RID: 1246
		public class request : SprotoTypeBase
		{
			// Token: 0x060024AB RID: 9387 RVA: 0x000A66A4 File Offset: 0x000A48A4
			public request() : base(request_update_friend_useinfo.request.max_field_count)
			{
			}

			// Token: 0x060024AC RID: 9388 RVA: 0x000A66B4 File Offset: 0x000A48B4
			public request(byte[] buffer) : base(request_update_friend_useinfo.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A27 RID: 2599
			// (get) Token: 0x060024AE RID: 9390 RVA: 0x000A66D0 File Offset: 0x000A48D0
			// (set) Token: 0x060024AF RID: 9391 RVA: 0x000A66D8 File Offset: 0x000A48D8
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

			// Token: 0x17000A28 RID: 2600
			// (get) Token: 0x060024B0 RID: 9392 RVA: 0x000A66F0 File Offset: 0x000A48F0
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A29 RID: 2601
			// (get) Token: 0x060024B1 RID: 9393 RVA: 0x000A6700 File Offset: 0x000A4900
			// (set) Token: 0x060024B2 RID: 9394 RVA: 0x000A6708 File Offset: 0x000A4908
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type = value;
				}
			}

			// Token: 0x17000A2A RID: 2602
			// (get) Token: 0x060024B3 RID: 9395 RVA: 0x000A6720 File Offset: 0x000A4920
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060024B4 RID: 9396 RVA: 0x000A6730 File Offset: 0x000A4930
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
							this.type = this.deserialize.read_integer();
						}
					}
					else
					{
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060024B5 RID: 9397 RVA: 0x000A67A8 File Offset: 0x000A49A8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C7C RID: 7292
			private static int max_field_count = 2;

			// Token: 0x04001C7D RID: 7293
			private long _characterId;

			// Token: 0x04001C7E RID: 7294
			private long _type;
		}
	}
}
