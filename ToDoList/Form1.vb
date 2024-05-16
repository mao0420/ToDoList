Imports System.Windows.Forms

Public Class Form1

    Dim todoList(2, 30) As Object
    Dim inputList = New List(Of String)()

    Dim listCheck As New CheckBox()
    Dim deleteButton As New Button()
    Dim listLabel As New Label()


    Private Sub additionButton_Click(sender As Object, e As EventArgs) Handles additionButton.Click
        '入力フォームに記載した内容をリストに保存
        inputList.add(inputForm.Text)

        'リストに保存されている分だけ行を作成する。
        For i As Integer = 0 To inputList.Count - 1
            listLabel.Text = inputList(i)
            deleteButton.Text = "削除"
            'チェックボタンを配置
            todoList(0, i) = listCheck
            '入力フォームの内容を配置
            todoList(1, i) = listLabel
            '削除ボタンを配置
            todoList(2, i) = deleteButton
        Next

        '上記で作成した行をTableLayoutPanelに反映させる
        '(最新行以前の内容が表示されない為、修正の必要あり)
        For i As Integer = 0 To 2
            For k As Integer = 0 To inputList.Count - 1
                TableLayoutPanel1.Controls.Add(todoList(i, k), i, k)
            Next
        Next


    End Sub

End Class
