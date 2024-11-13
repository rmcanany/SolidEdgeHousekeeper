Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Reflection

Public Class ListViewCollapsible
    Inherits ListView
    Private Const LVM_FIRST As Integer = &H1000
    ' ListView messages
    Private Const LVM_SETGROUPINFO As Integer = (LVM_FIRST + 147)
    ' ListView messages Setinfo on Group
    Private Const WM_LBUTTONUP As Integer = &H202

    ' Aggiunto per minimizare Flickering, su Win10 sembra essere ok anche senza.
    Public Sub New()
        MyBase.New()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or
                        ControlStyles.AllPaintingInWmPaint Or
                        ControlStyles.EnableNotifyMessage, True)

    End Sub

    ' Windows message left button
    Private Delegate Sub CallBackSetGroupState(lstvwgrp As ListViewGroup, state As ListViewGroupState)
    Private Delegate Sub CallbackSetGroupString(lstvwgrp As ListViewGroup, value As String)

    ''' <summary>
    ''' Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message. 
    ''' To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function. To post a message to a thread's message queue and return immediately, use the PostMessage or PostThreadMessage function.
    ''' </summary>
    ''' <param name="hWnd">
    ''' [in] Handle to the window whose window procedure will receive the message. 
    ''' If this parameter is HWND_BROADCAST, the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child windows. 
    ''' Microsoft Windows Vista and later. Message sending is subject to User Interface Privilege Isolation (UIPI). The thread of a process can send messages only to message queues of threads in processes of lesser or equal integrity level.
    ''' </param>
    ''' <param name="Msg">[in] Specifies the message to be sent.</param>
    ''' <param name="wParam">[in] Specifies additional message-specific information.</param>
    ''' <param name="lParam">[in] Type of LVGROUP, Specifies additional message-specific information.</param>
    ''' <returns>
    ''' Microsoft Windows Vista and later. When a message is blocked by UIPI the last error, retrieved with GetLastError, is set to 5 (access denied).
    ''' Applications that need to communicate using HWND_BROADCAST should use the RegisterWindowMessage function to obtain a unique message for inter-application communication.
    ''' The system only does marshalling for system messages (those in the range 0 to (WM_USER-1)). To send other messages (those >= WM_USER) to another process, you must do custom marshalling.
    ''' If the specified window was created by the calling thread, the window procedure is called immediately as a subroutine. If the specified window was created by a different thread, the system switches to that thread and calls the appropriate window procedure. Messages sent between threads are processed only when the receiving thread executes message retrieval code. The sending thread is blocked until the receiving thread processes the message. However, the sending thread will process incoming nonqueued messages while waiting for its message to be processed. To prevent this, use SendMessageTimeout with SMTO_BLOCK set. For more information on nonqueued messages, see Nonqueued Messages.
    ''' Windows 95/98/Me: SendMessageW is supported by the Microsoft Layer for Unicode (MSLU). To use this, you must add certain files to your application, as outlined in Microsoft Layer for Unicode on Windows 95/98/Me Systems.
    ''' </returns>
    <DllImport("User32.dll"), Description("Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message. To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function. To post a message to a thread's message queue and return immediately, use the PostMessage or PostThreadMessage function.")>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As Integer, lParam As IntPtr) As Integer
    End Function

    Private Shared Function GetGroupID(lstvwgrp As ListViewGroup) As System.Nullable(Of Integer)
        Dim rtnval As System.Nullable(Of Integer) = Nothing
        Dim GrpTp As Type = lstvwgrp.[GetType]()
        If GrpTp IsNot Nothing Then
            Dim pi As PropertyInfo = GrpTp.GetProperty("ID", BindingFlags.NonPublic Or BindingFlags.Instance)
            If pi IsNot Nothing Then
                Dim tmprtnval As Object = pi.GetValue(lstvwgrp, Nothing)
                If tmprtnval IsNot Nothing Then
                    rtnval = CInt(tmprtnval)
                End If
            End If
        End If
        Return rtnval
    End Function

    Private Shared Sub setGrpState(lstvwgrp As ListViewGroup, state As ListViewGroupState)
        If Environment.OSVersion.Version.Major < 6 Then
            'Only Vista and forward allows collaps of ListViewGroups
            Return
        End If
        If lstvwgrp Is Nothing OrElse lstvwgrp.ListView Is Nothing Then
            Return
        End If
        If lstvwgrp.ListView.InvokeRequired Then
            lstvwgrp.ListView.Invoke(New CallBackSetGroupState(AddressOf setGrpState), lstvwgrp, state)
        Else
            Dim GrpId As System.Nullable(Of Integer) = GetGroupID(lstvwgrp)
            Dim gIndex As Integer = lstvwgrp.ListView.Groups.IndexOf(lstvwgrp)
            Dim group As New LVGROUP
            group.CbSize = Marshal.SizeOf(group)
            group.State = state
            group.Mask = ListViewGroupMask.State
            Dim ip As IntPtr = IntPtr.Zero
            Try
                If GrpId IsNot Nothing Then
                    group.IGroupId = GrpId.Value
                    ip = Marshal.AllocHGlobal(group.CbSize)
                    Marshal.StructureToPtr(group, ip, False)

                    SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, GrpId.Value, ip)
                    SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, GrpId.Value, ip)
                Else
                    group.IGroupId = gIndex

                    ip = Marshal.AllocHGlobal(group.CbSize)
                    Marshal.StructureToPtr(group, ip, False)

                    SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, gIndex, ip)
                    SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, gIndex, ip)
                End If
                lstvwgrp.ListView.Refresh()
            Finally
                If (ip <> IntPtr.Zero) Then Marshal.FreeHGlobal(ip)
            End Try
        End If
    End Sub

    Private Shared Sub setGrpFooter(lstvwgrp As ListViewGroup, footer As String)
        If Environment.OSVersion.Version.Major < 6 Then
            'Only Vista and forward allows footer on ListViewGroups
            Return
        End If
        If lstvwgrp Is Nothing OrElse lstvwgrp.ListView Is Nothing Then
            Return
        End If
        If lstvwgrp.ListView.InvokeRequired Then
            lstvwgrp.ListView.Invoke(New CallbackSetGroupString(AddressOf setGrpFooter), lstvwgrp, footer)
        Else
            Dim GrpId As System.Nullable(Of Integer) = GetGroupID(lstvwgrp)
            Dim gIndex As Integer = lstvwgrp.ListView.Groups.IndexOf(lstvwgrp)
            Dim group As New LVGROUP
            group.CbSize = Marshal.SizeOf(group)
            group.PszFooter = footer
            group.Mask = ListViewGroupMask.Footer
            Dim ip As IntPtr = IntPtr.Zero
            Try
                If GrpId IsNot Nothing Then
                    group.IGroupId = GrpId.Value
                    ip = Marshal.AllocHGlobal(group.CbSize)
                    Marshal.StructureToPtr(group, ip, False)
                    SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, GrpId.Value, ip)
                Else
                    group.IGroupId = gIndex
                    ip = Marshal.AllocHGlobal(group.CbSize)
                    Marshal.StructureToPtr(group, ip, False)
                    SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, gIndex, ip)
                End If
            Finally
                If (ip <> IntPtr.Zero) Then Marshal.FreeHGlobal(ip)
            End Try
        End If
    End Sub

    Public Sub SetGroupState(state As ListViewGroupState)
        For Each lvg As ListViewGroup In Me.Groups
            setGrpState(lvg, state)
        Next
    End Sub

    Public Sub SetGroupState(state As ListViewGroupState, lvg As ListViewGroup)
        setGrpState(lvg, state)
    End Sub

    Public Sub SetGroupFooter(lvg As ListViewGroup, footerText As String)
        setGrpFooter(lvg, footerText)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_LBUTTONUP Then
            MyBase.DefWndProc(m)
        End If
        MyBase.WndProc(m)
    End Sub
End Class


''' <summary>
''' LVGROUP StructureUsed to set and retrieve groups.
''' </summary>
''' <example>
''' LVGROUP myLVGROUP = new LVGROUP();
''' myLVGROUP.CbSize    // is of managed type uint
''' myLVGROUP.Mask  // is of managed type uint
''' myLVGROUP.PszHeader // is of managed type string
''' myLVGROUP.CchHeader // is of managed type int
''' myLVGROUP.PszFooter // is of managed type string
''' myLVGROUP.CchFooter // is of managed type int
''' myLVGROUP.IGroupId  // is of managed type int
''' myLVGROUP.StateMask // is of managed type uint
''' myLVGROUP.State // is of managed type uint
''' myLVGROUP.UAlign    // is of managed type uint
''' myLVGROUP.PszSubtitle   // is of managed type IntPtr
''' myLVGROUP.CchSubtitle   // is of managed type uint
''' myLVGROUP.PszTask   // is of managed type string
''' myLVGROUP.CchTask   // is of managed type uint
''' myLVGROUP.PszDescriptionTop // is of managed type string
''' myLVGROUP.CchDescriptionTop // is of managed type uint
''' myLVGROUP.PszDescriptionBottom  // is of managed type string
''' myLVGROUP.CchDescriptionBottom  // is of managed type uint
''' myLVGROUP.ITitleImage   // is of managed type int
''' myLVGROUP.IExtendedImage    // is of managed type int
''' myLVGROUP.IFirstItem    // is of managed type int
''' myLVGROUP.CItems    // is of managed type IntPtr
''' myLVGROUP.PszSubsetTitle    // is of managed type IntPtr
''' myLVGROUP.CchSubsetTitle    // is of managed type IntPtr
''' </example>
''' <remarks>
''' The LVGROUP structure was created by Paw Jershauge
''' Created: Jan. 2008.
''' The LVGROUP structure code is based on information from Microsoft's MSDN2 website.
''' The structure is generated via an automated converter and is as is.
''' The structure may or may not hold errors inside the code, so use at own risk.
''' Reference url: http://msdn.microsoft.com/en-us/library/bb774769(VS.85).aspx
''' </remarks>
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode), Description("LVGROUP StructureUsed to set and retrieve groups.")>
Public Structure LVGROUP
    ''' <summary>
    ''' Size of this structure, in bytes.
    ''' </summary>
    <Description("Size of this structure, in bytes.")>
    Public CbSize As Integer

    ''' <summary>
    ''' Mask that specifies which members of the structure are valid input. One or more of the following values:LVGF_NONENo other items are valid.
    ''' </summary>
    <Description("Mask that specifies which members of the structure are valid input. One or more of the following values:LVGF_NONE No other items are valid.")>
    Public Mask As ListViewGroupMask

    ''' <summary>
    ''' Pointer to a null-terminated string that contains the header text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the header text.
    ''' </summary>
    <Description("Pointer to a null-terminated string that contains the header text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the header text.")>
    <MarshalAs(UnmanagedType.LPWStr)>
    Public PszHeader As String

    ''' <summary>
    ''' Size in TCHARs of the buffer pointed to by the pszHeader member. If the structure is not receiving information about a group, this member is ignored.
    ''' </summary>
    <Description("Size in TCHARs of the buffer pointed to by the pszHeader member. If the structure is not receiving information about a group, this member is ignored.")>
    Public CchHeader As Integer

    ''' <summary>
    ''' Pointer to a null-terminated string that contains the footer text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the footer text.
    ''' </summary>
    <Description("Pointer to a null-terminated string that contains the footer text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the footer text.")>
    <MarshalAs(UnmanagedType.LPWStr)>
    Public PszFooter As String

    ''' <summary>
    ''' Size in TCHARs of the buffer pointed to by the pszFooter member. If the structure is not receiving information about a group, this member is ignored.
    ''' </summary>
    <Description("Size in TCHARs of the buffer pointed to by the pszFooter member. If the structure is not receiving information about a group, this member is ignored.")>
    Public CchFooter As Integer

    ''' <summary>
    ''' ID of the group.
    ''' </summary>
    <Description("ID of the group.")>
    Public IGroupId As Integer

    ''' <summary>
    ''' Mask used with LVM_GETGROUPINFO (Microsoft Windows XP and Windows Vista) and LVM_SETGROUPINFO (Windows Vista only) to specify which flags in the state value are being retrieved or set.
    ''' </summary>
    <Description("Mask used with LVM_GETGROUPINFO (Microsoft Windows XP and Windows Vista) and LVM_SETGROUPINFO (Windows Vista only) to specify which flags in the state value are being retrieved or set.")>
    Public StateMask As Integer

    ''' <summary>
    ''' Flag that can have one of the following values:LVGS_NORMALGroups are expanded, the group name is displayed, and all items in the group are displayed.
    ''' </summary>
    <Description("Flag that can have one of the following values:LVGS_NORMAL Groups are expanded, the group name is displayed, and all items in the group are displayed.")>
    Public State As ListViewGroupState

    ''' <summary>
    ''' Indicates the alignment of the header or footer text for the group. It can have one or more of the following values. Use one of the header flags. Footer flags are optional. Windows XP: Footer flags are reserved.LVGA_FOOTER_CENTERReserved.
    ''' </summary>
    <Description("Indicates the alignment of the header or footer text for the group. It can have one or more of the following values. Use one of the header flags. Footer flags are optional. Windows XP: Footer flags are reserved.LVGA_FOOTER_CENTERReserved.")>
    Public UAlign As UInteger

    ''' <summary>
    ''' Windows Vista. Pointer to a null-terminated string that contains the subtitle text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the subtitle text. This element is drawn under the header text.
    ''' </summary>
    <Description("Windows Vista. Pointer to a null-terminated string that contains the subtitle text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the subtitle text. This element is drawn under the header text.")>
    Public PszSubtitle As IntPtr

    ''' <summary>
    ''' Windows Vista. Size, in TCHARs, of the buffer pointed to by the pszSubtitle member. If the structure is not receiving information about a group, this member is ignored.
    ''' </summary>
    <Description("Windows Vista. Size, in TCHARs, of the buffer pointed to by the pszSubtitle member. If the structure is not receiving information about a group, this member is ignored.")>
    Public CchSubtitle As UInteger

    ''' <summary>
    ''' Windows Vista. Pointer to a null-terminated string that contains the text for a task link when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the task text. This item is drawn right-aligned opposite the header text. When clicked by the user, the task link generates an LVN_LINKCLICK notification.
    ''' </summary>
    <Description("Windows Vista. Pointer to a null-terminated string that contains the text for a task link when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the task text. This item is drawn right-aligned opposite the header text. When clicked by the user, the task link generates an LVN_LINKCLICK notification.")>
    <MarshalAs(UnmanagedType.LPWStr)>
    Public PszTask As String

    ''' <summary>
    ''' Windows Vista. Size in TCHARs of the buffer pointed to by the pszTask member. If the structure is not receiving information about a group, this member is ignored.
    ''' </summary>
    <Description("Windows Vista. Size in TCHARs of the buffer pointed to by the pszTask member. If the structure is not receiving information about a group, this member is ignored.")>
    Public CchTask As UInteger

    ''' <summary>
    ''' Windows Vista. Pointer to a null-terminated string that contains the top description text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the top description text. This item is drawn opposite the title image when there is a title image, no extended image, and uAlign==LVGA_HEADER_CENTER.
    ''' </summary>
    <Description("Windows Vista. Pointer to a null-terminated string that contains the top description text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the top description text. This item is drawn opposite the title image when there is a title image, no extended image, and uAlign==LVGA_HEADER_CENTER.")>
    <MarshalAs(UnmanagedType.LPWStr)>
    Public PszDescriptionTop As String

    ''' <summary>
    ''' Windows Vista. Size in TCHARs of the buffer pointed to by the pszDescriptionTop member. If the structure is not receiving information about a group, this member is ignored.
    ''' </summary>
    <Description("Windows Vista. Size in TCHARs of the buffer pointed to by the pszDescriptionTop member. If the structure is not receiving information about a group, this member is ignored.")>
    Public CchDescriptionTop As UInteger

    ''' <summary>
    ''' Windows Vista. Pointer to a null-terminated string that contains the bottom description text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the bottom description text. This item is drawn under the top description text when there is a title image, no extended image, and uAlign==LVGA_HEADER_CENTER.
    ''' </summary>
    <Description("Windows Vista. Pointer to a null-terminated string that contains the bottom description text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the bottom description text. This item is drawn under the top description text when there is a title image, no extended image, and uAlign==LVGA_HEADER_CENTER.")>
    <MarshalAs(UnmanagedType.LPWStr)>
    Public PszDescriptionBottom As String

    ''' <summary>
    ''' Windows Vista. Size in TCHARs of the buffer pointed to by the pszDescriptionBottom member. If the structure is not receiving information about a group, this member is ignored.
    ''' </summary>
    <Description("Windows Vista. Size in TCHARs of the buffer pointed to by the pszDescriptionBottom member. If the structure is not receiving information about a group, this member is ignored.")>
    Public CchDescriptionBottom As UInteger

    ''' <summary>
    ''' Windows Vista. Index of the title image in the control imagelist.
    ''' </summary>
    <Description("Windows Vista. Index of the title image in the control imagelist.")>
    Public ITitleImage As Integer

    ''' <summary>
    ''' Windows Vista. Index of the extended image in the control imagelist.
    ''' </summary>
    <Description("Windows Vista. Index of the extended image in the control imagelist.")>
    Public IExtendedImage As Integer

    ''' <summary>
    ''' Windows Vista. Read-only.
    ''' </summary>
    <Description("Windows Vista. Read-only.")>
    Public IFirstItem As Integer

    ''' <summary>
    ''' Windows Vista. Read-only in non-owner data mode.
    ''' </summary>
    <Description("Windows Vista. Read-only in non-owner data mode.")>
    Public CItems As IntPtr

    ''' <summary>
    ''' Windows Vista. NULL if group is not a subset. Pointer to a null-terminated string that contains the subset title text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the subset title text.
    ''' </summary>
    <Description("Windows Vista. NULL if group is not a subset. Pointer to a null-terminated string that contains the subset title text when item information is being set. If group information is being retrieved, this member specifies the address of the buffer that receives the subset title text.")>
    Public PszSubsetTitle As IntPtr

    ''' <summary>
    ''' Windows Vista. Size in TCHARs of the buffer pointed to by the pszSubsetTitle member. If the structure is not receiving information about a group, this member is ignored.
    ''' </summary>
    <Description("Windows Vista. Size in TCHARs of the buffer pointed to by the pszSubsetTitle member. If the structure is not receiving information about a group, this member is ignored.")>
    Public CchSubsetTitle As IntPtr
End Structure

Public Enum ListViewGroupMask
    None = &H0
    Header = &H1
    Footer = &H2
    State = &H4
    Align = &H8
    GroupId = &H10
    SubTitle = &H100
    Task = &H200
    DescriptionTop = &H400
    DescriptionBottom = &H800
    TitleImage = &H1000
    ExtendedImage = &H2000
    Items = &H4000
    Subset = &H8000
    SubsetItems = &H10000
End Enum

Public Enum ListViewGroupState
    ''' <summary>
    ''' Groups are expanded, the group name is displayed, and all items in the group are displayed.
    ''' </summary>
    Normal = 0
    ''' <summary>
    ''' The group is collapsed.
    ''' </summary>
    Collapsed = 1
    ''' <summary>
    ''' The group is hidden.
    ''' </summary>
    Hidden = 2
    ''' <summary>
    ''' Version 6.00 and Windows Vista. The group does not display a header.
    ''' </summary>
    NoHeader = 4
    ''' <summary>
    ''' Version 6.00 and Windows Vista. The group can be collapsed.
    ''' </summary>
    Collapsible = 8
    ''' <summary>
    ''' Version 6.00 and Windows Vista. The group has keyboard focus.
    ''' </summary>
    Focused = 16
    ''' <summary>
    ''' Version 6.00 and Windows Vista. The group is selected.
    ''' </summary>
    Selected = 32
    ''' <summary>
    ''' Version 6.00 and Windows Vista. The group displays only a portion of its items.
    ''' </summary>
    SubSeted = 64
    ''' <summary>
    ''' Version 6.00 and Windows Vista. The subset link of the group has keyboard focus.
    ''' </summary>
    SubSetLinkFocused = 128

End Enum

