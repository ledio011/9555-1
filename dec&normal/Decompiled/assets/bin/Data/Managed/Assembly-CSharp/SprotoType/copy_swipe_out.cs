using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000365 RID: 869
	public class copy_swipe_out
	{
		// Token: 0x02000366 RID: 870
		public class request : SprotoTypeBase
		{
			// Token: 0x060019E3 RID: 6627 RVA: 0x00090F7C File Offset: 0x0008F17C
			public request() : base(copy_swipe_out.request.max_field_count)
			{
			}

			// Token: 0x060019E4 RID: 6628 RVA: 0x00090F8C File Offset: 0x0008F18C
			public request(byte[] buffer) : base(copy_swipe_out.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700062B RID: 1579
			// (get) Token: 0x060019E6 RID: 6630 RVA: 0x00090FA8 File Offset: 0x0008F1A8
			// (set) Token: 0x060019E7 RID: 6631 RVA: 0x00090FB0 File Offset: 0x0008F1B0
			public string copyInfoId
			{
				get
				{
					return this._copyInfoId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._copyInfoId = value;
				}
			}

			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00090FC8 File Offset: 0x0008F1C8
			public bool HasCopyInfoId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00090FD8 File Offset: 0x0008F1D8
			// (set) Token: 0x060019EA RID: 6634 RVA: 0x00090FE0 File Offset: 0x0008F1E0
			public bool reaminItem
			{
				get
				{
					return this._reaminItem;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._reaminItem = value;
				}
			}

			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x060019EB RID: 6635 RVA: 0x00090FF8 File Offset: 0x0008F1F8
			public bool HasReaminItem
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060019EC RID: 6636 RVA: 0x00091008 File Offset: 0x0008F208
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
							this.reaminItem = this.deserialize.read_boolean();
						}
					}
					else
					{
						this.copyInfoId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060019ED RID: 6637 RVA: 0x00091080 File Offset: 0x0008F280
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.copyInfoId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_boolean(this.reaminItem, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x040019A4 RID: 6564
			private static int max_field_count = 2;

			// Token: 0x040019A5 RID: 6565
			private string _copyInfoId;

			// Token: 0x040019A6 RID: 6566
			private bool _reaminItem;
		}
	}
}
