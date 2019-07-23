using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotoView : Page
    {
        public PhotoView()
        {
            this.InitializeComponent();
        }

        public async System.Threading.Tasks.Task ShowImage(StorageFile file)
        {
            var bitmap = new BitmapImage();
            using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
            {
                stream.Seek(0);
                await bitmap.SetSourceAsync(stream);
                this.img.Source = bitmap;
                this.img.Name = file.DisplayName;
            }
        }
    }
}
