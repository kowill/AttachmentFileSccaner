using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AttachmentFileScanner
{
    [ComVisible(false)]
    internal enum ATTACHMENT_PROMPT
    {
        ATTACHMENT_PROMPT_NONE = 0x0000,
        ATTACHMENT_PROMPT_SAVE = 0x0001,
        ATTACHMENT_PROMPT_EXEC = 0x0002,
        ATTACHMENT_PROMPT_EXEC_OR_SAVE = 0x0003
    };
    [ComVisible(false)]
    internal enum ATTACHMENT_ACTION
    {
        ATTACHMENT_ACTION_CANCEL = 0x0000,
        ATTACHMENT_ACTION_SAVE = 0x0001,
        ATTACHMENT_ACTION_EXEC = 0x0002
    }

    [ComImport, Guid("73db1241-1e85-4581-8e4f-a81e1d0f8c57"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), ComVisible(false)]
    internal interface IAttachmentExecute
    {
        int SetClientTitle(string pszTitle);
        void SetClientGuid(ref Guid guid);
        void SetLocalPath(string pszLocalPath);
        void SetFileName(string pszFileName);
        int SetSource(string pszSource);
        int SetReferrer(string pszReferrer);
        int CheckPolicy();
        int Prompt(IntPtr hwnd, ATTACHMENT_PROMPT prompt, out ATTACHMENT_ACTION paction);
        [PreserveSig]
        int Save();
        int Execute(IntPtr hwnd, string pszVerb, ref IntPtr phProcess);
        int SaveWithUI(IntPtr hwnd);
        int ClearClientState();

    }
}
