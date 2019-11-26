using SFbric.FClient;
using SFbric.FTask;
using System;
using System.IO;
using System.Threading;

namespace Fbric
{
    class xxProgram
    {
        static void xxMain(string[] args)
        {
            Console.WriteLine("Fbric Hello World!");
             

            if (args.Length < 1)
            {
                Console.WriteLine("Hello World! xxxx");
                return;
            }
            var changeSet = args[0];
            var env = "";
            if (args.Length > 1)
            {
                env = args[1];
            }
            switch (env)
            {
                case "sit":
                    SitTask(changeSet);
                    break;
                case "test":
                    TestTask(changeSet);
                    break;
                default:
                    Console.WriteLine($"Hello {env}!");
                    break;
            }

        }

        static void SitTask(string changeset)
        {
            Console.WriteLine($"开始部署 SIT  {changeset}");
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
            var remoteBasePath = "/home";


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


            Console.WriteLine($"APIS 开始上传……");
            apisFile.RunTaskAsync();
            Console.WriteLine($"APIS 上传完成");

            Console.WriteLine($"WEBS 开始上传……");
            websFile.RunTaskAsync();
            Console.WriteLine($"WEBS 上传完成");

            // 执行命令
            var apisCmd = new FbricCmdTask(groupApis);
            var websCmd = new FbricCmdTask(groupWebs);

            apisCmd.AppendCommand($"cd {remoteBasePath}");
            apisCmd.AppendCommand("supervisorctl stop all");
            apisCmd.AppendCommand("unzip -O CP936 apis.zip");
            apisCmd.AppendCommand("mv zhdj71-sit zhdj71-sit-old");
            apisCmd.AppendCommand("mv apis zhdj71-sit");

            apisCmd.AppendCommand("supervisorctl restart DataGather");
            apisCmd.AppendCommand("supervisorctl restart DemocracyOrgApi");
            apisCmd.AppendCommand("supervisorctl restart DemocraticLifesApi");
            apisCmd.AppendCommand("supervisorctl restart MessageCenter");
            apisCmd.AppendCommand("supervisorctl restart MyVoteApi");
            apisCmd.AppendCommand("supervisorctl restart PartyMemberConcern");
            apisCmd.AppendCommand("supervisorctl restart PartyOrgApi");
            apisCmd.AppendCommand("supervisorctl restart QuartzService");
            apisCmd.AppendCommand("supervisorctl restart Questionnaire");
            apisCmd.AppendCommand("supervisorctl restart ReportAppraisedApi");
            apisCmd.AppendCommand("supervisorctl restart SecurityApi");
            apisCmd.AppendCommand("supervisorctl restart TenantWeb");
            apisCmd.AppendCommand("supervisorctl restart WebApi");
            apisCmd.AppendCommand("supervisorctl restart PartyWork");
            apisCmd.AppendCommand("supervisorctl restart PartyMember");


            Console.WriteLine($"APIS 开始解压重启……");
            apisCmd.RunTaskAsync();
            Console.WriteLine($"APIS 重启完成");

            websCmd.AppendCommand($"cd {remoteBasePath}");
            websCmd.AppendCommand("supervisorctl stop all");
            websCmd.AppendCommand("unzip -O CP936 webs.zip");
            websCmd.AppendCommand("mv zhdj71-sit zhdj71-sit-old");
            websCmd.AppendCommand("mv webs zhdj71-sit");

            websCmd.AppendCommand("supervisorctl restart ApiGateway");
            websCmd.AppendCommand("supervisorctl restart IdentityServer");
            websCmd.AppendCommand("supervisorctl restart InternalApiGateway");
            websCmd.AppendCommand("supervisorctl restart Services");
            websCmd.AppendCommand("supervisorctl restart WebMvc");

            Console.WriteLine($"WEBS 开始解压重启……");
            websCmd.RunTaskAsync();
            Console.WriteLine($"WEBS 重启完成");

        }

        static void TestTask(string changeset)
        {
            Console.WriteLine($"开始部署 test-SIT  {changeset}");
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


            Console.WriteLine($"APIS 开始上传……");
            apisFile.RunTaskAsync();
            Console.WriteLine($"APIS 上传完成");

            Console.WriteLine($"WEBS 开始上传……");
            websFile.RunTaskAsync();
            Console.WriteLine($"WEBS 上传完成");

            // 执行命令
            var apisCmd = new FbricCmdTask(groupApis);
            var websCmd = new FbricCmdTask(groupWebs);

            apisCmd.AppendCommand($"cd {remoteBasePath}");
            // apisCmd.AppendCommand("supervisorctl stop all");
            apisCmd.AppendCommand("unzip -O CP936 apis.zip");
            apisCmd.AppendCommand("mv zhdj71-sit zhdj71-sit-old");
            apisCmd.AppendCommand("mv apis zhdj71-sit");
            apisCmd.AppendCommand("supervisorctl status");

            Console.WriteLine($"APIS 开始解压重启……");
            apisCmd.RunTaskAsync();
            Console.WriteLine($"APIS 重启完成");

            websCmd.AppendCommand($"cd {remoteBasePath}");
            // websCmd.AppendCommand("supervisorctl stop all");
            websCmd.AppendCommand("unzip -O CP936 webs.zip");
            websCmd.AppendCommand("mv zhdj71-sit zhdj71-sit-old");
            websCmd.AppendCommand("mv webs zhdj71-sit");
            websCmd.AppendCommand("supervisorctl status");

            Console.WriteLine($"WEBS 开始解压重启……");
            websCmd.RunTaskAsync();
            Console.WriteLine($"WEBS 重启完成");

        }
    }
}
