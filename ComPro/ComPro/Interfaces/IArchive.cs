using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ComPro.Models;

namespace ComPro.Interfaces
{
    public interface IArchive
    {
        ArchiveModel CreateArchive(ArchiveModel model);
        ICollection<string> GetBlobs();
        void UploadFiles(IEnumerable<HttpPostedFileBase> files,ArchiveModel archive);
        ICollection<ArchiveModel> GetArchiveContents();
        void DownloadFile(string filename);

    }
}
