using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002FE RID: 766
	public class approve_resverve_friend
	{
		// Token: 0x020002FF RID: 767
		public class request : SprotoTypeBase
		{
			// Token: 0x06001569 RID: 5481 RVA: 0x000878C8 File Offset: 0x00085AC8
			public request() : base(approve_resverve_friend.request.max_field_count)
			{
			}

			// Token: 0x0600156A RID: 5482 RVA: 0x000878D8 File Offset: 0x00085AD8
			public request(byte[] buffer) : base(approve_resverve_friend.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000423 RID: 1059
			// (get) Token: 0x0600156C RID: 5484 RVA: 0x000878F4 File Offset: 0x00085AF4
			// (set) Token: 0x0600156D RID: 5485 RVA: 0x000878FC File Offset: 0x00085AFC
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

			// Token: 0x17000424 RID: 1060
			// (get) Token: 0x0600156E RID: 5486 RVA: 0x00087914 File Offset: 0x00085B14
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000425 RID: 1061
			// (get) Token: 0x0600156F RID: 5487 RVA: 0x00087924 File Offset: 0x00085B24
			// (set) Token: 0x06001570 RID: 5488 RVA: 0x0008792C File Offset: 0x00085B2C
			public long isAgree
			{
				get
				{
					return this._isAgree;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._isAgree = value;
				}
			}

			// Token: 0x17000426 RID: 1062
			// (get) Token: 0x06001571 RID: 5489 RVA: 0x00087944 File Offset: 0x00085B44
			public bool HasIsAgree
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001572 RID: 5490 RVA: 0x00087954 File Offset: 0x00085B54
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
							this.isAgree = this.deserialize.read_integer();
						}
					}
					else
					{
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001573 RID: 5491 RVA: 0x000879CC File Offset: 0x00085BCC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.isAgree, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001856 RID: 6230
			private static int max_field_count = 2;

			// Token: 0x04001857 RID: 6231
			private long _characterId;

			// Token: 0x04001858 RID: 6232
			private long _isAgree;
		}
	}
}
