Imports System.Windows.Forms

Public Class Form1

    'Dim todoList(2, 30) As Object
    Dim inputList = New List(Of String)()

    'Dim listCheck As New CheckBox()
    'Dim deleteButton As New Button()
    'Dim listLabel As New Label()

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

        'リストに保存されている分だけ行を作成する。
        'For i As Integer = 0 To inputList.Count - 1
        listLabel1.Text = inputList(0)
        deleteButton1.Text = "削除"

        'チェックボタンを配置
        TableLayoutPanel1.Controls.Add(listCheck1, 0, 0)

        '入力フォームの内容を配置
        TableLayoutPanel1.Controls.Add(listLabel1, 1, 0)

        '削除ボタンを配置
        TableLayoutPanel1.Controls.Add(deleteButton1, 2, 0)

        If inputList.Count - 1 >= 1 Then
            listLabel2.Text = inputList(1)
            deleteButton2.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck2, 0, 1)
            TableLayoutPanel1.Controls.Add(listLabel2, 1, 1)
            TableLayoutPanel1.Controls.Add(deleteButton2, 2, 1)
        End If
        If inputList.Count - 1 >= 2 Then
            listLabel3.Text = inputList(2)
            deleteButton3.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck3, 0, 2)
            TableLayoutPanel1.Controls.Add(listLabel3, 1, 2)
            TableLayoutPanel1.Controls.Add(deleteButton3, 2, 2)
        End If
        If inputList.Count - 1 >= 3 Then
            listLabel4.Text = inputList(3)
            deleteButton4.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck4, 0, 3)
            TableLayoutPanel1.Controls.Add(listLabel4, 1, 3)
            TableLayoutPanel1.Controls.Add(deleteButton4, 2, 3)
        End If
        If inputList.Count - 1 >= 4 Then
            listLabel5.Text = inputList(4)
            deleteButton5.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck5, 0, 4)
            TableLayoutPanel1.Controls.Add(listLabel5, 1, 4)
            TableLayoutPanel1.Controls.Add(deleteButton5, 2, 4)
        End If
        If inputList.Count - 1 >= 5 Then
            listLabel6.Text = inputList(5)
            deleteButton6.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck6, 0, 5)
            TableLayoutPanel1.Controls.Add(listLabel6, 1, 5)
            TableLayoutPanel1.Controls.Add(deleteButton6, 2, 5)
        End If
        If inputList.Count - 1 >= 6 Then
            listLabel7.Text = inputList(6)
            deleteButton7.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck7, 0, 6)
            TableLayoutPanel1.Controls.Add(listLabel7, 1, 6)
            TableLayoutPanel1.Controls.Add(deleteButton7, 2, 6)
        End If
        If inputList.Count - 1 >= 7 Then
            listLabel8.Text = inputList(7)
            deleteButton8.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck8, 0, 7)
            TableLayoutPanel1.Controls.Add(listLabel8, 1, 7)
            TableLayoutPanel1.Controls.Add(deleteButton8, 2, 7)
        End If
        If inputList.Count - 1 >= 8 Then
            listLabel9.Text = inputList(8)
            deleteButton9.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck9, 0, 8)
            TableLayoutPanel1.Controls.Add(listLabel9, 1, 8)
            TableLayoutPanel1.Controls.Add(deleteButton9, 2, 8)
        End If
        If inputList.Count - 1 >= 9 Then
            listLabel10.Text = inputList(9)
            deleteButton10.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck10, 0, 9)
            TableLayoutPanel1.Controls.Add(listLabel10, 1, 9)
            TableLayoutPanel1.Controls.Add(deleteButton10, 2, 9)
        End If
        If inputList.Count - 1 >= 10 Then
            listLabel11.Text = inputList(10)
            deleteButton11.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck11, 0, 10)
            TableLayoutPanel1.Controls.Add(listLabel11, 1, 10)
            TableLayoutPanel1.Controls.Add(deleteButton11, 2, 10)
        End If
        If inputList.Count - 1 >= 11 Then
            listLabel12.Text = inputList(11)
            deleteButton12.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck12, 0, 11)
            TableLayoutPanel1.Controls.Add(listLabel12, 1, 11)
            TableLayoutPanel1.Controls.Add(deleteButton12, 2, 11)
        End If
        If inputList.Count - 1 >= 12 Then
            listLabel13.Text = inputList(12)
            deleteButton13.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck13, 0, 12)
            TableLayoutPanel1.Controls.Add(listLabel13, 1, 12)
            TableLayoutPanel1.Controls.Add(deleteButton13, 2, 12)
        End If
        If inputList.Count - 1 >= 13 Then
            listLabel14.Text = inputList(13)
            deleteButton14.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck14, 0, 13)
            TableLayoutPanel1.Controls.Add(listLabel14, 1, 13)
            TableLayoutPanel1.Controls.Add(deleteButton14, 2, 13)
        End If
        If inputList.Count - 1 >= 14 Then
            listLabel15.Text = inputList(14)
            deleteButton15.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck15, 0, 14)
            TableLayoutPanel1.Controls.Add(listLabel15, 1, 14)
            TableLayoutPanel1.Controls.Add(deleteButton15, 2, 14)
        End If
        If inputList.Count - 1 >= 15 Then
            listLabel16.Text = inputList(15)
            deleteButton16.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck16, 0, 15)
            TableLayoutPanel1.Controls.Add(listLabel16, 1, 15)
            TableLayoutPanel1.Controls.Add(deleteButton16, 2, 15)
        End If
        If inputList.Count - 1 >= 16 Then
            listLabel17.Text = inputList(16)
            deleteButton17.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck17, 0, 16)
            TableLayoutPanel1.Controls.Add(listLabel17, 1, 16)
            TableLayoutPanel1.Controls.Add(deleteButton17, 2, 16)
        End If
        If inputList.Count - 1 >= 17 Then
            listLabel18.Text = inputList(17)
            deleteButton18.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck18, 0, 17)
            TableLayoutPanel1.Controls.Add(listLabel18, 1, 17)
            TableLayoutPanel1.Controls.Add(deleteButton18, 2, 17)
        End If
        If inputList.Count - 1 >= 18 Then
            listLabel19.Text = inputList(18)
            deleteButton19.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck19, 0, 18)
            TableLayoutPanel1.Controls.Add(listLabel19, 1, 18)
            TableLayoutPanel1.Controls.Add(deleteButton19, 2, 18)
        End If
        If inputList.Count - 1 >= 19 Then
            listLabel20.Text = inputList(19)
            deleteButton20.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck20, 0, 19)
            TableLayoutPanel1.Controls.Add(listLabel20, 1, 19)
            TableLayoutPanel1.Controls.Add(deleteButton20, 2, 19)
        End If
        If inputList.Count - 1 >= 20 Then
            listLabel21.Text = inputList(20)
            deleteButton21.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck21, 0, 20)
            TableLayoutPanel1.Controls.Add(listLabel21, 1, 20)
            TableLayoutPanel1.Controls.Add(deleteButton21, 2, 20)
        End If
        If inputList.Count - 1 >= 21 Then
            listLabel22.Text = inputList(21)
            deleteButton22.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck22, 0, 21)
            TableLayoutPanel1.Controls.Add(listLabel22, 1, 21)
            TableLayoutPanel1.Controls.Add(deleteButton22, 2, 21)
        End If
        If inputList.Count - 1 >= 22 Then
            listLabel23.Text = inputList(22)
            deleteButton23.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck23, 0, 22)
            TableLayoutPanel1.Controls.Add(listLabel23, 1, 22)
            TableLayoutPanel1.Controls.Add(deleteButton23, 2, 22)
        End If
        If inputList.Count - 1 >= 23 Then
            listLabel24.Text = inputList(23)
            deleteButton24.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck24, 0, 23)
            TableLayoutPanel1.Controls.Add(listLabel24, 1, 23)
            TableLayoutPanel1.Controls.Add(deleteButton24, 2, 23)
        End If
        If inputList.Count - 1 >= 24 Then
            listLabel25.Text = inputList(24)
            deleteButton25.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck25, 0, 24)
            TableLayoutPanel1.Controls.Add(listLabel25, 1, 24)
            TableLayoutPanel1.Controls.Add(deleteButton25, 2, 24)
        End If
        If inputList.Count - 1 >= 25 Then
            listLabel26.Text = inputList(25)
            deleteButton26.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck26, 0, 25)
            TableLayoutPanel1.Controls.Add(listLabel26, 1, 25)
            TableLayoutPanel1.Controls.Add(deleteButton26, 2, 25)
        End If
        If inputList.Count - 1 >= 26 Then
            listLabel27.Text = inputList(26)
            deleteButton27.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck27, 0, 26)
            TableLayoutPanel1.Controls.Add(listLabel27, 1, 26)
            TableLayoutPanel1.Controls.Add(deleteButton27, 2, 26)
        End If
        If inputList.Count - 1 >= 27 Then
            listLabel28.Text = inputList(27)
            deleteButton28.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck28, 0, 27)
            TableLayoutPanel1.Controls.Add(listLabel28, 1, 27)
            TableLayoutPanel1.Controls.Add(deleteButton28, 2, 27)
        End If
        If inputList.Count - 1 >= 28 Then
            listLabel29.Text = inputList(28)
            deleteButton29.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck29, 0, 28)
            TableLayoutPanel1.Controls.Add(listLabel29, 1, 28)
            TableLayoutPanel1.Controls.Add(deleteButton29, 2, 28)
        End If
        If inputList.Count - 1 >= 29 Then
            listLabel30.Text = inputList(29)
            deleteButton30.Text = "削除"
            TableLayoutPanel1.Controls.Add(listCheck30, 0, 29)
            TableLayoutPanel1.Controls.Add(listLabel30, 1, 29)
            TableLayoutPanel1.Controls.Add(deleteButton30, 2, 29)
        End If

        'Next

        '上記で作成した行をTableLayoutPanelに反映させる
        '(最新行以前の内容が表示されない為、修正の必要あり)
        'For i As Integer = 0 To 2
        'For k As Integer = 0 To inputList.Count - 1
        'TableLayoutPanel1.Controls.Add(todoList(i, k), i, k)
        'Next
        'Next

    End Sub

    'リストへ反映させるメソッド。
    'Private Sub reflectionList


    Private Sub saveButton_Click(sender As Object, e As EventArgs) Handles saveButton.Click
        For i As Integer = 0 To inputList.Count - 1
            Dim c0 As CheckBox = TableLayoutPanel1.GetControlFromPosition(0, i)
            Dim c1 As Label = TableLayoutPanel1.GetControlFromPosition(1, i)
            outputData = outputData & c0.Checked & "," & c1.Text & ","
        Next

        Dim outputFile As String
        outputFile = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Desktop, "ToDoList.txt")
        My.Computer.FileSystem.WriteAllText(outputFile, outputData, False)
    End Sub


    Private Sub loadButton_Click(sender As Object, e As EventArgs) Handles loadButton.Click
        Dim ofd As New OpenFileDialog()
        ofd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        ofd.Filter = "テキストファイル(.txt)|*.txt|すべてのファイル(*.*)|*.*"
        ofd.Title = "保存したToDoリストのtxtファイルを選択してください"

        'ダイアログを表示する
        If ofd.ShowDialog() = DialogResult.OK Then
            '変数srをSystem.IO.StreamReader型で生成
            Dim sr As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(ofd.FileName)
            '変数FirstLineをString型で生成
            Dim FirstLine As String = sr.ReadLine() & vbCrLf & sr.ReadLine()
            Dim delimiter As String = ","
            Dim target As String = "FirstLine"

            inputData.add = target.Split(delimiter)

            For i As Integer = 0 To inputData.Count / 2 - 1
                '分割して配列に入れた文章をリストに反映させる。


            Next
        End If
    End Sub
End Class
