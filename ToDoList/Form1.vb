Imports System.Diagnostics.Eventing
Imports System.IO
Imports System.Windows.Forms

Public Class Form1

    'Dim todoList(2, 30) As Object
    Dim inputList = New List(Of String)()

    Dim listCheck() As CheckBox
    Dim listLabel() As Label
    Dim deleteButton() As Button

    Dim listCheck1 As New CheckBox()
    Dim listCheck2 As New CheckBox()
    Dim listCheck3 As New CheckBox()
    Dim listCheck4 As New CheckBox()
    Dim listCheck5 As New CheckBox()
    Dim listCheck6 As New CheckBox()
    Dim listCheck7 As New CheckBox()
    Dim listCheck8 As New CheckBox()
    Dim listCheck9 As New CheckBox()
    Dim listCheck10 As New CheckBox()
    Dim listCheck11 As New CheckBox()
    Dim listCheck12 As New CheckBox()
    Dim listCheck13 As New CheckBox()
    Dim listCheck14 As New CheckBox()
    Dim listCheck15 As New CheckBox()
    Dim listCheck16 As New CheckBox()
    Dim listCheck17 As New CheckBox()
    Dim listCheck18 As New CheckBox()
    Dim listCheck19 As New CheckBox()
    Dim listCheck20 As New CheckBox()
    Dim listCheck21 As New CheckBox()
    Dim listCheck22 As New CheckBox()
    Dim listCheck23 As New CheckBox()
    Dim listCheck24 As New CheckBox()
    Dim listCheck25 As New CheckBox()
    Dim listCheck26 As New CheckBox()
    Dim listCheck27 As New CheckBox()
    Dim listCheck28 As New CheckBox()
    Dim listCheck29 As New CheckBox()
    Dim listCheck30 As New CheckBox()

    Dim listLabel1 As New Label()
    Dim listLabel2 As New Label()
    Dim listLabel3 As New Label()
    Dim listLabel4 As New Label()
    Dim listLabel5 As New Label()
    Dim listLabel6 As New Label()
    Dim listLabel7 As New Label()
    Dim listLabel8 As New Label()
    Dim listLabel9 As New Label()
    Dim listLabel10 As New Label()
    Dim listLabel11 As New Label()
    Dim listLabel12 As New Label()
    Dim listLabel13 As New Label()
    Dim listLabel14 As New Label()
    Dim listLabel15 As New Label()
    Dim listLabel16 As New Label()
    Dim listLabel17 As New Label()
    Dim listLabel18 As New Label()
    Dim listLabel19 As New Label()
    Dim listLabel20 As New Label()
    Dim listLabel21 As New Label()
    Dim listLabel22 As New Label()
    Dim listLabel23 As New Label()
    Dim listLabel24 As New Label()
    Dim listLabel25 As New Label()
    Dim listLabel26 As New Label()
    Dim listLabel27 As New Label()
    Dim listLabel28 As New Label()
    Dim listLabel29 As New Label()
    Dim listLabel30 As New Label()

    Dim deleteButton1 As New Button()
    Dim deleteButton2 As New Button()
    Dim deleteButton3 As New Button()
    Dim deleteButton4 As New Button()
    Dim deleteButton5 As New Button()
    Dim deleteButton6 As New Button()
    Dim deleteButton7 As New Button()
    Dim deleteButton8 As New Button()
    Dim deleteButton9 As New Button()
    Dim deleteButton10 As New Button()
    Dim deleteButton11 As New Button()
    Dim deleteButton12 As New Button()
    Dim deleteButton13 As New Button()
    Dim deleteButton14 As New Button()
    Dim deleteButton15 As New Button()
    Dim deleteButton16 As New Button()
    Dim deleteButton17 As New Button()
    Dim deleteButton18 As New Button()
    Dim deleteButton19 As New Button()
    Dim deleteButton20 As New Button()
    Dim deleteButton21 As New Button()
    Dim deleteButton22 As New Button()
    Dim deleteButton23 As New Button()
    Dim deleteButton24 As New Button()
    Dim deleteButton25 As New Button()
    Dim deleteButton26 As New Button()
    Dim deleteButton27 As New Button()
    Dim deleteButton28 As New Button()
    Dim deleteButton29 As New Button()
    Dim deleteButton30 As New Button()

    Dim outputTemporary As String
    Dim outputData As String
    Dim inputData = New List(Of String)()

    Private Sub additionButton_Click(sender As Object, e As EventArgs) Handles additionButton.Click
        '入力フォームに記載した内容をリストに保存
        inputList.add(inputForm.Text)
        reflectionList()

        'Next

        '上記で作成した行をTableLayoutPanelに反映させる
        '(最新行以前の内容が表示されない為、修正の必要あり)
        'For i As Integer = 0 To 2
        'For k As Integer = 0 To inputList.Count - 1
        'TableLayoutPanel1.Controls.Add(todoList(i, k), i, k)
        'Next
        'Next

    End Sub

    Private Sub controlList()

    End Sub

    'リストへ反映させるメソッド。
    Private Sub reflectionList()
        '一度パネル上のコントロールを削除してから再度配置する。
        TableLayoutPanel1.Controls.Clear()
        ReDim listCheck(inputList.Count - 1)
        ReDim listLabel(inputList.Count - 1)
        ReDim deleteButton(inputList.Count - 1)

        'リストに保存されている分だけ行を作成する。
        For i As Integer = 0 To inputList.Count - 1
            listCheck(i) = New CheckBox

            listLabel(i) = New Label()
            listLabel(i).Text = inputList(i)

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
    End Sub


    Private Sub loadButton_Click(sender As Object, e As EventArgs) Handles loadButton.Click
        '読込ボタンをクリックすると、
        'ファイルの選択画面が表示されるため、上記で保存したリストのファイルを選択すると、
        '保存していた内容がToDoリスト内に反映される。
        Dim textFilename As String

        Dim ofd As New OpenFileDialog()
        ofd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        ofd.Filter = "テキストファイル(.txt)|*.txt|すべてのファイル(*.*)|*.*"
        ofd.Title = "保存したToDoリストのtxtファイルを選択してください"
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

            For i As Integer = 0 To inputData.length / 2 - 1
                inputList.add(inputData(i))
                reflectionList()
            Next
        End If
    End Sub
End Class
