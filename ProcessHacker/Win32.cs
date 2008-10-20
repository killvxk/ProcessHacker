﻿/*
 * Process Hacker
 * 
 * Copyright (C) 2008 wj32
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

// you won't get some of this stuff from anywhere else... :)

namespace ProcessHacker
{
    public class Win32
    {
        #region Imported Consts

        public const int DONT_RESOLVE_DLL_REFERENCES = 0x1;
        public const int ERROR_NO_MORE_ITEMS = 259;
        public const int PROCESS_DUP_HANDLE = 0x0040;
        public const int PROCESS_QUERY_INFORMATION = 0x0400;
        public const int PROCESS_VM_OPERATION = 0x0008;
        public const int PROCESS_VM_READ = 0x0010;
        public const int PROCESS_VM_WRITE = 0x0020;
        public const uint SE_PRIVILEGE_ENABLED = 0x00000002;
        public const uint SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;
        public const int SEE_MASK_INVOKEIDLIST = 0xc;
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;
        public const uint SHGFI_SMALLICON = 0x1;
        public const uint STATUS_INFO_LENGTH_MISMATCH = 0xc0000004;
        public const int SW_SHOW = 5;
        public const int THREAD_SUSPEND_RESUME = 0x0002;
        public const int THREAD_TERMINATE = 0x0001;
        public const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        public const int TOKEN_QUERY = 0x00000008;

        #endregion    

        #region Imported Enums   
  
        public enum DEPFLAGS : int
        {
            PROCESS_DEP_DISABLE = 0x00000000,
            PROCESS_DEP_ENABLE = 0x00000001,
            PROCESS_DEP_DISABLE_ATL_THUNK_EMULATION = 0x00000002
        }

        public enum HEAPENTRY32FLAGS : int
        {
            LF32_FIXED = 0x00000001,
            LF32_FREE = 0x00000002,
            LF32_MOVEABLE = 0x00000004
        }

        public enum MEMORY_PROTECTION : int
        {
            PAGE_ACCESS_DENIED = 0x0,
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400,
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08
        }

        public enum MEMORY_STATE : int
        {
            MEM_COMMIT = 0x1000,
            MEM_FREE = 0x10000,
            MEM_RESERVE = 0x2000
        }

        public enum MEMORY_TYPE : int
        {
            MEM_IMAGE = 0x1000000,
            MEM_MAPPED = 0x40000,
            MEM_PRIVATE = 0x20000
        }

        public enum OBJECT_INFORMATION_CLASS : int
        {
            ObjectBasicInformation,
            ObjectTypeInformation
        }

        public enum SnapshotFlags : uint
        {
            HeapList = 0x00000001,
            Process = 0x00000002,
            Thread = 0x00000004,
            Module = 0x00000008,
            Module32 = 0x00000010,
            Inherit = 0x80000000,
            All = 0x0000001F
        }

        public enum SYSTEM_HANDLE_FLAGS : byte
        {
            PROTECT_FROM_CLOSE = 0x1,
            INHERIT = 0x2
        }

        public enum SYSTEM_INFORMATION_CLASS : int
        {
            SystemBasicInformation,              
            SystemProcessorInformation,          
            SystemPerformanceInformation,        
            SystemTimeOfDayInformation,          
            SystemNotImplemented1,               
            SystemProcessesAndThreadsInformation,
            SystemCallCounts,                    
            SystemConfigurationInformation,      
            SystemProcessorTimes,                
            SystemGlobalFlag,                    
            SystemNotImplemented2,               
            SystemModuleInformation,             
            SystemLockInformation,               
            SystemNotImplemented3,               
            SystemNotImplemented4,               
            SystemNotImplemented5,               
            SystemHandleInformation,             
            SystemObjectInformation,             
            SystemPagefileInformation,           
            SystemInstructionEmulationCounts,    
            SystemInvalidInfoClass1,             
            SystemCacheInformation,              
            SystemPoolTagInformation,            
            SystemProcessorStatistics,           
            SystemDpcInformation,                
            SystemNotImplemented6,               
            SystemLoadImage,                     
            SystemUnloadImage,                   
            SystemTimeAdjustment,                
            SystemNotImplemented7,               
            SystemNotImplemented8,               
            SystemNotImplemented9,               
            SystemCrashDumpInformation,          
            SystemExceptionInformation,          
            SystemCrashDumpStateInformation,     
            SystemKernelDebuggerInformation,     
            SystemContextSwitchInformation,      
            SystemRegistryQuotaInformation,      
            SystemLoadAndCallImage,              
            SystemPrioritySeparation,            
            SystemNotImplemented10,              
            SystemNotImplemented11,              
            SystemInvalidInfoClass2,             
            SystemInvalidInfoClass3,             
            SystemTimeZoneInformation,           
            SystemLookasideInformation,          
            SystemSetTimeSlipEvent,              
            SystemCreateSession,                 
            SystemDeleteSession,                 
            SystemInvalidInfoClass4,             
            SystemRangeStartInformation,         
            SystemVerifierInformation,           
            SystemAddVerifier,                   
            SystemSessionProcessesInformation    
        }

        public enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId
        }

        #endregion

        #region Imported Functions

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int OpenProcessToken(int ProcessHandle, int DesiredAccess,
            ref int TokenHandle);

        [DllImport("advapi32", CharSet = CharSet.Auto)]
        public static extern int GetTokenInformation(
            int hToken,
            TOKEN_INFORMATION_CLASS tokenInfoClass,
            IntPtr TokenInformation,
            int tokeInfoLength,
            ref int reqLength
        );

        [DllImport("advapi32", CharSet = CharSet.Auto)]
        public static extern int ConvertSidToStringSid(
            int pSID,
            [In, Out, MarshalAs(UnmanagedType.LPTStr)] ref string pStringSid
        );

        [DllImport("advapi32", CharSet = CharSet.Auto)]
        public static extern int ConvertStringSidToSid(
            [In, MarshalAs(UnmanagedType.LPTStr)] string pStringSid,
            ref IntPtr pSID
        );

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int LookupPrivilegeValue(string SystemName, string PrivilegeName,
            [MarshalAs(UnmanagedType.Struct)] ref LUID Luid);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int AdjustTokenPrivileges(int TokenHandle, int DisableAllPrivileges,
            [MarshalAs(UnmanagedType.Struct)] ref TOKEN_PRIVILEGES NewState, int BufferLength,
            int PreviousState, int ReturnLength);

        [DllImport("kernel32.dll")]
        public static extern int GetProcessDEPPolicy(int ProcessHandle, ref DEPFLAGS Flags, ref int Permanent);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int CloseHandle(int Handle);

        [DllImport("kernel32.dll")]
        public static extern int TerminateProcess(int ProcessHandle, int ExitCode);

        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(int DesiredAccess, int InheritHandle, int ProcessId);

        [DllImport("kernel32.dll")]
        public static extern int OpenThread(int DesiredAccess, int InheritHandle, int ThreadId);

        [DllImport("kernel32.dll")]
        public static extern int TerminateThread(int ThreadHandle, int ExitCode);

        [DllImport("kernel32.dll")]
        public static extern int SuspendThread(int ThreadHandle);

        [DllImport("kernel32.dll")]
        public static extern int ResumeThread(int ThreadHandle);

        [DllImport("shell32.dll")]
        public static extern int ShellExecuteEx(
            [MarshalAs(UnmanagedType.Struct)] ref SHELLEXECUTEINFO s);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int LoadLibrary(string FileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int LoadLibraryEx(string FileName, int File, int Flags);

        [DllImport("kernel32.dll")]
        public static extern int FreeLibrary(int Handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetModuleHandle(string ModuleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetProcAddress(int Module, string ProcName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetProcAddress(int Module, int ProcOrdinal);

        [DllImport("kernel32.dll")]
        public static extern int VirtualQueryEx(int Process, int Address,
            [MarshalAs(UnmanagedType.Struct)] ref MEMORY_BASIC_INFORMATION Buffer, int Size);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int VirtualProtect(int Address, int Size, int NewProtect, ref int OldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int VirtualProtectEx(int Process, int Address, int Size, int NewProtect, ref int OldProtect);

        [DllImport("kernel32.dll")]
        public static extern int DebugActiveProcess(int PID);

        [DllImport("kernel32.dll")]
        public static extern int DebugActiveProcessStop(int PID);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int ReadProcessMemory(int Process, int BaseAddress, byte[] Buffer, int Size, ref int BytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int ReadProcessMemory(int Process, int BaseAddress, int Buffer, int Size, ref int BytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int WriteProcessMemory(int Process, int BaseAddress, byte[] Buffer, int Size, ref int BytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int CreateToolhelp32Snapshot(SnapshotFlags dwFlags, int th32ProcessID);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Process32First(int hSnapshot,
            [MarshalAs(UnmanagedType.Struct)] ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Process32Next(int hSnapshot,
            [MarshalAs(UnmanagedType.Struct)] ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Thread32First(int hSnapshot,
            [MarshalAs(UnmanagedType.Struct)] ref THREADENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Thread32Next(int hSnapshot,
            [MarshalAs(UnmanagedType.Struct)] ref THREADENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Module32First(int hSnapshot,
            [MarshalAs(UnmanagedType.Struct)] ref MODULEENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Module32Next(int hSnapshot,
            [MarshalAs(UnmanagedType.Struct)] ref MODULEENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Heap32ListFirst(int hSnapshot,
            [MarshalAs(UnmanagedType.Struct)] ref HEAPLIST32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Heap32ListNext(int hSnapshot,
            [MarshalAs(UnmanagedType.Struct)] ref HEAPLIST32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Heap32First([MarshalAs(UnmanagedType.Struct)] ref HEAPENTRY32 lppe,
            int ProcessID, int HeapID);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Heap32Next([MarshalAs(UnmanagedType.Struct)] ref HEAPENTRY32 lppe);

        [DllImport("kernel32.dll")]
        public static extern bool GetThreadTimes(int hThread, out long lpCreationTime,
           out long lpExitTime, out long lpKernelTime, out long lpUserTime);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int DuplicateHandle(int hSourceProcessHandle,
           int hSourceHandle, int hTargetProcessHandle, ref int lpTargetHandle,
           uint dwDesiredAccess, int bInheritHandle, uint dwOptions);

        [DllImport("kernel32.dll")]
        public static extern int SearchPath(
            int Zero0, [MarshalAs(UnmanagedType.LPTStr)] string FileName,
            int Zero1, int BufferLength,
            [Out] System.Text.StringBuilder Buffer, int Zero2); 

        [DllImport("ntdll.dll")]
        public static extern uint ZwDuplicateObject(int SourceProcessHandle, int SourceHandle,
            int TargetProcessHandle, ref int TargetHandle, int DesiredAccess, int Attributes, int Options);

        [DllImport("ntdll.dll")]
        public static extern uint ZwQuerySystemInformation(SYSTEM_INFORMATION_CLASS SystemInformationClass,
            SYSTEM_PROCESS_INFORMATION SystemInformation, int SystemInformationLength, ref int ReturnLength);

        [DllImport("ntdll.dll")]
        public static extern uint ZwQuerySystemInformation(SYSTEM_INFORMATION_CLASS SystemInformationClass,
            SYSTEM_HANDLE_INFORMATION SystemInformation, int SystemInformationLength, ref int ReturnLength);

        [DllImport("ntdll.dll")]
        public static extern uint ZwQuerySystemInformation(SYSTEM_INFORMATION_CLASS SystemInformationClass,
            ulong[] dummy, int SystemInformationLength, ref int ReturnLength);

        [DllImport("ntdll.dll")]
        public static extern uint NtQueryObject(int Handle, OBJECT_INFORMATION_CLASS ObjectInformationClass,
            ref OBJECT_BASIC_INFORMATION ObjectInformation, int ObjectInformationLength, ref int ReturnLength);

        [DllImport("ntdll.dll")]
        public static extern uint NtQueryObject(int Handle, OBJECT_INFORMATION_CLASS ObjectInformationClass,
            ref OBJECT_TYPE_INFORMATION ObjectInformation, int ObjectInformationLength, ref int ReturnLength);

        [DllImport("psapi.dll", SetLastError = true)]
        public static extern int EnumDeviceDrivers(int[] ImageBases, int Size, ref int Needed);

        [DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetDeviceDriverBaseName(int ImageBase,
            [Out] System.Text.StringBuilder FileName, int Size);

        [DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetDeviceDriverFileName(int ImageBase, 
            [Out] System.Text.StringBuilder FileName, int Size);

        [DllImport("user32.dll")]
        public static extern int EnumWindows([MarshalAs(UnmanagedType.FunctionPtr)] EnumWindowsProc Callback, int param);

        public delegate int EnumWindowsProc(int hwnd, int param);

        [DllImport("user32.dll")]
        public static extern int SetActiveWindow(int hWnd); 

        [DllImport("shell32.dll")]
        public extern static int ExtractIconEx(string libName, int iconIndex,
        IntPtr[] largeIcon, IntPtr[] smallIcon, int nIcons);

        [DllImport("shell32.dll")]
        public static extern int SHGetFileInfo(string pszPath,
                                    uint dwFileAttributes,
                                    ref SHFILEINFO psfi,
                                    uint cbSizeFileInfo,
                                    uint uFlags);

        #endregion

        #region Imported Structs

        [StructLayout(LayoutKind.Sequential)]
        public struct HEAPENTRY32
        {
            public int dwSize;
            public int hHandle;
            public int dwAddress;
            public int dwBlockSize;
            public HEAPENTRY32FLAGS dwFlags;
            public int dwLockCount;
            public int dwResvd;
            public int th32ProcessID;
            public int th32HeapID;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HEAPLIST32
        {
            public int dwSize;
            public int th32ProcessID;
            public int th32HeapID;
            public int dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID
        {
            public int LowPart;
            public int HighPart;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public MEMORY_PROTECTION AllocationProtect;
            public int RegionSize;
            public MEMORY_STATE State;
            public MEMORY_PROTECTION Protect;
            public MEMORY_TYPE Type;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MODULEENTRY32
        {
            public int dwSize;
            public int th32ModuleID;
            public int th32ProcessID;
            public int GlblcntUsage;
            public int ProccntUsage;
            public int modBaseAddr;
            public int modBaseSize;
            public int hModule;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szModule;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExePath;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OBJECT_BASIC_INFORMATION
        {
            public int Attributes;
            public int GrantedAccess;
            public int HandleCount;
            public int PointerCount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public int[] Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OBJECT_TYPE_INFORMATION
        {
            [MarshalAs(UnmanagedType.Struct)]
            public UNICODE_STRING TypeName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
            public int[] Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESSENTRY32
        {
            public int dwSize;
            public int cntUsage;
            public int th32ProcessID;
            public int th32DefaultHeapID;
            public int th32ModuleID;
            public int cntThreads;
            public int th32ParentProcessID;
            public int pcPriClassBase;
            public int dwFlags;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHELLEXECUTEINFO
        {
            public int cbSize;
            public int fMask;
            public int hWnd;
            public string lpVerb;
            public string lpFile;
            public string lpParameters;
            public string lpDirectory;
            public int nShow;
            public int hInstApp;

            public int lpIDList;
            public string lpClass;
            public int hkeyClass;
            public int dwHotKey;
            public int hIcon;
            public int hProcess;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }    

        [StructLayout(LayoutKind.Sequential)]
        public struct SID_AND_ATTRIBUTES
        {
            public int SID; // ptr to a SID object
            public int Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_HANDLE_INFORMATION
        {
            int ProcessId;
            byte ObjectTypeNumber;
            SYSTEM_HANDLE_FLAGS Flags;
            short Handle;
            int Object;
            int GrantedAccess;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_PROCESS_INFORMATION
        {
            public uint NextEntryOffset;
            public uint NumberOfThreads;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            public byte[] Reserved1;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public int[] Reserved2;

            public int UniqueProcessId;

            public int Reserved3;
            public uint HandleCount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Reserved4;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public int[] Reserved5;

            public uint PeakPagefileUsage;
            public uint PrivatePageCount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public long[] Reserved6;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct THREADENTRY32
        {
            public int dwSize;
            public int cntUsage;
            public int th32ThreadID;
            public int th32OwnerProcessID;
            public int tpBasePri;
            public int tpDeltaPri;
            public int dwFlags;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TOKEN_PRIVILEGES
        {
            public UInt32 PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }  

        [StructLayout(LayoutKind.Sequential)]
        public struct TOKEN_USER
        {
            public SID_AND_ATTRIBUTES User;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct UNICODE_STRING
        {
            public ushort Length;
            public ushort MaximumLength;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string Buffer;
        }

        #endregion

        public static string GetNameFromPID(int pid)
        {
            PROCESSENTRY32 proc = new PROCESSENTRY32();
            int snapshot = 0;

            snapshot = CreateToolhelp32Snapshot(SnapshotFlags.Process, pid);

            if (snapshot == 0)
                return "(error)";

            proc.dwSize = Marshal.SizeOf(typeof(PROCESSENTRY32));

            Process32First(snapshot, ref proc);

            do
            {
                if (proc.th32ProcessID == pid)
                    return proc.szExeFile;
            } while (Process32Next(snapshot, ref proc) != 0);

            return "(unknown)";
        }

        public static int EnableTokenPrivilege(string Privilege)
        {
            int token = 0;
            TOKEN_PRIVILEGES tkp = new TOKEN_PRIVILEGES();

            tkp.Privileges = new LUID_AND_ATTRIBUTES[1];

            if (OpenProcessToken(Process.GetCurrentProcess().Handle.ToInt32(),
                 TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref token) == 0)
                return 0;

            if (LookupPrivilegeValue(null, Privilege, ref tkp.Privileges[0].Luid) == 0)
                return 0;

            tkp.PrivilegeCount = 1;
            tkp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;

            AdjustTokenPrivileges(token, 0, ref tkp, 0, 0, 0);

            if (Marshal.GetLastWin32Error() != 0)
                return 0;

            return 1;
        }
    }
}
