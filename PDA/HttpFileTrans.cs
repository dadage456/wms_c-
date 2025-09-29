using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace PDA
{
    /// <summary>
    /// обтьнд╪Ч
    /// </summary>
    class HttpFileTrans
    {
        public HttpFileTrans(Uri serverFile, FileInfo localFile)
        {
            this.serverFileUri = serverFile;
            this.localFileInfo = localFile;
        }

        Uri serverFileUri;
        FileInfo localFileInfo;

        public readonly int CacheLength = 0x1000;
        public event EventHandler Transing;

        public Uri ServerFileUri
        {
            get { return serverFileUri; }
            set { serverFileUri = value; }
        }

        public FileInfo LocalFileInfo
        {
            get { return localFileInfo; }
            set { localFileInfo = value; }
        }

        public void Download()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(serverFileUri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            FileStream outStream = LocalFileInfo.Open(FileMode.Create);
            using (outStream)
            {
                long len = response.ContentLength;
                Stream inStream = response.GetResponseStream();
                using (inStream)
                {
                    byte[] cache = new byte[CacheLength];
                    int readCount;
                    int eventTicks = (int)len / CacheLength / 20;
                    int count = 0;
                    for (long i = 0; i < len; )
                    {
                        readCount = inStream.Read(cache, 0, CacheLength);
                        outStream.Write(cache, 0, readCount);
                        i += readCount;
                        count++;
                        if (count > eventTicks && Transing != null)
                        {
                            Transing(Convert.ToInt32(i * 100 / len), EventArgs.Empty);
                            count = 0;
                        }
                    }
                }
            }
        }
    }
}
