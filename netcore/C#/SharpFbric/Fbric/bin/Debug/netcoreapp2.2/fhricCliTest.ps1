

D:
cd D:\workspaces\code-all\netcore\C#\SharpFbric\Fbric\bin\Debug\netcoreapp2.2


dotnet Fbric.dll ssh 99.13.135.181,99.13.135.182 qadmsom Fhmb7rV% "cd /home;ls;mkdir test-fbric-cli;"
dotnet Fbric.dll ssh 99.13.135.181,99.13.135.182 qadmsom Fhmb7rV% "cd /home;ls;"

# 上传文件
$localFilePaht="C:\\Users\\wr801024\\Pictures\123456.Jpg"

dotnet Fbric.dll sftp 99.13.135.183,99.13.135.184 qadmsom Fhmb7rV% $localFilePaht "/home/123456.Jpg"