using System;
using System.Text;
using System.IO;


public class Program
{
    static long directorySize = 0;
    static void Main(string[] args)
    {
        string dirName = "D:\\2. Кадры";
        string directory = dirName;



        CleanDirectory(directory);







    }

    static long sizeOfFolder(string folder)
    {
        try
        {

            DirectoryInfo directory = new DirectoryInfo(folder);
            DirectoryInfo[] directories = directory.GetDirectories();
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {

                directorySize = directorySize + file.Length;
            }

            foreach (DirectoryInfo fdirectory in directories)
            {

                sizeOfFolder(fdirectory.FullName);
            }

            return directorySize;
        }

        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }

        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }
    }

    static void CleanDirectory(string path)
    {
        int countdeletedFolder = 0;
        int countdeletedFile = 0;
        long firstsize = sizeOfFolder(path);
        directorySize = 0;
        Console.WriteLine($"First size of directory {path} - {firstsize}");



        var directory = new DirectoryInfo(path);
        try
        {
            if (directory.Exists)
            {

                FileInfo[] files = directory.GetFiles();
                DirectoryInfo[] dirs = directory.GetDirectories();
                foreach (DirectoryInfo dir in dirs)

                {


                    var first = dir.LastAccessTime;
                    var secondt = DateTime.Now;
                    var delyt = secondt - first;
                    var dely = TimeSpan.FromMinutes(0);

                    if (delyt >= dely && dir.Exists)
                    {
                        dir.Delete(true);
                        countdeletedFolder++;

                    }

                }





                foreach (FileInfo file in files)
                {



                    var first = file.CreationTime;
                    var secondt = DateTime.Now;
                    var delyt = secondt - first;
                    var dely = TimeSpan.FromMinutes(0);
                    if (delyt >= dely && file.Exists)
                    {
                        file.Delete();


                        countdeletedFile++;
                    }




                }

            }
            else { Console.WriteLine("Directory doesn't exist"); }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

        long secondsize = sizeOfFolder(path);
        long dif = firstsize - secondsize;

        Console.WriteLine($"Folder was deleted:{countdeletedFolder}\nFile was deleted:{countdeletedFile}\nSize removed {dif}\nNow size of directory {path} - {secondsize}");
        directorySize = 0;
    }


}
