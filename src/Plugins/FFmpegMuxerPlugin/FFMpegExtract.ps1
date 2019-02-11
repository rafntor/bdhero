
cd $PSScriptRoot

$ToolPath = $args[0]
$FFMPEGZIP = Get-ChildItem -path "$ToolPath\ffmpeg*.zip"
$FFMPEGZIP = $FFMPEGZIP[0] # get first, if multiple

$SEVENZIP = Get-ChildItem -path "..\..\7z*.exe" -recurse
$SEVENZIP = $SEVENZIP[0] # get first, if multiple

#write-host $FFMPEGZIP 
#write-host $FFMPEGZIP.BaseName
$FFMPEGEXE = $FFMPEGZIP.BaseName + "\bin\ffmpeg.exe"
#write-host  $FFMPEGEXE

& $SEVENZIP e -y $FFMPEGZIP $FFMPEGEXE # extract just ffmpeg.exe
