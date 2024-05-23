Imports System.Diagnostics.Eventing
Imports System.IO
Imports System.Windows.Forms

Public Class Form1

    'データ保管用の配列
    Dim inputCheck = New List(Of String)()
    Dim inputText = New List(Of String)()

    'リスト配置用の各種コントロール
    Dim listCheck() As CheckBox
    Dim listTextbox() As TextBox
    Dim deleteButton() As Button

    'リスト配置用の列番号
    Dim columnCheck = 0
    Dim columnTextbox = 1
    Dim columnButton = 2

    'ファイル保存時のデータ格納用の変数
    Dim outputData As String

    'ファイル読込時のデータ格納用の配列
    Dim inputData = New List(Of String)()

    'ポップアップのYes/No入力確認用変数
    Dim saveMessage As Long
    Dim loadMessage As Long

    '追加ボタンクリックメソッド
    Private Sub additionButton_Click(sender As Object, e As EventArgs) Handles additionButton.Click
        '追加ボタンをクリックした際の処理

        '入力フォームに何も入力していない場合エラーメッセージを表示
        If inputForm.Text = "" Then
            MsgBox("文字が入力されていません。")
            '入力フォームにスペースだけが入っている場合エラーメッセージを表示
        ElseIf Replace(Replace(inputForm.Text, " ", ""), "　", "") = "" Then
            MsgBox("空白文字のみが入力されています。")
            '入力フォームに30文字以上入っている場合エラーメッセージを表示
            '(設定で30文字以上入らないようにしています。)
        ElseIf Len(inputForm.Text) > 30 Then
            MsgBox("30文字以内で入力してください。")
        Else
            'チェックボックスの配列にチェックなしの項目を追加
            inputCheck.add(False)
            '入力フォームに記載した内容をテキストボックスの配列に追加
            inputText.add(inputForm.Text)
            'リスト反映メソッドの呼び出し
            reflectionList()
        End If
        'エラーの有無に関わらず、入力フォームをクリア
        inputForm.Text = Nothing

    End Sub

    'リスト反映メソッド
    Private Sub reflectionList()

        'リストに追加するために配列を一つ追加
        Array.Resize(listCheck, inputText.Count)
        Array.Resize(listTextbox, inputText.Count)
        Array.Resize(deleteButton, inputText.Count)

        'チェックボックスの設定
        listCheck(inputText.Count - 1) = New CheckBox
        listCheck(inputText.Count - 1).Checked = inputCheck(inputText.Count - 1)

        'テキストボックスの設定
        listTextbox(inputText.Count - 1) = New TextBox()
        listTextbox(inputText.Count - 1).Text = inputText(inputText.Count - 1)
        listTextbox(inputText.Count - 1).Font = New Font("MS UI Gothic", 13)
        listTextbox(inputText.Count - 1).Size = New Size(450, 26)
        listTextbox(inputText.Count - 1).BorderStyle = BorderStyle.FixedSingle
        listTextbox(inputText.Count - 1).MaxLength = 30

        '削除ボタンの設定
        deleteButton(inputText.Count - 1) = New Button()
        deleteButton(inputText.Count - 1).Text = "削除"
        deleteButton(inputText.Count - 1).Name = "deleteButton" + (inputText.Count - 1).ToString()
        AddHandler deleteButton(inputText.Count - 1).Click, AddressOf deleteButton_Click

        'チェックボタンを配置
        TableLayoutPanel1.Controls.Add(listCheck(inputText.Count - 1), columnCheck, inputText.Count - 1)
        '入力フォームの内容をテキストボックスに反映させて配置
        TableLayoutPanel1.Controls.Add(listTextbox(inputText.Count - 1), columnTextbox, inputText.Count - 1)
        '削除ボタンを配置
        TableLayoutPanel1.Controls.Add(deleteButton(inputText.Count - 1), columnButton, inputText.Count - 1)

    End Sub

    'コントロール情報取得メソッド
    Private Sub getControlList()

        'リストに表示されているコントロールの情報を取得
        For i As Integer = 0 To inputText.Count - 1
            'リストのチェックボックスの状態とテキストボックスの内容を取得
            Dim checkboxTemp As CheckBox = TableLayoutPanel1.GetControlFromPosition(columnCheck, i)
            Dim textTemp As TextBox = TableLayoutPanel1.GetControlFromPosition(columnTextbox, i)
            If checkboxTemp Is Nothing Then
                Exit For
            ElseIf textTemp Is Nothing Then
                Exit For
            Else
                'それぞれの内容が「Nothing」でない場合、チェックボックスとテキストボックスの配列の対象行に反映
                inputCheck(i) = checkboxTemp.Checked
                inputText(i) = textTemp.Text
            End If
        Next

    End Sub

    '削除ボタンクリックメソッド
    Private Sub deleteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '削除ボタンをクリックすると、
        'ボタンを押した対象行が削除される。

        'どの削除ボタンが押されたか判別
        '削除ボタン名の数値を削除対象の行数として使用
        Dim deleteButtonEvent As Button = CType(sender, Button)
        Dim deleteArray As Integer = Replace(deleteButtonEvent.Name, "deleteButton", "")

        '現在リストに登録されているコントロールの情報を取得
        getControlList()

        '情報を保存しているチェックボックスとテキストボックスの配列から対象の要素を削除
        inputCheck.RemoveAt(deleteArray)
        inputText.RemoveAt(deleteArray)

        '削除対象の行から最終行の一つ前の行に対して、
        '順にチェックボックスとテキストボックスの内容を一つ後ろの行の内容で反映
        For i As Integer = deleteArray To inputText.Count - 1
            'チェックボックスの内容を反映
            listCheck(i).Checked = inputCheck(i)
            'テキストボックスの内容を反映
            listTextbox(i).Text = inputText(i)
        Next

        'TableLayoutPanelから最終行のコントロールを削除
        TableLayoutPanel1.Controls.Remove(listCheck(inputText.Count))
        TableLayoutPanel1.Controls.Remove(listTextbox(inputText.Count))
        TableLayoutPanel1.Controls.Remove(deleteButton(inputText.Count))

    End Sub

    '保存ボタンクリックメソッド
    Private Sub saveButton_Click(sender As Object, e As EventArgs) Handles saveButton.Click
        '保存ボタンをクリックすると、
        '現在リストに表示されているチェックボックスの状態とテキストボックスの内容をテキストファイルに保存

        If inputText.Count > 0 Then
            saveMessage = MsgBox("現在のリストの状態をデスクトップに保存します。" & vbCrLf &
                                 "よろしいですか？", vbYesNo + vbQuestion)
            If saveMessage = vbYes Then

                'チェックボックスの状態とテキストボックスの内容を文字列に変換して格納、
                '格納時にそれぞれ末尾に「,」の区切り文字を追加
                For i As Integer = 0 To inputText.Count - 1
                    Dim c0 As CheckBox = TableLayoutPanel1.GetControlFromPosition(columnCheck, i)
                    Dim c1 As TextBox = TableLayoutPanel1.GetControlFromPosition(columnTextbox, i)
                    outputData = outputData & c0.Checked & "," & c1.Text & ","
                Next

                '全ての格納が完了後、文末の「,」を削除
                outputData = outputData.TrimEnd(CType(",", Char))

                '上記で読み込んだ内容を"ToDoList.txt"としてデスクトップに保存
                Dim outputFile As String
                outputFile = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Desktop, "ToDoList.txt")
                My.Computer.FileSystem.WriteAllText(outputFile, outputData, False)
                MsgBox("「ToDoList.txt」の保存が完了しました。")
            End If
        Else
            'リストに何も登録されていない場合、エラーメッセージを表示して保存せず終了
            MsgBox("リストが空の為、保存できません。")
        End If

    End Sub

    '読込ボタンクリックメソッド
    Private Sub loadButton_Click(sender As Object, e As EventArgs) Handles loadButton.Click
        '読込ボタンをクリックすると、
        'ファイルの選択画面が表示されるため、上記で保存したリストのファイルを選択すると、
        '保存していた内容がToDoリスト内に反映される。

        '確認メッセージを表示
        loadMessage = MsgBox("読み込まれたリストは、" & vbCrLf &
                             "現在表示されているリストの下に追加されます。" & vbCrLf &
                             "リストの読込を行います。", vbYesNo + vbQuestion)

        '確認メッセージでYesを押した場合、ファイル選択のダイアログボックスの設定を実施
        If loadMessage = vbYes Then
            Dim ofd As New OpenFileDialog()
            ofd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            ofd.Filter = "テキストファイル(.txt)|*.txt|すべてのファイル(*.*)|*.*"
            ofd.Title = "保存したToDoList.txtのテキストファイルを選択してください"

            '設定したダイアログボックスを表示し、対象ファイルを選択しOKを押すと対象ファイルが読み込まれる為、
            '読み込まれたファイル内容を「,」の区切り文字で分割し配列に格納
            If ofd.ShowDialog() = DialogResult.OK Then
                Dim sr As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(ofd.FileName)
                Dim firstLine As String = sr.ReadLine()
                Dim delimiter As String = ","
                inputData = Split(firstLine, delimiter)

                '配列に格納されたチェックボックスの状態とテキストボックスの内容を、
                'それぞれの配列に格納し1行毎にリストに反映
                For i As Integer = 0 To inputData.length - 1
                    If i Mod 2 = 0 Then
                        inputCheck.add(inputData(i))
                    Else
                        inputText.add(inputData(i))
                        'リスト反映メソッドの呼び出し
                        reflectionList()
                    End If
                Next

                '読み込みが完了したファイルを閉じる
                sr.Close()
                '処理が完了した事のメッセージを表示
                MsgBox("読込が完了しました。")
            End If
        End If

    End Sub

End Class
