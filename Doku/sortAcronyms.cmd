@echo off 
    setlocal enableextensions disabledelayedexpansion

    set "search={\textunderscore }"
    set "replace=_"

    set "textFile=bibliographie_Joern.bib"

    for /f "delims=" %%i in ('type "%textFile%" ^& break ^> "%textFile%" ') do (
        set "line=%%i"
        setlocal enabledelayedexpansion
        >>"%textFile%" echo(!line:%search%=%replace%!
        endlocal
    )
sort ads/acronyms.tex > ads/sortedAcronyms.tex
exit