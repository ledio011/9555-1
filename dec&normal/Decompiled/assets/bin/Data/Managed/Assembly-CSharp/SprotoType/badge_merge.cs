using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000317 RID: 791
	public class badge_merge
	{
		// Token: 0x02000318 RID: 792
		public class request : SprotoTypeBase
		{
			// Token: 0x060016BE RID: 5822 RVA: 0x0008A668 File Offset: 0x00088868
			public request() : base(badge_merge.request.max_field_count)
			{
			}

			// Token: 0x060016BF RID: 5823 RVA: 0x0008A678 File Offset: 0x00088878
			public request(byte[] buffer) : base(badge_merge.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0008A694 File Offset: 0x00088894
			// (set) Token: 0x060016C2 RID: 5826 RVA: 0x0008A69C File Offset: 0x0008889C
			public long indexId
			{
				get
				{
					return this._indexId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._indexId = value;
				}
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x060016C3 RID: 5827 RVA: 0x0008A6B4 File Offset: 0x000888B4
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x060016C4 RID: 5828 RVA: 0x0008A6C4 File Offset: 0x000888C4
			// (set) Token: 0x060016C5 RID: 5829 RVA: 0x0008A6CC File Offset: 0x000888CC
			public string nextItemId
			{
				get
				{
					return this._nextItemId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._nextItemId = value;
				}
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0008A6E4 File Offset: 0x000888E4
			public bool HasNextItemId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0008A6F4 File Offset: 0x000888F4
			// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0008A6FC File Offset: 0x000888FC
			public long count
			{
				get
				{
					return this._count;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._count = value;
				}
			}

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0008A714 File Offset: 0x00088914
			public bool HasCount
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060016CA RID: 5834 RVA: 0x0008A724 File Offset: 0x00088924
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.indexId = this.deserialize.read_integer();
						break;
					case 1:
						this.nextItemId = this.deserialize.read_string();
						break;
					case 2:
						this.count = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060016CB RID: 5835 RVA: 0x0008A7B8 File Offset: 0x000889B8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.nextItemId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.count, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x040018BA RID: 6330
			private static int max_field_count = 3;

			// Token: 0x040018BB RID: 6331
			private long _indexId;

			// Token: 0x040018BC RID: 6332
			private string _nextItemId;

			// Token: 0x040018BD RID: 6333
			private long _count;
		}

		// Token: 0x02000319 RID: 793
		public class response : SprotoTypeBase
		{
			// Token: 0x060016CC RID: 5836 RVA: 0x0008A848 File Offset: 0x00088A48
			public response() : base(badge_merge.response.max_field_count)
			{
			}

			// Token: 0x060016CD RID: 5837 RVA: 0x0008A858 File Offset: 0x00088A58
			public response(byte[] buffer) : base(badge_merge.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x060016CF RID: 5839 RVA: 0x0008A874 File Offset: 0x00088A74
			// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0008A87C File Offset: 0x00088A7C
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0008A894 File Offset: 0x00088A94
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060016D2 RID: 5842 RVA: 0x0008A8A4 File Offset: 0x00088AA4
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
						this.state = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060016D3 RID: 5843 RVA: 0x0008A900 File Offset: 0x00088B00
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018BE RID: 6334
			private static int max_field_count = 1;

			// Token: 0x040018BF RID: 6335
			private long _state;
		}
	}
}
