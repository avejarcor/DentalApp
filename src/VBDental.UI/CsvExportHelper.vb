Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

''' <summary>Exporta el contenido visible de un DataGridView a un archivo CSV.</summary>
Public Module CsvExportHelper

    Public Sub ExportarACsv(dgv As DataGridView, nombreArchivoSugerido As String)
        If dgv.Rows.Count = 0 Then
            MessageBox.Show("No hay datos para exportar.", "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using dialogo As New SaveFileDialog()
            dialogo.Filter = "Archivo CSV (*.csv)|*.csv"
            dialogo.FileName = nombreArchivoSugerido

            If dialogo.ShowDialog() <> DialogResult.OK Then Return

            Using writer As New StreamWriter(dialogo.FileName, False, Encoding.UTF8)
                Dim encabezados = dgv.Columns.Cast(Of DataGridViewColumn)().
                    Where(Function(c) c.Visible).
                    Select(Function(c) EscaparCampo(c.HeaderText))
                writer.WriteLine(String.Join(",", encabezados))

                For Each fila As DataGridViewRow In dgv.Rows
                    If fila.IsNewRow Then Continue For
                    Dim valores = dgv.Columns.Cast(Of DataGridViewColumn)().
                        Where(Function(c) c.Visible).
                        Select(Function(c) EscaparCampo(If(fila.Cells(c.Index).Value?.ToString(), "")))
                    writer.WriteLine(String.Join(",", valores))
                Next
            End Using

            MessageBox.Show("Exportación completada.", "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Using
    End Sub

    Private Function EscaparCampo(valor As String) As String
        If valor.Contains(",") OrElse valor.Contains(""""c) OrElse valor.Contains(vbLf) OrElse valor.Contains(vbCr) Then
            Return """" & valor.Replace("""", """""") & """"
        End If
        Return valor
    End Function

End Module
