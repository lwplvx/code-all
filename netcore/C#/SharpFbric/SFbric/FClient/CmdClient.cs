using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;

namespace SFbric.FClient
{
    public class CmdClient : IDisposable
    {
        readonly SshClient ssh;
        readonly List<string> commandList;
        public CmdClient(string host, string username, string password)
        {
            ssh = new SshClient(host, username, password);
            commandList = new List<string>();
            Connect();
        }
        public CmdClient(FbricClientConfig config)
            : this(config.Host, config.UserName, config.Password)
        {
        }

        public void Connect()
        {
            if (!ssh.IsConnected)
            {
                ssh.Connect();
            }
        }

        public void Disconnect()
        {
            if (ssh != null && ssh.IsConnected)
            {
                ssh.Disconnect();
            }
        }

        public void AppendCommand(string line)
        {
            commandList.Add(line);
        }

        public string RunCommand()
        {
            if (commandList.Count > 0)
            {
                var cmdStr = string.Join(";", commandList);
                Console.WriteLine($"{ssh.ConnectionInfo.Host} 开始执行命令……");
                var res = RunCommand(cmdStr);
                Console.WriteLine($"{ssh.ConnectionInfo.Host} 执行结束：{res}");
                return res;
            }
            return string.Empty;
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
        }
    }
    public class FbricClientConfig
    {
        public string UserName { get; set; }
        public string Host { get; set; }
        //  public string[] Hosts { get; set; }
        public string Password { get; set; }
    }


}

