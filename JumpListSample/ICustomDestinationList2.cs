// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace JumpListSample
{
	// https://github.com/GigabyteProductions/classicshell/blob/HEAD/src/ClassicStartMenu/ClassicStartMenuDLL/JumpLists.cpp#L397
	public unsafe partial struct ICustomDestinationList2
	{
#pragma warning disable CS0649 // Field 'field' is never assigned to, and will always have its default value 'value'
		private void** lpVtbl;
#pragma warning restore CS0649 // Field 'field' is never assigned to, and will always have its default value 'value'

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT SetMinItems(uint dwMinItems)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<ICustomDestinationList2*, int>)lpVtbl[3])((ICustomDestinationList2*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT SetApplicationID(PWSTR pszAppID)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<ICustomDestinationList2*, int>)lpVtbl[3])((ICustomDestinationList2*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetSlotCount(uint* pdwSlotCount)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<ICustomDestinationList2*, int>)lpVtbl[3])((ICustomDestinationList2*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetCategoryCount(uint* pdwCategoryCount)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<ICustomDestinationList2*, int>)lpVtbl[3])((ICustomDestinationList2*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetCategory(uint a1, GETCATFLAG dwFlags, APPDESTCATEGORY* pADC)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<ICustomDestinationList2*, int>)lpVtbl[3])((ICustomDestinationList2*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT DeleteCategory(uint a1, int a2)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<ICustomDestinationList2*, int>)lpVtbl[3])((ICustomDestinationList2*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT EnumerateCategoryDestinations(uint a1, Guid* a2, void** ppvObject)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<ICustomDestinationList2*, int>)lpVtbl[3])((ICustomDestinationList2*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT RemoveDestination(IUnknown* a1)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<ICustomDestinationList2*, int>)lpVtbl[3])((ICustomDestinationList2*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT HasListEx(int* a1, int* a2)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<ICustomDestinationList2*, int>)lpVtbl[3])((ICustomDestinationList2*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT ClearRemovedDestinations()
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<ICustomDestinationList2*, int>)lpVtbl[3])((ICustomDestinationList2*)Unsafe.AsPointer(ref this));
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct APPDESTCATEGORY
	{
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		public struct _Anonymous_e__Union
		{
			[FieldOffset(0)]
			public PWSTR Name;

			[FieldOffset(0)]
			public int SubType;
		}

		public int Type;

		public _Anonymous_e__Union Anonymous;

		public int count;

		public fixed int pad[10];
	}

	public enum GETCATFLAG : uint
	{
		// 1 is the only valid value?
		DEFAULT = 1,
	}
}
