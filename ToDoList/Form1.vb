Imports System.Diagnostics.Eventing
Imports System.IO
Imports System.Windows.Forms

Public Class Form1

    Dim stateCheck = New List(Of String)()
    Dim inputList = New List(Of String)()

    Dim listCheck() As CheckBox
    Dim listLabel() As Label
    Dim deleteButton() As Button

    Dim outputData As String
    Dim inputData = New List(Of String)()

    Dim saveMessage As Long
    Dim loadMessage As Long

    Private Sub additionButton_Click(sender As Object, e As EventArgs) Handles additionButton.Click
        stateCheck.add(False)
        '入力フォームに記載した内容をリストに保存
        inputList.add(inputForm.Text)
        reflectionList()

    End Sub

    'リストへ反映させるメソッド。
    Private Sub reflectionList()
        ReDim listCheck(inputList.Count - 1)
        ReDim listLabel(inputList.Count - 1)
        ReDim deleteButton(inputList.Count - 1)

        'リストに登録されているコントロールの情報を取得します。
        For i As Integer = 0 To inputList.Count - 2
            Dim c0 As CheckBox = TableLayoutPanel1.GetControlFromPosition(0, i)
            Dim c1 As Label = TableLayoutPanel1.GetControlFromPosition(1, i)
            If c0 Is Nothing Then
                Exit For
            ElseIf c1 Is Nothing Then
                Exit For
            Else
                stateCheck(i) = c0.Checked
                inputList(i) = c1.Text
            End If
        Next

        '一度パネル上のコントロールを削除してから再度配置する。
        TableLayoutPanel1.Controls.Clear()

        'リストに保存されている分だけ行を作成する。
        For i As Integer = 0 To inputList.Count - 1
            listCheck(i) = New CheckBox
            listCheck(i).Checked = stateCheck(i)

            listLabel(i) = New Label()
            listLabel(i).Text = inputList(i)
            listLabel(i).Font = New Font("MS UI Gothic", 14)
            listLabel(i).Size = New Size(400, 26)
            listLabel(i).BorderStyle = BorderStyle.FixedSingle

            deleteButton(i) = New Button()
            deleteButton(i).Text = "削除"

            'チェックボタンを配置
            TableLayoutPanel1.Controls.Add(listCheck(i), 0, i)
            '入力フォームの内容を配置
            TableLayoutPanel1.Controls.Add(listLabel(i), 1, i)
            '削除ボタンを配置
            TableLayoutPanel1.Controls.Add(deleteButton(i), 2, i)
        Next
    End Sub


    Private Sub saveButton_Click(sender As Object, e As EventArgs) Handles saveButton.Click
        '保存ボタンをクリックすると、
        '現在リストに表示されているチェックボックスの状態とテキスト内容をテキストファイルに保存する。
        saveMessage = MsgBox("現在のリストの状態をデスクトップに保存します。" & vbCrLf & "よろしいですか？", vbYesNo + vbQuestion)
        If saveMessage = vbYes Then
            '保存する際に,でそれぞれを区切る様にする、文末の,は削除する。
            For i As Integer = 0 To inputList.Count - 1
                Dim c0 As CheckBox = TableLayoutPanel1.GetControlFromPosition(0, i)
                Dim c1 As Label = TableLayoutPanel1.GetControlFromPosition(1, i)
                outputData = outputData & c0.Checked & "," & c1.Text & ","
            Next
            '文末の,を削除する。
            outputData = outputData.TrimEnd(CType(",", Char))

            '上記で読み込んだ内容を"ToDoList.txt"として保存する。
            Dim outputFile As String
            outputFile = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Desktop, "ToDoList.txt")
            My.Computer.FileSystem.WriteAllText(outputFile, outputData, False)
            MsgBox("保存が完了しました。")
        End If
    End Sub


    Private Sub loadButton_Click(sender As Object, e As EventArgs) Handles loadButton.Click
        '読込ボタンをクリックすると、
        'ファイルの選択画面が表示されるため、上記で保存したリストのファイルを選択すると、
        '保存していた内容がToDoリスト内に反映される。
        loadMessage = MsgBox("読み込まれたリストは、" & vbCrLf & "現在表示されているリストの下に追加されます。" & vbCrLf & "リストの読込を行います。", vbYesNo + vbQuestion)
        If loadMessage = vbYes Then
            Dim textFilename As String

            Dim ofd As New OpenFileDialog()
            ofd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            ofd.Filter = "テキストファイル(.txt)|*.txt|すべてのファイル(*.*)|*.*"
            ofd.Title = "保存したToDoList.txtのテキストファイルを選択してください"
            textFilename = ofd.FileName

            'ダイアログを表示する
            If ofd.ShowDialog() = DialogResult.OK Then
                '変数srをSystem.IO.StreamReader型で生成
                Dim sr As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(ofd.FileName)
                '変数FirstLineをString型で生成
                Dim FirstLine As String = sr.ReadLine()
                Dim delimiter As String = ","
                Dim target As String = FirstLine
                inputData = Split(target, delimiter)

                For i As Integer = 0 To inputData.length - 1
                    If i Mod 2 = 0 Then
                        stateCheck.add(inputData(i))
                    Else
                        inputList.add(inputData(i))
                    End If
                Next
                reflectionList()
                MsgBox("読込が完了しました。")
            End If
        End If
    End Sub

End Class
