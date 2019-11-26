using SFbric.FClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SFbric.FTask
{

    public class FbricCmdTask : FbricTask
    {
        readonly FbricClientConfig[] _configs;
        readonly List<string> commandList;
        public FbricCmdTask(FbricClientConfig[] configs)
        {
            _configs = configs;
            commandList = new List<string>();
        }

        public void AppendCommand(string line)
        {
            commandList.Add(line);
        }
        public void RunTask()
        {
            foreach (var item in _configs)
            {
                RunSigleClient(item);
            }
            commandList.Clear();
        }

        public void RunTaskAsync()
        {
            var taskList = new List<Task>();
            foreach (var item in _configs)
            {
                var task = Task.Run(() => { RunSigleClient(item); });
                taskList.Add(task);
            } 
            Task.WaitAll(taskList.ToArray());

            commandList.Clear();
        } 
        private string RunSigleClient(FbricClientConfig config)
        {
            using (CmdClient client = new CmdClient(config))
            {
                foreach (var item in commandList)
                {
                    client.AppendCommand(item);
                } 
                var res = client.RunCommand(); 
                return res;
            }
        }
    }
}

