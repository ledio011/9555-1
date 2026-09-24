using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000371 RID: 881
	public class del_friend
	{
		// Token: 0x02000372 RID: 882
		public class request : SprotoTypeBase
		{
			// Token: 0x06001A86 RID: 6790 RVA: 0x00092530 File Offset: 0x00090730
			public request() : base(del_friend.request.max_field_count)
			{
			}

			// Token: 0x06001A87 RID: 6791 RVA: 0x00092540 File Offset: 0x00090740
			public request(byte[] buffer) : base(del_friend.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000675 RID: 1653
			// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0009255C File Offset: 0x0009075C
			// (set) Token: 0x06001A8A RID: 6794 RVA: 0x00092564 File Offset: 0x00090764
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

			// Token: 0x17000676 RID: 1654
			// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0009257C File Offset: 0x0009077C
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0009258C File Offset: 0x0009078C
			// (set) Token: 0x06001A8D RID: 6797 RVA: 0x00092594 File Offset: 0x00090794
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

			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x06001A8E RID: 6798 RVA: 0x000925AC File Offset: 0x000907AC
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001A8F RID: 6799 RVA: 0x000925BC File Offset: 0x000907BC
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

			// Token: 0x06001A90 RID: 6800 RVA: 0x00092634 File Offset: 0x00090834
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

			// Token: 0x040019D3 RID: 6611
			private static int max_field_count = 2;

			// Token: 0x040019D4 RID: 6612
			private long _characterId;

			// Token: 0x040019D5 RID: 6613
			private long _type;
		}
	}
}
