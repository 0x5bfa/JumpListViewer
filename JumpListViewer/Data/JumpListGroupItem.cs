// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace JumpListViewer.Data
{
	public class JumpListGroupItem : List<BaseJumpListItem>
	{
		public required string Key { get; set; }
	}
}
