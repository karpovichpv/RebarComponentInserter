$date = Get-Date -Format "yyyy-MM-dd_HH-mm-ss"
$outputPath = Join-Path $PSScriptRoot "RebarComponentInserter\Resources\BuildDate.md"
New-Item -ItemType Directory -Force -Path (Split-Path $outputPath) | Out-Null
Set-Content -Path $outputPath -Value $date -NoNewline
