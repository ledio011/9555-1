using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000344 RID: 836
	public class character_list
	{
		// Token: 0x02000345 RID: 837
		public class response : SprotoTypeBase
		{
			// Token: 0x06001867 RID: 6247 RVA: 0x0008DD84 File Offset: 0x0008BF84
			public response() : base(character_list.response.max_field_count)
			{
			}

			// Token: 0x06001868 RID: 6248 RVA: 0x0008DD94 File Offset: 0x0008BF94
			public response(byte[] buffer) : base(character_list.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x0600186A RID: 6250 RVA: 0x0008DDB0 File Offset: 0x0008BFB0
			// (set) Token: 0x0600186B RID: 6251 RVA: 0x0008DDB8 File Offset: 0x0008BFB8
			public Dictionary<long, character_overview> character
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

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x0600186C RID: 6252 RVA: 0x0008DDD0 File Offset: 0x0008BFD0
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600186D RID: 6253 RVA: 0x0008DDE0 File Offset: 0x0008BFE0
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
						this.character = this.deserialize.read_map<long, character_overview>((character_overview v) => v.id);
					}
				}
			}

			// Token: 0x0600186E RID: 6254 RVA: 0x0008DE58 File Offset: 0x0008C058
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, character_overview>(this.character, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001933 RID: 6451
			private static int max_field_count = 1;

			// Token: 0x04001934 RID: 6452
			private Dictionary<long, character_overview> _character;
		}
	}
}
