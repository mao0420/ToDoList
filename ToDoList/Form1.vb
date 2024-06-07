Imports System.Diagnostics.Eventing
Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class Form1

    'データ保管用の配列
    Dim dataNumber = New List(Of Integer)()
    Dim dataCheck = New List(Of Boolean)()
    Dim dataText = New List(Of String)()
    Dim dataAlarmtime = New List(Of Date)()
    Dim dataAlarmbox = New List(Of Boolean)()
    Dim dataDelete = New List(Of Boolean)()

    'リスト配置用の各種コントロール
    Dim listCheck() As CheckBox
    Dim listTextbox() As TextBox
    Dim listAlarmtime() As DateTimePicker
    Dim listAlarmbox() As ComboBox
    Dim deleteButton() As Button

    'アラーム時間の設定
    Dim alarmEnableTime As Date

    'アラーム対象行
    Dim enableAlarmArray As Integer

    'チェックボックス設定用変数
    Dim checkSize = New Size(15, 26)

    'テキストボックス設定用変数
    Dim textFont = New Font("MS UI Gothic", 13)
    Dim textSize = New Size(450, 26)
    Dim textBorderStyle = BorderStyle.FixedSingle
    Dim textMaxLength = 30

    'アラーム時間設定用変数
    Dim alarmtimeInitialValue = "2024/01/01 00:00"
    Dim alarmtimeSize = New Size(135, 26)
    Dim alarmtimeCustomFormat = "yyyy/MM/dd | HH:mm"
    Dim alarmtimeName = "alarmtime"

    'アラーム起動ボックス設定用変数
    Dim alarmboxSize = New Size(85, 26)
    Dim alarmboxTrue = "アラームON"
    Dim alarmboxFalse = "アラームOFF"
    Dim alarmboxName = "alarmbox"

    '削除ボタン設定用変数
    Dim buttonText = "削除"
    Dim buttonSize = New Size(45, 26)
    Dim buttonName = "deleteButton"

    'リスト配置用の列番号
    Dim columnCheck = 0
    Dim columnTextbox = 1
    Dim columnAlarmtime = 2
    Dim columnAlarmBox = 3
    Dim columnButton = 4

    'リスト配置用の行番号
    Dim rowPanel As Integer

    'ポップアップのYes/No入力確認用変数
    Dim saveSelect As Long
    Dim loadSelect As Long

    'メッセージボックスのメッセージ内容
    Dim messageBootAlarmTimeOver = ("上記対象のアラーム時間が過ぎています。" & vbCrLf &
                                    "対象のアラームをOFFにします。")
    Dim messageAlarmTime = ("の時間になりました。" & vbCrLf &
                            "対象のアラームをOFFにします。")
    Dim messageErrorNotGetTime = "エラーが発生しました、時刻を取得出来ませんでした。"
    Dim messageErrorNotinput = "文字が入力されていません。"
    Dim messageErrorBlankCharactor = "空白文字のみが入力されています。"
    Dim messageSaveConfirmation = ("現在のリストの状態をデータベースに保存します。" & vbCrLf &
                                   "よろしいですか？")
    Dim messageSaveComplete = "データベースへの保存が完了しました。"
    Dim messageSaveListEmpty = "何も登録されていない為、保存できません。"
    Dim messageLoadConfirmation = ("データベースから最後に保存されたリストの状態を読み込みます。" & vbCrLf &
                                   "読み込みの際に現在表示されているリストは削除されます。" & vbCrLf &
                                    "リストの読込を行いますか？")
    Dim messageLoadComplete = "読込が完了しました。"

    Dim errorDatabaseEmpty = "データベースから読み込めるデータがありませんでした"
    Dim errorDatabaseLoad = "データベースの読込に失敗しました"

    'データベースへのログイン情報
    Const DB_Source As String = "192.168.1.6"
    Const DB_Port As String = "3306"
    Const DB_Name As String = "todo_list"
    Const DB_Id As String = "sample"
    Const DB_Pw As String = "testpass"

    '起動時データベース情報取得メソッド
    Sub bootGetDatabase() Handles Me.Shown
        'アプリケーションの起動時にデータベースからリスト情報を取得して反映します。

        'データベースへの接続情報を入力
        Using Conn As New MySqlConnection("Database=" + DB_Name _
                                            + ";Data Source=" + DB_Source _
                                            + ";Port=" + DB_Port _
                                            + ";User Id=" + DB_Id _
                                            + ";Password=" + DB_Pw _
                                            + ";sqlservermode=True;")
            Try
                'データベースへの接続を開始
                Conn.Open()

                'SELECT文でデータベースから必要な情報を取得
                Using cmd As MySqlCommand = New MySqlCommand("SELECT id,input_checkbox,input_text,alarm_time,alarm_box,delete_flag FROM list", Conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        'データベースにデータがない場合、エラーメッセージを表示して読込を終了
                        If reader.Read = Nothing Then
                            MsgBox(errorDatabaseEmpty)
                            Conn.Close()
                            Exit Sub
                        End If

                        Dim enableAlarmOverTimeTemp = ""
                        '取得した情報を各配列に格納し、1行ずつリストに反映
                        While reader.Read()
                            dataNumber.add(reader.GetInt16(0))
                            dataCheck.add(reader.GetBoolean(1))
                            dataText.add(reader.GetString(2))
                            dataAlarmtime.add(reader.GetDateTime(3))

                            'アラームがONになっていてアラーム時間が過ぎている対象をテキストに格納しアラームをOFFに変更、それ以外は通常通りに格納
                            If reader.GetBoolean(4) = True Then
                                Dim timeToWait As TimeSpan = reader.GetDateTime(3) - DateTime.Now
                                If timeToWait.TotalMilliseconds < 0 Then
                                    enableAlarmOverTimeTemp = enableAlarmOverTimeTemp + "「" & reader.GetString(2) & "」" & vbCrLf
                                    dataAlarmbox.add(False)
                                Else
                                    dataAlarmbox.add(reader.GetBoolean(4))
                                End If
                            Else
                                dataAlarmbox.add(reader.GetBoolean(4))
                            End If

                            dataDelete.add(reader.GetBoolean(5))

                            'リスト反映メソッドの呼び出し
                            reflectionList()

                        End While

                        '上記でテキストに格納した対象がある場合、アラーム時間が過ぎているメッセージを表示。
                        If Not enableAlarmOverTimeTemp = "" Then
                            My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Asterisk)
                            MsgBox(enableAlarmOverTimeTemp & messageBootAlarmTimeOver)
                        End If
                    End Using
                End Using

                'MySQLへの接続失敗時のエラー処理
            Catch ex As MySql.Data.MySqlClient.MySqlException
                MsgBox(errorDatabaseLoad)
            End Try

            'データベースへの接続を終了
            Conn.Close()
        End Using
    End Sub

    '追加ボタンクリックメソッド
    Private Sub additionButton_Click(sender As Object, e As EventArgs) Handles additionButton.Click
        '追加ボタンをクリックした際の処理

        '入力フォームに何も入力していない場合エラーメッセージを表示
        If inputForm.Text = "" Then
            MsgBox(messageErrorNotinput)
            '入力フォームにスペースだけが入っている場合エラーメッセージを表示
        ElseIf Replace(Replace(inputForm.Text, " ", ""), "　", "") = "" Then
            MsgBox(messageErrorBlankCharactor)
        Else
            'ナンバーの現在の最大の数値に+1した数を配列に追加
            dataNumber.add(dataNumber.Count)
            'チェックボックスの配列にチェックなしの項目を追加
            dataCheck.add(False)
            '入力フォームに記載した内容をテキストボックスの配列に追加
            dataText.add(inputForm.Text)
            'アラーム時間の初期値を配列に追加
            dataAlarmtime.add(alarmtimeInitialValue)
            'アラーム選択の初期値を配列に追加
            dataAlarmbox.add(False)
            '削除フラグの配列に未削除の項目を追加
            dataDelete.add(False)
            'リスト反映メソッドの呼び出し
            reflectionList()
        End If
        'エラーの有無に関わらず、入力フォームをクリア
        inputForm.Text = Nothing

    End Sub

    'リスト反映メソッド
    Private Sub reflectionList()

        If dataDelete(dataDelete.Count - 1) = False Then
            rowPanel = -1

            For i As Integer = 0 To dataDelete.Count - 1
                If dataDelete(i) = False Then
                    rowPanel = rowPanel + 1
                End If
            Next

            'リストに追加するために配列を一つ追加
            Array.Resize(listCheck, rowPanel + 1)
            Array.Resize(listTextbox, rowPanel + 1)
            Array.Resize(listAlarmtime, rowPanel + 1)
            Array.Resize(listAlarmbox, rowPanel + 1)
            Array.Resize(deleteButton, rowPanel + 1)

            'チェックボックスの設定
            listCheck(rowPanel) = New CheckBox
            listCheck(rowPanel).Size = checkSize
            listCheck(rowPanel).Checked = dataCheck(dataCheck.Count - 1)

            'テキストボックスの設定
            listTextbox(rowPanel) = New TextBox()
            listTextbox(rowPanel).Text = dataText(dataText.Count - 1)
            listTextbox(rowPanel).Font = textFont
            listTextbox(rowPanel).Size = textSize
            listTextbox(rowPanel).BorderStyle = textBorderStyle
            listTextbox(rowPanel).MaxLength = textMaxLength

            'アラーム時間の設定
            listAlarmtime(rowPanel) = New DateTimePicker()
            listAlarmtime(rowPanel).Text = dataAlarmtime(dataAlarmtime.Count - 1)
            listAlarmtime(rowPanel).Size = alarmtimeSize
            listAlarmtime(rowPanel).Format = DateTimePickerFormat.Custom
            listAlarmtime(rowPanel).CustomFormat = alarmtimeCustomFormat
            listAlarmtime(rowPanel).Name = alarmtimeName + (rowPanel).ToString()
            AddHandler listAlarmtime(rowPanel).TextChanged, AddressOf alarmTime_TextChanged

            'アラーム起動ボックスの設定
            listAlarmbox(rowPanel) = New ComboBox()
            listAlarmbox(rowPanel).Items.Add(alarmboxFalse)
            listAlarmbox(rowPanel).Items.Add(alarmboxTrue)
            listAlarmbox(rowPanel).DropDownStyle = ComboBoxStyle.DropDownList
            If dataAlarmbox(dataAlarmbox.Count - 1) = True Then
                listAlarmbox(rowPanel).Text = alarmboxTrue
            Else
                listAlarmbox(rowPanel).Text = alarmboxFalse
            End If
            listAlarmbox(rowPanel).Size = alarmboxSize
            listAlarmbox(rowPanel).Name = alarmboxName + (rowPanel).ToString()
            AddHandler listAlarmbox(rowPanel).SelectedIndexChanged, AddressOf comboBox_SelectedIndexChanged

            '削除ボタンの設定
            deleteButton(rowPanel) = New Button()
            deleteButton(rowPanel).Text = buttonText
            deleteButton(rowPanel).Size = buttonSize
            deleteButton(rowPanel).Name = buttonName + (rowPanel).ToString()
            AddHandler deleteButton(rowPanel).Click, AddressOf deleteButton_Click

            'チェックボタンを配置
            TableLayoutPanel1.Controls.Add(listCheck(rowPanel), columnCheck, rowPanel)
            '入力フォームの内容をテキストボックスに反映させて配置
            TableLayoutPanel1.Controls.Add(listTextbox(rowPanel), columnTextbox, rowPanel)
            'アラーム時間設定をデータタイムピッカーに反映させて配置
            TableLayoutPanel1.Controls.Add(listAlarmtime(rowPanel), columnAlarmtime, rowPanel)
            'アラーム起動ボックス
            TableLayoutPanel1.Controls.Add(listAlarmbox(rowPanel), columnAlarmBox, rowPanel)
            '削除ボタンを配置
            TableLayoutPanel1.Controls.Add(deleteButton(rowPanel), columnButton, rowPanel)

        End If

    End Sub

    'コントロール情報取得メソッド
    Private Sub getControlList()

        Dim tempDelete As Integer = 0
        'リストに表示されているコントロールの情報を取得
        For i As Integer = 0 To listTextbox.Count - 1
            If dataDelete(i + tempDelete) = True Then
                tempDelete = tempDelete + 1
                i = i - 1
            Else
                'リストのチェックボックスの状態とテキストボックスの内容を取得
                Dim checkboxTemp As CheckBox = TableLayoutPanel1.GetControlFromPosition(columnCheck, i)
                Dim textTemp As TextBox = TableLayoutPanel1.GetControlFromPosition(columnTextbox, i)
                Dim alarmtimeTemp As DateTimePicker = TableLayoutPanel1.GetControlFromPosition(columnAlarmtime, i)
                Dim alarmboxTemp As ComboBox = TableLayoutPanel1.GetControlFromPosition(columnAlarmBox, i)
                If checkboxTemp Is Nothing Then
                    Exit For
                ElseIf textTemp Is Nothing Then
                    Exit For
                ElseIf alarmtimeTemp Is Nothing Then
                    Exit For
                ElseIf alarmboxTemp Is Nothing Then
                    Exit For
                Else
                    'それぞれの内容が「Nothing」でない場合、それぞれの配列の対象行に反映
                    dataCheck(i + tempDelete) = checkboxTemp.Checked
                    dataText(i + tempDelete) = textTemp.Text
                    dataAlarmtime(i + tempDelete) = Replace(alarmtimeTemp.Text, "| ", "")
                    If alarmboxTemp.Text = alarmboxTrue Then
                        dataAlarmbox(i + tempDelete) = Replace(alarmboxTemp.Text, alarmboxTrue, True)
                    Else
                        dataAlarmbox(i + tempDelete) = Replace(alarmboxTemp.Text, alarmboxFalse, False)
                    End If
                End If
            End If
        Next

    End Sub

    'アラームON・OFF変更時メソッド
    Private Sub comboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '受け取ったコンボボックス名末尾の数値を取得
        Dim selectComboEvent As ComboBox = CType(sender, ComboBox)
        Dim comboArray As Integer = Replace(selectComboEvent.Name, alarmboxName, "")

        '数値を行数としてパネル上のアラーム状態を確認
        If TableLayoutPanel1.GetControlFromPosition(columnAlarmBox, comboArray).Text = alarmboxTrue Then
            dataAlarmbox(comboArray) = True
        Else
            dataAlarmbox(comboArray) = False
        End If

        'アラーム更新メソッドへ移行
        updateAlarm(comboArray)

    End Sub

    'アラーム時間変更時メソッド
    Private Sub alarmTime_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'アラームの時間が変更された時に、日時を取得して格納
        Dim selectTimeEvent As DateTimePicker = CType(sender, DateTimePicker)
        Dim timeArray As Integer = Replace(selectTimeEvent.Name, alarmtimeName, "")
        dataAlarmtime(timeArray) = Replace(TableLayoutPanel1.GetControlFromPosition(columnAlarmtime, timeArray).Text, "| ", "")

        'アラームがONの場合のみアラームの更新メソッドに移行
        If dataAlarmbox(timeArray) = True Then
            'アラーム更新メソッドへ移行
            updateAlarm(timeArray)
        End If
    End Sub

    'アラーム更新メソッド
    Private Sub updateAlarm(listArray)
        Dim enableAlarmTimeTemp = New List(Of Date)()

        'アラームONになっている対象を確認
        For i As Integer = 0 To dataAlarmbox.Count - 1
            If dataAlarmbox(i) = True Then
                enableAlarmTimeTemp.Add(dataAlarmtime(i))
            End If
        Next

        'アラームが全てOFFの場合はタイマーを終了させる
        If enableAlarmTimeTemp.Count = 0 Then
            Timer1.Enabled = False
        Else

            '最小値の行を取得するための変数を初期化
            enableAlarmArray = Nothing

            'アラームがONになっている中で最小の値を取得
            For i As Integer = 0 To dataAlarmbox.Count - 1
                If dataAlarmbox(i) = True Then
                    If dataAlarmtime(i) = enableAlarmTimeTemp.Min Then
                        enableAlarmArray = i
                        Exit For
                    End If
                End If
            Next

            '上記でアラームがONになっている対象を取得出来ない場合にエラーメッセージを表示してアラームをオフに変更
            If enableAlarmArray < 0 Then
                Timer1.Enabled = False
                MsgBox(messageErrorNotGetTime)
            Else

                'アラームがONの中で最小の値から現在時刻を引いた数値を取得
                alarmEnableTime = enableAlarmTimeTemp.Min
                Dim timeToWait As TimeSpan = alarmEnableTime - DateTime.Now

                '上記で計算した時刻の数値がマイナス(過去日時)の場合、対象のアラーム時間が過ぎている事を通知してアラームを終了
                If timeToWait.TotalMilliseconds < 0 Then
                    My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Asterisk)
                    MsgBox("「" & listTextbox(enableAlarmArray).Text & "」" & messageAlarmTime)
                    listAlarmbox(listArray).Text = alarmboxFalse
                    dataAlarmbox(enableAlarmArray) = False

                    '時間がかかりすぎる場合オーバーフローになるため、一定以内の日付の場合のみタイマーをセット
                ElseIf timeToWait.TotalMilliseconds < 2000000000 Then
                    '上記で計算した時刻の数値がプラス(未来日時)の場合、計算結果の数値をタイマーにセットして起動
                    Timer1.Interval = timeToWait.TotalMilliseconds
                    Timer1.Enabled = True

                    '時間がかかりすぎる場合は一度タイマーをオフにして終了
                Else
                    dataAlarmbox(enableAlarmArray) = False
                End If
            End If
        End If

    End Sub

    '削除ボタンクリックメソッド
    Private Sub deleteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '削除ボタンをクリックすると、
        'ボタンを押した対象行が削除される。

        'どの削除ボタンが押されたか判別
        '削除ボタン名の数値を削除対象の行数として使用
        Dim deleteButtonEvent As Button = CType(sender, Button)
        Dim deleteArray As Integer = Replace(deleteButtonEvent.Name, buttonName, "")

        '現在リストに登録されているコントロールの情報を取得
        getControlList()

        Dim tempDelete As Integer = 0

        '対象行のデータ行数を確認
        For i As Integer = 0 To deleteArray

            If dataDelete(i + tempDelete) = True Then
                tempDelete = tempDelete + 1
                i = i - 1
            End If

        Next

        '対象行の削除フラグをTrueに変更
        dataDelete(deleteArray + tempDelete) = True
        '対象行のアラーム選択をFalseに変更
        dataAlarmbox(deleteArray + tempDelete) = False

        '削除対象の行から最終行の一つ前の行に対して、
        'リスト順に削除ボタン以外の内容を一つ後ろの行の内容で反映
        For i As Integer = deleteArray To listCheck.Count - 2

            If dataDelete(i + tempDelete) = True Then
                tempDelete = tempDelete + 1
                i = i - 1
            Else
                'チェックボックスの内容を反映
                listCheck(i).Checked = dataCheck(i + tempDelete)
                'テキストボックスの内容を反映
                listTextbox(i).Text = dataText(i + tempDelete)
                'アラーム時間の内容を反映
                listAlarmtime(i).Text = dataAlarmtime(i + tempDelete)
                'アラーム状態の内容を反映
                If dataAlarmbox(i + tempDelete) = True Then
                    listAlarmbox(i).Text = alarmboxTrue
                Else
                    listAlarmbox(i).Text = alarmboxFalse
                End If
            End If
        Next

        'TableLayoutPanelから最終行のコントロールを削除
        TableLayoutPanel1.Controls.Remove(listCheck(listCheck.Count - 1))
        TableLayoutPanel1.Controls.Remove(listTextbox(listTextbox.Count - 1))
        TableLayoutPanel1.Controls.Remove(listAlarmtime(listAlarmtime.Count - 1))
        TableLayoutPanel1.Controls.Remove(listAlarmbox(listAlarmbox.Count - 1))
        TableLayoutPanel1.Controls.Remove(deleteButton(deleteButton.Count - 1))

        'リスト用の配列から最終行をそれぞれ削除
        Array.Resize(listCheck, listCheck.Count - 1)
        Array.Resize(listTextbox, listTextbox.Count - 1)
        Array.Resize(listAlarmtime, listAlarmtime.Count - 1)
        Array.Resize(listAlarmbox, listAlarmbox.Count - 1)
        Array.Resize(deleteButton, deleteButton.Count - 1)

    End Sub

    '保存ボタンクリックメソッド
    Private Sub saveButton_Click(sender As Object, e As EventArgs) Handles saveButton.Click
        '保存ボタンをクリックすると、
        '現在リストに表示されているチェックボックスの状態とテキストボックスの内容をテキストファイルに保存

        If dataText.Count > 0 Then
            saveSelect = MsgBox(messageSaveConfirmation, vbYesNo + vbQuestion)
            If saveSelect = vbYes Then

                'データベースへの接続情報を入力
                Using Conn As New MySqlConnection("Database=" + DB_Name _
                                            + ";Data Source=" + DB_Source _
                                            + ";Port=" + DB_Port _
                                            + ";User Id=" + DB_Id _
                                            + ";Password=" + DB_Pw _
                                            + ";sqlservermode=True;")
                    Try
                        'データベースへの接続を開始
                        Conn.Open()

                        '現在リストに登録されているコントロールの情報を取得
                        getControlList()

                        'SELECT文で必要な情報を取得
                        Using cmd As MySqlCommand = New MySqlCommand("SELECT id,input_checkbox,input_text,alarm_time,alarm_box,delete_flag FROM list", Conn)
                            Using reader As MySqlDataReader = cmd.ExecuteReader()
                                '取得した情報を各配列に格納し、1行ずつリストに反映
                                Dim i = 0
                                Dim j = 0
                                Dim update = New List(Of String)()
                                While reader.Read()
                                    '情報に更新がある行のみUPDATEクエリを保存
                                    If Not dataCheck(i) = reader.GetBoolean(1) OrElse
                                       Not dataText(i) = reader.GetString(2) OrElse
                                       Not dataAlarmtime(i) = reader.GetDateTime(3) OrElse
                                       Not dataAlarmbox(i) = reader.GetBoolean(4) OrElse
                                       Not dataDelete(i) = reader.GetBoolean(5) Then
                                        update.Add("UPDATE list SET input_checkbox = " & dataCheck(i) &
                                               ",input_text = '" & dataText(i) &
                                               "',alarm_time = '" & dataAlarmtime(i) &
                                               "',alarm_box = " & dataAlarmbox(i) &
                                               ",delete_flag = " & dataDelete(i) &
                                               " WHERE id = " & dataNumber(i))
                                    End If
                                    i = i + 1
                                End While

                                'リーダーをクローズ
                                reader.Close()

                                '保存したUPDATEクエリを送信
                                While j < update.Count
                                    Using updateCommand As New MySqlCommand(update(j), Conn)
                                        updateCommand.ExecuteNonQuery()
                                    End Using
                                    j = j + 1
                                End While

                                '追加分のINSERTクエリを送信
                                While i < dataNumber.Count
                                    Dim Insert As MySqlCommand = New MySqlCommand("INSERT INTO list
                                                                               (input_checkbox, input_text, alarm_time, 
                                                                                alarm_box, delete_flag) VALUES 
                                                                                (" & dataCheck(i) & ",'" & dataText(i) & "','" & dataAlarmtime(i) & "'," &
                                                                                dataAlarmbox(i) & "," & dataDelete(i) & ")", Conn)
                                    Insert.ExecuteNonQuery()
                                    i = i + 1
                                End While
                            End Using
                        End Using
                        'MySQLへの接続失敗時のエラー処理
                    Catch ex As MySql.Data.MySqlClient.MySqlException
                        MsgBox(errorDatabaseLoad)
                        Conn.Close()
                        Exit Sub
                    End Try
                    'データベースへの接続を終了
                    Conn.Close()

                End Using
                '保存が完了した事のメッセージを表示
                MsgBox(messageSaveComplete)

            End If

        Else
            'リストに一度も登録していない旨のメッセージを表示
            MsgBox(messageSaveListEmpty)

        End If

    End Sub

    '読込ボタンクリックメソッド
    Private Sub loadButton_Click(sender As Object, e As EventArgs) Handles loadButton.Click
        '読込ボタンをクリックすると、
        'ファイルの選択画面が表示されるため、上記で保存したリストのファイルを選択すると、
        '保存していた内容がToDoリスト内に反映される。

        '確認メッセージを表示
        loadSelect = MsgBox(messageLoadConfirmation, vbYesNo + vbQuestion)

        '確認メッセージでYesを押した場合、ファイル選択のダイアログボックスの設定を実施
        If loadSelect = vbYes Then
            'データベースへの接続情報を入力
            Using Conn As New MySqlConnection("Database=" + DB_Name _
                                + ";Data Source=" + DB_Source _
                                + ";Port=" + DB_Port _
                                + ";User Id=" + DB_Id _
                                + ";Password=" + DB_Pw _
                                + ";sqlservermode=True;")
                Try
                    'データベースへの接続を開始
                    Conn.Open()

                    'SELECT文で必要な情報を取得
                    Using cmd As MySqlCommand = New MySqlCommand("SELECT id,input_checkbox,input_text,alarm_time,alarm_box,delete_flag FROM list", Conn)
                        Using reader As MySqlDataReader = cmd.ExecuteReader()

                            'データベースにデータがない場合、エラーメッセージを表示して終了
                            If reader.Read = Nothing Then
                                MsgBox(errorDatabaseEmpty)
                                Conn.Close()
                                Exit Sub
                            End If

                            'データ保管用の配列を初期化
                            dataNumber = New List(Of Integer)()
                            dataCheck = New List(Of Boolean)()
                            dataText = New List(Of String)()
                            dataAlarmtime = New List(Of Date)()
                            dataAlarmbox = New List(Of Boolean)()
                            dataDelete = New List(Of Boolean)()

                            'リストにデータが入っている場合、リストを全て削除
                            If listCheck.Count > 0 Then
                                For i As Integer = 0 To listCheck.Count - 1
                                    'TableLayoutPanelからコントロールを削除
                                    TableLayoutPanel1.Controls.Remove(listCheck(i))
                                    TableLayoutPanel1.Controls.Remove(listTextbox(i))
                                    TableLayoutPanel1.Controls.Remove(listAlarmtime(i))
                                    TableLayoutPanel1.Controls.Remove(listAlarmbox(i))
                                    TableLayoutPanel1.Controls.Remove(deleteButton(i))
                                Next
                            End If

                            'リスト用の配列から行をそれぞれ削除
                            Array.Resize(listCheck, 0)
                            Array.Resize(listTextbox, 0)
                            Array.Resize(listAlarmtime, 0)
                            Array.Resize(listAlarmbox, 0)
                            Array.Resize(deleteButton, 0)

                            '取得した情報を各配列に格納し、1行ずつリストに反映
                            Dim enableAlarmOverTimeTemp = ""
                            While reader.Read()
                                dataNumber.add(reader.GetInt16(0))
                                dataCheck.add(reader.GetBoolean(1))
                                dataText.add(reader.GetString(2))
                                dataAlarmtime.add(reader.GetDateTime(3))

                                'アラームがONになっていてアラーム時間が過ぎている対象をテキストに格納しアラームをOFFに変更、それ以外は通常通りに格納
                                If reader.GetBoolean(4) = True Then
                                    Dim timeToWait As TimeSpan = reader.GetDateTime(3) - DateTime.Now
                                    If timeToWait.TotalMilliseconds < 0 Then
                                        enableAlarmOverTimeTemp = enableAlarmOverTimeTemp + "「" & reader.GetString(2) & "」" & vbCrLf
                                        dataAlarmbox.add(False)
                                    Else
                                        dataAlarmbox.add(reader.GetBoolean(4))
                                    End If
                                Else
                                    dataAlarmbox.add(reader.GetBoolean(4))
                                End If

                                dataDelete.add(reader.GetBoolean(5))

                                'リスト反映メソッドの呼び出し
                                reflectionList()

                            End While

                            '上記でテキストに格納したアラーム時間が過ぎている対象がある場合、アラーム時間が過ぎている旨のメッセージを表示
                            If Not enableAlarmOverTimeTemp = "" Then
                                My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Asterisk)
                                MsgBox(enableAlarmOverTimeTemp & messageBootAlarmTimeOver)
                            End If
                        End Using
                    End Using
                    'MySQLへの接続失敗時のエラー処理
             　   Catch ex As MySql.Data.MySqlClient.MySqlException
                    MsgBox(errorDatabaseLoad)
                    Conn.Close()
                    Exit Sub
                End Try
                'データベースへの接続を終了
                Conn.Close()
            End Using
            '処理が完了した事のメッセージを表示
            MsgBox(messageLoadComplete)
        End If

    End Sub

    'アラーム動作メソッド
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Asterisk)
        '設定したアラーム時間にメッセージを表示
        MsgBox("「" & listTextbox(enableAlarmArray).Text & "」" & messageAlarmTime)
        'リストの対象行をアラームオフに変更
        listAlarmbox(enableAlarmArray).Text = alarmboxFalse
        '対象行のアラーム設定をオフに変更
        dataAlarmbox(enableAlarmArray) = False
        '他に設定されているアラームが無いか確認の為、アラーム更新メソッドへ移行
        updateAlarm(enableAlarmArray)
    End Sub

    'データ更新メソッド
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'タイマーのオーバーフロー対策にアプリケーションを起動してから24時間毎にアラームを再設定します。
        If dataAlarmbox.Contains(True) = True Then
            updateAlarm(enableAlarmArray)
        End If
    End Sub

End Class
