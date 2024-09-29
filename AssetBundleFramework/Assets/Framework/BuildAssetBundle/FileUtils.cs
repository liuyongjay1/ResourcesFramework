using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;


    public class WaitForCopyDone : CustomYieldInstruction
    {
        public volatile int finishedCount = 0;        
        public override bool keepWaiting => !isDone;

        public bool isDone { get; set; }
    }

    public static class FileUtils
    {
        public static void CopyDirectInfo(string sourceDir, string toDir, List<string> includeTypes = null)
        {
            if (!Directory.Exists(sourceDir))
            {
                throw new ApplicationException("Source directory does not exist");
            }
           
            if (!Directory.Exists(toDir))
            {
                Directory.CreateDirectory(toDir);
            }
            
            DirectoryInfo directInfo = new DirectoryInfo(sourceDir);
            //copy files
            FileInfo[] filesInfos = directInfo.GetFiles();
            foreach (FileInfo fileinfo in filesInfos)
            {
                string fileName = fileinfo.Name;
                // Debug.Log($"fileName is {fileName}");
                if (includeTypes != null && includeTypes.Count > 0)
                {
                    for (int i = 0; i < includeTypes.Count; i++)
                    {
                        if (fileName.EndsWith(includeTypes[i]))
                        {
                            // Debug.Log($"real copy is {fileName}");
                            File.Copy(fileinfo.FullName, toDir + @"/" + fileName, true);
                        }
                    }
                }
                else
                {
                    File.Copy(fileinfo.FullName, toDir + @"/" + fileName, true);
                }
            }
            //copy directory
            foreach (DirectoryInfo directoryPath in directInfo.GetDirectories())
            {
                if (directoryPath.Name.StartsWith(".")) //忽略隐藏文件
                    continue;
                
                string toDirPath = toDir + @"/" + directoryPath.Name;
                // Debug.Log($"Create toDirPath is {toDirPath}");
                CopyDirectInfo(directoryPath.FullName, toDirPath, includeTypes);
            }
        }

        public static void DeleteDir(string srcPath)
        {
            if (!Directory.Exists(srcPath))
            {
                return;
            }
            
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos(); //返回目录中所有文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo) //判断是否文件夹
                {
                    DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                    subdir.Delete(true); //删除子目录和文件
                }
                else
                {
                    File.Delete(i.FullName); //删除指定文件
                }
            }

            dir.Delete(true); //删除子目录和文件
        }

        public static void DeleteFile(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
			{
                return;
			}

            // Use a try block to catch IOExceptions, to
            // handle the case of the file already being
            // opened by another process.
            try
            {
                System.IO.File.Delete(fileName);
            }
            catch (System.IO.IOException e)
            {
                Debug.LogError(e.Message);
                return;
            }
        }

        /// <summary>
        /// 获取文件夹下所有文件路径
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="result"></param>
        public static void GetAllFilePaths(string dirPath,ref List<string> result)
        {
            if (!Directory.Exists(dirPath))
            {
                return;
            }
            
            DirectoryInfo directInfo = new DirectoryInfo(dirPath);
            //copy files
            FileInfo[] filesInfos = directInfo.GetFiles();
            foreach (FileInfo fileinfo in filesInfos)
            {
                result.Add(fileinfo.FullName);
            }
            //copy directory
            foreach (DirectoryInfo directoryPath in directInfo.GetDirectories())
            {
                GetAllFilePaths(directoryPath.FullName, ref result);
            }
        }

        public static WaitForCopyDone CopyFilesAsync(string fromDir, string toDir)
        {
            var op = new WaitForCopyDone();
            Task.Factory.StartNew(() =>
            {
                var directoryInfo = new DirectoryInfo(fromDir);
                Internal_CopyFilesRecursive(fromDir, toDir, ref op);
                op.isDone = true;
            });
            return op;
        }

        private static void Internal_CopyFilesRecursive(string fromDir, string toDir, ref WaitForCopyDone op)
        {
            if (!Directory.Exists(fromDir))
            {
                throw new ApplicationException($"Source directory does not exist, {fromDir}");
            }
            if (!Directory.Exists(toDir))
            {
                Directory.CreateDirectory(toDir);
            }
            var directoryInfo = new DirectoryInfo(fromDir);
            var files = directoryInfo.GetFiles();
            foreach (var fi in files)
            {
                File.Copy(fi.FullName, $"{toDir}/{fi.Name}", true);
                op.finishedCount++;
            }
            foreach (var subDir in directoryInfo.GetDirectories())
            {
                var toSubDir = $"{toDir}/{subDir.Name}";
                Internal_CopyFilesRecursive(subDir.FullName, toSubDir, ref op);
            }
        }
    }
