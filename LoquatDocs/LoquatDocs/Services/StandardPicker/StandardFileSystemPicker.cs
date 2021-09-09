using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WinRT;

namespace LoquatDocs.Services {
  public abstract class StandardFileSystemPicker {

    protected void SetWindowHandler(IWinRTObject winRTObject) {
      if (Window.Current is null) {
        IInitializeWithWindow initializeWithWindowWrapper = winRTObject.As<IInitializeWithWindow>();
        IntPtr hwnd = GetActiveWindow();
        initializeWithWindowWrapper.Initialize(hwnd);
      }
    }

    [ComImport, Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    protected interface IInitializeWithWindow {
      void Initialize([In] IntPtr hwnd);
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto, PreserveSig = true, SetLastError = false)]
    protected static extern IntPtr GetActiveWindow();
  }
}

