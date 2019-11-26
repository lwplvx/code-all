using System.Collections.Generic;
using System.IO;

namespace SharpFbric.Applacation
{
    public interface IMySshClient
    {
        void Connect();
        void Disconnect();
        ulong SendFileOverSftp(Stream stream, string remoteFilePath);
        List<string> TestCommand();
        void TestSendFile();
    }
}