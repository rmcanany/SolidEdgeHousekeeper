Option Strict On

Public Class UtilsFilenameCharmap
    Private Charmap As New Dictionary(Of String, String)


    Public Sub New()
        LoadCharmap()

    End Sub


    Public Function SubstituteIllegalCharacters(Instring As String) As String
        Dim Outstring As String = Instring
        Dim Key As String
        Dim Value As String

        For Each Key In Charmap.Keys
            Value = Charmap(Key)
            Outstring = Outstring.Replace(Key, Value)
        Next

        Return Outstring
    End Function

    Private Sub LoadCharmap()
        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim CharmapArray As String() = Nothing
        Dim CharmapList As New List(Of String)

        Dim KVPair As String
        Dim Key As String
        Dim Value As String

        Dim CharmapFilename As String = String.Format("{0}\Preferences\filename_charmap.txt", StartupPath)

        CreateCharmapFile()

        CharmapArray = IO.File.ReadAllLines(CharmapFilename)

        For Each KVPair In CharmapArray
            If KVPair.Length > 0 Then  ' Ignoring blank lines
                Key = KVPair(0)

                If Key.ToLower = "c" Then
                    Continue For  ' Ignoring comment line
                End If

                If KVPair.Length = 1 Then
                    Value = ""
                    Charmap(Key) = Value
                    Continue For
                End If

                Value = KVPair.Substring(1).Trim()
                If Value.Length = 0 Then
                    Value = " "
                    Charmap(Key) = Value
                Else
                    Charmap(Key) = Value
                End If

            End If
        Next


    End Sub

    Private Sub CreateCharmapFile()
        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim Charmap As String() = Nothing
        Dim CharmapList As New List(Of String)

        Dim CharmapFilename As String = String.Format("{0}\Preferences\filename_charmap.txt", StartupPath)

        Try
            Charmap = IO.File.ReadAllLines(CharmapFilename)
        Catch ex As Exception
            ' File does not exist.  Create it.
            ' https://www.mtu.edu/umc/services/websites/writing/characters-avoid/

            CharmapList.Add("c This file contains a list of common illegal characters")
            CharmapList.Add("c for files and directories, and their replacements.")
            CharmapList.Add("c ")
            CharmapList.Add("c It covers operating systems such as Windows, Mac and Linux")
            CharmapList.Add("c and devices such as desktops, tablets and smartphones.")
            CharmapList.Add("c ")
            CharmapList.Add("c The format is <illegal character><space><replacement value>")
            CharmapList.Add("c ")
            CharmapList.Add("c So for example, '# my_replacement' means ")
            CharmapList.Add("c If '#' is found, replace it with the text 'my_replacement'.")
            CharmapList.Add("c Another example, '#' means ")
            CharmapList.Add("c If '#' is found, replace it with '' (ie, just delete it).")
            CharmapList.Add("c ")
            CharmapList.Add("c (Note, in the default mapping below, I replace all illegal")
            CharmapList.Add("c characters with '~'.  Feel free to change it to your preference.)  ")
            CharmapList.Add("c ")
            CharmapList.Add("c If you're just using Windows, you probably only have to define")
            CharmapList.Add("c replacements for the characters    < > : "" / \ | ? * !")
            CharmapList.Add("c and comment out or delete the rest.")
            CharmapList.Add("c ")
            CharmapList.Add("c (Actually Windows doesn't care about !, but Solid Edge does.")
            CharmapList.Add("c It used to identify Assembly Family members.)")
            CharmapList.Add("c ")
            CharmapList.Add("c If you mess up, you can delete this file and Housekeeper will")
            CharmapList.Add("c regenerate it next time you start the program.")
            CharmapList.Add("c ")
            CharmapList.Add("c To comment out a line (so Housekeeper ignores it) start the line")
            CharmapList.Add("c with 'c' as done in this header text.")
            CharmapList.Add("c ")
            CharmapList.Add("c There is no error checking when the program reads in this file.")
            CharmapList.Add("c So don't do stuff like '? *' or '   c This is too complicated!!!'.")
            CharmapList.Add("c ")
            CharmapList.Add("c ")
            CharmapList.Add("c Character mapping below")
            CharmapList.Add("")

            CharmapList.Add("# ~")
            CharmapList.Add("% ~")
            CharmapList.Add("& ~")
            CharmapList.Add("{ ~")
            CharmapList.Add("} ~")
            CharmapList.Add("\ ~")
            CharmapList.Add("< ~")
            CharmapList.Add("> ~")
            CharmapList.Add("* ~")
            CharmapList.Add("? ~")
            CharmapList.Add("/ ~")
            CharmapList.Add("$ ~")
            CharmapList.Add("! ~")
            CharmapList.Add("' ~")

            'CharmapList.Add("".doublequote.")
            CharmapList.Add(String.Format("{0} {1}", Chr(34), "~"))

            CharmapList.Add(": ~")
            CharmapList.Add("@ ~")
            CharmapList.Add("+ ~")
            CharmapList.Add("` ~")
            CharmapList.Add("| ~")
            CharmapList.Add("= ~")

            CharmapList.Add("")
            CharmapList.Add("c Note the following replacement (of a space character) is commented out.")
            CharmapList.Add("c  ~")



            IO.File.WriteAllLines(CharmapFilename, CharmapList)

        End Try

    End Sub

End Class
