using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000338 RID: 824
	public class change_skill_index
	{
		// Token: 0x02000339 RID: 825
		public class request : SprotoTypeBase
		{
			// Token: 0x06001796 RID: 6038 RVA: 0x0008C118 File Offset: 0x0008A318
			public request() : base(change_skill_index.request.max_field_count)
			{
			}

			// Token: 0x06001797 RID: 6039 RVA: 0x0008C128 File Offset: 0x0008A328
			public request(byte[] buffer) : base(change_skill_index.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x06001799 RID: 6041 RVA: 0x0008C144 File Offset: 0x0008A344
			// (set) Token: 0x0600179A RID: 6042 RVA: 0x0008C14C File Offset: 0x0008A34C
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._index = value;
				}
			}

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x0600179B RID: 6043 RVA: 0x0008C164 File Offset: 0x0008A364
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600179C RID: 6044 RVA: 0x0008C174 File Offset: 0x0008A374
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
						this.index = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600179D RID: 6045 RVA: 0x0008C1D0 File Offset: 0x0008A3D0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.index, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018F1 RID: 6385
			private static int max_field_count = 1;

			// Token: 0x040018F2 RID: 6386
			private long _index;
		}
	}
}
