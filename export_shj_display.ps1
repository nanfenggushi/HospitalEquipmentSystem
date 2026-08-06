$ErrorActionPreference = 'Stop'
$cs = 'Server=nanfeng.database.windows.net;Database=HospitalEquipmentDB;User Id=nanfeng;Password=qbtEtznieTWQ6Hs;Encrypt=True;TrustServerCertificate=True;Connect Timeout=15'
$outDir = 'D:\山海鲸导出文件'
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

$cn = New-Object System.Data.SqlClient.SqlConnection $cs
$cn.Open()

$sql = @"
SELECT m.RepairNo,
       e.EquipmentName,
       m.FaultType,
       m.FaultDesc,
       ISNULL(m.RepairResult, '') AS RepairResult,
       m.Status,
       m.ReportTime
FROM MaintenanceRecords m
LEFT JOIN Equipment e ON m.EquipmentId = e.EquipmentId
WHERE m.RepairNo IS NOT NULL
ORDER BY m.ReportTime DESC
"@
$cmd = $cn.CreateCommand()
$cmd.CommandText = $sql
$r = $cmd.ExecuteReader()

function Get-StatusCn($s) {
    switch ($s) {
        'Pending'     { return '待处理' }
        'Assigned'    { return '已派单' }
        'InProgress'  { return '处理中' }
        'Completed'   { return '已完成' }
        'Done'        { return '已完成' }
        'Cancelled'   { return '已取消' }
        default       { return $s }
    }
}

function Format-CsvField($value) {
    if ($value -eq $null) { return '' }
    $s = $value.ToString()
    if ($s -match '[",\r\n]') {
        $s = '"' + $s.Replace('"', '""') + '"'
    }
    return $s
}

$lines = New-Object System.Collections.Generic.List[string]
$lines.Add('DisplayText,RepairNo,EquipmentName,FaultType,FaultDesc,RepairResult,Status,ReportTime')

while ($r.Read()) {
    $repairNo   = if ($r.IsDBNull(0)) { '' } else { $r.GetString(0) }
    $eqName     = if ($r.IsDBNull(1)) { '' } else { $r.GetString(1) }
    $faultType  = if ($r.IsDBNull(2)) { '' } else { $r.GetString(2) }
    $faultDesc  = if ($r.IsDBNull(3)) { '' } else { $r.GetString(3) }
    $result     = if ($r.IsDBNull(4)) { '' } else { $r.GetString(4) }
    $status     = if ($r.IsDBNull(5)) { '' } else { $r.GetString(5) }
    $reportTime = if ($r.IsDBNull(6)) { '' } else { $r.GetDateTime(6).ToString('yyyy-MM-dd HH:mm:ss') }

    $statusCn = Get-StatusCn $status
    $resultTxt = if ($result) { $result } else { '-' }
    $display = "【$repairNo】$eqName ｜ $faultDesc ｜ 结果：$resultTxt ｜ 状态：$statusCn"

    $row = @(
        (Format-CsvField $display),
        (Format-CsvField $repairNo),
        (Format-CsvField $eqName),
        (Format-CsvField $faultType),
        (Format-CsvField $faultDesc),
        (Format-CsvField $result),
        (Format-CsvField $status),
        (Format-CsvField $reportTime)
    )
    $lines.Add(($row -join ','))
}
$r.Close()
$cn.Close()

$file = Join-Path $outDir 'MaintenanceRecords_展示.csv'
$gbk = [System.Text.Encoding]::GetEncoding(936)
[System.IO.File]::WriteAllLines($file, $lines, $gbk)
Write-Output ("rows: {0}  ->  {1}" -f ($lines.Count - 1), $file)
Write-Output 'DONE'
