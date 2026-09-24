using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002F0 RID: 752
	public class aoi_social_dance
	{
		// Token: 0x020002F1 RID: 753
		public class request : SprotoTypeBase
		{
			// Token: 0x06001521 RID: 5409 RVA: 0x00087040 File Offset: 0x00085240
			public request() : base(aoi_social_dance.request.max_field_count)
			{
			}

			// Token: 0x06001522 RID: 5410 RVA: 0x00087050 File Offset: 0x00085250
			public request(byte[] buffer) : base(aoi_social_dance.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700040F RID: 1039
			// (get) Token: 0x06001524 RID: 5412 RVA: 0x0008706C File Offset: 0x0008526C
			// (set) Token: 0x06001525 RID: 5413 RVA: 0x00087074 File Offset: 0x00085274
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

			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x06001526 RID: 5414 RVA: 0x0008708C File Offset: 0x0008528C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x06001527 RID: 5415 RVA: 0x0008709C File Offset: 0x0008529C
			// (set) Token: 0x06001528 RID: 5416 RVA: 0x000870A4 File Offset: 0x000852A4
			public string danceId
			{
				get
				{
					return this._danceId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._danceId = value;
				}
			}

			// Token: 0x17000412 RID: 1042
			// (get) Token: 0x06001529 RID: 5417 RVA: 0x000870BC File Offset: 0x000852BC
			public bool HasDanceId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600152A RID: 5418 RVA: 0x000870CC File Offset: 0x000852CC
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
							this.danceId = this.deserialize.read_string();
						}
					}
					else
					{
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600152B RID: 5419 RVA: 0x00087144 File Offset: 0x00085344
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.danceId, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001845 RID: 6213
			private static int max_field_count = 2;

			// Token: 0x04001846 RID: 6214
			private long _id;

			// Token: 0x04001847 RID: 6215
			private string _danceId;
		}
	}
}
