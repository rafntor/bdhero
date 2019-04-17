
# https://ffmpeg.zeranoe.com/builds/win64/static/?C=M&O=D
# https://ffmpeg.zeranoe.com/builds/win64/static/ffmpeg-4.1-win64-static.zip

cd $PSScriptRoot\..\Tools

$VERSION = "4.1"
$DESTFILE = "ffmpeg-$VERSION-win64-static.zip"
$SOURCE = "https://ffmpeg.zeranoe.com/builds/win64/static/$DESTFILE"

if (test-path $DESTFILE)
{
  write-host "ffmpeg already exist, skipping download"
}
else
{
  write-host "downloading ffmpeg"
  Invoke-WebRequest -OutFile $DESTFILE $SOURCE
}
