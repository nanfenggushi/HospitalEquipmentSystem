$ErrorActionPreference = 'Stop'
$cs = 'Server=nanfeng.database.windows.net;Database=HospitalEquipmentDB;User Id=nanfeng;Password=qbtEtznieTWQ6Hs;Encrypt=True;TrustServerCertificate=True;Connect Timeout=15'
$outDir = 'D:\' + [char]0x5C71 + [char]0x6D77 + [char]0x9CB2 + [char]0x5BFC + [char]0x51FA + [char]0x6587 + [char]0x4EF6
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

$cn = New-Object System.Data.SqlClient.SqlConnection $cs
$cn.Open()

$cmd = $cn.CreateCommand()
$cmd.CommandText = 'SELECT name FROM sys.tables ORDER BY name'
$tables = @()
$r = $cmd.ExecuteReader()
while ($r.Read()) { $tables += $r['name'].ToString() }
$r.Close()

function Format-CsvField($value) {
    if ($value -eq $null) { return '' }
    $s = $value.ToString()
    if ($s -match '[",\r\n]') {
        $s = '"' + $s.Replace('"', '""') + '"'
    }
    return $s
}

$gbk = [System.Text.Encoding]::GetEncoding(936)
foreach ($t in $tables) {
    $cmd.CommandText = "SELECT * FROM [$t]"
    $r = $cmd.ExecuteReader()
    $cols = @()
    for ($i = 0; $i -lt $r.FieldCount; $i++) { $cols += $r.GetName($i) }
    $lines = New-Object System.Collections.Generic.List[string]
    $header = ($cols | ForEach-Object { Format-CsvField $_ }) -join ','
    $lines.Add($header)
    while ($r.Read()) {
        $fields = New-Object System.Collections.Generic.List[string]
        for ($i = 0; $i -lt $r.FieldCount; $i++) {
            if ($r.IsDBNull($i)) {
                $fields.Add('')
            } else {
                $v = $r.GetValue($i)
                if ($v -is [System.DateTime]) { $v = $v.ToString('yyyy-MM-dd HH:mm:ss') }
                $fields.Add((Format-CsvField $v))
            }
        }
        $lines.Add(($fields -join ','))
    }
    $r.Close()
    $file = Join-Path $outDir ($t + '.csv')
    [System.IO.File]::WriteAllLines($file, $lines, $gbk)
    Write-Output ("{0,-22} {1} rows  ->  {2}" -f $t, ($lines.Count - 1), $file)
}
$cn.Close()
Write-Output 'DONE'
