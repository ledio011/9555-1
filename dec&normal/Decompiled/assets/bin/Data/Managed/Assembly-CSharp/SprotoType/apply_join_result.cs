using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002F8 RID: 760
	public class apply_join_result
	{
		// Token: 0x020002F9 RID: 761
		public class request : SprotoTypeBase
		{
			// Token: 0x06001548 RID: 5448 RVA: 0x000874D0 File Offset: 0x000856D0
			public request() : base(apply_join_result.request.max_field_count)
			{
			}

			// Token: 0x06001549 RID: 5449 RVA: 0x000874E0 File Offset: 0x000856E0
			public request(byte[] buffer) : base(apply_join_result.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000419 RID: 1049
			// (get) Token: 0x0600154B RID: 5451 RVA: 0x000874FC File Offset: 0x000856FC
			// (set) Token: 0x0600154C RID: 5452 RVA: 0x00087504 File Offset: 0x00085704
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

			// Token: 0x1700041A RID: 1050
			// (get) Token: 0x0600154D RID: 5453 RVA: 0x0008751C File Offset: 0x0008571C
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700041B RID: 1051
			// (get) Token: 0x0600154E RID: 5454 RVA: 0x0008752C File Offset: 0x0008572C
			// (set) Token: 0x0600154F RID: 5455 RVA: 0x00087534 File Offset: 0x00085734
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

			// Token: 0x1700041C RID: 1052
			// (get) Token: 0x06001550 RID: 5456 RVA: 0x0008754C File Offset: 0x0008574C
			public bool HasIsAgree
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001551 RID: 5457 RVA: 0x0008755C File Offset: 0x0008575C
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

			// Token: 0x06001552 RID: 5458 RVA: 0x000875D4 File Offset: 0x000857D4
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

			// Token: 0x0400184E RID: 6222
			private static int max_field_count = 2;

			// Token: 0x0400184F RID: 6223
			private long _characterId;

			// Token: 0x04001850 RID: 6224
			private long _isAgree;
		}
	}
}
