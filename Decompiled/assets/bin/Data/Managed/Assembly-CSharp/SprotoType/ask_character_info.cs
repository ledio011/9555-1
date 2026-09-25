using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000300 RID: 768
	public class ask_character_info
	{
		// Token: 0x02000301 RID: 769
		public class request : SprotoTypeBase
		{
			// Token: 0x06001575 RID: 5493 RVA: 0x00087A40 File Offset: 0x00085C40
			public request() : base(ask_character_info.request.max_field_count)
			{
			}

			// Token: 0x06001576 RID: 5494 RVA: 0x00087A50 File Offset: 0x00085C50
			public request(byte[] buffer) : base(ask_character_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000427 RID: 1063
			// (get) Token: 0x06001578 RID: 5496 RVA: 0x00087A6C File Offset: 0x00085C6C
			// (set) Token: 0x06001579 RID: 5497 RVA: 0x00087A74 File Offset: 0x00085C74
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

			// Token: 0x17000428 RID: 1064
			// (get) Token: 0x0600157A RID: 5498 RVA: 0x00087A8C File Offset: 0x00085C8C
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600157B RID: 5499 RVA: 0x00087A9C File Offset: 0x00085C9C
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

			// Token: 0x0600157C RID: 5500 RVA: 0x00087AF8 File Offset: 0x00085CF8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001859 RID: 6233
			private static int max_field_count = 1;

			// Token: 0x0400185A RID: 6234
			private long _characterId;
		}

		// Token: 0x02000302 RID: 770
		public class response : SprotoTypeBase
		{
			// Token: 0x0600157D RID: 5501 RVA: 0x00087B40 File Offset: 0x00085D40
			public response() : base(ask_character_info.response.max_field_count)
			{
			}

			// Token: 0x0600157E RID: 5502 RVA: 0x00087B50 File Offset: 0x00085D50
			public response(byte[] buffer) : base(ask_character_info.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000429 RID: 1065
			// (get) Token: 0x06001580 RID: 5504 RVA: 0x00087B6C File Offset: 0x00085D6C
			// (set) Token: 0x06001581 RID: 5505 RVA: 0x00087B74 File Offset: 0x00085D74
			public character_look character
			{
				get
				{
					return this._character;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._character = value;
				}
			}

			// Token: 0x1700042A RID: 1066
			// (get) Token: 0x06001582 RID: 5506 RVA: 0x00087B8C File Offset: 0x00085D8C
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001583 RID: 5507 RVA: 0x00087B9C File Offset: 0x00085D9C
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
						this.character = this.deserialize.read_obj<character_look>();
					}
				}
			}

			// Token: 0x06001584 RID: 5508 RVA: 0x00087BF8 File Offset: 0x00085DF8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.character, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x0400185B RID: 6235
			private static int max_field_count = 1;

			// Token: 0x0400185C RID: 6236
			private character_look _character;
		}
	}
}
