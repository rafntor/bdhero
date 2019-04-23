
# https://mkvtoolnix.download/windows/releases/31.0.0/mkvtoolnix-64-bit-31.0.0.7z

cd $PSScriptRoot\..\Tools

$VERSION = "31.0.0"
$SOURCE = "https://mkvtoolnix.download/windows/releases/$VERSION/mkvtoolnix-64-bit-$VERSION.7z"
$DESTFILE = "mkvtoolnix-$VERSION.7z"

# workaround ssl/tls error when invoked by travis
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

if (test-path $DESTFILE)
{
  write-host "mkvtoolnix already exist, skipping download"
}
else
{
  write-host "downloading mkvtoolnix"
  Invoke-WebRequest -OutFile $DESTFILE $SOURCE
}
