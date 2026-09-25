using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200037F RID: 895
	public class enter_domin_pk_scene
	{
		// Token: 0x02000380 RID: 896
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B12 RID: 6930 RVA: 0x00093760 File Offset: 0x00091960
			public request() : base(enter_domin_pk_scene.request.max_field_count)
			{
			}

			// Token: 0x06001B13 RID: 6931 RVA: 0x00093770 File Offset: 0x00091970
			public request(byte[] buffer) : base(enter_domin_pk_scene.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006B1 RID: 1713
			// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0009378C File Offset: 0x0009198C
			// (set) Token: 0x06001B16 RID: 6934 RVA: 0x00093794 File Offset: 0x00091994
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

			// Token: 0x170006B2 RID: 1714
			// (get) Token: 0x06001B17 RID: 6935 RVA: 0x000937AC File Offset: 0x000919AC
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001B18 RID: 6936 RVA: 0x000937BC File Offset: 0x000919BC
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

			// Token: 0x06001B19 RID: 6937 RVA: 0x00093818 File Offset: 0x00091A18
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040019FA RID: 6650
			private static int max_field_count = 1;

			// Token: 0x040019FB RID: 6651
			private string _id;
		}
	}
}
