using SFbric.FClient; 
using SFbric.FTask;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace SFbric.XUnitTest
{
    public class FbricClientTest
    {
        [Fact]
        public void TestCmdClient()
        {
            var config = new FbricClientConfig()
            {
                Host = "99.13.135.181",
                UserName = "qadmsom",
                Password = "Fhmb7rV%"
            };
            using (var client = new CmdClient(config))
            {
                client.AppendCommand("cd /home/");
                client.AppendCommand("mkdir test-sfbric");
                client.AppendCommand("ls");
                client.AppendCommand("pwd");

                var res = client.RunCommand();

                Assert.True(true, res);
            }
        }
        [Fact]
        public void TestFileClient()
        {
            var config = new FbricClientConfig()
            {
                Host = "99.13.135.181",
                UserName = "qadmsom",
                Password = "Fhmb7rV%"
            };
            using (var client = new FileClient(config))
            {
                var filePath = @"D:\publish\2019-08-06\zhdj71-sit-ad3c65523";
                var fileName = "apis.zip";

                FileInfo fi = new FileInfo(Path.Combine(filePath, fileName));
                var allLength = fi.Length;
                using (var stream = new System.IO.FileStream(fi.FullName,
                    System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
                {
                    client.UploadFile(stream, $"/home/test-sfbric/{fileName}", (pro) =>
                   {
                       Console.WriteLine((pro * 1.0d / allLength * 1.0d).ToString("P"));
                   });
                }
                Assert.True(true);
            }
        }


        [Fact]
        public void TestFbricTask()
        {
            var changeset = "d9724c454"; 

            var groupWebs = new FbricClientConfig[] {
                new   FbricClientConfig () {
                    Host = "99.13.135.181",
                    UserName = "qadmsom",
                    Password = "Fhmb7rV%"
                },
                new   FbricClientConfig () {
                    Host = "99.13.135.182",
                    UserName = "qadmsom",
                    Password = "Fhmb7rV%"
                },
            };
            var groupApis = new FbricClientConfig[] {
                new   FbricClientConfig () {
                    Host = "99.13.135.183",
                    UserName = "qadmsom",
                    Password = "Fhmb7rV%"
                },
                new   FbricClientConfig () {
                    Host = "99.13.135.184",
                    UserName = "qadmsom",
                    Password = "Fhmb7rV%"
                },
            };

            var publishOutputPath = $@"D:\publish\{DateTime.Now.ToString("yyyy-MM-dd")}\zhdj71-sit-{changeset}";
            var remoteBasePath = "/home/test";


            var websOption = new FileOption()
            {
                LocalFilePath = Path.Combine(publishOutputPath, "webs.zip"),
                RemoteFilePath = $"{remoteBasePath}/webs.zip"
            };
            var apisOption = new FileOption()
            {
                LocalFilePath = Path.Combine(publishOutputPath, "apis.zip"),
                RemoteFilePath = $"{remoteBasePath}/apis.zip"
            };

            // 上传文件
            var websFile = new FbricFileTask(groupWebs, websOption);
            var apisFile = new FbricFileTask(groupApis, apisOption);

            // 命令 
            apisFile.AppendCommand($"cd {remoteBasePath}");
            apisFile.AppendCommand("rm -rf zhdj71-sit-old");
            apisFile.AppendCommand("rm -rf apis.zip");

            websFile.AppendCommand($"cd {remoteBasePath}");
            websFile.AppendCommand("rm -rf zhdj71-sit-old");
            websFile.AppendCommand("rm -rf webs.zip");


            apisFile.RunTaskAsync();
            websFile.RunTaskAsync();

            // 执行命令
            var apisCmd = new FbricCmdTask(groupApis);
            var websCmd = new FbricCmdTask(groupWebs);

            apisCmd.AppendCommand($"cd {remoteBasePath}");
            apisCmd.AppendCommand("supervisorctl status");
            apisCmd.AppendCommand("unzip -O CP936 *.zip");
            apisCmd.AppendCommand("mv zhdj71-sit zhdj71-sit-old");
            apisCmd.AppendCommand("mv apis zhdj71-sit");
            apisCmd.AppendCommand("supervisorctl status");
            apisCmd.RunTaskAsync();

            websCmd.AppendCommand($"cd {remoteBasePath}");
            websCmd.AppendCommand("supervisorctl status");
            websCmd.AppendCommand("unzip -O CP936 *.zip");
            websCmd.AppendCommand("mv zhdj71-sit zhdj71-sit-old");
            websCmd.AppendCommand("mv webs zhdj71-sit");
            websCmd.AppendCommand("supervisorctl status");
            websCmd.RunTaskAsync();

        }
    }
}
