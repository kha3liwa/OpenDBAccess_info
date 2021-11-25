Imports System.Data.OleDb
Imports System.Data
Imports System.IO


Public Class Form1
    'Private ReadOnly dt As String
    Public conn As New OleDbConnection
    Public ConnectionString As String
    Public cmd As New OleDbCommand
    Public dt As New DataTable
    'الاتصال بقاعده البيانات 

    Dim providerACE12 As String = "Provider = Microsoft.ACE.OLEDB.12.0;data source=  "
    Dim providerJet4 As String = "Provider=Microsoft.Jet.OLEDB.4.0;data source=  "
    Dim ACE_OLEDB12_withpass As String = "Jet OLEDB:Database Password=MyDbPassword "
    'Dim MyDbPassword As String = InputBox("Enter your password")
    Dim ds As DataSet
    Dim da As OleDbDataAdapter

    Private Sub bt4_Click(sender As Object, e As EventArgs) Handles bt4.Click

        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()

        ofd.Filter = "Access Databases|*.mdb"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Settings.conStr = ofd.FileName
            My.Settings.Save()
        Else
            Me.Hide()
        End If
        TitleListBox.DataSource = Nothing

        ConnectionString = providerJet4 & My.Settings.conStr & ";"
        Using conn As New OleDbConnection(ConnectionString)

            conn.Open()
            Dim dtNames As DataTable = conn.GetSchema("Tables", New String() {Nothing, Nothing, Nothing, "TABLE"})

            TitleListBox.DataSource = dtNames
            TitleListBox.DisplayMember = "TABLE_NAME"

            Label4.Text = TitleListBox.Items.Count

            txtPath.Text = ofd.FileName
            txtName.Text = Path.GetDirectoryName(ofd.FileName)
            txtExt.Text = Path.GetExtension(ofd.FileName)

        End Using
    End Sub

    Private Sub bt12_Click(sender As Object, e As EventArgs) Handles bt12.Click
        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()
        ofd.Filter = "Access Databases|*.accdb"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Settings.conStr = ofd.FileName
            My.Settings.Save()
        Else
            End
        End If
        TitleListBox.DataSource = Nothing

        ConnectionString = providerACE12 & My.Settings.conStr & ";"

        Using conn As New OleDbConnection(ConnectionString)

            conn.Open()
            Dim dtNames As DataTable = conn.GetSchema("Tables", New String() {Nothing, Nothing, Nothing, "TABLE"})

            TitleListBox.DataSource = dtNames
            TitleListBox.DisplayMember = "TABLE_NAME"

            Label4.Text = TitleListBox.Items.Count

            txtPath.Text = ofd.FileName
            txtName.Text = Path.GetDirectoryName(ofd.FileName)
            txtExt.Text = Path.GetExtension(ofd.FileName)
            conn.Close()

        End Using
    End Sub

    Private Sub TitleListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TitleListBox.SelectedIndexChanged

        TextBox2.Text = TitleListBox.GetItemText(TitleListBox.SelectedItem)
        Label5.Text = TitleListBox.SelectedIndex
        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()
        TextBox1.Clear()
        TextBox1.Refresh()
        conn.Close()
        dt.Clear()
        ''columns count
        'Dim name(dt.Columns.Count) As String
        'Dim i As Integer = 0
        'For Each column As DataColumn In dt.Columns
        '    name(i) = column.ColumnName
        '    i += 1
        '    TextBox1.Text = i
        '    DataGridView1.Refresh()
        '    'i = 0
        'Next
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Application.Exit()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()
        Dim conn As New System.Data.OleDb.OleDbConnection(ConnectionString)
        Try
            conn.Close()
            DataGridView1.DataSource = Nothing
            DataGridView1.Refresh()
            dt.Clear()
            cmd = New OleDbCommand("SELECT * FROM " + TextBox2.Text + "", conn)
            conn.Open()
            dt.Load(cmd.ExecuteReader)
            conn.Close()
            'DataGridView1.Refresh()
            DataGridView1.DataSource = dt
            DataGridView1.Refresh()
        Catch ex As Exception
            MsgBox("يجب تحديد قاعدة البيانات " & vbCrLf & " اختر احد الخيارين" & vbCrLf & "DB_providerACE12 " & vbCrLf & "DB_providerJet4 ", Nothing, "تحديد قاعدة البيانات")

        End Try
        conn.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn.Close()
        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()
        dt.Clear()
    End Sub
End Class
