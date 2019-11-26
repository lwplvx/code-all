using SFbric.FClient;
using SFbric.FTask;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Fbric
{
    class Program
    {
        static string helpInfo =
           $"--help Help\r\n" +
           $"[mode(ssh)] [host,host2,...] [username] [password] [command]\r\n" +
           $"[mode(sftp)] [host,host2,...] [username] [password] [localfile] [remotefile]\r\n" +
           $"--mode SSH,SFTP\r\n" +
           $"--host 远程地址\r\n" +
           $"--username UserName\r\n" +
           $"--password Password\r\n" +
           $"--command Command\r\n" +
           $"--localfile 本地文件路径\r\n" +
           $"--remotefile 远程文件路径\r\n";

        static string sshHelpInfo =
           $"--help Help\r\n" +
           $"[mode(ssh)] [host,host2,...] [username] [password] [command]\r\n" +
           $"--mode SSH,SFTP\r\n" +
           $"--host 远程地址\r\n" +
           $"--username UserName\r\n" +
           $"--password Password\r\n" +
           $"--command Command\r\n";

        static string sftpHelpInfo =
           $"--help Help\r\n" +
           $"[mode(sftp)] [host,host2,...] [username] [password] [localfile] [remotefile]\r\n" +
           $"--mode SSH,SFTP" +
           $"--host 远程地址\r\n" +
           $"--username UserName\r\n" +
           $"--password Password\r\n" +
           $"--localfile 本地文件路径\r\n" +
           $"--remotefile 远程文件路径\r\n";


        static void Main(string[] args)
        {

            Console.WriteLine("已进入 fbric ");


            if (args.Length > 0)
            {
                DoArgs(args);
            }
            else
            {
                DoReadLineArgs();
                Console.WriteLine("输入任意键退出……");
                Console.ReadKey();
            }

        }

        private static void DoArgs(string[] args)
        {
            try
            {
                var mode = args[0];

                switch (mode)
                {
                    case "ssh":
                        DoSSH(args);
                        break;
                    case "sftp":
                        DoSFTP(args);
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(helpInfo);
            }

        }

        private static void DoSSH(string[] args)
        {
            try
            {
                //   $"[mode(ssh)] [host] [username] [password] [command]" +
                var host = args[1];
                var hosts = new string[] { host };
                if (host.Contains(","))
                {
                    hosts = host.Split(",");
                }
                var username = args[2];
                var password = args[3];
                var command = args[4];


                var clientConfs = new List<FbricClientConfig>();
                foreach (var item in hosts)
                {
                    clientConfs.Add(new FbricClientConfig()
                    {
                        Host = item.Trim(),
                        UserName = username,
                        Password = password
                    });
                }

                var clientCmd = new FbricCmdTask(clientConfs.ToArray());

                clientCmd.AppendCommand(command);

                Console.WriteLine($"开始执行 {host} -> {command}");
                clientCmd.RunTaskAsync();
                Console.WriteLine($"{host} 执行完成。");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(sshHelpInfo);
            }

        }

        private static void DoSFTP(string[] args)
        {
            try
            {
                //  $"[mode(sftp)] [host,host2,...] [username] [password] [localfile] [remotefile]\r\n" +
                var host = args[1];
                var hosts = new string[] { host };
                if (host.Contains(","))
                {
                    hosts = host.Split(",");
                }
                var username = args[2];
                var password = args[3];
                var localfile = args[4];
                var remotefile = args[5];


                var clientConfs = new List<FbricClientConfig>();
                foreach (var item in hosts)
                {
                    clientConfs.Add(new FbricClientConfig()
                    {
                        Host = item.Trim(),
                        UserName = username,
                        Password = password
                    });
                }

                var fieOption = new FileOption() {
                     LocalFilePath= localfile,
                     RemoteFilePath= remotefile
                };

                var clientSftp = new FbricFileTask(clientConfs.ToArray(), fieOption);
                  
                Console.WriteLine($"开始执行 {host} -> {remotefile}");
                clientSftp.RunTaskAsync();
                Console.WriteLine($"{host} 执行完成。"); 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(sftpHelpInfo);
            }

        }


        private static void DoReadLineArgs()
        {
            var readStr = Console.ReadLine();
            var args = readStr.Split("--");
            while (readStr != "exit")
            {
                Console.WriteLine("输入 exit 可以退出");
                readStr = Console.ReadLine();
            }
        }
    }
}
