using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002E8 RID: 744
	public class add_friend
	{
		// Token: 0x020002E9 RID: 745
		public class request : SprotoTypeBase
		{
			// Token: 0x060014FA RID: 5370 RVA: 0x00086BB0 File Offset: 0x00084DB0
			public request() : base(add_friend.request.max_field_count)
			{
			}

			// Token: 0x060014FB RID: 5371 RVA: 0x00086BC0 File Offset: 0x00084DC0
			public request(byte[] buffer) : base(add_friend.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000405 RID: 1029
			// (get) Token: 0x060014FD RID: 5373 RVA: 0x00086BDC File Offset: 0x00084DDC
			// (set) Token: 0x060014FE RID: 5374 RVA: 0x00086BE4 File Offset: 0x00084DE4
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

			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x060014FF RID: 5375 RVA: 0x00086BFC File Offset: 0x00084DFC
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000407 RID: 1031
			// (get) Token: 0x06001500 RID: 5376 RVA: 0x00086C0C File Offset: 0x00084E0C
			// (set) Token: 0x06001501 RID: 5377 RVA: 0x00086C14 File Offset: 0x00084E14
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

			// Token: 0x17000408 RID: 1032
			// (get) Token: 0x06001502 RID: 5378 RVA: 0x00086C2C File Offset: 0x00084E2C
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001503 RID: 5379 RVA: 0x00086C3C File Offset: 0x00084E3C
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

			// Token: 0x06001504 RID: 5380 RVA: 0x00086CB4 File Offset: 0x00084EB4
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

			// Token: 0x0400183C RID: 6204
			private static int max_field_count = 2;

			// Token: 0x0400183D RID: 6205
			private long _characterId;

			// Token: 0x0400183E RID: 6206
			private long _type;
		}
	}
}
