

using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SharpFbric.Applacation
{
    public class MySshClient : IMySshClient
    {
        SshClient ssh;
        SftpClient sftp;
        public void Connect()
        {
            if (ssh == null)
            {
                ssh = new SshClient("99.13.135.184", "qadmsom", "Fhmb7rV%");
                ssh.Connect();
            }
            if (sftp == null)
            {
                sftp = new SftpClient("99.13.135.184", "qadmsom", "Fhmb7rV%");
                sftp.Connect();
            }

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


    }

}