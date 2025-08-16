// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License.

using CommunityToolkit.Mvvm.ComponentModel;
using JumpListViewer.Data;
using JumpListViewer.Utilities;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Security;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;
using Windows.Win32.System.SystemServices;
using Windows.Win32.UI.Shell;
using Windows.Win32.UI.Shell.PropertiesSystem;

namespace JumpListViewer.ViewModels
{
	public unsafe class MainPageViewModel : ObservableObject
	{
		public ObservableCollection<ApplicationItem> ApplicationItems { get; set; } = [];

		private ObservableCollection<JumpListGroupItem> GroupedJumpListItems { get; set; } = [];

		public CollectionViewSource? JumpListItems { get => field; set => SetProperty(ref field, value); }

		public int SelectedIndexOfApplicationItems { get; set; } = 0;

		public MainPageViewModel()
		{
			EnumerateApplicationItems();
		}

		public void EnumerateApplicationItems()
		{
			ApplicationItems.Clear();

			HRESULT hr = default;

			// Get the shell folder item
			using ComPtr<IShellItem> pShellItem = default;
			fixed (char* pwszShellAppsFolderPath = "Shell:AppsFolder")
				hr = PInvoke.SHCreateItemFromParsingName(pwszShellAppsFolderPath, null, IID.IID_IShellItem, (void**)pShellItem.GetAddressOf());

			// Get the enumerator of the shell folder
			using ComPtr<IEnumShellItems> pEnumShellItems = default;
			hr = pShellItem.Get()->BindToHandler(null, BHID.BHID_EnumItems, IID.IID_IEnumShellItems, (void**)pEnumShellItems.GetAddressOf());

			// Enumerate all child items one by one
			ComPtr<IShellItem> pChildShellItem = default;
			while (pEnumShellItems.Get()->Next(1, pChildShellItem.GetAddressOf()) == HRESULT.S_OK)
			{
				// Get the application name
				using ComHeapPtr<char> pName = default;
				hr = pChildShellItem.Get()->GetDisplayName(SIGDN.SIGDN_PARENTRELATIVEFORUI, (PWSTR*)pName.GetAddressOf());

				// Get the AMUID from the property store of the item
				ComPtr<IPropertyStore> pPropertyStore = default;
				PROPERTYKEY pKey = PInvoke.PKEY_AppUserModel_ID;
				PROPVARIANT pVar = default;
				hr = pChildShellItem.Get()->BindToHandler(null, BHID.BHID_PropertyStore, IID.IID_IPropertyStore, (void**)pPropertyStore.GetAddressOf());
				hr = pPropertyStore.Get()->GetValue(&pKey, &pVar);

				// Get the thumbnail
				var bitmapImage = ThumbnailHelper.GetThumbnail(pChildShellItem.Get())?.ToBitmap();

				// Insert the new item
				ApplicationItems.Add(new() { Icon = bitmapImage, Name = new(pName.Get()), AppUserModelID = new(pVar.Anonymous.Anonymous.Anonymous.pwszVal) });

				// Dispose the unmanaged memory
				PInvoke.CoTaskMemFree(pVar.Anonymous.Anonymous.Anonymous.pwszVal);
				pChildShellItem.Dispose();
			}
		}

		public void EnumerateJumpListItems()
		{
			JumpListItems = new()
			{
				IsSourceGrouped = true,
			};

			GroupedJumpListItems.Clear();

			var amuid = ApplicationItems.ElementAt(SelectedIndexOfApplicationItems).AppUserModelID;

			if (JumpListManager.Create(amuid) is not { } manager)
				throw new InvalidOperationException($"Failed to initialize {nameof(JumpListManager)}.");

			InsertAutomaticDestinationItems(manager);
			InsertCustomDestinationItems(manager);
			InsertTaskItems(manager);

			JumpListItems.Source = new ObservableCollection<JumpListGroupItem>(GroupedJumpListItems);

			manager.Dispose();
		}

		private void InsertAutomaticDestinationItems(JumpListManager manager)
		{
			var pinnedItems = new JumpListGroupItem() { Key = "Pinned" };

			if (manager.HasAutomaticDestinationsOf(DESTLISTTYPE.PINNED))
				foreach (var item in manager.EnumerateAutomaticDestinations(DESTLISTTYPE.PINNED))
					pinnedItems.Add(item);

			if (pinnedItems.Count is not 0)
				GroupedJumpListItems.Add(pinnedItems);

			var recentItems = new JumpListGroupItem() { Key = "Recent" };

			if (manager.HasAutomaticDestinationsOf(DESTLISTTYPE.RECENT))
			{
				foreach (var item in manager.EnumerateAutomaticDestinations(DESTLISTTYPE.RECENT))
				{
					if (recentItems.OfType<JumpListItem>().Where(x => x.IsPinned).Where(x => x.Text == item.Text).Any())
						continue;
					recentItems.Add(item);
				}
			}

			if (recentItems.Count is not 0)
				GroupedJumpListItems.Add(recentItems);
		}

		private void InsertCustomDestinationItems(JumpListManager manager)
		{
			foreach (var item in manager.EnumerateCustomDestinations())
			{
				GroupedJumpListItems.Add(item);
			}
		}

		private void InsertTaskItems(JumpListManager manager)
		{
			var taskItems = new JumpListGroupItem() { Key = "Tasks" };

			foreach (var item in manager.EnumerateTasks())
				taskItems.Add(item);

			if (taskItems.Count is not 0)
				GroupedJumpListItems.Add(taskItems);
		}
	}
}
