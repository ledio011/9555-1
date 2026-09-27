using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000629 RID: 1577
	public class tutorial_finish
	{
		// Token: 0x0200062A RID: 1578
		public class request : SprotoTypeBase
		{
			// Token: 0x06002DF3 RID: 11763 RVA: 0x000B8FD0 File Offset: 0x000B71D0
			public request() : base(tutorial_finish.request.max_field_count)
			{
			}

			// Token: 0x06002DF4 RID: 11764 RVA: 0x000B8FE0 File Offset: 0x000B71E0
			public request(byte[] buffer) : base(tutorial_finish.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D89 RID: 3465
			// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x000B8FFC File Offset: 0x000B71FC
			// (set) Token: 0x06002DF7 RID: 11767 RVA: 0x000B9004 File Offset: 0x000B7204
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._type = value;
				}
			}

			// Token: 0x17000D8A RID: 3466
			// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x000B901C File Offset: 0x000B721C
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002DF9 RID: 11769 RVA: 0x000B902C File Offset: 0x000B722C
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
						this.type = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002DFA RID: 11770 RVA: 0x000B9088 File Offset: 0x000B7288
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F0C RID: 7948
			private static int max_field_count = 1;

			// Token: 0x04001F0D RID: 7949
			private long _type;
		}
	}
}
