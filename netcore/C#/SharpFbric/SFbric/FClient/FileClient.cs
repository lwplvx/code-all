using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;

namespace SFbric.FClient
{
    public class FileClient : IDisposable
    {
        readonly SftpClient sftp;
        public FileClient(string host, string username, string password)
        {
            sftp = new SftpClient(host, username, password);
        }
        public FileClient(FbricClientConfig config)
            : this(config.Host, config.UserName, config.Password)
        {
            Connect();
        }

        public void Connect()
        {
            if (!sftp.IsConnected)
            {
                sftp.Connect();
            }
        }


        public void TestSendFile()
        {

            var filePath = @"C:\Users\wr801024\Desktop\Untitled-1.json";


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

        public void UploadFile(Stream stream, string remoteFilePath, Action<ulong> uploadCallback = null)
        {
            sftp.UploadFile(stream, remoteFilePath, uploadCallback);
        }

        public void UploadFile(FileOption fileOption, Action<ulong> uploadCallback = null)
        {
            Console.WriteLine($"{sftp.ConnectionInfo.Host} 开始上传, LocalFilePath:{fileOption.LocalFilePath}……");
            FileInfo fi = new FileInfo(fileOption.LocalFilePath);
            var allLength = fi.Length;

            using (var fs = new System.IO.FileStream(fileOption.LocalFilePath,
                System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
            {
                sftp.UploadFile(fs, fileOption.RemoteFilePath, (pro =>
                {
                    Console.WriteLine($"{sftp.ConnectionInfo.Host} 上传进度 {(pro * 1.0d / allLength * 1.0d).ToString("P")}");
                    uploadCallback?.Invoke(pro);
                }));
            }
            Console.WriteLine($"{sftp.ConnectionInfo.Host} 上传结束, RemoteFilePath:{fileOption.RemoteFilePath}");
        }

        public void Disconnect()
        {
            if (sftp != null && sftp.IsConnected)
            {
                sftp.Disconnect();
            }
        }



        public void Dispose()
        {
            Disconnect();

            if (sftp != null)
            {
                sftp.Dispose();
            }
        }
    }

    public class FileOption
    {
        /// <summary>
        /// 此路径需要带有文件名
        /// </summary>
        public string RemoteFilePath { get; set; }

        /// <summary>
        /// 此路径需要带有文件名
        /// </summary>
        public string LocalFilePath { get; set; }

    }

}

