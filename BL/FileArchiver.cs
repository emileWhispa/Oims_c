using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace orion.ims.BL
{
    public class FileArchiver
    {
        //private string sDir;
        //private string dDir;
        public string ErrorMessage { get; set; }

        public bool Archive(string sourceDir, string destDir,ArchiveType aType = ArchiveType.Move)
        {
            ErrorMessage = "";
            try
            {
                FileInfo fi;
                //---- Check source directory
                if (!Directory.Exists(sourceDir))
                {
                    ErrorMessage = "Source directory does not exist!";
                    return false;
                }
                //---- Check destination directory
                if (!Directory.Exists(destDir))
                    Directory.CreateDirectory(destDir);

                //---- Archive files here now
                foreach (var f in Directory.GetFiles(sourceDir))
                {
                    fi = new FileInfo(f);
                    if (aType == ArchiveType.Move)
                        fi.MoveTo(Path.Combine(destDir, fi.Name));
                    else
                        fi.CopyTo(Path.Combine(destDir, fi.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            return true;
        }
    }

    public enum ArchiveType { Copy = 0, Move = 1 }
}
