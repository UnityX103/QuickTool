using System.IO;
using System.Net;
using UnityEngine;

public class FTPHandler
{
    public static void ReplaceFolder(string ftpUrl,string userName,string password , string remoteFolderPath , string localFolderPath)
    {

        DeleteFolderFromFtp(ftpUrl + remoteFolderPath, userName, password);
        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpUrl + remoteFolderPath);
        ftpRequest.Credentials = new NetworkCredential(userName, password);
        ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
        var response = (FtpWebResponse)ftpRequest.GetResponse();
        response.Close();

    
        foreach (string filePath in Directory.GetFiles(localFolderPath))
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                FtpWebRequest fileRequest =
                    (FtpWebRequest)WebRequest.Create(ftpUrl + remoteFolderPath + "/" + Path.GetFileName(filePath));
                fileRequest.Credentials = new NetworkCredential(userName, password);
                fileRequest.Method = WebRequestMethods.Ftp.UploadFile;
                fileRequest.UseBinary = true;
                using (Stream stream = fileRequest.GetRequestStream())
                {
                    fileStream.CopyTo(stream);
                    stream.Close();
                }

                fileStream.Close();
                FtpWebResponse fileResponse = (FtpWebResponse)fileRequest.GetResponse();
                fileResponse.Close();
            }
        }

        foreach (string folderPath in Directory.GetDirectories(localFolderPath))
        {
            string folderName = Path.GetFileName(folderPath);
            string remoteFolderName = remoteFolderPath + "/" + folderName;
            FtpWebRequest folderRequest = (FtpWebRequest)WebRequest.Create(ftpUrl + remoteFolderName);
            folderRequest.Credentials = new NetworkCredential(userName, password);
            folderRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            FtpWebResponse folderResponse = (FtpWebResponse)folderRequest.GetResponse();
            folderResponse.Close();
            UploadFolder(folderPath, ftpUrl, remoteFolderName, userName, password);
        }

        response = (FtpWebResponse)ftpRequest.GetResponse();
        response.Close();
    }

    public static void UploadFolder(string localFolderPath, string ftpUrl, string remoteFolderPath, string userName,
        string password)
    {
        foreach (string filePath in Directory.GetFiles(localFolderPath))
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                FtpWebRequest fileRequest =
                    (FtpWebRequest)WebRequest.Create(ftpUrl + remoteFolderPath + "/" + Path.GetFileName(filePath));
                fileRequest.Credentials = new NetworkCredential(userName, password);
                fileRequest.Method = WebRequestMethods.Ftp.UploadFile;
                fileRequest.UseBinary = true;
                using (Stream stream = fileRequest.GetRequestStream())
                {
                    fileStream.CopyTo(stream);
                    stream.Close();
                }

                fileStream.Close();
                FtpWebResponse fileResponse = (FtpWebResponse)fileRequest.GetResponse();
                fileResponse.Close();
            }
        }

        foreach (string folderPath in Directory.GetDirectories(localFolderPath))
        {
            string folderName = Path.GetFileName(folderPath);
            string remoteFolderName = remoteFolderPath + "/" + folderName;
            
            FtpWebRequest folderRequest = (FtpWebRequest)WebRequest.Create(ftpUrl + remoteFolderName);
            folderRequest.Credentials = new NetworkCredential(userName, password);
            folderRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            FtpWebResponse folderResponse = (FtpWebResponse)folderRequest.GetResponse();
            folderResponse.Close();
            UploadFolder(folderPath, ftpUrl, remoteFolderName, userName, password);
        }
    }

    public static void DeleteFolderFromFtp(string ftpFolderUrl, string ftpUserName, string ftpPassword)
    {
        // Create a request using a URL that can receive a post.
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpFolderUrl);
        request.Method = WebRequestMethods.Ftp.ListDirectory;

        // This example assumes the FTP site uses anonymous logon.
        request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
        {
            // Get the data stream from the response.
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    // Iterate through the directories and files in the FTP folder.
                    string line = reader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        string path = ftpFolderUrl + "/" + line;
                        if (line.Contains("."))
                        {
                            // This is a file. Delete it.
                            DeleteFileFromFtp(path, ftpUserName, ftpPassword);
                        }
                        else
                        {
                            // This is a directory. Recursively call this method to delete it and its contents.
                            DeleteFolderFromFtp(path, ftpUserName, ftpPassword);
                        }

                        line = reader.ReadLine();
                    }
                }
            }
        }

        // Finally, delete the root directory itself.
        request = (FtpWebRequest)WebRequest.Create(ftpFolderUrl);
        request.Method = WebRequestMethods.Ftp.RemoveDirectory;
        request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
        {
            // Do nothing (we just need to make sure the request is sent).
        }
    }

    private static void DeleteFileFromFtp(string ftpFileUrl, string ftpUserName, string ftpPassword)
    {
        // Create a request using a URL that can receive a post. 
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpFileUrl);
        request.Method = WebRequestMethods.Ftp.DeleteFile;

        // This example assumes the FTP site uses anonymous logon. 
        request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

        // Get the response. 
        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
        {
            // Do nothing (we just need to make sure the request is sent).
        }
    }
}