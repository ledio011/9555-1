using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003E5 RID: 997
	public class guild_leave
	{
		// Token: 0x020003E6 RID: 998
		public class request : SprotoTypeBase
		{
			// Token: 0x06001EC9 RID: 7881 RVA: 0x0009B218 File Offset: 0x00099418
			public request() : base(guild_leave.request.max_field_count)
			{
			}

			// Token: 0x06001ECA RID: 7882 RVA: 0x0009B228 File Offset: 0x00099428
			public request(byte[] buffer) : base(guild_leave.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000847 RID: 2119
			// (get) Token: 0x06001ECC RID: 7884 RVA: 0x0009B244 File Offset: 0x00099444
			// (set) Token: 0x06001ECD RID: 7885 RVA: 0x0009B24C File Offset: 0x0009944C
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

			// Token: 0x17000848 RID: 2120
			// (get) Token: 0x06001ECE RID: 7886 RVA: 0x0009B264 File Offset: 0x00099464
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000849 RID: 2121
			// (get) Token: 0x06001ECF RID: 7887 RVA: 0x0009B274 File Offset: 0x00099474
			// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x0009B27C File Offset: 0x0009947C
			public long isCancel
			{
				get
				{
					return this._isCancel;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._isCancel = value;
				}
			}

			// Token: 0x1700084A RID: 2122
			// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x0009B294 File Offset: 0x00099494
			public bool HasIsCancel
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001ED2 RID: 7890 RVA: 0x0009B2A4 File Offset: 0x000994A4
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
							this.isCancel = this.deserialize.read_integer();
						}
					}
					else
					{
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001ED3 RID: 7891 RVA: 0x0009B31C File Offset: 0x0009951C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.isCancel, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B04 RID: 6916
			private static int max_field_count = 2;

			// Token: 0x04001B05 RID: 6917
			private long _characterId;

			// Token: 0x04001B06 RID: 6918
			private long _isCancel;
		}
	}
}
