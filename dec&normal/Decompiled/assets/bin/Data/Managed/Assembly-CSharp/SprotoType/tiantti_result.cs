using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200061E RID: 1566
	public class tiantti_result
	{
		// Token: 0x0200061F RID: 1567
		public class request : SprotoTypeBase
		{
			// Token: 0x06002D96 RID: 11670 RVA: 0x000B8418 File Offset: 0x000B6618
			public request() : base(tiantti_result.request.max_field_count)
			{
			}

			// Token: 0x06002D97 RID: 11671 RVA: 0x000B8428 File Offset: 0x000B6628
			public request(byte[] buffer) : base(tiantti_result.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D65 RID: 3429
			// (get) Token: 0x06002D99 RID: 11673 RVA: 0x000B8444 File Offset: 0x000B6644
			// (set) Token: 0x06002D9A RID: 11674 RVA: 0x000B844C File Offset: 0x000B664C
			public bool win
			{
				get
				{
					return this._win;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._win = value;
				}
			}

			// Token: 0x17000D66 RID: 3430
			// (get) Token: 0x06002D9B RID: 11675 RVA: 0x000B8464 File Offset: 0x000B6664
			public bool HasWin
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000D67 RID: 3431
			// (get) Token: 0x06002D9C RID: 11676 RVA: 0x000B8474 File Offset: 0x000B6674
			// (set) Token: 0x06002D9D RID: 11677 RVA: 0x000B847C File Offset: 0x000B667C
			public List<item> items
			{
				get
				{
					return this._items;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._items = value;
				}
			}

			// Token: 0x17000D68 RID: 3432
			// (get) Token: 0x06002D9E RID: 11678 RVA: 0x000B8494 File Offset: 0x000B6694
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000D69 RID: 3433
			// (get) Token: 0x06002D9F RID: 11679 RVA: 0x000B84A4 File Offset: 0x000B66A4
			// (set) Token: 0x06002DA0 RID: 11680 RVA: 0x000B84AC File Offset: 0x000B66AC
			public long rankPos1
			{
				get
				{
					return this._rankPos1;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._rankPos1 = value;
				}
			}

			// Token: 0x17000D6A RID: 3434
			// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000B84C4 File Offset: 0x000B66C4
			public bool HasRankPos1
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000D6B RID: 3435
			// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x000B84D4 File Offset: 0x000B66D4
			// (set) Token: 0x06002DA3 RID: 11683 RVA: 0x000B84DC File Offset: 0x000B66DC
			public long rankPos2
			{
				get
				{
					return this._rankPos2;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._rankPos2 = value;
				}
			}

			// Token: 0x17000D6C RID: 3436
			// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x000B84F4 File Offset: 0x000B66F4
			public bool HasRankPos2
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000D6D RID: 3437
			// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x000B8504 File Offset: 0x000B6704
			// (set) Token: 0x06002DA6 RID: 11686 RVA: 0x000B850C File Offset: 0x000B670C
			public long bestRankPos
			{
				get
				{
					return this._bestRankPos;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._bestRankPos = value;
				}
			}

			// Token: 0x17000D6E RID: 3438
			// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x000B8524 File Offset: 0x000B6724
			public bool HasBestRankPos
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000D6F RID: 3439
			// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000B8534 File Offset: 0x000B6734
			// (set) Token: 0x06002DA9 RID: 11689 RVA: 0x000B853C File Offset: 0x000B673C
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._type = value;
				}
			}

			// Token: 0x17000D70 RID: 3440
			// (get) Token: 0x06002DAA RID: 11690 RVA: 0x000B8554 File Offset: 0x000B6754
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x06002DAB RID: 11691 RVA: 0x000B8564 File Offset: 0x000B6764
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.win = this.deserialize.read_boolean();
						break;
					case 1:
						this.items = this.deserialize.read_obj_list<item>();
						break;
					case 2:
						this.rankPos1 = this.deserialize.read_integer();
						break;
					case 3:
						this.rankPos2 = this.deserialize.read_integer();
						break;
					case 4:
						this.bestRankPos = this.deserialize.read_integer();
						break;
					case 5:
						this.type = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002DAC RID: 11692 RVA: 0x000B8644 File Offset: 0x000B6844
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_boolean(this.win, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<item>(this.items, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.rankPos1, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.rankPos2, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.bestRankPos, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.type, 5);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EF3 RID: 7923
			private static int max_field_count = 6;

			// Token: 0x04001EF4 RID: 7924
			private bool _win;

			// Token: 0x04001EF5 RID: 7925
			private List<item> _items;

			// Token: 0x04001EF6 RID: 7926
			private long _rankPos1;

			// Token: 0x04001EF7 RID: 7927
			private long _rankPos2;

			// Token: 0x04001EF8 RID: 7928
			private long _bestRankPos;

			// Token: 0x04001EF9 RID: 7929
			private long _type;
		}
	}
}
