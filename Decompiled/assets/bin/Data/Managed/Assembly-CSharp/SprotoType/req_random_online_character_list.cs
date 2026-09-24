using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200048E RID: 1166
	public class req_random_online_character_list
	{
		// Token: 0x0200048F RID: 1167
		public class request : SprotoTypeBase
		{
			// Token: 0x06002389 RID: 9097 RVA: 0x000A490C File Offset: 0x000A2B0C
			public request() : base(req_random_online_character_list.request.max_field_count)
			{
			}

			// Token: 0x0600238A RID: 9098 RVA: 0x000A491C File Offset: 0x000A2B1C
			public request(byte[] buffer) : base(req_random_online_character_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A05 RID: 2565
			// (get) Token: 0x0600238C RID: 9100 RVA: 0x000A4938 File Offset: 0x000A2B38
			// (set) Token: 0x0600238D RID: 9101 RVA: 0x000A4940 File Offset: 0x000A2B40
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

			// Token: 0x17000A06 RID: 2566
			// (get) Token: 0x0600238E RID: 9102 RVA: 0x000A4958 File Offset: 0x000A2B58
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600238F RID: 9103 RVA: 0x000A4968 File Offset: 0x000A2B68
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

			// Token: 0x06002390 RID: 9104 RVA: 0x000A49C4 File Offset: 0x000A2BC4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C43 RID: 7235
			private static int max_field_count = 1;

			// Token: 0x04001C44 RID: 7236
			private long _characterId;
		}
	}
}
