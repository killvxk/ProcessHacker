#ifndef _PH_EMENU_H
#define _PH_EMENU_H

#define PH_EMENU_DISABLED 0x1
#define PH_EMENU_CHECKED 0x2
#define PH_EMENU_HIGHLIGHT 0x4
#define PH_EMENU_MENUBARBREAK 0x8
#define PH_EMENU_MENUBREAK 0x10
#define PH_EMENU_DEFAULT 0x20
#define PH_EMENU_MOUSESELECT 0x40
#define PH_EMENU_RADIOCHECK 0x80

#define PH_EMENU_SEPARATECHECKSPACE 0x100000
#define PH_EMENU_SEPARATOR 0x200000

#define PH_EMENU_TEXT_OWNED 0x80000000
#define PH_EMENU_BITMAP_OWNED 0x40000000

struct _PH_EMENU_ITEM;

typedef VOID (NTAPI *PPH_EMENU_ITEM_DELETE_FUNCTION)(
    _In_ struct _PH_EMENU_ITEM *Item
    );

typedef struct _PH_EMENU_ITEM
{
    ULONG Flags;
    ULONG Id;
    PWSTR Text;
    HBITMAP Bitmap;

    PVOID Parameter;
    PVOID Context;
    PPH_EMENU_ITEM_DELETE_FUNCTION DeleteFunction;
    PVOID Reserved;

    struct _PH_EMENU_ITEM *Parent;
    PPH_LIST Items;
} PH_EMENU_ITEM, *PPH_EMENU_ITEM;

typedef struct _PH_EMENU_ITEM PH_EMENU, *PPH_EMENU;

PHLIBAPI
PPH_EMENU_ITEM PhCreateEMenuItem(
    _In_ ULONG Flags,
    _In_ ULONG Id,
    _In_ PWSTR Text,
    _In_opt_ HBITMAP Bitmap,
    _In_opt_ PVOID Context
    );

PHLIBAPI
VOID PhDestroyEMenuItem(
    _In_ PPH_EMENU_ITEM Item
    );

#define PH_EMENU_FIND_DESCEND 0x1
#define PH_EMENU_FIND_STARTSWITH 0x2
#define PH_EMENU_FIND_LITERAL 0x4

PHLIBAPI
PPH_EMENU_ITEM PhFindEMenuItem(
    _In_ PPH_EMENU_ITEM Item,
    _In_ ULONG Flags,
    _In_opt_ PWSTR Text,
    _In_opt_ ULONG Id
    );

PPH_EMENU_ITEM PhFindEMenuItemEx(
    _In_ PPH_EMENU_ITEM Item,
    _In_ ULONG Flags,
    _In_opt_ PWSTR Text,
    _In_opt_ ULONG Id,
    _Out_opt_ PPH_EMENU_ITEM *FoundParent,
    _Out_opt_ PULONG FoundIndex
    );

PHLIBAPI
ULONG PhIndexOfEMenuItem(
    _In_ PPH_EMENU_ITEM Parent,
    _In_ PPH_EMENU_ITEM Item
    );

PHLIBAPI
VOID PhInsertEMenuItem(
    _Inout_ PPH_EMENU_ITEM Parent,
    _Inout_ PPH_EMENU_ITEM Item,
    _In_ ULONG Index
    );

PHLIBAPI
BOOLEAN PhRemoveEMenuItem(
    _Inout_opt_ PPH_EMENU_ITEM Parent,
    _In_opt_ PPH_EMENU_ITEM Item,
    _In_opt_ ULONG Index
    );

PHLIBAPI
VOID PhRemoveAllEMenuItems(
    _Inout_ PPH_EMENU_ITEM Parent
    );

PHLIBAPI
PPH_EMENU PhCreateEMenu(
    VOID
    );

PHLIBAPI
VOID PhDestroyEMenu(
    _In_ PPH_EMENU Menu
    );

#define PH_EMENU_CONVERT_ID 0x1

typedef struct _PH_EMENU_DATA
{
    PPH_LIST IdToItem;
} PH_EMENU_DATA, *PPH_EMENU_DATA;

VOID PhInitializeEMenuData(
    _Out_ PPH_EMENU_DATA Data
    );

VOID PhDeleteEMenuData(
    _Inout_ PPH_EMENU_DATA Data
    );

HMENU PhEMenuToHMenu(
    _In_ PPH_EMENU_ITEM Menu,
    _In_ ULONG Flags,
    _Inout_opt_ PPH_EMENU_DATA Data
    );

VOID PhEMenuToHMenu2(
    _In_ HMENU MenuHandle,
    _In_ PPH_EMENU_ITEM Menu,
    _In_ ULONG Flags,
    _Inout_opt_ PPH_EMENU_DATA Data
    );

VOID PhHMenuToEMenuItem(
    _Inout_ PPH_EMENU_ITEM MenuItem,
    _In_ HMENU MenuHandle
    );

PHLIBAPI
VOID PhLoadResourceEMenuItem(
    _Inout_ PPH_EMENU_ITEM MenuItem,
    _In_ HINSTANCE InstanceHandle,
    _In_ PWSTR Resource,
    _In_ ULONG SubMenuIndex
    );

#define PH_EMENU_SHOW_SEND_COMMAND 0x1
#define PH_EMENU_SHOW_LEFTRIGHT 0x2

PHLIBAPI
PPH_EMENU_ITEM PhShowEMenu(
    _In_ PPH_EMENU Menu,
    _In_ HWND WindowHandle,
    _In_ ULONG Flags,
    _In_ ULONG Align,
    _In_ ULONG X,
    _In_ ULONG Y
    );

// Convenience functions

PHLIBAPI
BOOLEAN PhSetFlagsEMenuItem(
    _Inout_ PPH_EMENU_ITEM Item,
    _In_ ULONG Id,
    _In_ ULONG Mask,
    _In_ ULONG Value
    );

FORCEINLINE BOOLEAN PhEnableEMenuItem(
    _Inout_ PPH_EMENU_ITEM Item,
    _In_ ULONG Id,
    _In_ BOOLEAN Enable
    )
{
    return PhSetFlagsEMenuItem(Item, Id, PH_EMENU_DISABLED, Enable ? 0 : PH_EMENU_DISABLED);
}

PHLIBAPI
VOID PhSetFlagsAllEMenuItems(
    _In_ PPH_EMENU_ITEM Item,
    _In_ ULONG Mask,
    _In_ ULONG Value
    );

#define PH_EMENU_MODIFY_TEXT 0x1
#define PH_EMENU_MODIFY_BITMAP 0x2

PHLIBAPI
VOID PhModifyEMenuItem(
    _Inout_ PPH_EMENU_ITEM Item,
    _In_ ULONG ModifyFlags,
    _In_ ULONG OwnedFlags,
    _In_opt_ PWSTR Text,
    _In_opt_ HBITMAP Bitmap
    );

#endif
