// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace JumpListSample
{
	public partial class JumpListViewModel : ObservableObject
	{
		public ObservableCollection<BaseJumpListItem> JumpListItems { get; } = [];

		public string? AppId
		{
			get => field;
			set
			{
				if (SetProperty(ref field, value))
				{
					OnAppIdChanged(value);
				}
			}
		}

		//public ICommand RefreshJumpListCommand;

		public JumpListViewModel()
		{
			//RefreshJumpListCommand = new RelayCommand(ExecuteRefreshJumpListCommand);
		}

		private void OnAppIdChanged(string? value)
		{
			JumpListItems.Clear();

			if (string.IsNullOrEmpty(AppId) || JumpListManager.Initialize(AppId) is not { } manager)
				return;

			if (manager.HasListOf(DESTLISTTYPE.PINNED))
			{
				JumpListItems.Add(new JumpListSectionItem() { Text = "Pinned" });
				foreach (var item in manager.EnumerateAutomaticDestinations(DESTLISTTYPE.PINNED))
				{
					JumpListItems.Add(item);
				}
			}
			if (manager.HasListOf(DESTLISTTYPE.RECENT))
			{
				JumpListItems.Add(new JumpListSectionItem() { Text = "Recent" });
				foreach (var item in manager.EnumerateAutomaticDestinations(DESTLISTTYPE.RECENT))
				{
					if (JumpListItems.Where(x => x.IsPinned).Where(x => x.Text == item.Text).Any())
						continue;
					JumpListItems.Add(item);
				}
			}

			manager.EnumerateCustomDestinations();

			manager.Dispose();
		}
	}
}
