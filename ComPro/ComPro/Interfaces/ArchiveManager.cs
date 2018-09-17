using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using ComPro.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ComPro.Interfaces
{
    public class ArchiveManager:IArchive
    {
        private readonly ApplicationDbContext _db;

        public ArchiveManager()
        {
            _db=new ApplicationDbContext();
        }
        public ArchiveModel CreateArchive(ArchiveModel model)
        {
            try
            {
                if (model != null)
                {
                    model.ActionDate = DateTime.Now;
                    model.IsVisible = true;
                    model.UploadDate = DateTime.Now;
                    model.Id = Guid.NewGuid().ToString();
                    _db.Archives.Add(model);

                    _db.SaveChanges();

                }
            }
            catch (DbEntityValidationException dbEx)
            {
                throw;
            }
          
            return model;
        }

        public ICollection<string> GetBlobs()
        {
            CloudBlobContainer container = GetCloudBlobContainer();

            List<string> blobs = new List<string>();

            // Loop over blobs within the container and output the URI to each of them
            //foreach (var blobItem in container.ListBlobs())
            //    blobs.Add(blobItem.Uri.ToString());

            foreach (IListBlobItem item in container.ListBlobs(useFlatBlobListing: true))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob blob = (CloudPageBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory dir = (CloudBlobDirectory)item;
                    blobs.Add(dir.Uri.ToString());
                }
            }

            return blobs;
        }

        public void UploadFiles(IEnumerable<HttpPostedFileBase> files,ArchiveModel archive)
        {
            if (archive != null && !string.IsNullOrEmpty(archive.Id))
            {
                CloudBlobContainer container = GetCloudBlobContainer();
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var ext = Path.GetExtension(file.FileName);
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid().ToString() + ext;

                        CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
                        blob.UploadFromStream(file.InputStream);

                        ArchiveFilesModel fm = new ArchiveFilesModel();
                        fm.ArchiveId = archive.Id;
                        fm.FilePath = fileName;
                        fm.Id = Guid.NewGuid().ToString();
                        _db.ArchiveFiles.Add(fm);
                        _db.SaveChanges();

                    }
                }
            }
          

           
        }

        public ICollection<ArchiveModel> GetArchiveContents()
        {
            var arcs = _db.Archives.Where(x => x.IsVisible).Include(x=>x.Files);

            return arcs.ToList();
        }

        public void DownloadFile(string filename)
        {
            CloudBlobContainer cloudBlobContainer = GetCloudBlobContainer();
            CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(filename);



            MemoryStream memStream = new MemoryStream();

            blockBlob.DownloadToStream(memStream);

            HttpContext.Current.Response.ContentType = blockBlob.Properties.ContentType.ToString();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "Attachment; filename=" + filename);

            HttpContext.Current.Response.AddHeader("Content-Length", blockBlob.Properties.Length.ToString());
            HttpContext.Current.Response.BinaryWrite(memStream.ToArray()); HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
        }

        private CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["BlobStorageConnectionString"]);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("bpid");
            return container;
        }
    }
}