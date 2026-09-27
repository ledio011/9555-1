using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000637 RID: 1591
	public class update_copyscene_info
	{
		// Token: 0x02000638 RID: 1592
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E35 RID: 11829 RVA: 0x000B9768 File Offset: 0x000B7968
			public request() : base(update_copyscene_info.request.max_field_count)
			{
			}

			// Token: 0x06002E36 RID: 11830 RVA: 0x000B9778 File Offset: 0x000B7978
			public request(byte[] buffer) : base(update_copyscene_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D99 RID: 3481
			// (get) Token: 0x06002E38 RID: 11832 RVA: 0x000B9794 File Offset: 0x000B7994
			// (set) Token: 0x06002E39 RID: 11833 RVA: 0x000B979C File Offset: 0x000B799C
			public copyscene_info copyscene
			{
				get
				{
					return this._copyscene;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._copyscene = value;
				}
			}

			// Token: 0x17000D9A RID: 3482
			// (get) Token: 0x06002E3A RID: 11834 RVA: 0x000B97B4 File Offset: 0x000B79B4
			public bool HasCopyscene
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002E3B RID: 11835 RVA: 0x000B97C4 File Offset: 0x000B79C4
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
						this.copyscene = this.deserialize.read_obj<copyscene_info>();
					}
				}
			}

			// Token: 0x06002E3C RID: 11836 RVA: 0x000B9820 File Offset: 0x000B7A20
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.copyscene, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F1B RID: 7963
			private static int max_field_count = 1;

			// Token: 0x04001F1C RID: 7964
			private copyscene_info _copyscene;
		}
	}
}
