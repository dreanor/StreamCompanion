using System;
using System.IO;
using System.Linq;
using DropNet;

namespace com.gmail.mikeundead.streamcompanion.controller
{
    public static class DropBoxHandler
    {
        public static void Upload(Guid userId, DropNetClient dropNetClient)
        {
            dropNetClient.UseSandbox = true;
            var meta = dropNetClient.GetMetaData();

            var found = false;
            foreach (var content in meta.Contents.Where(content => content.Name == userId.ToString()))
            {
                found = true;
            }

            if (!found)
            {
                dropNetClient.CreateFolderAsync(userId.ToString(), (metaData) => { }, (error) => { });
            }

            var data = ReadFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings\streams.json"));
            dropNetClient.UploadFileAsync("/" + userId, "stream.json", data, (response) => { }, (error) => { });
        }

        private static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
    }
}
