﻿Option Strict On

Public Class HCErrorLogger

    Public Property Timestamp As String
    Public Property LogfileName As String
    Public Property FileLoggers As List(Of Logger)
    Public Property Abort As Boolean

    'Public Property ActiveTaskStack As Stack(Of Logger)
    'Public Property HasErrors As Boolean

    Public Sub New()
        Me.Timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss")
        Me.LogfileName = String.Format("{0}\Housekeeper__{1}.log", IO.Path.GetTempPath, Timestamp)
        Me.FileLoggers = New List(Of Logger)
        Me.Abort = False

    End Sub

    Public Function FileLoggerHasErrors(Filename As String) As Boolean
        Dim tmpHasErrors As Boolean = False
        For Each FileLogger As Logger In FileLoggers
            If FileLogger.Name = Filename Then
                tmpHasErrors = FileLogger.HasErrors
                Exit For
            End If
        Next
        Return tmpHasErrors
    End Function

    Public Sub RequestAbort()
        Me.Abort = True
    End Sub

    Public Function AddFile(Name As String) As Logger
        Dim FileLogger As New Logger(Name, Nothing)
        Me.FileLoggers.Add(FileLogger)
        Return FileLogger
    End Function

    Public Function HasErrors() As Boolean
        For Each FileLogger As Logger In FileLoggers
            If FileLogger.HasErrors Then Return True
        Next
        Return False
    End Function

    Public Sub Save()
        Dim Outlist As New List(Of String)

        If HasErrors() Then
            For Each FileLogger As Logger In FileLoggers
                If FileLogger.HasErrors Then
                    BuildOutput(FileLogger, Outlist, Level:=0)
                End If
            Next

            IO.File.WriteAllLines(Me.LogfileName, Outlist)

        End If
    End Sub

    Private Sub BuildOutput(_Logger As Logger, Outlist As List(Of String), Level As Integer)
        Dim Indent As String = StrDup(4 * Level, " ")

        If Level = 0 Then  ' It's the filename
            Outlist.Add("")
            Outlist.Add(String.Format("{0} in {1}", IO.Path.GetFileName(_Logger.Name), IO.Path.GetDirectoryName(_Logger.Name)))
        Else
            Outlist.Add(String.Format("{0}{1}", Indent, _Logger.Name))
            Indent = StrDup(4 * (Level + 1), " ")
        End If

        For Each Message As String In _Logger.GetMessages
            Outlist.Add(String.Format("{0}{1}", Indent, Message))
        Next
        For Each tmpLogger As Logger In _Logger.Loggers
            If tmpLogger.HasErrors Then
                BuildOutput(tmpLogger, Outlist, Level + 1)
            End If
        Next
    End Sub

    'Public Function AddTask(_ParentLogger As Logger, Name As String, Activate As Boolean) As Logger
    '    Return _ParentLogger.AddLogger(Name, _ParentLogger)
    'End Function
End Class

Public Class Logger
    Public Property Name As String
    Public Property Parent As Logger
    Public Property Loggers As List(Of Logger)
    Public Property HasErrors As Boolean
    Private Property Messages As List(Of String)

    Public Sub New(_Name As String, _Parent As Logger)
        Me.Name = _Name
        Me.Parent = _Parent
        Me.Loggers = New List(Of Logger)
        Me.HasErrors = False
        Me.Messages = New List(Of String)
    End Sub

    Public Function AddLogger(Name As String) As Logger
        Dim SubLogger As New Logger(Name, Me)
        Loggers.Add(SubLogger)
        Return SubLogger
    End Function

    Public Sub AddMessage(Message As String)
        Me.HasErrors = True
        Me.Messages.Add(Message)

        ' Propagate error flag up to parent loggers
        Dim tmpParent As Logger = Me.Parent

        While tmpParent IsNot Nothing
            tmpParent.HasErrors = True
            tmpParent = tmpParent.Parent
        End While

    End Sub

    Public Function ContainsMessage(Message As String) As Boolean
        Return Me.Messages.Contains(Message)
    End Function

    Public Function GetMessages() As List(Of String)
        Return Me.Messages
    End Function

End Class
