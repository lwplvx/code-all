

using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SharpFbric.Applacation
{
    public class FbricClient : IMySshClient, IDisposable
    {
        readonly SshClient ssh;
        readonly SftpClient sftp;
        public FbricClient(string host, string username, string password)
        {
            ssh = new SshClient(host, username, password);
            sftp = new SftpClient(host, username, password);
        }
        public FbricClient(ClientConfig config)
            : this(config.Host, config.UserName, config.Password)
        {
        }

        public void Connect()
        {
            ssh.Connect();
            sftp.Connect();
        }
        public List<string> TestCommand()
        {
            // var cmd = ssh.RunCommand("pwd");
            var list = new List<string>
            {
                RunCommand("whoami"),
                RunCommand("cd /home;ls")
            };

            return list;
        }
        public void TestSendFile()
        {

            var filePath = @"C:\Users\wr801024\Desktop\Untitled-1.json";

            //using (var fs = new System.IO.FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            //{
            //    var len = SendFileOverSftp(fs, "/home");
            //    Console.WriteLine($"uploadLength:{len}");
            //}


            FileInfo fi = new FileInfo(filePath);
            var allLength = fi.Length;
            sftp.UploadFile(new System.IO.FileStream(fi.FullName,
                System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite),
                $"/home/Untitled-{DateTime.Now.Ticks}.json", (pro) =>
                {
                    Console.WriteLine((pro * 1.0d / allLength * 1.0d).ToString("P"));
                });
            Console.WriteLine("finished.");

        }

        public ulong SendFileOverSftp(Stream stream, string remoteFilePath)
        {
            ulong uploadLength = 0;

            sftp.UploadFile(stream, remoteFilePath, (uploadUlong) =>
            {
                // Console.WriteLine((pro * 1.0d / allLength * 1.0d).ToString("P"));
                uploadLength = uploadUlong;

            });

            return uploadLength;
        }


        public void Disconnect()
        {
            if (ssh != null && ssh.IsConnected)
            {
                ssh.Disconnect();
            }
            if (sftp != null && sftp.IsConnected)
            {
                sftp.Disconnect();
            }
        }
        private string RunCommand(string line)
        {
            var resultStr = string.Empty;
            var cmd = ssh.RunCommand(line);
            if (!string.IsNullOrWhiteSpace(cmd.Error))
                resultStr += cmd.Error;
            else
                resultStr += cmd.Result;

            return $"{line}:{resultStr}";
        }

        public void Dispose()
        {
            Disconnect();
            if (ssh != null)
            {
                ssh.Dispose();
            }
            if (sftp != null)
            {
                sftp.Dispose();
            }
        }
    }
    public class ClientConfig
    {
        public string UserName { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
    }

    public class FbricClientManager
    {

        public void Test()
        {
            var config = new ClientConfig() { };


            using (FbricClient client = new FbricClient(config))
            {

            }

        }
    }

    public abstract class FbricTask
    {

    }

    public class FbricCommandTask : FbricTask
    {

    }

    public class FbricFileTask : FbricTask
    {

    }
}