using System.IO.Compression;

namespace WebApp.ChainofResponsibility.ChainofResponsibility;

public class ZipProcessHandler<T> : ProcessHandler
{
    
    public override object handle(object o)
    {
        var excelM = o as MemoryStream;
        excelM.Position = 0;
        using (var packageStream = new MemoryStream())
        {
            using (var archive = new ZipArchive(packageStream,ZipArchiveMode.Create,true))
            {
                var zipFile = archive.CreateEntry($"{typeof(T).Name}.xlsx");
                using (var zipEntry = zipFile.Open())
                {
                    excelM.CopyTo(zipEntry);
                }

                return base.handle(o);
            }
        }
    }
}