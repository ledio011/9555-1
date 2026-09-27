using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003DB RID: 987
	public class guild_invite
	{
		// Token: 0x020003DC RID: 988
		public class request : SprotoTypeBase
		{
			// Token: 0x06001E93 RID: 7827 RVA: 0x0009ABA0 File Offset: 0x00098DA0
			public request() : base(guild_invite.request.max_field_count)
			{
			}

			// Token: 0x06001E94 RID: 7828 RVA: 0x0009ABB0 File Offset: 0x00098DB0
			public request(byte[] buffer) : base(guild_invite.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000837 RID: 2103
			// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0009ABCC File Offset: 0x00098DCC
			// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0009ABD4 File Offset: 0x00098DD4
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000838 RID: 2104
			// (get) Token: 0x06001E98 RID: 7832 RVA: 0x0009ABEC File Offset: 0x00098DEC
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001E99 RID: 7833 RVA: 0x0009ABFC File Offset: 0x00098DFC
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
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001E9A RID: 7834 RVA: 0x0009AC58 File Offset: 0x00098E58
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001AF7 RID: 6903
			private static int max_field_count = 1;

			// Token: 0x04001AF8 RID: 6904
			private long _id;
		}
	}
}
