$html = Get-Content -Raw -Encoding UTF8 "Particles.html"
$js   = Get-Content -Raw -Encoding UTF8 "particles.min.js"
$css  = Get-Content -Raw -Encoding UTF8 "particles.min.css"

$newHtml = $html -replace '<script src="particles.min.js"></script>', "<script>$js</script>" -replace '<link rel="stylesheet" href="particles.min.css">', "<style>$css</style>"

Write-Output $newHtml
