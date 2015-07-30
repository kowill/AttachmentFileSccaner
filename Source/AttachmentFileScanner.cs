using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AttachmentFileScanner
{
    [ComVisible(true)]
    public class FileScanner
    {
        public Task<ScanResult> StartCheckFile(string path)
        {
            var completionSource = new TaskCompletionSource<ScanResult>();
            var thread = new Thread(() =>
            {
                var result = ScanResult.Other;
                var iExecute = (IAttachmentExecute)new AttachmentServices();
                try
                {
                    var clientGuid = new Guid(0x7F29ECFC, 0x8C15, 0x42BC, 0x90, 0x57, 0x44, 0xBB, 0x17, 0x87, 0xCE, 0xA8);
                    iExecute.SetClientGuid(ref clientGuid);
                    iExecute.SetFileName(path);
                    iExecute.SetLocalPath(path);
                    switch (iExecute.Save())
                    {
                        case 0x0:
                            result = ScanResult.Ok;
                            break;
                        case unchecked((int)0x80070002):
                            result = ScanResult.NotFoundFile;
                            break;
                        case unchecked((int)0x800c000e):
                            result = ScanResult.HasSecurityRisk;
                            break;
                        default:
                            result = ScanResult.Other;
                            break;
                    }
                }
                finally
                {
                    iExecute.ClearClientState();
                    Marshal.ReleaseComObject(iExecute);
                }
                completionSource.SetResult(result);
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return completionSource.Task;
        }

        public enum ScanResult { Ok, NotFoundFile, HasSecurityRisk, Other }
    }
}
