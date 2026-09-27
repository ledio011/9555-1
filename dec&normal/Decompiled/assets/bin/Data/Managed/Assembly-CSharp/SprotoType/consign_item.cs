using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200035E RID: 862
	public class consign_item : SprotoTypeBase
	{
		// Token: 0x06001989 RID: 6537 RVA: 0x000903B8 File Offset: 0x0008E5B8
		public consign_item() : base(consign_item.max_field_count)
		{
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000903C8 File Offset: 0x0008E5C8
		public consign_item(byte[] buffer) : base(consign_item.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x000903E4 File Offset: 0x0008E5E4
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x000903EC File Offset: 0x0008E5EC
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

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x00090404 File Offset: 0x0008E604
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x00090414 File Offset: 0x0008E614
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x0009041C File Offset: 0x0008E61C
		public long characterId
		{
			get
			{
				return this._characterId;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._characterId = value;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00090434 File Offset: 0x0008E634
		public bool HasCharacterId
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x00090444 File Offset: 0x0008E644
		// (set) Token: 0x06001993 RID: 6547 RVA: 0x0009044C File Offset: 0x0008E64C
		public string itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._itemId = value;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00090464 File Offset: 0x0008E664
		public bool HasItemId
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x00090474 File Offset: 0x0008E674
		// (set) Token: 0x06001996 RID: 6550 RVA: 0x0009047C File Offset: 0x0008E67C
		public long quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._quality = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x00090494 File Offset: 0x0008E694
		public bool HasQuality
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x000904A4 File Offset: 0x0008E6A4
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x000904AC File Offset: 0x0008E6AC
		public long stack
		{
			get
			{
				return this._stack;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._stack = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x000904C4 File Offset: 0x0008E6C4
		public bool HasStack
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x000904D4 File Offset: 0x0008E6D4
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x000904DC File Offset: 0x0008E6DC
		public long price
		{
			get
			{
				return this._price;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._price = value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x000904F4 File Offset: 0x0008E6F4
		public bool HasPrice
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x00090504 File Offset: 0x0008E704
		// (set) Token: 0x0600199F RID: 6559 RVA: 0x0009050C File Offset: 0x0008E70C
		public long time
		{
			get
			{
				return this._time;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._time = value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00090524 File Offset: 0x0008E724
		public bool HasTime
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00090534 File Offset: 0x0008E734
		// (set) Token: 0x060019A2 RID: 6562 RVA: 0x0009053C File Offset: 0x0008E73C
		public long startTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._startTime = value;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00090554 File Offset: 0x0008E754
		public bool HasStartTime
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00090564 File Offset: 0x0008E764
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_integer();
					break;
				case 1:
					this.characterId = this.deserialize.read_integer();
					break;
				case 2:
					this.itemId = this.deserialize.read_string();
					break;
				case 3:
					this.quality = this.deserialize.read_integer();
					break;
				case 4:
					this.stack = this.deserialize.read_integer();
					break;
				case 5:
					this.price = this.deserialize.read_integer();
					break;
				case 6:
					this.time = this.deserialize.read_integer();
					break;
				case 7:
					this.startTime = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00090678 File Offset: 0x0008E878
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.characterId, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_string(this.itemId, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.quality, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.stack, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.price, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.time, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.startTime, 7);
			}
			return this.serialize.close();
		}

		// Token: 0x0400198A RID: 6538
		private static int max_field_count = 8;

		// Token: 0x0400198B RID: 6539
		private long _id;

		// Token: 0x0400198C RID: 6540
		private long _characterId;

		// Token: 0x0400198D RID: 6541
		private string _itemId;

		// Token: 0x0400198E RID: 6542
		private long _quality;

		// Token: 0x0400198F RID: 6543
		private long _stack;

		// Token: 0x04001990 RID: 6544
		private long _price;

		// Token: 0x04001991 RID: 6545
		private long _time;

		// Token: 0x04001992 RID: 6546
		private long _startTime;
	}
}
