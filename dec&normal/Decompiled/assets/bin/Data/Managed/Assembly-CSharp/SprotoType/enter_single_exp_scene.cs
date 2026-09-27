using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000391 RID: 913
	public class enter_single_exp_scene
	{
		// Token: 0x02000392 RID: 914
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B6F RID: 7023 RVA: 0x00094258 File Offset: 0x00092458
			public request() : base(enter_single_exp_scene.request.max_field_count)
			{
			}

			// Token: 0x06001B70 RID: 7024 RVA: 0x00094268 File Offset: 0x00092468
			public request(byte[] buffer) : base(enter_single_exp_scene.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006CB RID: 1739
			// (get) Token: 0x06001B72 RID: 7026 RVA: 0x00094284 File Offset: 0x00092484
			// (set) Token: 0x06001B73 RID: 7027 RVA: 0x0009428C File Offset: 0x0009248C
			public string id
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

			// Token: 0x170006CC RID: 1740
			// (get) Token: 0x06001B74 RID: 7028 RVA: 0x000942A4 File Offset: 0x000924A4
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001B75 RID: 7029 RVA: 0x000942B4 File Offset: 0x000924B4
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
						this.id = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001B76 RID: 7030 RVA: 0x00094310 File Offset: 0x00092510
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A10 RID: 6672
			private static int max_field_count = 1;

			// Token: 0x04001A11 RID: 6673
			private string _id;
		}
	}
}
