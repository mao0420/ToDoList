Imports System.Diagnostics.Eventing
Imports System.IO
Imports System.Windows.Forms

Public Class Form1

    Dim stateCheck = New List(Of String)()
    Dim inputList = New List(Of String)()

    Dim listCheck() As CheckBox
    Dim listTextbox() As TextBox
    Dim deleteButton() As Button

    Dim columnCheck = 0
    Dim columnTextbox = 1
    Dim columnButton = 2

    Dim outputData As String
    Dim inputData = New List(Of String)()

    Dim inputNothingMessage As Long
    Dim saveMessage As Long
    Dim loadMessage As Long

    Private Sub additionButton_Click(sender As Object, e As EventArgs) Handles additionButton.Click
        '追加ボタンをクリックした際の処理
        If inputForm.Text = "" Then
            MsgBox("文字が入力されていません。")
        ElseIf Replace(Replace(inputForm.Text, " ", ""), "　", "") = "" Then
            MsgBox("空白文字のみが入力されています。")
        ElseIf Len(inputForm.Text) > 30 Then
            MsgBox("30文字以内で入力してください。")
        Else
            'チェックボックスの項目に1つ追加
            stateCheck.add(False)
            '入力フォームに記載した内容をリストに保存
            inputList.add(inputForm.Text)
            reflectionList()
        End If
        inputForm.Text = Nothing
    End Sub

    'リストへ反映させるメソッド。
    Private Sub reflectionList()
        Array.Resize(listCheck, inputList.Count)
        Array.Resize(listTextbox, inputList.Count)
        Array.Resize(deleteButton, inputList.Count)

        'チェックボックスの設定
        listCheck(inputList.Count - 1) = New CheckBox
        listCheck(inputList.Count - 1).Checked = stateCheck(inputList.Count - 1)

        'テキストボックスの設定
        listTextbox(inputList.Count - 1) = New TextBox()
        listTextbox(inputList.Count - 1).Text = inputList(inputList.Count - 1)
        listTextbox(inputList.Count - 1).Font = New Font("MS UI Gothic", 13)
        listTextbox(inputList.Count - 1).Size = New Size(450, 26)
        listTextbox(inputList.Count - 1).BorderStyle = BorderStyle.FixedSingle
        listTextbox(inputList.Count - 1).MaxLength = 30

        '削除ボタンの設定
        deleteButton(inputList.Count - 1) = New Button()
        deleteButton(inputList.Count - 1).Text = "削除"
        deleteButton(inputList.Count - 1).Name = "deleteButton" + (inputList.Count - 1).ToString()
        AddHandler deleteButton(inputList.Count - 1).Click, AddressOf deleteButton_Click

        'チェックボタンを配置
        TableLayoutPanel1.Controls.Add(listCheck(inputList.Count - 1), columnCheck, inputList.Count - 1)
        '入力フォームの内容を配置
        TableLayoutPanel1.Controls.Add(listTextbox(inputList.Count - 1), columnTextbox, inputList.Count - 1)
        '削除ボタンを配置
        TableLayoutPanel1.Controls.Add(deleteButton(inputList.Count - 1), columnButton, inputList.Count - 1)
    End Sub

    Private Sub getControlList()
        'リストに登録されているコントロールの情報を取得します。
        For i As Integer = 0 To inputList.Count - 1
            Dim checkboxTemp As CheckBox = TableLayoutPanel1.GetControlFromPosition(columnCheck, i)
            Dim textTemp As TextBox = TableLayoutPanel1.GetControlFromPosition(columnTextbox, i)
            If checkboxTemp Is Nothing Then
                Exit For
            ElseIf textTemp Is Nothing Then
                Exit For
            Else
                stateCheck(i) = checkboxTemp.Checked
                inputList(i) = textTemp.Text
            End If
        Next
    End Sub

    Private Sub deleteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '削除ボタンをクリックすると、
        'ボタンを押した対象行が削除される。
        Dim deleteButtonEvent As Button = CType(sender, Button)

        Dim deleteArray As Integer = Replace(deleteButtonEvent.Name, "deleteButton", "")

        getControlList()

        stateCheck.RemoveAt(deleteArray)
        inputList.RemoveAt(deleteArray)

        'リストに保存されている分だけ行を作成する。
        For i As Integer = deleteArray To inputList.Count - 1
            'チェックボックスの設定
            listCheck(i).Checked = stateCheck(i)

            'ラベルの設定
            listTextbox(i).Text = inputList(i)
            listTextbox(i).Font = New Font("MS UI Gothic", 13)
            listTextbox(i).Size = New Size(450, 26)
            listTextbox(i).BorderStyle = BorderStyle.FixedSingle
            listTextbox(i).MaxLength = 30
        Next
        '最終行のチェックボタンを削除
        TableLayoutPanel1.Controls.Remove(listCheck(inputList.Count))
        '最終行のテキストボックスを削除
        TableLayoutPanel1.Controls.Remove(listTextbox(inputList.Count))
        '最終行の削除ボタンを削除
        TableLayoutPanel1.Controls.Remove(deleteButton(inputList.Count))
    End Sub

    Private Sub saveButton_Click(sender As Object, e As EventArgs) Handles saveButton.Click
        '保存ボタンをクリックすると、
        '現在リストに表示されているチェックボックスの状態とテキスト内容をテキストファイルに保存する。

        If inputList.Count > 0 Then
            saveMessage = MsgBox("現在のリストの状態をデスクトップに保存します。" & vbCrLf & "よろしいですか？", vbYesNo + vbQuestion)
            If saveMessage = vbYes Then
                '保存する際に,でそれぞれを区切る様にする、文末の,は削除する。
                For i As Integer = 0 To inputList.Count - 1
                    Dim c0 As CheckBox = TableLayoutPanel1.GetControlFromPosition(columnCheck, i)
                    Dim c1 As TextBox = TableLayoutPanel1.GetControlFromPosition(columnTextbox, i)
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
        Else
            MsgBox("リストが空の為、保存できません。")
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
                Dim sr As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(ofd.FileName)
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
