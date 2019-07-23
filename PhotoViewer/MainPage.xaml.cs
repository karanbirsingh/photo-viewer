using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PhotoViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<ElementNode> DataSource = new ObservableCollection<ElementNode>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                caption.Text = $"Viewing {file.DisplayName}";
                await photoViewer.ShowImage(file);
                using (var stream = await file.OpenStreamForReadAsync())
                {
                    DataSource.Clear();
                    var elementNode = MetadataHandler.LoadMetadata(stream);
                    if (elementNode != null)
                    {
                        DataSource.Add(elementNode);
                    }
                }
            }
        }

        private void AppBarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            this.splitView.IsPaneOpen = !this.splitView.IsPaneOpen;
        }
    }
}
