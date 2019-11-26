
using SFbric.FClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SFbric.FTask
{

    public class FbricFileTask : FbricTask
    {

        readonly FbricClientConfig[] _configs;
        readonly FileOption _fileOption;
        readonly List<string> commandList;
        public FbricFileTask(FbricClientConfig[] configs, FileOption fileOption)
        {
            commandList = new List<string>();
            _configs = configs;
            _fileOption = fileOption;
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
        private void RunSigleClient(FbricClientConfig config)
        {
            if (commandList.Count > 0)
            {
                using (CmdClient client = new CmdClient(config))
                {
                    foreach (var item in commandList)
                    {
                        client.AppendCommand(item);
                    }
                    var res = client.RunCommand();

                }
            }


            using (FileClient client = new FileClient(config))
            {
                client.UploadFile(_fileOption);
            }
        }
    }
}

