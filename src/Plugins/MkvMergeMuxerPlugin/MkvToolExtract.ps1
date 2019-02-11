
cd $PSScriptRoot

$ToolPath = $args[0]
$MKVTOOLNIX = Get-ChildItem -path "$ToolPath\mkvtoolnix*.7z"
$MKVTOOLNIX = $MKVTOOLNIX[0] # get first, if multiple

$SEVENZIP = Get-ChildItem -path "..\..\7z*.exe" -recurse
$SEVENZIP = $SEVENZIP[0] # get first, if multiple

& $SEVENZIP e -y $MKVTOOLNIX mkvtoolnix/mkvmerge.exe # extract just mkvmerge.exe
